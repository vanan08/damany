using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.ImageProcessing.Contracts
{
    public interface IFrameStream
    {
        Frame RetrieveFrame();
        int Id { get; }
    }
}
