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
            BitConverter = Configuration.EndianBitConverter;
            this.Encoding = Configuration.Encoding;
        }

        protected System.Text.Encoding Encoding;

        protected readonly MiscUtil.Conversion.EndianBitConverter BitConverter;
    }
}
