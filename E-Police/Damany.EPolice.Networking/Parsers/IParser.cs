using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    public interface IParser
    {
        bool CanParse(uint type);
        object Parse(byte[] bufer, int offset, int length);
    }
}
