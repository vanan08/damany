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
            string targetFolder = args[0];
            int camID = int.Parse(args[1]);


            string[] files = Directory.GetFiles(@"d:\20090505");

            int count = 0;
            foreach (string file in files)
            {
                if (!Path.GetExtension(file).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                string destPathName = Path.Combine(@"d:\UploadPool\02", Path.GetFileName(file));
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
