using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaceSearchWrapper;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using OpenCvSharp;
using Damany.Imaging.Extensions;
using Damany.Imaging.Common;
using Damany.Util;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FrontFaceVerifierTest
    {
        FaceProcessingWrapper.FrontFaceVerifier _frontFaceVerifier;
        private FaceSearchWrapper.FaceSearch _faceSearch;

        [FixtureSetUp]
        public void Load()
        {
            var eyeTemp = OpenCvSharp.IplImage.FromFile(@"f:\测试图片\eyeTemplate.jpg", LoadMode.GrayScale);
            this._frontFaceVerifier = new FrontFaceVerifier(eyeTemp);

            _faceSearch = new FaceSearch();
        }

        [Test]
        public void IsFrontTest()
        {

            foreach (var faceFile in System.IO.Directory.GetFiles(@"F:\测试图片\non-frontal", "*.jpg"))
            {
                OpenCvSharp.IplImage iplFace = OpenCvSharp.IplImage.FromFile(faceFile);
                var faceRects = iplFace.LocateFaces(_faceSearch);

                if (faceRects.Length > 0)
                {
                    iplFace.IsEnabledDispose = false;
                    var sub = iplFace.GetSub(faceRects[0]);
                    var isFront = this._frontFaceVerifier.IsFrontFace(sub);
                    System.Diagnostics.Debug.WriteLine(faceFile + " " + isFront.ToString());
                    //Assert.IsFalse(isFront);
                }
            }

            foreach (var faceFile in System.IO.Directory.GetFiles(@"F:\测试图片\frontal", "*.jpg"))
            {
                OpenCvSharp.IplImage iplFace = OpenCvSharp.IplImage.FromFile(faceFile);
                var faceRects = iplFace.LocateFaces(_faceSearch);

                if (faceRects.Length > 0)
                {
                    iplFace.IsEnabledDispose = false;
                    var sub = iplFace.GetSub(faceRects[0]);
                    var isFront = this._frontFaceVerifier.IsFrontFace(sub);
                    System.Diagnostics.Debug.WriteLine(faceFile + " " + isFront.ToString());
                    //Assert.IsTrue(isFront);
                }
            }
        }
    }
}
