using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public interface IIpCamera : IFrameStream
    {
        void Connect();

        Uri Location { get; set; }
    }
}
