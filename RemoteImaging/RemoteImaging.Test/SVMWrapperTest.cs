using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using OpenCvSharp;
using System.IO;

namespace RemoteImaging.Test
{
    [TestFixture]
    public class SVMWrapperTest
    {
        public void SVMInitialize()
        {
            RemoteImaging.SVMWrapper.InitSvmData(100*100, 40);
        }


        [Test]
        [Row(@"d:\091223140130077_+1_0004.jpg")]
        public void SVMPredict(string fileFullPath)
        {
            IplImage ipl = IplImage.FromFile(fileFullPath, LoadMode.GrayScale);

            System.Drawing.Bitmap bmp = ipl.ToBitmap();

            float[] imgData = ImageProcess.NativeIconExtractor.ResizeIplTo(ipl, 100, 100);

            var stream = File.OpenWrite(@"d:\data.txt");
            StreamWriter sw = new StreamWriter(stream);
            foreach (var f in imgData)
            {
                sw.WriteLine((int)f);
            }

            sw.Close();
            stream.Close();

            double result =  RemoteImaging.SVMWrapper.SvmPredict(imgData);

            System.Diagnostics.Trace.WriteLine(result);
        }
    }
}
