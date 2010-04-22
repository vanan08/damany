using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Damany.Util;

namespace Damany.Imaging.Processors
{

    public class FaceSearchController
    {
        private readonly IOperation<Frame> _frameProcessor;
        private readonly IConvertor<Frame, Portrait> _convertor;
        private readonly IOperation<Portrait> _portraitProcessor;

        private readonly Util.PersistentWorker _worker = new PersistentWorker();

        public FaceSearchController(IOperation<Frame> frameProcessor,
                                     IConvertor<Frame, Portrait> convertor,
                                     IOperation<Portrait> portraitProcessor)
        {
            _frameProcessor = frameProcessor;
            _convertor = convertor;
            _portraitProcessor = portraitProcessor;

            _worker.DoWork = delegate
            {
                var frames = frameProcessor.Execute(null).ToList();
                var portraits = _convertor.Execute(frames).ToList();
                var portraitsAfterProcess = _portraitProcessor.Execute(portraits).ToList();

                foreach (var portrait in portraitsAfterProcess)
                {
                    portrait.Dispose();
                }
            };
        }


        public void Start()
        {
            _worker.Start();
        }

        public void Stop()
        {
            _worker.Stop();
        }

        public void SpeedUp()
        {
            _worker.WorkFrequency *= 2;
        }

        public void SlowDown()
        {
            _worker.WorkFrequency /= 2;
        }

    }
}
