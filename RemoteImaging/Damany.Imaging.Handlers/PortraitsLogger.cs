using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;


namespace Damany.Imaging.Handlers
{

    public class PortraitsLogger : IPortraitHandler
    {
        public event EventHandler<MiscUtil.EventArgs<Exception>> Stopped;

        public PortraitsLogger(string outputDirectory)
        {
            this.outputDirectory = outputDirectory;

            if (!System.IO.Directory.Exists(this.outputDirectory))
            {
                System.IO.Directory.CreateDirectory(this.outputDirectory);
            }

            this.wantFrame = false;
            this.wantCopy = false;
            this.name = "Sync Portrait Logger";

        }


        public virtual void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
             SavePortraits(portraits);
        }


        public bool WantCopy
        {
            get { return this.wantCopy; }
        }

        public  bool WantFrame
        {
            get { return this.wantFrame; }
        }

        public bool UnloadOnError
        {
            get { return this.autoRemove; }
        }

        public virtual void Start(){}
        public virtual void Initialize(){}
        public virtual void Stop() { }


        protected void SavePortraits(IList<Portrait> portraits)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} Portraits captured", portraits.Count));

            portraits.ToList().ForEach(p =>
            {
                var rect = p.Bounds.FaceBoundsInPortrait;
                p.PortraitImage.DrawRect(rect.Location,
                                         new OpenCvSharp.CvPoint(rect.X + rect.Width, rect.Y + rect.Width),
                                         new OpenCvSharp.CvScalar(0));

                var outputPath = System.IO.Path.Combine(this.outputDirectory, p.Guid.ToString() + ".jpg");

                p.PortraitImage.SaveImage(outputPath);
            });
        }

        protected virtual void OnStopped( MiscUtil.EventArgs<Exception> args)
        {
            if (this.Stopped!=null)
            {
                this.Stopped(this, args);
            }
        }



        #region IPortraitHandler Members


        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.desc; }
        }

        public string Author
        {
            get { throw new NotImplementedException(); }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        private string outputDirectory;

        protected bool wantCopy;
        protected bool wantFrame;
        protected string name;
        protected string desc;
        protected bool autoRemove;


    }
}
