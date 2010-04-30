using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateUploadMonitor
    {
        public delegate LicensePlateUploadMonitor Factory(string pathToMonitor);

        public LicensePlateUploadMonitor(string pathToMonitor,
            ILicensePlateEventPublisher plateEventPublisher
                                         )
        {
            if (pathToMonitor == null) throw new ArgumentNullException("pathToMonitor");
            if (!System.IO.Directory.Exists(pathToMonitor))
                throw new System.IO.DirectoryNotFoundException(pathToMonitor + " not found");

            FileFilter = "*.jpg";

            _plateEventPublisher = plateEventPublisher;
            _pathToMonitor = pathToMonitor;
            var watcher = new System.IO.FileSystemWatcher(pathToMonitor);
            watcher.Filter = FileFilter;
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
            if (strings.Length < 3) throw new FormatException("file naming format incorrect");

            var licensePlateInfo = new LicensePlateInfo();

            try
            {
                var time = strings[1].Parse();
                var number = strings[2];
                var img = MiscHelper.FromFileBuffered(fullPath);

                File.Delete(fullPath);

                licensePlateInfo.CaptureTime = time;
                licensePlateInfo.LicensePlateImage = img;
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


        public string FileFilter { get; set; }
        public int CameraId { get; set; }


        private readonly ILicensePlateEventPublisher _plateEventPublisher;
        private readonly string _pathToMonitor;


    }
}
