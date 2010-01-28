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
            SuspectsRepositoryManager mnger = new SuspectsRepositoryManager();

            var p1 = new PersonInfo();
            p1.FileName = RelativePathToAbsolute("abc");

            var p2 = new PersonInfo() { FileName = RelativePathToAbsolute("def"), };

            mnger.Suspects.Add(p1.FileName, p1);
            mnger.Suspects.Add(p2.FileName, p2);

            mnger.SaveTo("wanted.xml");

            SuspectsRepositoryManager saved = SuspectsRepositoryManager.LoadFrom("wanted.xml");
            Assert.IsTrue( saved.Suspects.Count == 2);
        }


        string RelativePathToAbsolute(string path)
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), path);
        }
    }
}
