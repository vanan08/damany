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
        }

        private void UpdateStatusLabel(DrawFigureEventArgs e)
        {
            var status = string.Format("线圈位置：{0},{1}  线圈大小：{2}x{3}",
                                       e.Rectangle.Left, e.Rectangle.Top, e.Rectangle.Width, e.Rectangle.Height);
            //statusLabel.Caption = status;
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

            if (_camera == null)
            {
                var uri = string.Format("http://{0}/liveimg.cgi", cameraIp.EditValue);
                _camera = new Damany.Cameras.JPEGExtendStream(uri);
                _camera.Login = "guest";
                _camera.Password = "guest";
                _camera.FrameInterval = Properties.Settings.Default.FrameIntervalMs;
                _camera.RequireCookie = true;
                _camera.NewFrame += this.camera_NewFrame;
                _camera.Start();
            }
        }

        private void captureImage_Click(object sender, EventArgs e)
        {
            if (liveImg.Image != null)
            {
                pictureBox1.Image = (Image)liveImg.Image.Clone();
            }
        }
    }
}
