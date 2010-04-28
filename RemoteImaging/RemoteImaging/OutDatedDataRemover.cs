using System;
using System.Collections.Generic;
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
            Interval = 10*60*1000;
            ReservedDiskSpaceInGB = 1;


            var timer = new System.Timers.Timer();
            timer.Interval = Interval;
            timer.AutoReset = false;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var videos = _videoRepository.Videos;

            var ordered = from v in videos
                          where v.Deleted == false
                          orderby v.Date ascending
                          select v;



            
        }


        public double Interval { get; set; }
        public float ReservedDiskSpaceInGB { get; set; }

       

        private readonly IRepository _facesRepository;
        private readonly FileSystemStorage _videoRepository;
        private readonly string _outputDirectory;
    }
}
