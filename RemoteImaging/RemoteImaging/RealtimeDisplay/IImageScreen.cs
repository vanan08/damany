using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using RemoteImaging.Core;
using System.Drawing;
using Damany.PC.Domain;
using RemoteImaging.ImportPersonCompare;


namespace RemoteImaging.RealtimeDisplay
{
    public interface IImageScreen
    {
        CameraInfo[] Cameras { set; }

        CameraInfo GetSelectedCamera();

        Portrait SelectedPortrait
        {
            get;
        }

        Frame BigImage
        {
            set;
        }

        IImageScreenObserver Observer
        {
            get;
            set;
        }

        void ShowImages(ImageDetail[] images);


        bool ShowProgress { set; }
        void StepProgress();

        void ShowSuspects(PersonOfInterestDetectionResult result);
    }
}
