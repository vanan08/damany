using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using FaceProcessingWrapper;
using OpenCvSharp;
using FaceSearchWrapper;
using Damany.Util;
using System.Drawing;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class StaticFunctionTest
    {
        private IplImage x;
        private IplImage y;
        private FaceSearchWrapper.FaceSearch searcher;

        [FixtureSetUp]
        public void Setup()
        {
            searcher = new FaceSearch();

            x = OpenCvSharp.IplImage.FromFile(@"D:\target.jpg");
            x.ROI = new CvRect(54, 63, 72, 71);

            y = IplImage.FromFile(@"d:\suspect.jpg");
            y.ROI = new CvRect(58, 63, 71, 67);
        }


        [Test]
        public void LbpTest()
        {

            var faceX = SearchFace(x);
            faceX.DrawRect(faceX.ROI, CvColor.Black);

            var faceY = SearchFace(y);
            faceY.DrawRect(faceY.ROI, CvColor.Black);

            var bmpX = faceX.ToBitmap();
            var bmpY = faceY.ToBitmap();

            DrawRect(faceX, bmpX);
            DrawRect(faceY, bmpY);


            var compareAlgorith = new Damany.Imaging.PlugIns.LBPFaceComparer();
            var result = compareAlgorith.Compare(faceX, faceY);
            
        }

        private void DrawRect(IplImage faceX, Bitmap bmpX)
        {
            using (Graphics g = Graphics.FromImage(bmpX))
            {
                g.DrawRectangle(Pens.Black, faceX.ROI.ToRectangle());
            }
        }

        private IplImage SearchFace(IplImage img)
        {
            var roi = img.ROI;
            img.ResetROI();
            var frame = new Frame(img);
            frame.MotionRectangles.Add(img.ROI);
            searcher.AddInFrame(frame);
            var faces = searcher.SearchFaces();
            img.ROI = roi;
            var face = faces[0].Portraits[0].Face;
            var bmp = face.ToBitmap();
            face.ROI = faces[0].Portraits[0].FacesRectForCompare;
            return face;
        }
    }
}
