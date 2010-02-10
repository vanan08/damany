using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    public static class GlobalConfiguration
    {

        public static Endian Endian
        { 
            get
            {
                return endian;
            }
            set
            {
                endian = value;
            }
        }

        public static System.Text.Encoding Encoding
        { 
            get
            {
                return encoding;
            }
            set
            {
                encoding = value;
            }
        }

        private static Endian endian = Endian.Big;
        private static System.Text.Encoding encoding = System.Text.Encoding.ASCII;

    }
}
