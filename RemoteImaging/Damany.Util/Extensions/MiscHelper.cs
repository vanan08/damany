using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Damany.Util.Extensions
{
    public static class MiscHelper
    {
        public static Image FromFileBuffered(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }
    }
}
