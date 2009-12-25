using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using OpenCvSharp;

namespace RemoteImaging.Test
{
    [TestFixture]
    public class SVMWrapperTest
    {
        [SetUp]
        public void SVMInitialize()
        {
            RemoteImaging.SVMWrapper.InitSvmData(100*100, 40);
        }


        [Test]
        [Row("d:\abc.jpg")]
        public void SVMPredict(string fileFullPath)
        {
            IplImage ipl = IplImage.FromFile(fileFullPath);

            System.Drawing.Bitmap bmp = ipl.ToBitmap();

            float[] imgData = ImageProcess.NativeIconExtractor.ResizeIplTo(ipl, 100, 100, BitDepth.U8, 1);

            double result =  RemoteImaging.SVMWrapper.SvmPredict(imgData);

            System.Diagnostics.Trace.WriteLine(result);
        }
    }
}
