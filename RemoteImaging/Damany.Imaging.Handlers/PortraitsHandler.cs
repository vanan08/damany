using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;


namespace Damany.Imaging.Handlers
{

    public class PortraitsHandler : IPortraitHandler
    {
        #region IPortraitHandler Members

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} Portraits captured", portraits.Count));

            motionFrames.Dispose();
            portraits.ToList().ForEach(p => {
                var rect = p.Bounds.FaceBoundsInPortrait;
                p.PortraitImage.DrawRect(rect.Location,
                                         new OpenCvSharp.CvPoint(rect.X + rect.Width, rect.Y + rect.Width),
                                         new OpenCvSharp.CvScalar(0));
                p.PortraitImage.SaveImage(p.Guid.ToString() + ".jpg"); 
                p.Dispose();
            });
        }

        #endregion
    }
}
