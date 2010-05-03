using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateUploadMonitor : ILicensePlateEventGenerator
    {
        public delegate LicensePlateUploadMonitor Factory(string pathToMonitor);

        public LicensePlateUploadMonitor(string pathToMonitor,
                                         ILicensePlateEventPublisher plateEventPublisher)
        {
            if (pathToMonitor == null) throw new ArgumentNullException("pathToMonitor");
            if (!System.IO.Directory.Exists(pathToMonitor))
                throw new System.IO.DirectoryNotFoundException(pathToMonitor + " not found");


            _plateEventPublisher = plateEventPublisher;
            Configuration = new LicenseParsingConfig();
            _pathToMonitor = pathToMonitor;
        }

        public void Start()
        {
            var watcher = new System.IO.FileSystemWatcher(_pathToMonitor);
            watcher.IncludeSubdirectories = Configuration.IncludeSubdirectories;
            watcher.Filter = Configuration.Filter;
            watcher.Created += watcher_Created;
            watcher.EnableRaisingEvents = true;
            
        }

   

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (!IsInterestedEvent(e)) return;

            try
            {
                System.Threading.Thread.Sleep(2000);

                var licensePlateInfo = ParsePath(e.FullPath);
                licensePlateInfo.CapturedFrom = CameraId;
                _plateEventPublisher.PublishLicensePlate(licensePlateInfo);

                File.Delete(e.FullPath);

            }
            catch (FormatException)
            {
                //should log exception here
            }

        }

        private LicensePlateInfo ParsePath(string fullPath)
        {
            var fileName = System.IO.Path.GetFileName(fullPath);
            var strings = fileName.Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length < Configuration.LeastSectionCount) throw new FormatException("file naming format incorrect");

            var licensePlateInfo = new LicensePlateInfo();

            try
            {
                var time = strings[Configuration.TimeSectionIndex].Parse();
                var number = strings[Configuration.LicensePlateSectionIndex];

                licensePlateInfo.CaptureTime = time;
                licensePlateInfo.ImageData  = File.ReadAllBytes(fullPath);
                licensePlateInfo.LicensePlateNumber = number;

                return licensePlateInfo;
            }
            catch (System.Exception ex)
            {
                throw new FormatException("file naming format incorrect", ex);
            }
        }

        private bool IsInterestedEvent(FileSystemEventArgs e)
        {
            return e.ChangeType == WatcherChangeTypes.Created
                   && File.Exists(e.FullPath);
        }

        public int CameraId { get; set; }

        public LicenseParsingConfig Configuration { get; set; }


        private readonly ILicensePlateEventPublisher _plateEventPublisher;
        private readonly string _pathToMonitor;


    }
}
