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
        public class SearchLineBuilder
        {
            public delegate SearchLineBuilder SearchLineFactory(CameraInfo spec);

            public SearchLineBuilder(
                CameraInfo spec, 
                IEnumerable< IOperation<Damany.Imaging.Common.Frame> > framefilters,
                IConvertor<Damany.Imaging.Common.Frame, Damany.Imaging.Common.Portrait> convertor,
                IEnumerable< IOperation<Damany.Imaging.Common.Portrait>> portraitFilters)
            {
                _spec = spec;
                _framefilters = framefilters;
                _convertor = convertor;
                _portraitFilters = portraitFilters;
            }

            public FaceSearchController Build( )
            {
                var source = Damany.Cameras.Factory.NewFrameStream(_spec);
                source.Initialize();
                source.Connect();

                var frameFilters = _framefilters.ToList();
                frameFilters.Insert(0, new FrameReader(source));



                var ff = new Operations<Imaging.Common.Frame>();
                foreach (var frameFilter in frameFilters)
                {
                    ff.Register(frameFilter);
                }

                var pf = new Operations<Damany.Imaging.Common.Portrait>();
                foreach (var portraitFilter in _portraitFilters)
                {
                    pf.Register(portraitFilter);
                }

                var controller = FaceSearchFactory.CreateNewController(
                    source, 
                    ff, 
                    _convertor,
                    pf);

                return controller;
            }


            private readonly CameraInfo _spec;
            private readonly IEnumerable<IOperation<Imaging.Common.Frame>> _framefilters;
            private readonly IConvertor<Imaging.Common.Frame, Imaging.Common.Portrait> _convertor;
            private readonly IEnumerable<IOperation<Imaging.Common.Portrait>> _portraitFilters;
        }
}
