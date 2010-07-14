using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;
using Damany.EPolice.Common;

namespace Damany.EPolice.Networking.Simulator
{
    public static class PacketGenerator
    {
        public static byte[] BuildPacket(Packets.LicensePlatePacket plateExpected)
        {
            var mem = new MemoryStream();
            var writer = new EndianBinaryWriter(Configuration.EndianBitConverter, mem, Configuration.Encoding);

            var licenseBytes = Configuration.Encoding.GetBytes(plateExpected.LicensePlate.LicenseNumber);

            writer.Write(licenseBytes);
            writer.Write(01u);
            writer.Write((UInt32)plateExpected.CaptureTime.ToBinary());
            writer.Write((UInt32)plateExpected.CaptureLocation.Id);

            int imgCount = plateExpected.EvidenceImageData.Count;
            writer.Write((uint)imgCount);

            var imgData = plateExpected.EvidenceImageData;
            for (int i = 0; i < imgCount; ++i)
            {
                writer.Write((uint)imgData[i].Length);
            }

            for (int i = 0; i < imgCount; ++i)
            {
                writer.Write(imgData[i]);
            }

            return mem.ToArray();
        }

        public static Packets.LicensePlatePacket GetDefaultPacket()
        {
            var plateExpected = new Packets.LicensePlatePacket();

            plateExpected.LicensePlate = new LicensePlate("cd", "12345678", LicensePlateCategory.Car);
            plateExpected.CaptureLocation = new Location(2);


            IList<byte[]> imgData = new List<byte[]>();
            imgData.Add( Resource.Winter );
            imgData.Add( Resource.Winter );
            imgData.Add( Resource.Winter );

            plateExpected.EvidenceImageData = imgData;

            return plateExpected;

        }
    }
}
