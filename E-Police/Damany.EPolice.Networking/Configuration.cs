using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking
{
    public static class Configuration
    {


        public static MiscUtil.Conversion.EndianBitConverter EndianBitConverter
        {
            get
            {
                return endianBitConverter;
            }
            set
            {
                endianBitConverter = value;
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

        public static WorkingMode WorkingMode
        {
            get
            {
                return workingMode;
            }
            set
            {
                workingMode = value;
            }
        }


        public static string RemoteIp
        {
            get
            {
                return remoteIp;
            }
            set
            {
                remoteIp = value;
            }
        }

        public static int RemotePort
        {
            get
            {
                return remotePort;
            }
            set
            {
                remotePort = value;
            }
        }

        public static int LocalPort
        {
            get
            {
                return localPort;
            }
            set
            {
                localPort = value;
            }
        }

        private static MiscUtil.Conversion.EndianBitConverter endianBitConverter = MiscUtil.Conversion.BigEndianBitConverter.Big;
        private static System.Text.Encoding encoding = System.Text.Encoding.ASCII;
        private static WorkingMode workingMode = WorkingMode.Client;
        private static string remoteIp;
        private static int remotePort;
        private static int localPort;

    }
}
