using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Net
{
    public class MAC
    {
        byte[] buffer = new byte[6];
        public static readonly MAC BroadCast = new MAC(0xff, 0xff, 0xff, 0xff, 0xff, 0xff);

        public MAC(byte[] bytes, int index)
        {
            bytes.CopyTo(buffer, 0);
        }


        public MAC(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5)
            : this(new byte[] { b0, b1, b2, b3, b4, b5 }) { }

        public MAC(byte[] bytes) : this(bytes, 0) { }


        public override string ToString()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}:{4:D2}:{5:D2}",
                buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5]);
        }

        public byte[] GetBytes()
        {
            return (byte[])buffer.Clone();
        }


    }
}
