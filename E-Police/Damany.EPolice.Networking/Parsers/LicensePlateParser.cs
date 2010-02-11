using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking.Parsers
{
    using Common.Util;
    using Damany.EPolice.Common;

    public class LicensePlateParser : ParserBase, IParser
    {
        public static event EventHandler<MiscUtil.EventArgs<Packets.LicensePlatePacket>> handlers;

        #region IParser Members

        public bool CanParse(uint type)
        {
            return type == Settings.Default.LicensePlateType;
        }

        public object Parse(byte[] buffer, int offset, int length)
        {
            int cursor = offset;

            string plate = ParsePlateString(buffer, ref cursor);
            uint color = ParseColor(buffer, ref cursor);
            DateTime time = ParseTime(buffer, ref cursor);
            uint location = ParseLocation(buffer, ref cursor);

            uint imgCount = BitConverter.ToUInt32(buffer, cursor);
            cursor += 4;
            uint[] imgLen = ParseImageArrayLength(buffer, ref cursor, imgCount);
            IList<byte[]> images = ParseImgArray(buffer, ref cursor, imgCount, imgLen);

            var packet = new Packets.LicensePlatePacket();
            packet.CaptureLocation = new Location(location);
            packet.CaptureTime = time;
            packet.EvidenceImageData = images;
            packet.LicensePlate = new LicensePlate("", plate, LicensePlateCategory.Car);

            return packet;

        }


        private uint[] ParseImageArrayLength(byte[] buffer, ref int cursor, uint imgCount)
        {
            uint[] imgLen = new uint[imgCount];
            for (int i = 0; i < imgCount; ++i)
            {
                imgLen[i] = BitConverter.ToUInt32(buffer, cursor);
                cursor += 4;
            }
            return imgLen;
        }


        private static IList<byte[]> ParseImgArray(byte[] buffer, ref int cursor, uint imgCount, uint[] imgLen)
        {
            IList<byte[]> imgData = new List<byte[]>();
            for (int i = 0; i < imgCount; ++i)
            {
                uint len = imgLen[i];
                var anImgData = new byte[len];
                buffer.CopyTo(cursor, (int)len, anImgData, 0);

                imgData.Add(anImgData);

                cursor += (int)len;
            }

            return imgData;
        }


        private static string ParsePlateString(byte[] buffer, ref int cursor)
        {
            int plateLen = 16;
            string plate = Configuration.Encoding.GetString(buffer, cursor, plateLen);
            cursor += plateLen;

            return plate;
        }

        private uint ParseColor(byte[] buffer, ref int cursor)
        {
            uint color = BitConverter.ToUInt32(buffer, cursor);
            cursor += 4;

            return color;
        }

        private DateTime ParseTime(byte[] buffer, ref int cursor)
        {
            uint timeBinary = BitConverter.ToUInt32(buffer, cursor);
            cursor += 4;
            DateTime time = DateTime.FromBinary(timeBinary);
            return time;
        }

        private uint ParseLocation(byte[] buffer, ref int cursor)
        {
            uint locationId = BitConverter.ToUInt32(buffer, cursor);
            cursor += 4;
            return locationId;
        }


        #endregion
    }
}
