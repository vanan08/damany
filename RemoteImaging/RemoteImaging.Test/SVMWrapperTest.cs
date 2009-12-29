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
        [SetUp]
        public void SVMInitialize()
        {
            RemoteImaging.SVMWrapper.InitSvmData(100*100, 40);
        }


        [Test]
        [Row(@"d:\TestPic")]
        public void SVMPredict(string directoryPath)
        {
            string[] files = System.IO.Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                IplImage ipl = IplImage.FromFile(file, LoadMode.GrayScale);

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

                double result = RemoteImaging.SVMWrapper.SvmPredict(imgData);

                float expectedResult = 0;

                if (file.Contains("+1"))
                {
                    expectedResult = 1;
                }
                else if (file.Contains("-1"))
                {
                    expectedResult = -1;
                }

                Assert.AreEqual((int)result, expectedResult);

            }

        }
    }
}
