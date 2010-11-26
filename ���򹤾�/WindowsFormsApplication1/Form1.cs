using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Windows.Form;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Rectangle _rectangle = Rectangle.Empty;
        private ISetRectangle _rectangleSetter;
        private string _lastPeerAddress = string.Empty;
        private Damany.Cameras.JPEGExtendStream _camera;

        public Form1()
        {
            InitializeComponent();
        }


        private System.Threading.Tasks.Task _t;


        void camera_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            var img = eventArgs.Frame.Clone();
            this.BeginInvoke(new Action(() => liveImg.Image = (Image)img));

        }


        private void pictureBox1_FigureDrawn(object sender, Damany.Windows.Form.DrawFigureEventArgs e)
        {
            pictureBox1.Clear();
            pictureBox1.AddRectangle(e.Rectangle);

            _rectangle = e.Rectangle;
            UpdateStatusLabel(e);
            applyButton.Enabled = true;
            applyButton.Select();
        }

        private void UpdateStatusLabel(DrawFigureEventArgs e)
        {
            var status = string.Format("线圈位置：{0},{1}  线圈大小：{2}x{3}",
                                       e.Rectangle.Left, e.Rectangle.Top, e.Rectangle.Width, e.Rectangle.Height);
            statusLabel.Text = status;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            Properties.Settings.Default.Save();

            if (_camera != null)
            {
                _camera.SignalToStop();
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.PeerIpPort)) return;
            if (_rectangle == Rectangle.Empty) return;

            if (_rectangleSetter == null)
            {
                var ipport = Properties.Settings.Default.PeerIpPort;
                _lastPeerAddress = ipport.Replace(" ", "");
                var add = _lastPeerAddress.Split(new[] { ':' });
                _rectangleSetter = new TcpSetRectangle(add[0], int.Parse(add[1]));

            }

            applyButton.Enabled = false;

            _rectangleSetter.Set(_rectangle, error =>
                                                 {
                                                     Action ac = () =>
                                                                     {
                                                                         string msg = error == null
                                                                                          ? "设置成功"
                                                                                          : "设置失败\r\n\r\n" +
                                                                                            error.Message;
                                                                         var icon = error == null
                                                                                        ? MessageBoxIcon.Information
                                                                                        : MessageBoxIcon.Error;

                                                                         MessageBox.Show(this, msg, Text,
                                                                                         MessageBoxButtons.OK, icon);
                                                                         applyButton.Enabled = true;
                                                                     };

                                                     this.BeginInvoke(ac);
                                                 });

        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace((string)cameraIp.EditValue)) return;

            if (_camera != null)
            {
                _camera.SignalToStop();
            }

            var uri = string.Format("http://{0}/liveimg.cgi", cameraIp.EditValue);
            _camera = new Damany.Cameras.JPEGExtendStream(uri);
            _camera.Login = "guest";
            _camera.Password = "guest";
            _camera.FrameInterval = Properties.Settings.Default.FrameIntervalMs;
            _camera.RequireCookie = true;
            _camera.NewFrame += this.camera_NewFrame;
            _camera.Start();

            captureImage.Enabled = true;
            captureImage.Select();
        }

        private void captureImage_Click(object sender, EventArgs e)
        {
            if (_camera == null) return;

            captureImage.Enabled = false;

            var newFrames = Observable.FromEvent<AForge.Video.NewFrameEventArgs>(_camera, "NewFrame")
                .Select(arg => arg.EventArgs.Frame.Clone() as Image)
                .Take(Properties.Settings.Default.SnapCount);

            snapShots.Clear();
            imageList1.Images.Clear();
            int i = 0;
            newFrames.ObserveOn(this).Subscribe(img =>
                                    {
                                        imageList1.Images.Add(img);
                                        var item = snapShots.Items.Add((i + 1).ToString(), i);
                                        item.Tag = img;

                                        if (i == Properties.Settings.Default.SnapCount - 1)
                                        {
                                            captureImage.Enabled = true;
                                        }

                                        i++;
                                    });
        }

        private void snapShots_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                var img = e.Item.Tag as Image;
                if (img != null)
                {
                    pictureBox1.Image = (Image)img.Clone();
                    var status = string.Format("图像大小：{0}x{1}", img.Width, img.Height);
                    statusLabel.Text = status;
                }

            }
        }

        private void cameraIp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                connectButton_Click(null, null);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            connectButton_Click(null, null);
        }
    }
}
