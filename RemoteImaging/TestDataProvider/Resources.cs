using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace TestDataProvider
{
    public static class Data
    {
        public static IplImage GetFrame()
        {
            return IplImage.FromBitmap(Properties.Resources.frame).Clone();
        }

        public static void VisitDirectory(string directory, Action<string> visitor)
        {
            foreach (var file in System.IO.Directory.EnumerateFiles(directory))
            {
                visitor(file);
            }
        }

        public static IplImage GetPortrait()
        {
            return IplImage.FromBitmap(Properties.Resources.portrait).Clone();
        }

    }
}
