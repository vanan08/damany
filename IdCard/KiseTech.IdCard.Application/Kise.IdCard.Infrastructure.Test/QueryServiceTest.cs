using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kise.IdCard.Infrastructure.Test
{

    [TestClass]
    public class QueryServiceTest
    {

        [TestMethod]
        public void SequenceNumberIncrementTest()
        {
            //
            // TODO: Add test logic here
            //
            var t = new FakeLink();
            t.Return(q => new IncomingMessage());
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

        [TestMethod]
        public void QueryTest()
        {
            var message = "1*123456";
            var reply = "0*1*987654";



            var fakeT = new FakeLink();
            var fakeEp = new CellPhoneEndPoint();
            fakeT.Return(q => new IncomingMessage("0*abc"));
            var qs = GetQs(fakeT);
            var result = qs.QueryAsync(null, message).Result;
            Assert.AreEqual("abc", result.Message);
            //Assert.IsFalse(result.IsTimedOut);

            fakeT.Return(q => new IncomingMessage("0*abc"));
            result = qs.QueryAsync(null, message).Result;
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
            //Assert.IsTrue(result.IsTimedOut);


            fakeT.Return(q => new IncomingMessage("2*def"));
            result = qs.QueryAsync(null, message).Result;
            Assert.AreEqual("def", result.Message);

            fakeT.Return(q =>
                             {
                                 var idx = q.IndexOf("*");
                                 var sn = q.Substring(0, idx);
                                 return new IncomingMessage(sn.ToString() + "*" + DateTime.Now.Millisecond);
                             });

            for (int i = 0; i < 10; i++)
            {
                var t1 = qs.QueryAsync(fakeEp, message);
                System.Threading.Thread.Sleep(5);
                var t2 = qs.QueryAsync(fakeEp, message);
                System.Threading.Thread.Sleep(10);
                var t3 = qs.QueryAsync(fakeEp, message);
                System.Threading.Thread.Sleep(15);
                var t4 = qs.QueryAsync(fakeEp, message);
                System.Threading.Thread.Sleep(20);


                var all = TaskEx.WhenAll(t1, t2, t3, t4).ContinueWith(ant =>
                                                        {
                                                            AssertAndPrint(t1);
                                                            AssertAndPrint(t2);
                                                            AssertAndPrint(t3);
                                                            AssertAndPrint(t4);
                                                        });
                all.Wait();
            }

        }

        [TestMethod]
        public void QueryTimeOutTest()
        {
            var fl = new FakeLink();
            fl.Return(q => new IncomingMessage("0*123"));
            fl.DelayInMs = 30000;

            var qs = GetQs(fl);
            qs.TimeOutInSeconds = 2;

            var ep = new IPEndPoint(IPAddress.Loopback, 10000);
            var reply = qs.QueryAsync(ep, "").Result;
            //Assert.IsTrue(reply.IsTimedOut);

            fl.DelayInMs = 1000;
            fl.Return(q=> new IncomingMessage("1*123"));
            reply = qs.QueryAsync(ep, "dfdf").Result;
            //Assert.IsFalse(reply.IsTimedOut);
            Assert.AreEqual("123", reply.Message);
        }

        private void AssertAndPrint(Task<ReplyMessage> t1)
        {
            Assert.AreNotEqual(t1.Result.Message, String.Empty);
            System.Diagnostics.Debug.WriteLine(t1.Result.Message);
        }


        private void CallMethodGetNextSequenceNumber(QueryService qs)
        {
            // Reflector.InvokeMethod(qs, "GetNextSequenceNumber");
        }

        private int GetFieldNextSN(QueryService qs)
        {
            //return (int)Reflector.GetField(qs, "_nextSequenceNumber");
            return 0;
        }

        private QueryService GetQs(ILink t)
        {
            return new QueryService(t);
        }

    }
}
