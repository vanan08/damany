using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Contracts
{
    public interface IFrameStream
    {
        void Initialize();
        void Connect();
        void Close();

        Frame RetrieveFrame();
        int Id { get; set; }
        string Description { get; }
    }
}
