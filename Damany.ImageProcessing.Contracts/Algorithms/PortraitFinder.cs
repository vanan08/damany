using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;
using ImageProcess;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Common;

    public class PortraitFinder
    {

        public IEnumerable<Damany.Imaging.Common.IFacePostFilter> PostFilters { get; set; }

        public PortraitFinder()
        {
            this.searcher = new FaceSearchWrapper.FaceSearch();

            PostFilters = new List<IFacePostFilter>(0);
        }

        public List<Portrait> ProcessFrames(List<Frame> motionFrames)
        {

            var portraits = HandleMotionFrame(motionFrames);

            var filtered = PostProcessPortraits(portraits);

            return filtered;
        }

        private List<Portrait> PostProcessPortraits(List<Portrait> portraits)
        {
            for (var i = 0; i < portraits.Count; i++)
            {
                if (!IsFace(portraits[i]))
                {
                    portraits[i].Dispose();
                    portraits[i] = null;
                }
            }

            return portraits.Where(p => p != null).ToList();
        }





        bool IsFace(Portrait portrait)
        {
            foreach (var facePostFilter in PostFilters)
            {
                if (!facePostFilter.IsFace(portrait))
                {
                    return false;
                }
            }

            return true;
        }



        public List<Portrait> HandleMotionFrame(List<Frame> motionFrames)
        {
            return this.SearchIn(motionFrames);
        }



        private List<Portrait> SearchIn(List<Frame> motionFrames)
        {
            var mList = motionFrames;
            foreach (var item in mList)
            {
                this.searcher.AddInFrame(item);
            }

            var portraits = this.searcher.SearchFaces();

            var facelessFrames = GetFacelessFrames(mList, portraits);
            var faceFrames = GetFaceFrames(mList, portraits);
            var portraitList = ExpandPortraitsList(faceFrames, portraits);

            foreach (var facelessFrame in facelessFrames)
            {
                facelessFrame.Dispose();
            }

            return portraitList;

        }


        private static OpenCvSharp.CvRect FrameToPortrait(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            faceBounds.X -= bounds.X;
            faceBounds.Y -= bounds.Y;

            return faceBounds;
        }

        private static List<Portrait> ExpandPortraitsList(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Guid equals p.BaseFrame.guid
                                          select new { Frame = m, Portraits = p, };

            var expanedPortraits = from frame in portraitFoundFrameQuery
                                   from p in frame.Portraits.Portraits
                                   select new Portrait(p.Face)
                                   {
                                       FaceBounds = FrameToPortrait(p.FacesRect, p.FacesRectForCompare),
                                       Frame = frame.Frame.Clone(),
                                       CapturedAt = frame.Frame.CapturedAt,
                                       CapturedFrom = frame.Frame.CapturedFrom,
                                   };


            return expanedPortraits.ToList();
        }

        private static IEnumerable<Frame> GetFacelessFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var noPortraitFrameQuery = from m in motionFrames
                                       where !portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            return noPortraitFrameQuery;
        }

        private static IEnumerable<Frame> GetFaceFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFrameQuery = from m in motionFrames
                                     where portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                     select m;

            return portraitFrameQuery;
        }



        FaceSearchWrapper.FaceSearch searcher;
    }
}
