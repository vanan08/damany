using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FTPUploadSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetFolder = args[1];
            string sourceFolder = args[0];
            //int camID = int.Parse(args[1]);


            string[] files = Directory.GetFiles(sourceFolder);

            int count = 0;
            foreach (string file in files)
            {
                var ext = Path.GetExtension(file);
                if (!string.IsNullOrEmpty(ext) && ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string destPathName = Path.Combine(targetFolder, Path.GetFileName(file));
                File.Copy(file, destPathName);
                ++count;
                System.Threading.Thread.Sleep(500);
                if (count % 6 == 0)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }

        }
    }
}
