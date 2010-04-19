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

            this.rgBrightMode.SelectedIndex = Properties.Settings.Default.BrightMode;
            this.cmbComPort.SelectedText = Properties.Settings.Default.ComName;
            this.textBox4.Text = Properties.Settings.Default.CurIp;
        }

        public void AttachPresenter(OptionsPresenter presenter)
        {
            this._presenter = presenter;
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

        private IList<CameraInfo> _cameras;

        public IList<CameraInfo> Cameras
        {

            set
            {
                _cameras = value;
                camerasListBox.DataSource = value;
                camerasListBox.DisplayMember = "Name";


                foreach (var cameraInfo in value)
                {
                    cameraInfo.PropertyChanged += cameraInfo_PropertyChanged;
                }

            }
            get
            {
                return _cameras;
            }

        }

        void cameraInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateCamerasList();
        }

        private void UpdateCamerasList()
        {
            camerasListBox.DisplayMember = "";
            camerasListBox.DisplayMember = "Name";
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



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Properties.Settings.Default.ComName = cmbComPort.Text;
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

        private void camerasListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (camerasListBox.SelectedItem == null)
            {
                return;
                
            }

            var obj = camerasListBox.SelectedItem;
            propertyGrid1.SelectedObject = obj;
        }

        private void addCamera_Click(object sender, EventArgs e)
        {
            var item = new CameraInfo();
            item.Name = "新摄像头";
            _cameras.Add(item);
            UpdateCamerasList();

            item.PropertyChanged += cameraInfo_PropertyChanged;

            camerasListBox.SelectedItem = item;
        }

        private void removeCamera_Click(object sender, EventArgs e)
        {
            if (_cameras == null)
            {
                return;
            }

            var item = camerasListBox.SelectedIndex;
            if (item == -1)
            {
                return;
            }

            _cameras.RemoveAt(item);
            UpdateCamerasList();

        }
    }
}