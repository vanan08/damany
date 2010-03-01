using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using System.Timers;
using System.Runtime.InteropServices;

namespace RemoteImaging
{
    public partial class OptionsForm : Form
    {
        private static OptionsForm instance = null;

        public static OptionsForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OptionsForm();
                }
                return instance;
            }
        }

        private OptionsForm()
        {
            InitializeComponent();
            InitCamDatagridView();

            this.rgBrightMode.SelectedIndex = Properties.Settings.Default.BrightMode;
            this.cmbComPort.SelectedText = Properties.Settings.Default.ComName;
            this.textBox4.Text = Properties.Settings.Default.CurIp;
//             this.cameraSetting1.ImageGroupLength = Properties.Settings.Default.ImageArr;
//             this.cameraSetting1.MotionRegionAreaLimit = Properties.Settings.Default.Thresholding;
        }

        private void InitCamDatagridView()
        {
            InitCamList();

            this.dataGridCameras.AutoGenerateColumns = false;
            this.dataGridCameras.Columns[0].DataPropertyName = "Name";
            this.dataGridCameras.Columns[1].DataPropertyName = "ID";
            this.dataGridCameras.Columns[2].DataPropertyName = "IpAddress";
        }


        private void InitCamList()
        {
            Configuration config = Configuration.Instance;

            camList.Clear();
            foreach (var cam in config.Cameras)
            {
                camList.Add(cam);
            }
        }
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

        public IList<Camera> Cameras
        {
            get
            {
                IList<Camera> cams = new List<Camera>();

                foreach (Camera item in camList)
                {
                    cams.Add(item);
                }

                return cams;
            }

            set
            {
                camList.Clear();
                foreach (Camera item in value)
                {
                    camList.Add(item);
                }

                bs = new BindingSource();
                bs.DataSource = camList;

                this.dataGridCameras.DataSource = bs;
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

            Camera cam = bs.Current as Camera;
            if (string.IsNullOrEmpty(cam.IpAddress))
            {
                return;
            }

            using (FormConfigCamera form = new FormConfigCamera())
            {
                StringBuilder sb = new StringBuilder(form.Text);
                sb.Append("-[");
                sb.Append(cam.IpAddress);
                sb.Append("]");

                form.Navigate(cam.IpAddress);
                form.Text = sb.ToString();
                form.ShowDialog(this);
            }
        }



        private BindingList<Camera> camList =
            new BindingList<Camera>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BrightMode = this.rgBrightMode.SelectedIndex;
            Properties.Settings.Default.CurIp = this.textBox4.Text;
            Properties.Settings.Default.ComName = this.cmbComPort.Text;


            //调用的薛晓莉的接口
//             Properties.Settings.Default.ImageArr = this.cameraSetting1.ImageGroupLength;
//             Properties.Settings.Default.Thresholding = this.cameraSetting1.MotionRegionAreaLimit;
            
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
    }
}