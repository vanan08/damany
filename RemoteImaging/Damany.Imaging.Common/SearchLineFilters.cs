using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public class SearchLineFilters
    {
        private readonly IList<IOperation<Frame>> _frameOperations
            = new List<IOperation<Frame>>();

        private readonly IList<IOperation<Portrait>> _portraitOperations
            = new List<IOperation<Portrait>>();

        public void RegisterFrameOperation(IOperation<Frame> op)
        {
            _frameOperations.Add(op);
        }

        public IOperation<Frame> GetFrameOperation()
        {
            var ops = new Operations<Frame>();

            foreach (var frameOperation in _frameOperations)
            {
                ops.Register(frameOperation);
            }

            return ops;
        }

        public void RegisterFrameOperation(IOperation<Portrait> op)
        {
            _portraitOperations.Add(op);
        }

        public IOperation<Portrait> GetPortraitOperation()
        {
            var ops = new Operations<Portrait>();

            foreach (var portraitOperation in _portraitOperations)
            {
                ops.Register(portraitOperation);
            }

            return ops;
        }

    }
}
