using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
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

            public static FaceSearchController BuildNewSearchLine(
                CameraInfo cam, 
                IEnumerable< IOperation<Damany.Imaging.Common.Frame> > framefilters,
                IConvertor<Damany.Imaging.Common.Frame, Damany.Imaging.Common.Portrait> convertor,
                IEnumerable< IOperation<Damany.Imaging.Common.Portrait>> portraitFilters)
            {
                var source = Damany.Cameras.Factory.NewFrameStream(cam);
                source.Initialize();
                source.Connect();

                var frameFilters = framefilters.ToList();
                frameFilters.Insert(0, new FrameReader(source));

                return CreateProcessLine(source, frameFilters, convertor, portraitFilters);
            }

            private static FaceSearchController CreateProcessLine(
                IFrameStream source, 
                IEnumerable<IOperation<Damany.Imaging.Common.Frame>> frameFilters,
                IConvertor<Damany.Imaging.Common.Frame, Damany.Imaging.Common.Portrait> convertor,
                IEnumerable<IOperation<Damany.Imaging.Common.Portrait>> portraitFilters)
            {

                var ff = new Operations<Imaging.Common.Frame>();
                foreach (var frameFilter in frameFilters)
                {
                    ff.Register(frameFilter);
                }

                var pf = new Operations<Damany.Imaging.Common.Portrait>();
                foreach (var portraitFilter in portraitFilters)
                {
                    pf.Register(portraitFilter);
                }

                var controller = FaceSearchFactory.CreateNewController(
                    source, 
                    ff, 
                    convertor,
                    pf);

                return controller;
            }
        }
}
