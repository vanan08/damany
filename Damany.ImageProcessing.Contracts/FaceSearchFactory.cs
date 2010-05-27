using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public static class FaceSearchFactory
    {
        public static FaceSearchController CreateNewController(IFrameStream source, 
                                                               IOperation<Frame> frameProcessor,
                                                               IConvertor<Frame, Portrait> convertor,
                                                               IOperation<Portrait> portraitProcessor)
        {

            var controller = new FaceSearchController(source, frameProcessor, convertor, portraitProcessor);

            return controller;

        }
    }
}
