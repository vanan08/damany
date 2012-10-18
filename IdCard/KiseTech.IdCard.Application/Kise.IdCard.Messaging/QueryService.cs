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

        public Task<Messaging.WcfService.IdCardInfo> QueryByIdNumberAsync(string idNumber)
        {
            return TaskEx.Run(() =>
                                  {
                                      WcfService.IdQueryWcfServiceClient proxy = null;
                                      try
                                      {
                                          proxy = new WcfService.IdQueryWcfServiceClient();
                                          return proxy.QueryByIdNumber(idNumber);
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