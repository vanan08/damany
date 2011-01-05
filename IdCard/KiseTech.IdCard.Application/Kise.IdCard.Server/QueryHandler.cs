using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.Model;
using MiscUtil;
using RBSPAdapter_COM;

namespace Kise.IdCard.Server
{
    public class QueryHandler
    {
        private readonly ILink _incomingMessageLink;
        private readonly IIdQueryService _idQueryService;
        private Messaging.Link.TcpClientLink _client;
        private Random _random = new Random();
        private readonly IView _view;
        private int idx = 0;
        private readonly ILog _logger;
        private const string normalQueryType = "QueryQGRK";
        private const string suspectQueryType = "QueryZTK";

        private System.Collections.Concurrent.ConcurrentQueue<MiscUtil.EventArgs<IncomingMessage>> _outstandingQueue
            = new ConcurrentQueue<MiscUtil.EventArgs<IncomingMessage>>();

        private Thread _queryHandlerWorker;
        string[] mockReplies;

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        private void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }

        public QueryHandler(ILink incomingMessagelink, IIdQueryService idQueryService, IView view, ILog logger)
        {
            if (incomingMessagelink == null) throw new ArgumentNullException("incomingMessagelink");
            if (idQueryService == null) throw new ArgumentNullException("idQueryService");
            if (view == null) throw new ArgumentNullException("view");

            _logger = logger;
            _incomingMessageLink = incomingMessagelink;
            _idQueryService = idQueryService;
            _view = view;
            _incomingMessageLink.NewMessageReceived += (s, e) =>
                                                           {
                                                               _outstandingQueue.Enqueue(e);
                                                           };

            
            _queryHandlerWorker = new Thread(processQuery);
            _queryHandlerWorker.IsBackground = true;
            _queryHandlerWorker.Start();
            //结果代码0:正常，1:警报.
            mockReplies = new[] { "0*正常", "1*网上追逃", "1*虚假身份证" };

        }

        public void Start()
        {
            _incomingMessageLink.Start();
        }

        void _client_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {

            // System.Diagnostics.Debug.WriteLine("====received query result: ====\r\n " + e.Value.ReceiveTime);
            _incomingMessageLink.SendAsync("13547962367", e.Value.Message);
        }

        void processQuery(object userState)
        {
            while (true)
            {
                MiscUtil.EventArgs<IncomingMessage> e = null;
                if (_outstandingQueue.TryDequeue(out e))
                {
                    InvokeNewMessageReceived(e);

                    var unpackedMsg = string.Empty;
                    var sn = -1;
                    string queryIdNo = e.Value.Message;

                    var entry = new LogEntry();
                    entry.Sender = e.Value.Sender;
                    entry.Description = e.Value.Message.Insert(0, "收到查询: " + queryIdNo);
                    _logger.Log(entry);

                    QueryResult replyResult = new QueryResult();

                    entry = new LogEntry();
                    entry.Description = "查询数据库...";
                    _logger.Log(entry);

                    var idQueryString = string.Format("sfzh='{0}'", queryIdNo);
                    var normalResult = _idQueryService.QueryIdCard(normalQueryType, idQueryString);
                    var normalIdInfo = Helper.Parse(normalResult);

                    var suspectQuery = _idQueryService.QueryIdCard(suspectQueryType, idQueryString);
                    var suspectIdInfo = Helper.Parse(suspectQuery);

                    replyResult.IsSuspect = suspectIdInfo.Length > 0;

                    if (normalIdInfo.Length > 0)
                    {
                        replyResult.IdInfo = normalIdInfo[0];
                    }

                    var replyString = FormatReplyString(replyResult, queryIdNo);
                    entry = new LogEntry();
                    entry.Description = "发送应答：" + replyString;
                    _logger.Log(entry);

                    _incomingMessageLink.SendAsync(e.Value.Sender, replyString);

                }
                else
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }

        }

        //消息应答格式 id*errorCode*Name*SexCode*minorityCode*birthDay*isSuspect
        private string FormatReplyString(QueryResult replyResult, string idCardNo)
        {
            var strings = new List<string>();
            strings.Add(idCardNo);
            strings.Add(replyResult.ErrorCode.ToString());

            if (replyResult.ErrorCode == 0)
            {
                strings.Add(string.IsNullOrEmpty(replyResult.IdInfo.Name) ? Messaging.Constants.EmptyString : replyResult.IdInfo.Name);
                strings.Add(replyResult.IdInfo.SexCode.HasValue ? replyResult.IdInfo.SexCode.Value.ToString() : Messaging.Constants.EmptyString);
                strings.Add(replyResult.IdInfo.MinorityCode.HasValue ? replyResult.IdInfo.MinorityCode.ToString() : Messaging.Constants.EmptyString);
                strings.Add(replyResult.IdInfo.BornDate.HasValue ? replyResult.IdInfo.BornDate.Value.ToString(Messaging.Constants.BirthDayFormatString) : Messaging.Constants.EmptyString);
                strings.Add(replyResult.IsSuspect ? "1" : "0");
            }

            System.Diagnostics.Debug.WriteLine("name: " + replyResult.IdInfo.Name);

            var result = string.Join(Messaging.Constants.SplitterChar.ToString(), strings);

            System.Diagnostics.Debug.WriteLine("formatted string:" + result);


            return result;
        }



        private IdCard.Model.IdCardInfo UnpackMessage(string p)
        {
            try
            {
                var values = p.Split(new[] { '*' });
                var idInfo = new IdCard.Model.IdCardInfo();

                idInfo.IdCardNo = values[0];
                idInfo.Name = values[1];
                idInfo.SexCode = int.Parse(values[2]);
                return idInfo;
            }
            catch (IndexOutOfRangeException e)
            {
                throw new InvalidQueryFormatException(e);
            }
        }
    }
}
