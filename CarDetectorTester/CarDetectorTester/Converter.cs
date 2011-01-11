using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDetectorTester
{
    public static class Converter
    {
        public static byte[] StringToByteArray(String hex)
        {
            int numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string ByteArrayToString(byte[] ba, string formatString)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat(formatString, b);
            return hex.ToString();
        }
    }
}
