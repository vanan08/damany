using System;
using System.Collections.Generic;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.Server;
using Helper = Kise.IdCard.Server.Helper;

namespace Kise.IdCard.QueryServer.UI.App
{
    public class QueryHandler
    {
        private readonly ILink _incomingMessageLink;
        private readonly IdQueryServiceContract.IIdQueryProvider _idQueryService;
        private Messaging.Link.TcpClientLink _client;
        private Random _random = new Random();
        private readonly IView _view;
        private int idx = 0;
        private readonly ILog _logger;
        private const string normalQueryType = "QueryQGRK";
        private const string suspectQueryType = "QueryZTK";

        string[] mockReplies;

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        private void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }

        public QueryHandler(ILink incomingMessagelink, IdQueryServiceContract.IIdQueryProvider idQueryService, IView view, ILog logger)
        {
            if (incomingMessagelink == null) throw new ArgumentNullException("incomingMessagelink");
            if (idQueryService == null) throw new ArgumentNullException("idQueryService");
            if (view == null) throw new ArgumentNullException("view");

            _logger = logger;
            _incomingMessageLink = incomingMessagelink;
            _idQueryService = idQueryService;
            _view = view;
            _incomingMessageLink.NewMessageReceived += _link_NewMessageReceived;

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

        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
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

            try
            {
                var idQueryString = string.Format("sfzh='{0}'", queryIdNo);
                var queryResult = _idQueryService.QueryIdCard(idQueryString);
                var normalIdInfo = Helper.Parse(queryResult.NormalResult);
                var suspectIdInfo = Helper.Parse(queryResult.SuspectResult);
            }
            catch (System.Xml.XmlException)
            {
                replyResult.ErrorCode = 1;
            }

            var replyString = FormatReplyString(replyResult, queryIdNo);
            entry = new LogEntry();
            entry.Description = "发送应答：" + replyString;
            _logger.Log(entry);

            _incomingMessageLink.SendAsync(e.Value.Sender, replyString);
        }

        private string FormatReplyString(QueryResult queryResult, string idCardNo)
        {
            var strings = new List<string>();
            strings.Add(idCardNo);
            strings.Add(queryResult.ErrorCode.ToString());

            if (queryResult.ErrorCode == 0)
            {
                strings.Add(queryResult.IdInfo.Name);
                strings.Add(queryResult.IdInfo.SexCode.HasValue ? queryResult.IdInfo.SexCode.Value.ToString() : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IdInfo.MinorityCode.HasValue ? queryResult.IdInfo.MinorityCode.ToString() : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IdInfo.BornDate.HasValue ? queryResult.IdInfo.BornDate.Value.ToString(Messaging.Constants.BirthDayFormatString) : Messaging.Constants.EmptyString);
                strings.Add(queryResult.IsSuspect ? "1" : "0");
            }

            var result = string.Join(Messaging.Constants.SplitterChar.ToString(), strings);
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
