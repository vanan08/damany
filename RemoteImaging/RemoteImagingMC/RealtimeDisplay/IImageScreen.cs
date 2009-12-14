using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;

namespace RemoteImaging.RealtimeDisplay
{
    public interface IImageScreen
    {
        Camera[] Cameras { set; }

        Camera SelectedCamera
        {
            get;
        }

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

        bool ShowProgress { set; }
        void StepProgress();
    }
}
