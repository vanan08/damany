using System;
using System.Collections.Concurrent;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;
using Kise.IdCard.Messaging.Link;

namespace Kise.IdCard.Messaging
{
    public class QueryService
    {

        private object _locker = new object();
        private int _nextSequenceNumber;
        
        public int TimeOutInSeconds { get; set; }

        public QueryService()
        {

            TimeOutInSeconds = 120;
        }

        public void Start()
        {
            
        }

        public  Task<ReplyMessage> QueryAsync(string message)
        {
            return TaskEx.Run(() =>
                           {
                               var splits = message.Split(new[] {'*'});

                               WcfService.IdQueryWcfServiceClient proxy = null;
                               try
                               {
                                   proxy = new WcfService.IdQueryWcfServiceClient();
                                   
                                   var msg = proxy.QueryId(message);
                                  
                                   return new ReplyMessage(msg);
                               }
                               catch (Exception ex)
                               {
                                   if (proxy != null)
                                   {
                                       proxy.Abort();
                                   }
                                   
                                   var reply = new ReplyMessage(string.Empty);
                                   reply.Error = ex;
                                   return reply;
                               }
                               finally
                               {
                                   if (proxy != null && proxy.State == CommunicationState.Opened)
                                   {
                                        proxy.Close();
                                   }
                               }
                           });


        }
    }
}