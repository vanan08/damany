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
    public partial class Form1 : Form, IView
    {
        TcpServerLink _server = new TcpServerLink();
        TcpClientLink _client = new TcpClientLink();
        private Server.QueryHandler _queryHandler;
        private Server.IdQueryService _idQueryService;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await TaskEx.Delay(12);

            _queryHandler = new QueryHandler(_server, this);
            _queryHandler.NewMessageReceived += (s, arg) =>
                                                    {
                                                        var msg = string.Format("收到查询：{0}, 来自:{1}", arg.Value.Message,
                                                                                arg.Value.Sender);
                                                        AppendText(msg);
                                                    };
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
                queryBtn.Enabled = false;
                response.Text = "";
                Cursor = Cursors.WaitCursor;
                //_server.SendAsync("", this.name.Text);
                if (_idQueryService == null)
                {
                    _idQueryService = new IdQueryService();
                }

                var queryString = string.Format("xm='{0}' and csrq=19800208", name.Text);
                var reply = await _idQueryService.QueryAsync(queryString);
                response.Text = reply;

            }
            finally
            {
                queryBtn.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void clientSend_Click(object sender, EventArgs e)
        {
        }
    }
}
