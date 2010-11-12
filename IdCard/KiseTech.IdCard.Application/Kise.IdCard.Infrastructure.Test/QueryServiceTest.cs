using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gallio.Framework;
using Kise.IdCard.Infrastructure.Sms;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using MbUnit.Framework.Reflection;
using Moq;

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
            var t = new FakeTransport();
            t.Return(q => "");
            QueryService qs = GetQs(t);

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

        [Test]
        public void QueryTest()
        {
            var message = "1*123456";
            var reply = "0*1*987654";

            var moq = new Mock<ITransport>();
            moq.Setup(t => t.QueryAsync("", message)).Returns(TaskEx.Run(() => reply));


            var fakeT = new FakeTransport();
            fakeT.Return(q => "0*abc");
            var qs = GetQs(fakeT);
            var result = qs.QueryAsync("", message).Result;
            Assert.AreEqual("abc", result);

            fakeT.Return(q => "0*abc");
            result = qs.QueryAsync("", message).Result;
            Assert.AreEqual(string.Empty, result);

            fakeT.Return(q => "2*def");
            result = qs.QueryAsync("", message).Result;
            Assert.AreEqual("def", result);

        }


        private void CallMethodGetNextSequenceNumber(QueryService qs)
        {
            Reflector.InvokeMethod(qs, "GetNextSequenceNumber");
        }

        private int GetFieldNextSN(QueryService qs)
        {
            return (int)Reflector.GetField(qs, "_nextSequenceNumber");
        }

        private QueryService GetQs(ITransport t)
        {
            return new QueryService(t);
        }

    }
}
