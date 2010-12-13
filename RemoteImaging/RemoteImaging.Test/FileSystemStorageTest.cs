using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace RemoteImaging.Test
{
    [TestFixture]
    public class FileSystemStorageTest
    {
        [Test]
        public void Test()
        {
            DateTime dt = DateTime.Parse("2010-1-22 17:55");

            bool result = FileSystemStorage.FaceImagesCapturedWhen(2, dt);

            Assert.IsTrue(result);


        }
    }
}
