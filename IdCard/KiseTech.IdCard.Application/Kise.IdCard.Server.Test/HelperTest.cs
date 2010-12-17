using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Server.Test
{
    [TestFixture]
    public class HelperTest
    {
        [Test]
        public void ReturnCorrectResultTest()
        {
            //
            // TODO: Add test logic here
            //
            var xmlstring = System.IO.File.ReadAllText("normal.xml");

            var qr = Helper.Parse(xmlstring);

            Assert.IsTrue(string.IsNullOrEmpty(qr.IdInfos[0].IdNo) == false);
            Assert.IsTrue(string.IsNullOrEmpty(qr.IdInfos[0].IdNo) == false);
            Assert.AreNotEqual(DateTime.MinValue, qr.IdInfos[0].BornDate);

            foreach (var id in qr.IdInfos)
            {
                if (id.ImageData != null)
                {
                    var img = System.Drawing.Image.FromStream(new System.IO.MemoryStream(id.ImageData));
                    img.Save(id.IdNo + ".jpg");
                }

            }

        }
    }
}
