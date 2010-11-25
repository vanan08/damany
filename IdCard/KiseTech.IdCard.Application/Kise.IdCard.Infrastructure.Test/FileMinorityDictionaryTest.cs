using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using Kise.IdCard.Model;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace Kise.IdCard.Infrastructure.Test
{
    [TestFixture]
    public class FileMinorityDictionaryTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //

            var dictionary = FileMinorityDictionary.Instance;

            
            Assert.IsTrue(dictionary.Values.Count == 58);

            Assert.IsTrue(dictionary[1] == "汉");
            Assert.IsTrue(dictionary[27] == "纳西");
            Assert.IsTrue(dictionary[98] == "外国血统中国籍人士");

            Assert.IsFalse(dictionary[1] == "藏");
        }
    }
}
