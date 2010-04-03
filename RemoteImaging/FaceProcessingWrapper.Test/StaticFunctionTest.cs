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
        private FaceSearchWrapper.FaceSearch searcher;

        [FixtureSetUp]
        public void Setup()
        {
            searcher = new FaceSearch();

            x = OpenCvSharp.IplImage.FromFile(@"d:\target.jpg");
            x.ROI = new CvRect(54, 63, 72, 71);

        }


        [Test]
        [Timeout(0)]
        public void LbpTest()
        {
            var destDir = System.IO.Path.Combine(@"d:\searchResult", DateTime.Now.ToShortTimeString().Replace(":", "-"));

            var faceRectTarget = SearchFace(x);
            x.ROI = faceRectTarget;

            var compareAlgorith = new Damany.Imaging.PlugIns.LBPFaceComparer();

            foreach (var file in System.IO.Directory.GetFiles(@"d:\abc", "*.jpg"))
            {
                var img = IplImage.FromFile(file);
                var faceRectToBeCompared = SearchFace(img);
                img.ROI = faceRectToBeCompared;


                var matchResult = compareAlgorith.Compare(x, img);

                if (matchResult.IsSimilar)
                {
                    if (!System.IO.Directory.Exists(destDir))
                    {
                        System.IO.Directory.CreateDirectory(destDir);
                    }

                    System.Diagnostics.Debug.WriteLine(file);
                    var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file);
                    var ext = System.IO.Path.GetExtension(file);
                    var fileNameWithScore = string.Format("{0}_{1:f2}{2}", fileNameWithoutExt, matchResult.SimilarScore, ext);
                    var destFile = System.IO.Path.Combine(destDir, fileNameWithScore);
                    img.DrawRect(img.ROI, CvColor.Red, 2);
                    img.ResetROI();
                    img.SaveImage(destFile);
                }


            }


            
        }

        private void DrawRect(IplImage faceX, Bitmap bmpX)
        {
            using (Graphics g = Graphics.FromImage(bmpX))
            {
                g.DrawRectangle(Pens.Black, faceX.ROI.ToRectangle());
            }
        }

        private CvRect SearchFace(IplImage img)
        {
            var roi = img.ROI;
            img.ResetROI();
            var frame = new Frame(img);
            frame.MotionRectangles.Add(img.ROI);
            searcher.AddInFrame(frame);
            var faces = searcher.SearchFaces();
            img.ROI = roi;

            if (faces.Length == 0)
            {
                return new CvRect();
            }


            var face = faces[0].Portraits[0].Face;
            var bmp = face.ToBitmap();
             
            return faces[0].Portraits[0].FacesRectForCompare;;
        }
    }
}
