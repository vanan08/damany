using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    public interface IParser<T>
    {
        bool CanParse(byte[] bufer, int offset, int length);
        T Parse(byte[] bufer, int offset, int length);
    }
}
