using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;
using System.Drawing;
using Damany.PC.Domain;

namespace RemoteImaging.RealtimeDisplay
{
    public interface IImageScreen
    {
        CameraInfo[] CamerasInfo { set; }

        CameraInfo GetSelectedCamera();

        ImageDetail SelectedImage
        {
            get;
        }

        ImageDetail BigImage
        {
            set;
        }

        IImageScreenObserver Observer
        {
            get;
            set;
        }

        void ShowImages(ImageDetail[] images);

        void ShowFaceRecognitionResult(
            System.Drawing.Image captured,
            System.Drawing.Image fromLib,
            float similarity);

        bool ShowProgress { set; }
        void StepProgress();

        void ShowSuspects(ImportPersonCompare.ImportantPersonDetail[] suspects, Image captured);
    }
}
