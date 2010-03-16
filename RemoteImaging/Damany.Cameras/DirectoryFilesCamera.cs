using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;
using Damany.Imaging.Common;

namespace Damany.Cameras
{

    public class DirectoryFilesCamera : IFrameStream
    {
        public DirectoryFilesCamera(string directory, string imagePattern)
        {
            if (String.IsNullOrEmpty(directory))
                throw new ArgumentException("directory is null or empty.", "directory");

            if (!System.IO.Directory.Exists(directory))
                throw new System.IO.DirectoryNotFoundException(GetDirectoryStringForDisplay(directory) + " doesn't exist");
            
            this.imagePattern = imagePattern ?? "*.*";
            

            this.directory = directory;
            this.Repeat = true;
        }

        #region IFrameStream Members

        public void Initialize()
        {
            this.imageFiles = System.IO.Directory.GetFiles(this.directory, this.imagePattern);
            if (this.imageFiles.Length == 0)
            {
                throw new InvalidOperationException(GetDirectoryStringForDisplay(this.directory) + " is empty");
            }
        }

        public void Close() { }
        public void Connect() {}

        public Frame RetrieveFrame()
        {
            var ipl = IplImage.FromFile(this.imageFiles[current++]);

            if (this.Repeat)
            {
                this.current = this.current % this.imageFiles.Length;
            }


            var frame = new Frame(ipl);
            frame.CapturedFrom = this;

            return frame;
        }

        public int Id { get; set; }

        public string Description { get { return "Directory File Camera Simulator"; } }

        #endregion

        private static string GetDirectoryStringForDisplay(string directory)
        {
            return "\"" + directory + "\"";
        }
        public bool Repeat { get; set; }


        private string directory;
        private string imagePattern;
        private string[] imageFiles;
        private int current;
    }
}
