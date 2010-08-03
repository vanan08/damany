using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using MiscUtil.Threading;

namespace RemoteImaging
{
    public class OutDatedDataRemover
    {
        public OutDatedDataRemover(
            IRepository facesRepository,
            FileSystemStorage videoRepository,
            string outputDirectory)
        {
            _facesRepository = facesRepository;
            _videoRepository = videoRepository;
            _outputDirectory = outputDirectory;
            Interval = 10 * 60 * 1000;
            ReservedDiskSpaceInGb = 1;

#if DEBUG
            Interval = 10*1000;
#endif

            _timer = new System.Timers.Timer();
            _timer.Interval = Interval;
            _timer.AutoReset = false;
            _timer.Elapsed += timer_Elapsed;
            _timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DeleteData();

            _timer.Enabled = true;
        }

        private void DeleteData()
        {
            while (ShouldDoCleaning())
            {
                var videos = _videoRepository.Videos;

                var ordered = from v in videos
                              where v.Deleted == false
                              orderby v.Date ascending
                              select v;

                var first = ordered.FirstOrDefault();
                if (first != null)
                {
                    _videoRepository.DeleteVideos(first);

                    var range = new Damany.Util.DateTimeRange(first.Date, first.Date.AddDays(1));

                    _facesRepository.DeletePortraits(first.CameraId, range);
                    _facesRepository.DeleteFrames(first.CameraId, range);
                }
            }
        }

        private bool ShouldDoCleaning()
        {
            var freeSpace = GetFreeDiskSpaceInGb(_outputDirectory);
            return freeSpace <= ReservedDiskSpaceInGb;
        }

        public long GetFreeDiskSpaceInGb(string drive)
        {
            var driveInfo = new DriveInfo(drive);
            long freeSpace = driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024);

            return freeSpace;
        }



        public double Interval { get; set; }
        private float _reservedDiskSpaceInGb;

        public float ReservedDiskSpaceInGb
        {
            get
            {
                return _reservedDiskSpaceInGb;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("value should > 0");
                }

                _reservedDiskSpaceInGb = value;
            }
        }


        private readonly IRepository _facesRepository;
        private readonly FileSystemStorage _videoRepository;
        private readonly string _outputDirectory;

        private readonly System.Timers.Timer _timer;
    }
}
