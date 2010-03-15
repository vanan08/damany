using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Contracts;
using Damany.RemoteImaging.Common.Forms;
using Damany.RemoteImaging.Common;


namespace Damany.PC.Shell.Winform
{
    public partial class Form1 : Form, IPortraitHandler
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void SetPortrait(System.Drawing.Image img)
        {
            this.SetImage(img, this.portrait);
        }

        public void SetFrame(System.Drawing.Image img)
        {
            this.SetImage(img, this.frame);
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

        public void HandlePortraits(IList<Damany.Imaging.Contracts.Frame> motionFrames, IList<Portrait> portraits)
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

                    this.SetFrame(frame);
                    p.Dispose();
                });
            }

            if (portraits.Count > 0)
            {
                portraits.ToList().ForEach(p =>
                {
                    var portrait = portraits.Last().GetImage().ToBitmap();
                    var g = Graphics.FromImage(portrait);
                    g.DrawRectangle(Pens.Black, p.FaceBounds.ToRectangle());
                    g.Dispose();
                    this.SetPortrait(portrait);
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

        }

        public Damany.Imaging.Processors.FaceSearchController controller { get; set; }
        public Damany.PortraitCapturer.Repository.PersistenceService repository { get; set; }

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

        }

        void form_DoWork(object sender, DoWorkEventArgs e)
        {
            Damany.RemoteImaging.Common.BootLoader loader = new Damany.RemoteImaging.Common.BootLoader();

            BackgroundWorker worker = (BackgroundWorker)sender;
            loader.ReportProgress = worker.ReportProgress;

            loader.Load();

            foreach (var c in loader.controllers)
            {
                c.RegisterPortraitHandler(this);
                c.MotionDetector.DetectMethod = delegate( Damany.Imaging.Contracts.Frame frame, FaceProcessingWrapper.MotionDetectionResult result){
                    result.FrameGuid = frame.Guid;
                    result.MotionRect = new OpenCvSharp.CvRect(0, 0, frame.GetImage().Width, frame.GetImage().Height);
                    return true;
                    };
                c.Start();
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
            this.StartLoader();
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
    }
}
