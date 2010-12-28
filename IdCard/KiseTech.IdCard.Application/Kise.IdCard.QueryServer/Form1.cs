using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.Server;

namespace Kise.IdCard.QueryServer
{
    public partial class Form1 : Form, IView, ILog
    {
        private IdQueryService _idQueryService;
        TcpServerLink _server = new TcpServerLink();
        TcpClientLink _client = new TcpClientLink();
        private Messaging.Link.SmsLink _sms;
        private Server.QueryHandler _queryHandler;

        public Form1()
        {
            InitializeComponent();

            _sms = new SmsLink("com10", 9600);
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            await TaskEx.Delay(12);

            _idQueryService = new IdQueryService(new IdLookupServiceMock());

            _queryHandler = new QueryHandler(_sms, this, this);
            //_queryHandler.NewMessageReceived += (s, arg) =>
            //                                        {
            //                                            var msg = string.Format("收到查询：{0}, 来自:{1}", arg.Value.Message,
            //                                                                    arg.Value.Sender);
            //                                            AppendText(msg);
            //                                        };
            _queryHandler.Start();

        }

        public void AppendText(string text)
        {
            this.listView1.Items.Add(text);
            this.listView1.EnsureVisible(this.listView1.Items.Count - 1);
        }

        private async void serverSend_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;
                response.Text = "";
                Cursor = Cursors.WaitCursor;

                var queryString = string.Format("sfzh='{0}'", idCardNo.Text);
                var reply = _idQueryService.QueryAsync(queryString);
            }
            finally
            {
                btnQuery.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void clientSend_Click(object sender, EventArgs e)
        {
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                var r = await TaskEx.Run(() =>
                                {
                                    var svcTest = new UI.ServiceTest.IdQueryProviderClient();
                                    var result = svcTest.QueryIdCard("QueryQGRK", "sfzh='510403197309112610'");
                                    return result;

                                });
                System.Diagnostics.Debug.WriteLine(r);

                r = await TaskEx.Run(() =>
                {
                    var svcTest = new UI.ServiceTest.IdQueryProviderClient();
                    var result = svcTest.QueryIdCard("QueryZTK", "sfzh='371522198708239233'");
                    return result;

                });
                System.Diagnostics.Debug.WriteLine(r);
            }
            finally
            {
                btnQuery.Enabled = true;
                Cursor = Cursors.Default;
            }

        }

        public void Log(LogEntry entry)
        {
            var item = new ListViewItem();
            item.Text = entry.Time.ToString();
            item.SubItems.Add(entry.Sender);
            item.SubItems.Add(entry.Description);

            this.BeginInvoke((Action)delegate { listView1.Items.Add(item); });
        }
    }
}
