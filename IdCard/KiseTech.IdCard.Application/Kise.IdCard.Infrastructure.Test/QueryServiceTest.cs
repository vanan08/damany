using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using Kise.IdCard.Infrastructure.Sms;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using MbUnit.Framework.Reflection;

namespace Kise.IdCard.Infrastructure.Test
{
    [TestFixture]
    public class QueryServiceTest
    {

        [Test]
        public void SequenceNumberIncrementTest()
        {
            //
            // TODO: Add test logic here
            //

            QueryService qs = GetQs();

            var message = "123456";

            int sn = GetFieldNextSN(qs);
            Assert.AreEqual(0, sn);

            for (int i = 0; i < 15; i++)
            {
                CallMethodGetNextSequenceNumber(qs);
            }

            sn = GetFieldNextSN(qs);
            Assert.AreEqual(15, sn);

            CallMethodGetNextSequenceNumber(qs);
            sn = GetFieldNextSN(qs);
            Assert.AreEqual(0, sn);
        }

        private void CallMethodGetNextSequenceNumber(QueryService qs)
        {
            Reflector.InvokeMethod(qs, "GetNextSequenceNumber");
        }

        private int GetFieldNextSN(QueryService qs)
        {
            return (int)Reflector.GetField(qs, "_nextSequenceNumber");
        }

        private QueryService GetQs()
        {
            var t = new Sms.FakeTransport();
            return new QueryService(t);
        }
    }
}
