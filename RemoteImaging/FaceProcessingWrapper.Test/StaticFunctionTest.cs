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

            x = OpenCvSharp.IplImage.FromFile(@"D:\ImageOutput\Images\2010\4\4\14\03d5ee92-ad5f-4ebc-8a81-6a896d3fba8d.jpg");

        }


        [Test]
        [Timeout(0)]
        public void LbpTest()
        {
            var destDir = System.IO.Path.Combine(@"d:\searchResult", DateTime.Now.ToShortTimeString().Replace(":", "-"));

            var faceRectTarget = SearchFace(x);
            x.ROI = faceRectTarget;

            var comparer = new Damany.Imaging.PlugIns.LbpFaceComparer();
            var repo = new PersonOfInterest[1];
            repo[0] = new PersonOfInterest(x);
            comparer.Load( repo.ToList());

            foreach (var file in System.IO.Directory.GetFiles(@"D:\ImageOutput\Images\2010\4\4\14", "*.jpg"))
            {
                var img = IplImage.FromFile(file);
                var faceRectToBeCompared = SearchFace(img);

                if (faceRectToBeCompared.Width == 0 || faceRectToBeCompared.Height == 0)
                {
                    continue;
                }

                img.ROI = faceRectToBeCompared;


                var matchResult = comparer.CompareTo(img);

                foreach (var repositoryCompareResult in matchResult)
                {
                    if (!System.IO.Directory.Exists(destDir))
                    {
                        System.IO.Directory.CreateDirectory(destDir);
                    }

                    System.Diagnostics.Debug.WriteLine(file);
                    var fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file);
                    var ext = System.IO.Path.GetExtension(file);

                    System.Diagnostics.Debug.WriteLine(repositoryCompareResult.Similarity);
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

            return faces[0].Portraits[0].FacesRectForCompare; ;
        }
    }
}
