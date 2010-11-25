using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Kise.IdCard.Application;
using Kise.IdCard.Infrastructure.CardReader;
using Kise.IdCard.Messaging;
using Kise.IdCard.Messaging.Link;
using Kise.IdCard.Model;
using Kise.IdCard.UI;

namespace Kise.IdCard.UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm, IIdCardView
    {
        private IdService _idService;

        public MainForm()
        {
            InitializeComponent();

            idCardControl1.MinorityDictionary = FileMinorityDictionary.Instance;

            ILink lnk = null;
            IIdCardReader cardReader = null;

            if (Program.IsDebug)
            {
                lnk = new TcpClientLink();
                cardReader = new FakeIdCardReader();
            }
            else
            {
                lnk = new SmsLink("com3", 9600);
                cardReader = new IdCardReader(1001);
            }

            _idService = new IdService(cardReader, lnk);
            _idService.AttachView(this);

        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            var progress = new System.Threading.EventProgress<ProgressIndicator>();
            progress.ProgressChanged += (s, arg) =>
                                            {
                                                statusLabel.Caption = arg.Value.Status;
                                                progressBar.Visibility = arg.Value.LongOperation
                                                                             ? BarItemVisibility.Always
                                                                             : BarItemVisibility.Never;
                                            };

            _idService.Start(progress);
        }

        public IdCardInfo CurrentIdCardInfo
        {
            get { return idCardControl1.IdCardInfo; }
            set { idCardControl1.IdCardInfo = value; }
        }

        private void buttonQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = new FormIdQuery();
            form.ShowDialog(this);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var report = new IdReport();
            report.DataSource = xpCollection1;

            report.ShowPreviewDialog();
        }
    }
}