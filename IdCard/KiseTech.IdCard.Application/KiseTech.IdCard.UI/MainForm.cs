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
using Kise.IdCard.Model;

namespace Kise.IdCard.UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm, IIdCardView
    {
        private IdService _idService;

        public MainForm()
        {
            InitializeComponent();


            idCardControl1.MinorityDictionary = FileMinorityDictionary.LoadDictionary("MinorityCode.txt");

            _idService = new IdService(new FakeIdCardReader());
            _idService.AttachView(this);

        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            _idService.Start();
        }

        public IdCardInfo CurrentIdCardInfo
        {
            get { return idCardControl1.IdCardInfo; }
            set { idCardControl1.IdCardInfo = value; }
        }
    }
}