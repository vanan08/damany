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
        TcpServerLink _server = new TcpServerLink();
        TcpClientLink _client = new TcpClientLink();
        private Server.QueryHandler _queryHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await TaskEx.Delay(12);

            _queryHandler = new QueryHandler(_server, this, this);
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
                var reply = IdQueryService.Instance.QueryAsync(queryString);
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                btnQuery.Enabled = false;
                response.Text = "";
                Cursor = Cursors.WaitCursor;

                var queryString = string.Format("sfzh='{0}'", IdNo.Text);
                var reply = IdQueryService.Instance.QueryAsync(queryString);
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
            item.Text = "";
            item.SubItems.Add(entry.Time.ToString());
            item.SubItems.Add(entry.Sender);
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add(entry.Description);

            this.BeginInvoke((Action) delegate { listView1.Items.Add(item); });
        }
    }
}
