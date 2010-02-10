using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using MiscUtil.Conversion;
using MiscUtil.IO;
using System.IO;
using Damany.EPolice.Common;

namespace Damany.EPolice.Networking.Test
{
    [TestFixture]
    public class LicensePlateParserTest
    {
        [Test]
        public void ParseTest()
        {

            var mem = new MemoryStream();
            var writer = new EndianBinaryWriter(BigEndianBitConverter.Big, mem, System.Text.ASCIIEncoding.ASCII);

            var plateExpected = new Packets.LicensePlatePacket();
            plateExpected.LicensePlate = new LicensePlate("cd", "12345678", LicensePlateCategory.Car);
            plateExpected.CaptureLocation = new Location(2);

            IList<byte[]> imgData = new List<byte[]>();
            imgData.Add(new byte[] { 0, 1 });
            imgData.Add(new byte[] { 0, 1, 2});
            imgData.Add(new byte[] { 0, 1, 2, 3});

            plateExpected.EvidenceImageData = imgData;

            var licenseBytes = System.Text.Encoding.Unicode.GetBytes(plateExpected.LicensePlate.LicenseNumber);

            writer.Write(licenseBytes);
            writer.Write(01u);
            writer.Write((UInt32) plateExpected.CaptureTime.ToBinary());
            writer.Write((UInt32) plateExpected.CaptureLocation.Id);

            int imgCount = plateExpected.EvidenceImageData.Count;
            writer.Write((uint)imgCount);

            for (int i = 0; i < imgCount;++i)
            {
                writer.Write((uint)imgData[i].Length);
            }

            for (int i = 0; i < imgCount; ++i)
            {
                writer.Write(imgData[i]);
            }

            Networking.Parsers.GlobalConfiguration.Encoding = System.Text.Encoding.Unicode;

            var parser = new Networking.Parsers.LicensePlateParser();
            var buffer = mem.ToArray();
            var plateActual =  parser.Parse(buffer, 0, buffer.Length);

            Assert.AreEqual(plateActual.CaptureLocation.Id, plateExpected.CaptureLocation.Id);
            Assert.AreEqual(plateExpected.LicensePlate.LicenseNumber, plateActual.LicensePlate.LicenseNumber);



        }
    }
}
