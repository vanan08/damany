﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;


namespace Damany.Imaging.Handlers
{

    public class PortraitsLogger : IPortraitHandler
    {
        public PortraitsLogger(string outputDirectory)
        {
            this.outputDirectory = outputDirectory;

            if (!System.IO.Directory.Exists(this.outputDirectory))
            {
                System.IO.Directory.CreateDirectory(this.outputDirectory);
            }

        }

        #region IPortraitHandler Members

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} Portraits captured", portraits.Count));

            portraits.ToList().ForEach(p => {
                var rect = p.Bounds.FaceBoundsInPortrait;
                p.PortraitImage.DrawRect(rect.Location,
                                         new OpenCvSharp.CvPoint(rect.X + rect.Width, rect.Y + rect.Width),
                                         new OpenCvSharp.CvScalar(0));

                var outputPath = System.IO.Path.Combine(this.outputDirectory, p.Guid.ToString() + ".jpg");

                p.PortraitImage.SaveImage( outputPath ); 
                p.Dispose();
            });
        }

        #endregion

        private string outputDirectory;
    }
}
