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
            PassOnPortraits(motionFrames, portraitList);
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


        private void PassOnPortraits(IList<Frame> motionFrames, IList<Portrait> portraitList)
        {
            this.successor.HandlePortraits(motionFrames, portraitList);
        }


        IPortraitHandler successor;
        FaceSearchWrapper.FaceSearch searcher;
    }
}
