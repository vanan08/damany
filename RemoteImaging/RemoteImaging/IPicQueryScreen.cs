using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public interface IPicQueryScreen
    {
        void Show();
        void Clear();
        void AddItem(Damany.Imaging.Common.Portrait item);
        void AttachPresenter(IPicQueryPresenter presenter);

        void EnableSearchButton(bool enable);
        void EnableNavigateButtons(bool enable);


        Damany.Util.DateTimeRange TimeRange { get; set; }
        Damany.Imaging.Common.Portrait SelectedItem { get; set; }
        Damany.PC.Domain.CameraInfo[] Cameras { get; set; }
        string[] Machines { get; set; }
        
        Damany.PC.Domain.Destination SelectedCamera { get; set; }
    }
}
