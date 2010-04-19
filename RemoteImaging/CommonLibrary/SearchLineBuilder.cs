using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL.Providers;
using Damany.Imaging.Processors;
using Damany.Imaging.Handlers;
using Damany.PC.Domain;

namespace Damany.RemoteImaging.Common
{
        public static class SearchLineBuilder
        {
            private static LocalDb4oProvider persistenceService = null;
            static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);

            public static Damany.Imaging.Processors.FaceSearchController BuildNewSearchLine(CameraInfo cam)
            {
                var source = Damany.Cameras.Factory.NewFrameStream(cam);
                source.Initialize();
                source.Connect();

                return CreateProcessLine(source, 
                                       new Damany.Imaging.SearchLineOptions
                                           {
                                               FaceCompareEnabled = cam.FaceCompareEnabled
                                           });
            }

            private static FaceSearchController CreateProcessLine(
                Damany.Imaging.Common.IFrameStream source,
                Damany.Imaging.SearchLineOptions config)
            {
                var controller = FaceSearchFactory.CreateNewController(source, config);

                return controller;
            }
        }
}
