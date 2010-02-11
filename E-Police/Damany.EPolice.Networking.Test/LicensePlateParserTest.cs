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
        [FixtureSetUp]
        public void Setup()
        {
            Configuration.Encoding = System.Text.Encoding.Unicode;

        }



        [Test]
        public void ParseTest()
        {
            var plateExpected = Simulator.PacketGenerator.GetDefaultPacket();
            var buffer = Simulator.PacketGenerator.BuildPacket(plateExpected);

            var parser = new Networking.Parsers.LicensePlateParser();
            var plateActual = parser.Parse(buffer, 0, buffer.Length) as Packets.LicensePlatePacket;

            Assert.AreEqual(plateActual.CaptureLocation.Id, plateExpected.CaptureLocation.Id);
            Assert.AreEqual(plateExpected.LicensePlate.LicenseNumber, plateActual.LicensePlate.LicenseNumber);
        }
    }
}
