using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.RemoteImaging.Common;
using Damany.Util;

namespace RemoteImaging.LicensePlate
{
    public partial class FormLicensePlateQuery : Form, ILicenseplateSearchScreen
    {
        private readonly ConfigurationManager _configurationManager;
        private ILicensePlateSearchPresenter _presenter;


        public FormLicensePlateQuery(ConfigurationManager configurationManager)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");

            _configurationManager = configurationManager;
            InitializeComponent();

            var now = DateTime.Now;
            to.EditValue = now;
            from.EditValue = now.AddDays(-1);
        }

        public void AttachPresenter(ILicensePlateSearchPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException("presenter");

            _presenter = presenter;
        }

        public new void Show()
        {
            this.ShowDialog(Application.OpenForms[0]);
        }

        public void AddLicensePlateInfo(LicensePlateInfo licensePlateInfo)
        {
            if (InvokeRequired)
            {
                Action<LicensePlateInfo> action = AddLicensePlateInfo;
                BeginInvoke(action, licensePlateInfo);
                return;
            }


            var item = new ListViewItem();
            item.SubItems.Add(licensePlateInfo.LicensePlateNumber);
            item.SubItems.Add(licensePlateInfo.CaptureTime.ToString());
            var name = _configurationManager.GetName(licensePlateInfo.CapturedFrom);
            item.SubItems.Add(name ?? "未知摄像头");
            item.Tag = licensePlateInfo;

            licensePlateList.Items.Add(item);
        }

        public void Clear()
        {
            licensePlateList.Items.Clear();
        }

        public bool MatchLicenseNumber
        {
            get { return matchLicenseNumber.Checked; }
            set { throw new NotImplementedException(); }
        }

        public string LicenseNumber
        {
            get { return licensePlateNumber.Text; }
            set { throw new NotImplementedException(); }
        }

        public bool MatchTimeRange
        {
            get { return mathTimeRange.Checked; }
            set { throw new NotImplementedException(); }
        }

        public DateTimeRange Range
        {
            get
            {
                var range = new Damany.Util.DateTimeRange((DateTime) from.EditValue, (DateTime) to.EditValue);
                return range;
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        private void searchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(licensePlateNumber.Text) && matchLicenseNumber.Checked )
            {
                MessageBox.Show(this, "车牌号为空");
                return;
            }

            _presenter.Search();
        }

        private void licensePlateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (licensePlateList.SelectedItems.Count <= 0)
            {
                return;
            }

            var licenseInfo = (LicensePlateInfo) licensePlateList.SelectedItems[0].Tag;
            var img = licenseInfo.LoadImage();

            pictureBox1.Image = img;
        }

        private void mathTimeRange_CheckedChanged(object sender, EventArgs e)
        {
            from.Enabled = mathTimeRange.Checked;
            to.Enabled = mathTimeRange.Checked;
        }

        private void matchLicenseNumber_CheckedChanged(object sender, EventArgs e)
        {
            licensePlateNumber.Enabled = matchLicenseNumber.Checked;
        }
    }
}
