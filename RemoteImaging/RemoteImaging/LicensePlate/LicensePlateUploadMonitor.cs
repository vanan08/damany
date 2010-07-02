using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
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
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = Configuration.ScanInterval*1000;
                _timer.AutoReset = false;
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
            }
            
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ScanDirectory();
        }

        private void ScanDirectory()
        {
            var files = System.IO.Directory.GetFiles(_pathToMonitor, Configuration.Filter);
            foreach (var file in files)
            {
                ProcessFile(file);
            }

            _timer.Enabled = true;
        }
   

        private void ProcessFile(string  filePath)
        {
            try
            {
                var licensePlateInfo = ParsePath(filePath);
                licensePlateInfo.CapturedFrom = CameraId;
                licensePlateInfo.ImageData = File.ReadAllBytes(filePath);
                _plateEventPublisher.PublishLicensePlate(licensePlateInfo);

                File.Delete(filePath);
            }
            catch (FormatException)
            {}
            catch(System.IO.IOException)
            {}
            
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
        private System.Timers.Timer _timer;


    }
}
