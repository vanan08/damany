using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Component;

namespace RemoteImaging
{
    class MockCamera : ICamera
    {
        int idx;
        string[] files;
        string directoryPath;

        public MockCamera(string path)
        {
            this.directoryPath = path;
        }

        private void LoadFolder(string path)
        {
            files = System.IO.Directory.GetFiles(path, "*.jpg");
            Array.Sort(files);

            this.Repeat = true;
        }
        public bool Repeat { get; set; }


        #region ICamera Members

        public System.Drawing.Image CaptureImage()
        {
            throw new NotImplementedException();
        }

        public byte[] CaptureImageBytes()
        {
            try
            {
                string file = files[idx];

                byte[] data = System.IO.File.ReadAllBytes(file);

                idx = (idx + 1) % this.files.Length;

                return data;
            }
            catch (System.IO.IOException ex)
            {
                throw new System.Net.WebException("can't load file", ex);
            }
        }



        public void Connect()
        {
            LoadFolder(this.directoryPath);
        }

        #endregion

        #region ICamera Members


        public bool Record
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICamera Members


        public void SetAGCMode(bool enableAGC, bool enableDigitalGain)
        {
        }

        public void SetIris(IrisMode mode, int level)
        {
        }

        public void SetShutter(ShutterMode mode, int level)
        {
        }

        #endregion

        public Uri Location { get; set; }
        public int ID { get; set; }

    }
}
