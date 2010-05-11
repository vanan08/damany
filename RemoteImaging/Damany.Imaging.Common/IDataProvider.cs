using System.Collections.Generic;

namespace Damany.Imaging.Common
{
    public interface IRepository
    {
        void SavePortrait(Portrait portrait);
        void SaveFrame(Frame frame);

        Frame GetFrame(System.Guid frameId);
        Portrait GetPortrait(System.Guid portraitId);

        IList<Frame> GetFrames(int cameraId, Damany.Util.DateTimeRange range);
        IList<Portrait> GetPortraits(int dameraId, Damany.Util.DateTimeRange range);

        void DeletePortrait(System.Guid portraitId);
        void DeleteFrame(System.Guid frameId);

        void DeletePortraits(int cameraId, Util.DateTimeRange range);
        void DeleteFrames(int cameraId, Util.DateTimeRange range);

    }
}
