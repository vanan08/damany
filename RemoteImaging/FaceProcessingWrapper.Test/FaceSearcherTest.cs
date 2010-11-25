using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Imaging.Extensions;
using OpenCvSharp;
using System.IO;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FaceSearcherTest
    {
        [Test]
        public void Test()
        {
            var imageFiles = new Damany.Cameras.DirectoryFilesCamera(@"M:\测试图片\市场", "*.jpg");
            imageFiles.Repeat = false;
            imageFiles.Initialize();

            var searcher = new FaceSearchWrapper.FaceSearch();

            var outputDir = DateTime.Now.ToString().Replace(':', '-');
            Directory.CreateDirectory(outputDir);

            while (true)
            {
                var frame = imageFiles.RetrieveFrame();
                if (frame == null)
                    break;

                //frame.MotionRectangles.Add(new CvRect(0, 0, frame.GetImage().Width, frame.GetImage().Height));

                //searcher.AddInFrame(frame);
                var faces = searcher.SearchFaces();

                foreach (var face in faces)
                {
                    foreach (var portraitInfo in face.Portraits)
                    {
                        var path = Path.Combine(outputDir, Guid.NewGuid() + ".jpg");
                        portraitInfo.Face.SaveImage(path);
                    }
                }

            }

        }
    }
}
