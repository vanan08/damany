using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Damany.PC.Domain;
using System.Timers;
using System.Runtime.InteropServices;

namespace RemoteImaging
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            InitCamDatagridView();

            this.rgBrightMode.SelectedIndex = Properties.Settings.Default.BrightMode;
            this.cmbComPort.SelectedText = Properties.Settings.Default.ComName;
            this.textBox4.Text = Properties.Settings.Default.CurIp;
//             this.cameraSetting1.ImageGroupLength = Properties.Settings.Default.ImageArr;
//             this.cameraSetting1.MotionRegionAreaLimit = Properties.Settings.Default.Thresholding;
        }

        public void AttachPresenter(OptionsPresenter presenter)
        {
            this._presenter = presenter;
        }


        private void InitCamDatagridView()
        {

            this.dataGridCameras.AutoGenerateColumns = false;
            this.dataGridCameras.Columns[0].DataPropertyName = "Name";
            this.dataGridCameras.Columns[1].DataPropertyName = "Id";

            this.dataGridCameras.Columns[2].DataPropertyName = "LoginUserName";
            this.dataGridCameras.Columns[3].DataPropertyName = "LoginPassword";

            this.dataGridCameras.Columns[4].DataPropertyName = "Location";
            this.dataGridCameras.Columns[5].DataPropertyName = "Provider";
            this.comboBoxColumnProvider.DataSource = Enum.GetValues(typeof (CameraProvider));
        }

        Damany.RemoteImaging.Common.ConfigurationManager configManager
            = Damany.RemoteImaging.Common.ConfigurationManager.GetDefault();
        
        private void browseForUploadFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.ImageUploadPool = dlg.SelectedPath;
            }

        }

        private void browseForOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.OutputPath = dlg.SelectedPath;
            }
        }

        public IList<CameraInfo> Cameras
        {
 
            set
            {
                camList.Clear();
                foreach (var item in value)
                {
                    camList.Add(item);
                }

                bs = new BindingSource();
                bs.DataSource = camList;

                this.dataGridCameras.DataSource = bs;
            }
            get
            {
                var list = from c in this.camList
                           select c;

                var returnList = list.ToList();

                return returnList;
            }



        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void linkLabelConfigCamera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (bs.Current == null)
            {
                return;
            }

            CameraInfo cam = bs.Current as CameraInfo;
            if (cam == null)
            {
                return;
            }

            using (FormConfigCamera form = new FormConfigCamera())
            {
                StringBuilder sb = new StringBuilder(form.Text);
                sb.Append("-[");
                sb.Append(cam.Location.ToString());
                sb.Append("]");

                form.Navigate(cam.Location.ToString());
                form.Text = sb.ToString();
                form.ShowDialog(this);
            }
        }



        private BindingList<CameraInfo> camList =
            new BindingList<CameraInfo>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {
           this._presenter.UpdateConfig();
        }


        


        private void OptionsForm_Load(object sender, EventArgs e)
        {

        }

        


        #region 弹出窗口的操作
        public void ShowResDialog(int picIndex, string msg)
        {
            AlertSettingRes asr = new AlertSettingRes(msg, picIndex);
            asr.HeightMax = 169;
            asr.WidthMax = 175;
            asr.ShowDialog(this);
        }
        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ComName = cmbComPort.Text;
        }


     

        private void btnBrowseSavePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.WarnPicSavePath = dlg.SelectedPath;
            }
        }

        private OptionsPresenter _presenter;
    }
}