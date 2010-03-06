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
            this.SearchIn(motionFrames);
        }

        #endregion


        private void SearchIn(IList<MotionFrame> motionFrames)
        {
            foreach (var item in motionFrames)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            var noPortraitFrameQuery
                                = from m in motionFrames
                                  where !portraits.Any( t => t.BaseFrame.guid.Equals(m.Frame.Guid) )
                                  select m;

            Array.ForEach(noPortraitFrameQuery.ToArray(), mf => { motionFrames.Remove(mf); mf.Dispose(); });

            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Frame.Guid equals p.BaseFrame.guid
                                          select new
                                          {
                                              F = m,
                                              P = p,
                                          };

            var expanded = from item in portraitFoundFrameQuery
                           from  p in item.P.Portraits
                           select new Portrait
                           {
                               ContainedIn = item.F,
                               FaceImage = new BitmapIplUnion(p.Face),
                               FaceRect = p.FacesRect,
                               RectInMotionFrame = p.FacesRectForCompare,
                           };

            var portraitList = expanded.ToList();

            this.successor.HandlePortraits(portraitList);
            
        }

        IPortraitHandler successor;
        FaceSearchWrapper.FaceSearch searcher;
    }
}
