using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using MiscUtil;

namespace Kise.IdCard.Server
{
    public class QueryHandler
    {
        private readonly ILink _link;
        private Random _random = new Random();
        private readonly IView _view;
        private readonly IdQueryService _idQueryService;
        private int idx = 0;
        private readonly ILog _logger;

        string[] mockReplies;

        public event EventHandler<MiscUtil.EventArgs<IncomingMessage>> NewMessageReceived;

        private void InvokeNewMessageReceived(MiscUtil.EventArgs<IncomingMessage> e)
        {
            EventHandler<MiscUtil.EventArgs<IncomingMessage>> handler = NewMessageReceived;
            if (handler != null) handler(this, e);
        }

        public QueryHandler(IdQueryService idQueryService, ILink link, IView view, ILog logger)
        {
            _idQueryService = idQueryService;
            _logger = logger;
            if (link == null) throw new ArgumentNullException("link");
            if (view == null) throw new ArgumentNullException("view");

            _link = link;
            _view = view;
            _link.NewMessageReceived += _link_NewMessageReceived;

            mockReplies = new[] { "0*正常", "1*网上追逃", "1*虚假身份证" };
        }

        public void Start()
        {
            _link.Start();
        }

        void _link_NewMessageReceived(object sender, MiscUtil.EventArgs<IncomingMessage> e)
        {
            InvokeNewMessageReceived(e);

            var entry = new LogEntry();
            entry.Sender = e.Value.Sender;
            entry.Description = e.Value.Message.Insert(0, "收到查询: ");

            _logger.Log(entry);

            var unpackedMsg = string.Empty;
            var sn = -1;
            var idInfo = UnpackMessage(e.Value.Message);

            entry = new LogEntry();
            entry.Description = "查询数据库";
            _logger.Log(entry);
            var result =  _idQueryService.QueryAsync(idInfo.IdCardNo);

            var replyStrings = new string[] { idInfo.IdCardNo, result.NormalResult, result.SuspectResult };
            var replyMsg = string.Join("*", replyStrings);
            

#if DEBUG
            var idx = _random.Next(0, 2);
            var msg = idInfo.IdCardNo + "*" + mockReplies[idx];
            _link.SendAsync(e.Value.Sender, msg);
#else
            _link.SendAsync(e.Value.Sender, replyMsg);
#endif

            entry = new LogEntry();
            entry.Description = "发送应答：" + replyMsg;
            _logger.Log(entry);


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
