using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Contracts;

    public class PortraitFinder : IMotionFrameHandler
    {
        public PortraitFinder()
        {
            this.listeners = new List<IPortraitHandler>();
            this.searcher = new FaceSearchWrapper.FaceSearch();
        }

        public void AddListener(IPortraitHandler l)
        {
            this.listeners.Add(l);
        }

        public void RemoveListener(IPortraitHandler l)
        {
            this.listeners.Remove(l);
        }


        #region IMotionFrameHandler Members

        public void HandleMotionFrame(IList<Frame> motionFrames)
        {
            this.SearchIn(motionFrames);
        }

        #endregion


        private void SearchIn(IList<Frame> motionFrames)
        {
            foreach (var item in motionFrames)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            DisposeFacelessFrames(motionFrames, portraits);
            var portraitList = ExpandPortraitsList(motionFrames, portraits);
            NotifyListeners(motionFrames, portraitList);
        }

        private static PortraitBounds CreateBounds(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            var pb = new PortraitBounds();

            faceBounds.X -= bounds.X;
            faceBounds.Y -= bounds.Y;

            pb.Bounds = bounds;
            pb.FaceBoundsInPortrait = faceBounds;

            return pb;
        }

        private static List<Portrait> ExpandPortraitsList(IList<Frame> motionFrames, ImageProcess.Target[] portraits)
        {
            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Guid equals p.BaseFrame.guid
                                          select new { Frame = m, Portraits = p, };

            var expanedPortraits = from frame in portraitFoundFrameQuery
                                   from p in frame.Portraits.Portraits
                                   let bounds = CreateBounds(p.FacesRect, p.FacesRectForCompare)
                                   select new Portrait(p.Face)
                                   {
                                       Bounds = bounds,
                                       FrameId = frame.Frame.Guid,
                                       CapturedAt = frame.Frame.CapturedAt,
                                       CapturedFrom = frame.Frame.CapturedFrom,
                                   };


            return expanedPortraits.ToList();
        }

        private static void DisposeFacelessFrames(IList<Frame> motionFrames, ImageProcess.Target[] portraits)
        {
            var noPortraitFrameQuery = from m in motionFrames
                                       where !portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            Array.ForEach(noPortraitFrameQuery.ToArray(), mf => { motionFrames.Remove(mf); mf.Dispose(); });
        }


        private void NotifyListeners(IList<Frame> motionFrames, IList<Portrait> portraitList)
        {
            foreach (var listener in this.listeners)
            {
                if (listener.WantCopy)
                {
                    listener.HandlePortraits(
                        motionFrames.ToList().ConvertAll(m => m.Clone()),
                        portraitList.ToList().ConvertAll(p => p.Clone())
                        );
                }
                else
                {
                    listener.HandlePortraits(motionFrames, portraitList);

                }
            }


            portraitList.ToList().ForEach(p => p.Dispose());
            motionFrames.ToList().ForEach(f => f.Dispose());
        }


        List<IPortraitHandler> listeners;
        FaceSearchWrapper.FaceSearch searcher;
    }
}
