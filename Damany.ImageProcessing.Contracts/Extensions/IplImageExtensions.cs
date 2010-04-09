﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Damany.Util;
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

        public static IplImage LoadIntoIpl(this string path)
        {
            return IplImage.FromFile(path);
        }

        [Conditional("DEBUG")]
        public static void CheckWithBmp(this IplImage ipl)
        {
            var bmp = ipl.ToBitmap();

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawRectangle(Pens.Red, ipl.ROI.ToRectangle());
            }
            
        }
    }
}
