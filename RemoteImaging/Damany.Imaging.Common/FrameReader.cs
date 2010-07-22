using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public class FrameReader
    {
        private readonly IFrameStream _source;

        public FrameReader(IFrameStream source)
        {
            _source = source;
        }

        public IEnumerable<Frame> Execute(IEnumerable<Frame> inputs)
        {
            var f = _source.RetrieveFrame();
            yield return f;

        }
    }
}
