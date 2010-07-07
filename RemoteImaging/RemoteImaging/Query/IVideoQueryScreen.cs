using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util;

namespace RemoteImaging.Query
{
    public interface IVideoQueryScreen
    {
        DateTimeRange TimeRange { get; }

        DateTimeRange CurrentRange { set; }

        

        Core.Video SelectedVideoFile { get; }

        SearchScope SearchScope { get; }

        Damany.PC.Domain.CameraInfo SelectedCamera { get; }
        Damany.PC.Domain.CameraInfo[] Cameras { set; }

        bool Busy { set; }

        void ClearAll();
        void ClearFacesList();

        void AddFace(Damany.Imaging.Common.Portrait p);

        void AddVideo(RemoteImaging.Core.Video video);
        void PlayVideoInPlace(string videoPath);

        void AttachPresenter(IVideoQueryPresenter presenter);

        void Show();

        void ShowMessage(string msg);
    }
}
