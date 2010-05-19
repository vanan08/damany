using System;
using Damany.Util;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public class PersonOfInterest
    {
        public static PersonOfInterest FromIplImage(IplImage image)
        {
            return new PersonOfInterest(image);
        }

        public PersonOfInterest()
        {
            this.Guid = System.Guid.NewGuid();
        }

        public PersonOfInterest(IplImage image)
            :this()
        {
            if (image == null) throw new ArgumentNullException("image");
            if (image.ROI.Size.Width > image.Size.Height || image.ROI.Height > image.Size.Height)
            {
                throw new ArgumentException("ROI is invlid");
            }

            this.Ipl = image;
            
        }

        public System.Drawing.Image GetImage()
        {
            return this.Ipl.ToBitmap();
        }

        public IplImage Ipl { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string SN { get; set; }
        public Guid Guid { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string ImageFilePath { get; set; }



    }
}
