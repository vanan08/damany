﻿using System;
using System.Collections.Generic;
using System.Text;
using Kise.IdCard.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kise.IdCard.Infrastructure.Test
{
    using CardReader;
    using System.Threading.Tasks;
    using Kise.IdCard.Messaging;

    [TestClass]
    public class CardReaderTest
    {
        [TestMethod]
        public void ReadSuccess()
        {
            //
            // TODO: Add test logic here
            //
            var info = ReadInfoAsync(1001).Result;

            Assert.IsNotNull(info.Address);
            Assert.IsNotNull(info.BornDate);
            Assert.IsNotNull(info.GrantDept);
            Assert.IsNotNull(info.IdCardNo);
            Assert.IsTrue(info.MinorityCode.HasValue);
            Assert.IsNotNull(info.Name);
            Assert.IsNotNull(info.PhotoData);
            Assert.IsNotNull(info.SexCode.HasValue);
            Assert.IsNotNull(info.ValidateFrom);
            Assert.IsNotNull(info.ValidateUntil);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadReaderDisconnected()
        {
            var info = ReadInfoAsync(1001);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadWithWrongPortNumber()
        {
            var info = ReadInfoAsync(9999);
            var result = info.Result;
        }

        private async Task<Model.IdCardInfo> ReadInfoAsync(int port)
        {
            var reader = new IdCardReader(port);
            var info = await reader.ReadAsync();
            return info;
        }

    }
}