using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.Repository;
using Damany.Imaging.Contracts;
using Damany.Imaging.Processors;
using Damany.Imaging.Handlers;

namespace Damany.RemoteImaging.Common
{
        public static class SearchLineBuilder
        {
            private static PersistenceService persistenceService = null;
            static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);


            public static PersistenceService GetDefaultPersistenceService()
            {
                if (persistenceService == null)
                {
                    persistenceService = PersistenceService.CreateDefault(@".\");
                }

                return persistenceService;
            }

            public static Damany.Imaging.Processors.FaceSearchController BuildNewSearchLine(string url, string cameraType)
            {
                Uri uri = new Uri(url);

                var source = Damany.Cameras.Factory.NewFrameStream(uri, cameraType);
                source.Initialize();
                source.Connect();

                return CreateProcessLine(source);
            }

            private static Damany.Imaging.Processors.FaceSearchController CreateProcessLine(IFrameStream source)
            {
                var controller = FaceSearchFactory.CreateNewController(source);

                var writer = new PersistenceWriter(GetDefaultPersistenceService());

                controller.RegisterPortraitHandler(writer);

                return controller;
            }
        }
}
