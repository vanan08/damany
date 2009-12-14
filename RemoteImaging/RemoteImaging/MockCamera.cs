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

        public MockCamera(string path)
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

            string file = files[idx];

            System.Diagnostics.Debug.WriteLine(idx);

            byte[] data = System.IO.File.ReadAllBytes(file);

            idx = (idx + 1) % this.files.Length;

            return data;
        }



        public void Connect()
        {
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
    }
}
