using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Core
{
    public class ImageDetail
    {
        private ImageDetail()
        {

        }

        public static ImageDetail FromPath(string path)
        {
            ImageDetail img = new ImageDetail();
            img.ParsePath(path);
            return img;
        }

        public void MoveTo(string destDirectory)
        {
            string destPath = System.IO.Path.Combine(destDirectory, this.Name);
            System.IO.File.Move(this.Path, destPath);
            this.ParsePath(destPath);
        }

        public DateTime CaptureTime { get; private set; }

        public int FromCamera { get; private set; }

        public string Name { get; private set; }

        public string ContainedBy { get; private set; }

        public string Path { get; private set; }

        private void ParsePath(string path)
        {
            this.Path = path;
            this.Name = System.IO.Path.GetFileName(path);
            this.ContainedBy = System.IO.Path.GetDirectoryName(this.Path);

            this.FromCamera = int.Parse(this.Name.Substring(0, 2));

            int year = int.Parse(this.Name.Substring(3, 2)) + 2000;
            int month = int.Parse(this.Name.Substring(5, 2));
            int day = int.Parse(this.Name.Substring(7, 2));
            int hour = int.Parse(this.Name.Substring(9, 2));
            int min = int.Parse(this.Name.Substring(11, 2));
            int sec = int.Parse(this.Name.Substring(13, 2));
            DateTime dt = new DateTime(year, month, day, hour, min, sec, 0);
            this.CaptureTime = dt;
        }
    }
}
