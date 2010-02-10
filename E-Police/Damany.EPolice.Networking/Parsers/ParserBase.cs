using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    public class ParserBase
    {
        public ParserBase()
        {
            if (GlobalConfiguration.Endian == Endian.Big)
            {
                BitConverter = new MiscUtil.Conversion.BigEndianBitConverter();
            }
            else
            {
                BitConverter = new MiscUtil.Conversion.LittleEndianBitConverter();
            }

        }

        protected readonly MiscUtil.Conversion.EndianBitConverter BitConverter;
    }
}
