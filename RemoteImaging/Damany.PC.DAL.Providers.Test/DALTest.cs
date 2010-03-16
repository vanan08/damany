using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.PortraitCapturer.DAL;
using Damany.PortraitCapturer.DAL.Providers;
using Damany.Imaging.Common;
using TestDataProvider;

namespace Damany.PC.DAL.Providers.Test
{
    [TestFixture]
    public class DALTest
    {
        [Test]
        public void Test()
        {
            IDataProvider provider = new Db4oProvider( "faces.db4o" ) ;

            Func<Frame, string> f1 = f => f.Guid.ToString() +".jpg" ;
            Func<Portrait, string> f2 = p => p.Guid.ToString() + ".jpg";

            var repository = new Damany.PortraitCapturer.Repository.PersistenceService(
                                    provider, f1, f2);

            var frame = new Frame(Data.GetFrame());
            var mockCamera = new Damany.Cameras.DirectoryFilesCamera(@"c:\", "*.jpg");
            mockCamera.Id = 3;
            frame.CapturedFrom = mockCamera;

            repository.SaveFrame(frame);

            var fromDb = repository.GetFrame(frame.Guid);

            Assert.AreEqual(fromDb.Guid, frame.Guid);

            var portrait = new Portrait(Data.GetPortrait());
            portrait.FaceBounds = new OpenCvSharp.CvRect(0, 0, 100, 100);
            portrait.FrameId = frame.Guid;
            portrait.CapturedFrom = mockCamera;

            repository.SavePortrait(portrait);

            var portraitFromDb = repository.GetPortrait(portrait.Guid);
            Assert.AreEqual( portraitFromDb.Guid, portrait.Guid);

            
        }
    }
}
