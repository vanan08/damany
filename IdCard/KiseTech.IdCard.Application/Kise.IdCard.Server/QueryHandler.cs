using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var idInfo = UnpackMessage(e.Value.Message);

            var entry = new LogEntry();
            entry.Sender = e.Value.Sender;
            entry.Description = e.Value.Message.Insert(0, "收到查询: " + idInfo.IdCardNo);
            _logger.Log(entry);


            entry = new LogEntry();
            entry.Description = "查询数据库";
            _logger.Log(entry);

            var queryString = string.Format("sfzh='{0}'", idInfo.IdCardNo);
            var normalResult = _idQueryService.QueryIdCard(normalQueryType, queryString);
            var idNormalInfo = Helper.Parse(normalResult);


            var suspectQueryString = string.Format("sfzh='{0}'", idInfo.IdCardNo);
            var suspectQuery = _idQueryService.QueryIdCard(suspectQueryType, suspectQueryString);
            var suspectRresult = Helper.Parse(suspectQuery);

            var replyStrings = new string[] { idInfo.IdCardNo, normalResult, suspectRresult };
            var replyMsg = string.Join("*", replyStrings);


#if DEBUG
            var idx = _random.Next(0, 2);
            var msg = idInfo.IdCardNo + "*" + mockReplies[idx];
            _incomingMessageLink.SendAsync(e.Value.Sender, msg);
#else
            _link.SendAsync(e.Value.Sender, replyMsg);
#endif

            entry = new LogEntry();
            entry.Description = "发送应答：" + replyMsg;
            _logger.Log(entry);


        }

        private List<string> GetUnMatchedFields(IdCardInfo idInfoX, IdCardInfo idInfoY)
        {
            var sb = new StringBuilder();





            return sb.ToString();
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
