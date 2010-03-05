using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public interface IIpCamera : IFrameStream
    {
        Uri Location { get; set; }
    }
}
