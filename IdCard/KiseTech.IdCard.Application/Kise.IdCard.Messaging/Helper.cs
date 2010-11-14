using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kise.IdCard.Messaging
{
    public static class Helper
    {
        public static string PackMessage(string message, int sn)
        {
            return sn.ToString() + Constants.SplitterChar + message;
        }

        public static bool TryUnpackMessage(string message, out int sequenceNumber, out string payload)
        {
            if (message == null) throw new ArgumentNullException("message");

            sequenceNumber = -1;
            payload = null;

            var tokens = message.Split(Constants.SplitterChar);
            if (tokens.Length >= 2)
            {
                if (int.TryParse(tokens[0], out sequenceNumber))
                {
                    payload = string.Join(new string(new char[] { Constants.SplitterChar }), tokens, 1, tokens.Length - 1);
                    return true;
                }
            }

            return false;
        }
    }
}
