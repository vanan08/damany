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

                return CreateProcessLine(source);
            }

            private static Damany.Imaging.Processors.FaceSearchController CreateProcessLine(Damany.Imaging.Common.IFrameStream source)
            {
                var controller = FaceSearchFactory.CreateNewController(source);

                return controller;
            }
        }
}
