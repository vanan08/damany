using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaceProcessingWrapper;
using OpenCvSharp;

namespace Damany.Imaging.Extensions
{
    public static class IplImageExtensions
    {
        public static FaceSearchWrapper.FaceSearch searcher = new FaceSearchWrapper.FaceSearch();

        public static CvRect[] LocateFaces(this IplImage img)
        {
            return LocateFaces(img, new CvRect(0,0,0,0));
        }

        public static CvRect[] LocateFaces(this IplImage img, CvRect rectToLookin)
        {
            var frame = new Common.Frame(img);
            frame.MotionRectangles.Add(rectToLookin);
            searcher.AddInFrame(frame);
            var faces = searcher.SearchFaces();

            var faceRects = from t in faces
                            from f in t.Portraits
                            select f.FacesRectForCompare;

            return faceRects.ToArray();
        }

        public static CvRect BoundsRect(this IplImage img)
        {
            return new CvRect(0, 0, img.Width, img.Height);
            
        }
    }
}
