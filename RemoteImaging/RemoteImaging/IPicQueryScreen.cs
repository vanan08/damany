using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using System.Drawing;

namespace RemoteImaging
{
    public interface IPicQueryScreen
    {
        void Show();
        void Clear();
        void AddItem(Damany.Imaging.Common.Portrait item);
        void AttachPresenter(IPicQueryPresenter presenter);

        void ShowStatus(string status);

        void EnableSearchButton(bool enable);
        void EnableNavigateButtons(bool enable);

        void ShowUserIsBusy(bool busy);


        Damany.Util.DateTimeRange TimeRange { get; set; }
        Portrait SelectedItem { get; set; }

        Damany.PC.Domain.CameraInfo[] Cameras { get; set; }
        Damany.PC.Domain.Destination SelectedCamera { get; set; }


        string[] Machines { get; set; }
        string SelectedMachine { get; set; }

        int PageSize { get; set; }
        Portrait CurrentPortrait { get; set; }
        Image CurrentBigPicture { get; set; }

        int CurrentPage { get; set; }
        int TotalPage { get; set; }

    }
}
