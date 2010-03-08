using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Processors
{
    using Damany.ImageProcessing.Contracts;

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

        public void HandleMotionFrame(IList<MotionFrame> motionFrames)
        {
            var cloned = motionFrames.ToList().ConvertAll(f => f.Clone());
            this.SearchIn(cloned);
        }

        #endregion


        private void SearchIn(IList<MotionFrame> motionFrames)
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

        private static List<Portrait> ExpandPortraitsList(IList<MotionFrame> motionFrames, ImageProcess.Target[] portraits)
        {
            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Guid equals p.BaseFrame.guid
                                          select new
                                          {
                                              F = m,
                                              P = p,
                                          };

            var expanded = from item in portraitFoundFrameQuery
                           from p in item.P.Portraits
                           select new Portrait (item.F.Clone())
                           {
                                BoundsInParent = new PortraitBounds
                                {
                                     Bounds = p.FacesRect,
                                     FaceBounds = p.FacesRectForCompare,
                                }
                           };

            var portraitList = expanded.ToList();
            return portraitList;
        }

        private static void DisposeFacelessFrames(IList<MotionFrame> motionFrames, ImageProcess.Target[] portraits)
        {
            var noPortraitFrameQuery  = from m in motionFrames
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
