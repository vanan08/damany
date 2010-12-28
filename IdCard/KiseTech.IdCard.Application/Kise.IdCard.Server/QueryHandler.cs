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
        private readonly ILink _link;
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

        public QueryHandler(ILink link, IView view, ILog logger)
        {

            if (link == null) throw new ArgumentNullException("link");
            if (view == null) throw new ArgumentNullException("view");

            _logger = logger;
            _link = link;
            _view = view;
            _link.NewMessageReceived += _link_NewMessageReceived;

            //结果代码0:正常，1:警报.
            mockReplies = new[] { "0*正常", "1*网上追逃", "1*虚假身份证" };

        }

        public async void Start()
        {
            _link.Start();
            _client = new TcpClientLink();
            _client.NewMessageReceived += new EventHandler<MiscUtil.EventArgs<IncomingMessage>>(_client_NewMessageReceived);
            _client.Start();
        }

        void _client_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {

            // System.Diagnostics.Debug.WriteLine("====received query result: ====\r\n " + e.Value.ReceiveTime);
            _link.SendAsync("13547962367", e.Value.Message);
        }

        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {
            InvokeNewMessageReceived(e);

            _client.SendAsync("dfdf", e.Value.Message);

            //            var entry = new LogEntry();
            //            entry.Sender = e.Value.Sender;
            //            entry.Description = e.Value.Message.Insert(0, "收到查询: ");

            //            _logger.Log(entry);

            //            var unpackedMsg = string.Empty;
            //            var sn = -1;
            //            var idInfo = new IdCardInfo();// UnpackMessage(e.Value.Message);
            //            idInfo.IdCardNo = "510403197309112610";

            //            entry = new LogEntry();
            //            entry.Description = "查询数据库";
            //            _logger.Log(entry);

            //            var queryString = string.Format("sfzh='{0}'", idInfo.IdCardNo);
            //            var normalQuery = new IdQuery.IdQueryProviderClient();
            //            var normalResult = normalQuery.QueryIdCard(normalQueryType, queryString);
            //            normalQuery.Close();
            //            var idNormalInfo = Helper.Parse(normalResult);

            //            var stringMatchResult = GetIdInfoMatchResult(idInfo, idNormalInfo.IdInfos[0]);

            //            var suspectQueryString = string.Format("sfzh='{0}'", idInfo.IdCardNo);
            //            var suspectQuery = new IdQuery.IdQueryProviderClient();
            //            var suspectRresult = suspectQuery.QueryIdCard(suspectQueryType, queryString);
            //            suspectQuery.Close();
            //            var suspectInfo = Helper.Parse(suspectRresult);

            //            string resultCode = "0";

            //            var replyStrings = new string[] { idInfo.IdCardNo, normalResult, suspectRresult };
            //            var replyMsg = string.Join("*", replyStrings);


            //#if DEBUG
            //            var idx = _random.Next(0, 2);
            //            var msg = idInfo.IdCardNo + "*" + mockReplies[idx];
            //            _link.SendAsync(e.Value.Sender, msg);
            //#else
            //            _link.SendAsync(e.Value.Sender, replyMsg);
            //#endif

            //            entry = new LogEntry();
            //            entry.Description = "发送应答：" + replyMsg;
            //            _logger.Log(entry);


        }

        private string GetIdInfoMatchResult(Model.IdCardInfo idInfo, Model.IdCardInfo idCardInfo)
        {
            var sb = new StringBuilder();
            return sb.ToString();
        }

        private IdCard.Model.IdCardInfo UnpackMessage(string p)
        {
            var values = p.Split(new[] { '*' });
            var idInfo = new IdCard.Model.IdCardInfo();

            idInfo.IdCardNo = values[0];
            idInfo.Name = values[1];
            idInfo.SexCode = int.Parse(values[2]);
            return idInfo;
        }
    }
}
