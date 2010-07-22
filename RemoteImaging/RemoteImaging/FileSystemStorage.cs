using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using RemoteImaging.Core;
using System.IO;
using Damany.Imaging.Common;
using OpenCvSharp;
using RemoteControlService;
using Damany.Util.Extensions;

namespace RemoteImaging
{
    public class FileSystemStorage
    {
        private readonly string _outputRoot;


        public FileSystemStorage(string outputRoot)
        {
            if (outputRoot == null) throw new ArgumentNullException("outputRoot");

            if (!Directory.Exists(outputRoot))
            {
                Directory.CreateDirectory(outputRoot);
            }

            _outputRoot = outputRoot;

            InitializeVideoRepository();

            InitializeFileWatcher();
        }



        private void InitializeVideoRepository()
        {
            var cameraIds = System.IO.Directory.GetDirectories(_outputRoot);

            foreach (var cameraId in cameraIds)
            {
                var dirName = cameraId.Split(Path.DirectorySeparatorChar).Last();

                int id;
                if (int.TryParse(dirName, out id))
                {
                    SearchAllDaysSaved(id);
                }
            }
        }

        private void InitializeFileWatcher()
        {
            var watcher = new FileSystemWatcher(_outputRoot);
            watcher.IncludeSubdirectories = true;
            watcher.NotifyFilter = NotifyFilters.DirectoryName;
            watcher.Created += watcher_Created;
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            var date = this.GetDayFromPath(e.FullPath);

            if (date != null)
            {
                _daysSaved.Add(date);
            }
        }


        public bool DriveRemoveable(string drive)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo(_outputRoot);
            return (di.DriveType == DriveType.Removable);
        }


        private string FaceImageFileNameOf(string bigImagePath, int indexOfFace)
        {
            int idx = bigImagePath.IndexOf('.');
            string faceFileName = bigImagePath.Insert(idx, "-" + indexOfFace.ToString("d4"));
            return faceFileName;
        }

        private string ContainerDirectoryOfFaceAt(int camID, DateTime dt)
        {
            string folderForFaces = BuildDestDirectory(RootStoragePathForCamera(camID),
                                                dt, Properties.Settings.Default.IconDirectoryName);
            return folderForFaces;
        }

        private string EnsureFolderForFacesAt(int camID, DateTime dt)
        {
            string folderForFaces = ContainerDirectoryOfFaceAt(camID, dt);

            if (!Directory.Exists(folderForFaces))
                Directory.CreateDirectory(folderForFaces);

            return folderForFaces;
        }


        public bool FaceImagesCapturedWhen(int camID, DateTime time)
        {
            string path = ContainerDirectoryOfFaceAt(camID, time);

            return Directory.Exists(path);
        }

        public bool MotionImagesCapturedWhen(int camID, DateTime time)
        {
            string root = RootStoragePathForCamera(camID);
            string path = BuildBigImgPath(root, time);

            return Directory.Exists(path);
        }


        public string BuildBigImgPath(string outputPath, DateTime time)
        {
            return BuildDestDirectory(outputPath, time, Properties.Settings.Default.BigImageDirectoryName);
        }


        public string BuildFaceImgPath(string outputPath, DateTime time)
        {
            return BuildDestDirectory(outputPath, time, Properties.Settings.Default.IconDirectoryName);
        }



        public string BuildDestDirectory(string outputPathRoot,
           DateTime dt,
           string subFoldername
           )
        {


            StringBuilder sb = new StringBuilder();
            sb.Append(dt.Year.ToString("D4"));
            sb.Append(Path.DirectorySeparatorChar);
            sb.Append(dt.Month.ToString("D2"));
            sb.Append(Path.DirectorySeparatorChar);
            sb.Append(dt.Day.ToString("D2"));
            sb.Append(Path.DirectorySeparatorChar);
            if (!string.IsNullOrEmpty(subFoldername))
            {
                sb.Append(subFoldername);
                sb.Append(Path.DirectorySeparatorChar);
            }
            string temp = ToStringYearToMinute(dt);
            sb.Append(temp);
            sb.Append(Path.DirectorySeparatorChar);
            string destPath = Path.Combine(outputPathRoot, sb.ToString());
            return destPath;
        }

        public string BigImgPathForFace(ImageDetail face)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(face.Name);
            int idx = nameWithoutExtension.LastIndexOf('-');
            nameWithoutExtension = nameWithoutExtension.Remove(idx);

            string bigPicName = nameWithoutExtension + Path.GetExtension(face.Name);
            string bigPicFolder = Directory.GetParent(face.ContainedBy).ToString();

            bigPicFolder = bigPicFolder.Replace(Properties.Settings.Default.IconDirectoryName, "");

            bigPicFolder = Path.Combine(bigPicFolder, Properties.Settings.Default.BigImageDirectoryName);
            bigPicFolder = Path.Combine(bigPicFolder, ToStringYearToMinute(face.CaptureTime));
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }


        private string VideoFilePathFrom(DateTime time, int camID)
        {
            string rootFolder = Path.Combine(_outputRoot,
                                        camID.ToString("D2"));

            DateTime utcTime = time.ToUniversalTime();
            string relPath = RelativePathNameForVideoFile(utcTime);

            string videoFilePath = Path.Combine(rootFolder, relPath);
            return videoFilePath;
        }

        public string VideoFilePathNameIfExists(DateTime time, int camID)
        {
            string videoFilePath = VideoFilePathFrom(time, camID);
            if (System.IO.File.Exists(videoFilePath))
                return videoFilePath;
            else
                return string.Empty;

        }


        public string[] VideoFilesOfImage(ImageDetail img)
        {
            string videoFilePath = VideoFilePathNameIfExists(img.CaptureTime, img.FromCamera);
            if (File.Exists(videoFilePath))
            {
                string[] videos = new string[] { videoFilePath };
                return videos;
            }
            else
            {
                return new string[0];
            }
        }

        public IEnumerable<RemoteImaging.Core.Video> VideoFilesBetween(int cameraID, DateTime startLocalTime, DateTime endLocalTime)
        {
            string rootFolder = Path.Combine(_outputRoot, cameraID.ToString("D2"));

            DateTime startUTC = startLocalTime.ToUniversalTime();
            DateTime endUTC = endLocalTime.ToUniversalTime();
            List<RemoteImaging.Core.Video> videos = new List<RemoteImaging.Core.Video>();

            var time = startUTC;

            while (time <= endUTC)
            {
                string relativePath = RelativePathNameForVideoFile(time);
                string path = Path.Combine(rootFolder, relativePath);

                if (File.Exists(path))
                {
                    yield return new RemoteImaging.Core.Video
                    {
                        Path = path,
                        CapturedAt = time.ToLocalTime().RoundToMinute(),
                    };
                }

                time = time.AddMinutes(1);

            }

        }

        private string RelativePathNameForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }

        public void DeleteVideoFileAt(DateTime time)
        {
            string m4vFile = VideoFilePathNameIfExists(time, 2);
            if (File.Exists(m4vFile))
            {
                File.Delete(m4vFile);
            }

            string idvFile = m4vFile.Replace(".m4v", ".idv");
            if (File.Exists(idvFile))
            {
                File.Delete(idvFile);
            }
        }

        private string TheOldestSubDirectory(string root, string pattern)
        {
            string oldestName = (from y in System.IO.Directory.GetDirectories(root, pattern)
                                 orderby y
                                 select y).FirstOrDefault();

            if (string.IsNullOrEmpty(oldestName)) return string.Empty;

            return System.IO.Path.Combine(root, oldestName);

        }


        private void DeleteVideoForDay(int y, int m, int d)
        {
            DateTime dt = new DateTime(y, m, d);

            for (int hour = 0; hour < 24; hour++)
            {
                for (int minute = 0; minute < 60; minute++)
                {
                    DateTime newdt = new DateTime(y, m, d, hour, minute, 0);

                    string path = VideoFilePathNameIfExists(newdt, 2);

                    if (!string.IsNullOrEmpty(path))
                    {
                        DirectoryInfo mi = new DirectoryInfo(path);
                        DirectoryInfo hi = mi.Parent;
                        DirectoryInfo di = hi.Parent;

                        DeleteVideoFileAt(newdt);

                        if (hi.GetFiles().Length == 0)
                        {
                            hi.Delete();
                        }
                        if (di.GetDirectories().Length == 0)
                        {
                            di.Delete();
                        }

                    }

                }
            }
        }

        public void DeleteVideos(DateAndDeleteFlag videoToDelete)
        {
            if (Directory.Exists(videoToDelete.AbsoluteDirectory))
            {
                Directory.Delete(videoToDelete.AbsoluteDirectory, true);
                videoToDelete.Deleted = true;
            }

        }

        public void DeleteMostOutDatedDataForDay(int days, int cameraId)
        {
            string root = RootStoragePathForCamera(cameraId);
            DateTime now = DateTime.Now;

            string yS, mS, dS;
            int y = now.Year, m = now.Month, d = now.Day;

            var year = TheOldestSubDirectory(root, "20??");
            if (string.IsNullOrEmpty(year)) return;

            yS = new DirectoryInfo(year).Name;
            int.TryParse(yS, out y);

            if (Directory.GetDirectories(year).Length == 0 && y != now.Year)
            {
                Directory.Delete(year, true);
                return;
            }

            var month = TheOldestSubDirectory(year, "??");
            if (string.IsNullOrEmpty(month)) return;


            mS = new DirectoryInfo(month).Name;
            int.TryParse(mS, out m);

            if (Directory.GetDirectories(month).Length == 0 && m != now.Month)
            {
                Directory.Delete(month, true);
                return;
            }

            var day = TheOldestSubDirectory(month, "??");
            if (string.IsNullOrEmpty(day)) return;


            dS = new DirectoryInfo(day).Name; ;
            int.TryParse(dS, out d);

            if (Directory.GetDirectories(day).Length == 0 && d != now.Day)
            {
                Directory.Delete(day, true);
                return;
            }

            Directory.Delete(day, true);

            DeleteVideoForDay(y, m, d);

        }

        public int GetTotalStorageMB()
        {
            int mb = (int)new System.IO.DriveInfo(_outputRoot).TotalSize / (1024 * 1024);
            return mb;

        }

        private string ToStringYearToMinute(DateTime dt)
        {
            return dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2");
        }

        private string RootStoragePathForCamera(int cameraID)
        {
            return Path.Combine(_outputRoot, cameraID.ToString("D2"));
        }


        private void SearchAllDaysSaved(int id)
        {

            string dayAbsoluteDir = GetAbsoluteDirectoryForDay(id);
            if (!Directory.Exists(dayAbsoluteDir)) return;

            foreach (var dayDir in Directory.GetDirectories(dayAbsoluteDir))
            {
                var date = GetDayFromPath(dayDir);
                if (date != null)
                {
                    _daysSaved.Add(date);
                }
            }
        }


        private DateAndDeleteFlag GetDayFromPath(string absolutePath)
        {
            var dirs = absolutePath.Split(Path.DirectorySeparatorChar).Select(s => s.ToUpper()).ToArray();
            if (!dirs.Contains(DirNameNormal)) return null;

            int idx = -1;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i] == DirNameNormal)
                {
                    idx = i;
                    break;
                }
            }

            if (idx == -1) return null;

            if (idx == 0 || idx == dirs.Length - 1) return null;

            var cameraIdDirName = dirs[idx - 1];
            var dayDirName = dirs[idx + 1].Trim();

            if (dayDirName.Length < 8) return null;

            var cameraId = 0;
            if (!int.TryParse(cameraIdDirName, out cameraId)) return null;

            int y;
            if (!int.TryParse(dayDirName.Substring(0, 4), out y)) return null;

            int m;
            if (!int.TryParse(dayDirName.Substring(4, 2), out m)) return null;

            int d;
            if (!int.TryParse(dayDirName.Substring(6, 2), out d)) return null;

            var date = new DateTime(y, m, d, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();

            var absoluteDayDir = string.Join(Path.DirectorySeparatorChar.ToString(), dirs, 0, idx + 2);

            var dateAndDeleteFlag = new DateAndDeleteFlag() { AbsoluteDirectory = absoluteDayDir, Date = date, Deleted = false, CameraId = cameraId };

            return dateAndDeleteFlag;

        }


        private static int GetNormalDirNameIndex(string absoluteDayDir)
        {
            return absoluteDayDir.ToUpper().IndexOf(DirNameNormal);
        }

        private string GetAbsoluteDirectoryForDay(int id)
        {
            var dayRelativeDir = string.Format(@"{0:d2}\Normal\", id);
            return System.IO.Path.Combine(_outputRoot, dayRelativeDir);
        }


        public class DateAndDeleteFlag
        {
            public int CameraId { get; set; }
            public DateTime Date { get; set; }
            public string AbsoluteDirectory { get; set; }
            public bool Deleted { get; set; }
        }

        private void AddToVideoList(DateAndDeleteFlag flag)
        {
            lock (_locker)
            {
                _daysSaved.Add(flag);
            }
        }

        public DateAndDeleteFlag[] Videos
        {
            get
            {
                lock (_locker)
                {
                    var query = _daysSaved.ToArray();
                    return query;
                }
            }
        }


        private const string DirNameNormal = "NORMAL";

        private object _locker = new object();
        private readonly IList<DateAndDeleteFlag> _daysSaved
              = new List<DateAndDeleteFlag>();


    }
}
