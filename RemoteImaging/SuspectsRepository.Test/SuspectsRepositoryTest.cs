using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Imaging.PlugIns;

namespace SuspectsRepository.Test
{
    [TestFixture]
    public class SuspectsRepositoryTest
    {
        [Test]
        public void SaveToTest()
        {
            SuspectsRepositoryManager mnger = SuspectsRepositoryManager.LoadFrom(@"d:\imglib");


            Assert.IsTrue( mnger.Peoples.Count() == 1  );

        }


        string RelativePathToAbsolute(string path)
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);
        }
    }
}
