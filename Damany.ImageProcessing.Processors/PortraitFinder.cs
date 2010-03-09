using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Contracts;

    public class PortraitFinder : IMotionFrameHandler
    {
        public PortraitFinder(IPortraitHandler successorHandler)
        {
            if (successorHandler == null)
                throw new ArgumentNullException("successorHandler", "successorHandler is null.");

            this.successor = successorHandler;
            this.searcher = new FaceSearchWrapper.FaceSearch();
        }


        #region IMotionFrameHandler Members

        public void HandleMotionFrame(IList<Frame> motionFrames)
        {
            var cloned = motionFrames.ToList().ConvertAll(f => f.Clone());
            this.SearchIn(cloned);
        }

        #endregion


        private void SearchIn(List<Frame> motionFrames)
        {
            foreach (var item in motionFrames)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            DisposeFacelessFrames(motionFrames, portraits);
            var portraitList = ExpandPortraitsList(motionFrames, portraits);
            PassOnPortraits(portraitList);
        }

        private static PortraitBounds CreateBounds(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            var pb = new PortraitBounds();
            return pb;

        }

        private static List<Portrait> ExpandPortraitsList(List<Frame> motionFrames, ImageProcess.Target[] portraits)
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


        private void PassOnPortraits(List<Portrait> portraitList)
        {
            this.successor.HandlePortraits(portraitList);
            portraitList.ForEach(p => p.Dispose());
        }


        IPortraitHandler successor;
        FaceSearchWrapper.FaceSearch searcher;
    }
}
