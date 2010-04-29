using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.RemoteImaging.Common.Forms;
using Damany.RemoteImaging.Common;
using Damany.Util;


namespace Damany.PC.Shell.Winform
{
    public partial class Form1 : Form, IPortraitHandler
    {
        public Form1()
        {
            InitializeComponent();


            for (int i = 0; i < 9;++i )
            {
                var pip = new Damany.Windows.Form.PipPictureBox();
                pip.Text = (i+1).ToString();
                pip.Tag = i;
                pip.Image = TestDataProvider.Data.GetFrame().ToBitmap();
                pip.SmallImage = TestDataProvider.Data.GetPortrait().ToBitmap();

                pip.Dock = DockStyle.Fill;

                this.tableLayoutPanel1.Controls.Add(pip);
                this.Pips.Add(pip);
            }
        }


        public void SetPortrait(System.Drawing.Image img, int cameraId)
        {
            if (this.InvokeRequired)
            {
                Action<Image, int> action = this.SetPortrait;

                this.BeginInvoke(action, img, cameraId);
                return;
            }

            foreach (var pip in this.Pips)
            {
                int id = (int) pip.Tag ;
                if (id == cameraId)
                {
                    pip.SmallImage = img;
                }
            }
        }

        public void SetFrame(System.Drawing.Image img, int cameraId)
        {
            //this.SetImage(img, this.frame);
        }

        private void SetImage(System.Drawing.Image img, PictureBox box)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<System.Drawing.Image, PictureBox>(this.SetImage), img, box);
                return;
            }
            else
            {
                if (box.Image != null)
                {
                    box.Image.Dispose();
                }

                box.Image = img;
            }

        }

        #region IPortraitHandler Members

        public void Initialize()
        {

        }

        public void Start()
        {

        }

        public void HandlePortraits(IList<Damany.Imaging.Common.Frame> motionFrames, IList<Damany.Imaging.Common.Portrait> portraits)
        {
            if (motionFrames.Count > 0)
            {

                motionFrames.ToList().ForEach(p =>
                {
                    var frame = motionFrames.Last().GetImage().ToBitmap();

                    using (Graphics g = Graphics.FromImage(frame))
                    using (Font font = new Font(FontFamily.GenericSansSerif, 150))
                    {
                        g.DrawString(p.CapturedFrom.Id.ToString() +"-" + p.CapturedAt.ToShortTimeString() , font, Brushes.Black, 0, 0);
                    }

                    //this.SetFrame(frame);
                    p.Dispose();
                });
            }

            if (portraits.Count > 0)
            {
                portraits.ToList().ForEach(p =>
                {
                    var portrait = portraits.Last().GetIpl().ToBitmap();
                    var g = Graphics.FromImage(portrait);
                    g.DrawRectangle(Pens.Black, p.FaceBounds.ToRectangle());
                    g.Dispose();
                   // this.SetPortrait(portrait);
                    p.Dispose();
                });
            }


        }

        public void Stop()
        {

        }

        public string Description
        {
            get { return string.Empty; }
        }

        public string Author
        {
            get { return string.Empty; }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantCopy
        {
            get { return true; }
        }

        public bool WantFrame
        {
            get { return true; }
        }

        public event EventHandler<MiscUtil.EventArgs<Exception>> Stopped;

        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.StartLoader();

        }

        public Damany.Imaging.Processors.FaceSearchController controller { get; set; }
        public IRepository repository { get; set; }

        public void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(this.ShowMessage), msg);
                return;
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.ShowMessage("hello");
        }

        private void StartLoader()
        {
            ProgressForm form = new ProgressForm();
            form.Text = this.Text;

            form.DoWork += new DoWorkEventHandler(form_DoWork);
            form.WorkIsDone += new RunWorkerCompletedEventHandler(form_WorkIsDone);
            form.ShowDialog(this);
        }

        void form_WorkIsDone(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (e.Error != null)
            {
                this.BeginInvoke(new MethodInvoker(() => MessageBox.Show(this,
                    e.Error.ToString(),
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    )));
            }

            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(8000);
            listener.Start(1);
            listener.BeginAcceptTcpClient(ClientConnected, listener);
        }


        void ClientConnected(IAsyncResult result)
        {
            System.Net.Sockets.TcpListener listener = result.AsyncState as System.Net.Sockets.TcpListener;

            System.Net.Sockets.TcpClient socket = listener.EndAcceptTcpClient(result);

            Damany.RemoteImaging.Common.ObjectSender sender = new Damany.RemoteImaging.Common.ObjectSender(socket);
            
            foreach (var c in this.controllers)
            {

            }

        }

        void form_DoWork(object sender, DoWorkEventArgs e)
        {
            Damany.RemoteImaging.Common.BootLoader loader = new Damany.RemoteImaging.Common.BootLoader();

            BackgroundWorker worker = (BackgroundWorker)sender;
            loader.ReportProgress = worker.ReportProgress;

            loader.Load(@".\data");
            this.repository = loader.repository;

            foreach (var c in loader.controllers)
            {
                this.controllers.Add(c);
            }

        }

        private IList<Damany.Imaging.Processors.FaceSearchController> controllers
            = new List<Damany.Imaging.Processors.FaceSearchController>();

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            ShowOptions();
        }



        private void startButton_Click(object sender, EventArgs e)
        {
            foreach (var c in this.controllers)
            {
                c.Start();
            }
        }

        private void options_Click(object sender, EventArgs e)
        {
            this.ShowOptions();


        }
        private void ShowOptions()
        {
            OptionsForm options = new OptionsForm();
            options.Presenter = new OptionPresenter(ConfigurationManager.GetDefault(), options);
            options.ShowDialog(this);
        }

        private void slowDown_Click(object sender, EventArgs e)
        {
            foreach (var c in this.controllers)
            {
                c.SlowDown();
            }

            Frequency /= 2;

        }

        private void speedUp_Click(object sender, EventArgs e)
        {
            foreach (var c in this.controllers)
            {
                c.SpeedUp();
            }

            Frequency *= 2;

        }

        public float Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                this.Text = string.Format("{0}帧/s", value);
            }
        }
        float frequency = 2;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.repository.GetFrames(-1, new Damany.Util.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now));
        }

        List<Damany.Windows.Form.PipPictureBox> Pips = 
            new List<Damany.Windows.Form.PipPictureBox>();

    }
}
