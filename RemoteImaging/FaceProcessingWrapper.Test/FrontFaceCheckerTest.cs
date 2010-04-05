using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FrontFaceCheckerTest
    {
        FaceProcessingWrapper.FrontFaceChecker frontFaceChecker;

        [FixtureSetUp]
        public void Load()
        {

            this.frontFaceChecker =
                FaceProcessingWrapper.FrontFaceChecker.FromFile(@"C:\faceRecognition\FrontalFace\eyeTemplate.jpg");
        }

        [Test]
        public void IsFrontTest()
        {
            OpenCvSharp.IplImage frontFace = OpenCvSharp.BitmapConverter.ToIplImage(Resource.front);
            frontFace.IsEnabledDispose = false;
            bool isFront = this.frontFaceChecker.IsFront(frontFace);
            //Assert.IsTrue(isFront);


            OpenCvSharp.IplImage nonFrontFace = OpenCvSharp.BitmapConverter.ToIplImage(Resource.non_front);
            nonFrontFace.IsEnabledDispose = false;
            isFront = this.frontFaceChecker.IsFront(nonFrontFace);
            //Assert.IsFalse(isFront);

            foreach (var faceFile in System.IO.Directory.GetFiles(@"m:\front", "*.jpg"))
            {
                OpenCvSharp.IplImage iplFace = OpenCvSharp.IplImage.FromFile(faceFile, OpenCvSharp.LoadMode.GrayScale);
                iplFace.IsEnabledDispose = false;
                isFront = this.frontFaceChecker.IsFront(iplFace);
                System.Diagnostics.Debug.WriteLine(faceFile + " " + isFront.ToString());
               // Assert.IsTrue(isFront);

            }
        }
    }
}
