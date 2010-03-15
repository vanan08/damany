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
                
                motionFrames.ToList().ForEach(p => {
                    var frame = motionFrames.Last().GetImage().ToBitmap();
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
            ProgressForm form = new ProgressForm();
            form.Text = this.Text;

            form.DoWork += new DoWorkEventHandler(form_DoWork);
            form.WorkIsDone += new RunWorkerCompletedEventHandler(form_WorkIsDone);
            form.ShowDialog(this);



        }

        public Damany.Imaging.Processors.FaceSearchController controller { get; set; }
        public Damany.PortraitCapturer.Repository.PersistenceService repository { get; set; }

        public void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke( new Action<string>(this.ShowMessage), msg);
                return;
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            this.newToolStripButton.Enabled = false;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    var query = this.repository.GetPortraits(new Damany.Util.DateTimeRange(DateTime.Now.AddDays(-1), DateTime.Now));
                    this.ShowMessage("query found: " + query.Count);
                }
                finally
                {
                    this.EnableButton(true);
                }
                
            });
        }

        public void EnableButton(bool enable)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke( (Action<bool>)( b => this.newToolStripButton.Enabled =b), enable );
                return;
            }

            this.newToolStripButton.Enabled = enable;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.ShowMessage("hello");
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
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
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(0, "初始化数据库");
            SearchLineBuilder.GetDefaultPersistenceService();
            this.repository = SearchLineBuilder.GetDefaultPersistenceService();
            worker.ReportProgress(50, "数据库初始化成功");

            string url = @"D:\20090505";
            worker.ReportProgress(0, "初始化摄像头: " + url );
            var controller = SearchLineBuilder.BuildNewSearchLine(url, "dir");
            this.controllers.Add(controller);
            worker.ReportProgress(100, "初始化摄像头: " + url +"成功");

            worker.ReportProgress(0, "启动摄像头: " + url);
            controller.RegisterPortraitHandler(this);
            controller.Start();
            worker.ReportProgress(100, "启动摄像头: " + url + "成功");

        }

        private IList<Damany.Imaging.Processors.FaceSearchController> controllers
            = new List<Damany.Imaging.Processors.FaceSearchController>();
    }
}
