using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Contracts;


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

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
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
            BootStrapper.finder.AddListener(this);

            this.driver.Start();

        }

        public Damany.Util.PersistentWorker driver { get; set; }
    }
}
