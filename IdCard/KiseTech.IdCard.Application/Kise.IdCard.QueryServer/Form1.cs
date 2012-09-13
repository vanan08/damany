using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.QueryServer.UI;
using Kise.IdCard.QueryServer.UI.App;
using Kise.IdCard.QueryServer.UI.Service;
using Kise.IdCard.Server;

namespace Kise.IdCard.QueryServer
{
    public partial class Form1 : Form, IView, ILog
    {
        private ServiceHost _host;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeServiceHost()
        {
            Thread t = new Thread(()=>
                                      {
                                          IdQueryWcfService._logger = this;
            _host = new ServiceHost(typeof (IdQueryWcfService));
            _host.Open();
                                      });
            t.IsBackground = true;
            t.Start();

        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            InitializeServiceHost();

        }

        public void AppendText(string text)
        {
            this.listView1.Items.Add(text);
            this.listView1.EnsureVisible(this.listView1.Items.Count - 1);
        }

        private void clientSend_Click(object sender, EventArgs e)
        {
        }


        public void Log(LogEntry entry)
        {
            var item = new ListViewItem();
            item.Text = entry.Time.ToString();
            item.SubItems.Add(entry.Sender);
            item.SubItems.Add(entry.Description);

            this.BeginInvoke((Action)delegate { listView1.Items.Add(item); });
        }

        private void 选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormOptions())
            {
                form.ShowDialog(this);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
