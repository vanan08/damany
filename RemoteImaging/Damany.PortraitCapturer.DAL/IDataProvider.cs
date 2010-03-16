using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.PortraitCapturer.DAL
{
    public interface IDataProvider
    {
        void SavePortrait(DTO.Portrait portrait);
        void SaveFrame(DTO.Frame frame);

        DTO.Frame GetFrame(System.Guid frameId);
        DTO.Portrait GetPortrait(System.Guid portraitId);

        IList<DTO.Frame> GetFrames(Damany.Util.DateTimeRange range);
        IList<DTO.Portrait> GetPortraits(Damany.Util.DateTimeRange range);

        void DeletePortrait(System.Guid portraitId);
        void DeleteFrame(System.Guid frameId);

    }
}
