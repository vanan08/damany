using System.Collections.Generic;

namespace Damany.Imaging.Common
{
    public interface IRepository
    {
        void SavePortrait(Portrait portrait);
        void SaveFrame(Frame frame);

        Frame GetFrame(System.Guid frameId);
        Portrait GetPortrait(System.Guid portraitId);

        IList<Frame> GetFrames(Damany.Util.DateTimeRange range);
        IList<Portrait> GetPortraits(Damany.Util.DateTimeRange range);

        void DeletePortrait(System.Guid portraitId);
        void DeleteFrame(System.Guid frameId);

    }
}
