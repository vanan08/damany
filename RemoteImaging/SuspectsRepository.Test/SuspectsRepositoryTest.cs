using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace SuspectsRepository.Test
{
    [TestFixture]
    public class SuspectsRepositoryTest
    {
        [Test]
        public void SaveToTest()
        {
            SuspectsRepositoryManager mnger = SuspectsRepositoryManager.CreateNewIn("lib");

            var p1 = new PersonInfo();
            p1.FileName = RelativePathToAbsolute("abc");

            var p2 = new PersonInfo() { FileName = RelativePathToAbsolute("def"), };


            mnger.SaveTo("wanted.xml");

            SuspectsRepositoryManager saved = SuspectsRepositoryManager.LoadFrom("wanted.xml");
            Assert.IsTrue( saved.Count == 2);

        }


        string RelativePathToAbsolute(string path)
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);
        }
    }
}
