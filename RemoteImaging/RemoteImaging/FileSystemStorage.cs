using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;
using System.IO;
using Damany.Imaging.Common;
using OpenCvSharp;
using RemoteControlService;

namespace RemoteImaging
{
    public static class FileSystemStorage
    {
        private static string ToStringYearToMinute(DateTime dt)
        {
            return dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2");
        }

        private static string RootStoragePathForCamera(int cameraID)
        {
            return Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));
        }


        public static long GetFreeDiskSpaceBytes(string drive)
        {
            DriveInfo driveInfo = new DriveInfo(drive);
            long FreeSpace = driveInfo.AvailableFreeSpace;

            return FreeSpace;
        }

        public static bool DriveRemoveable(string drive)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo(Properties.Settings.Default.OutputPath);
            return (di.DriveType == DriveType.Removable);
        }

        public static void SaveFrame(Frame frame)
        {
            IplImage ipl = frame.image;
            ipl.IsEnabledDispose = false;

            string path = frame.GetFileName();
            DateTime dt = DateTime.FromBinary(frame.timeStamp);

            string root = RootStoragePathForCamera(frame.cameraID);
            string folder = BuildBigImgPath(root, dt);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            path = Path.Combine(folder, path);
            ipl.SaveImage(path);
        }



        public static string PathForFaceImage(Frame frame, int sequence)
        {
            DateTime dt = DateTime.FromBinary(frame.timeStamp);

            string folderFace = FileSystemStorage.EnsureFolderForFacesAt(frame.cameraID, dt);

            string faceFileName = FileSystemStorage.FaceImageFileNameOf(frame.GetFileName(), sequence);

            string facePath = Path.Combine(folderFace, faceFileName);
            return facePath;
        }

        private static string FaceImageFileNameOf(string bigImagePath, int indexOfFace)
        {
            int idx = bigImagePath.IndexOf('.');
            string faceFileName = bigImagePath.Insert(idx, "-" + indexOfFace.ToString("d4"));
            return faceFileName;
        }

        private static string ContainerDirectoryOfFaceAt(int camID, DateTime dt)
        {
            string folderForFaces = BuildDestDirectory(RootStoragePathForCamera(camID),
                                                dt, Properties.Settings.Default.IconDirectoryName);
            return folderForFaces;
        }

        private static string EnsureFolderForFacesAt(int camID, DateTime dt)
        {
            string folderForFaces = ContainerDirectoryOfFaceAt(camID, dt);

            if (!Directory.Exists(folderForFaces))
                Directory.CreateDirectory(folderForFaces);

            return folderForFaces;
        }


        public static bool FaceImagesCapturedWhen(int camID, DateTime time)
        {
            string path = ContainerDirectoryOfFaceAt(camID, time);

            return Directory.Exists(path);
        }

        public static bool MotionImagesCapturedWhen(int camID, DateTime time)
        {
            string root = RootStoragePathForCamera(camID);
            string path = BuildBigImgPath(root, time);

            return Directory.Exists(path);
        }


        public static string BuildBigImgPath(string outputPath, DateTime time)
        {
            return BuildDestDirectory(outputPath, time, Properties.Settings.Default.BigImageDirectoryName);
        }


        public static string BuildFaceImgPath(string outputPath, DateTime time)
        {
            return BuildDestDirectory(outputPath, time, Properties.Settings.Default.IconDirectoryName);
        }



        public static string BuildDestDirectory(string outputPathRoot,
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

        public static string BigImgPathForFace(ImageDetail face)
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


        private static string VideoFilePathFrom(DateTime time, int camID)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath,
                                        camID.ToString("D2"));

            DateTime utcTime = time.ToUniversalTime();
            string relPath = RelativePathNameForVideoFile(utcTime);

            string videoFilePath = Path.Combine(rootFolder, relPath);
            return videoFilePath;
        }

        public static string VideoFilePathNameIfExists(DateTime time, int camID)
        {
            string videoFilePath = VideoFilePathFrom(time, camID);
            if (System.IO.File.Exists(videoFilePath))
                return videoFilePath;
            else
                return string.Empty;

        }




        public static string[] VideoFilesOfImage(ImageDetail img)
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

        public static RemoteImaging.Core.Video[] VideoFilesBetween(int cameraID, DateTime startLocalTime, DateTime endLocalTime)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));

            DateTime startUTC = startLocalTime.ToUniversalTime();
            DateTime endUTC = endLocalTime.ToUniversalTime();
            List<RemoteImaging.Core.Video> videos = new List<RemoteImaging.Core.Video>();

            while (startUTC <= endUTC)
            {
                string relativePath = RelativePathNameForVideoFile(startUTC);
                string path = Path.Combine(rootFolder, relativePath);

                if (File.Exists(path))
                {
                    bool hasFaceCaptured =
                        FaceImagesCapturedWhen(cameraID, startUTC.ToLocalTime());
                    bool isMotionLess =
                        !MotionImagesCapturedWhen(cameraID, startUTC.ToLocalTime());
                    bool isMotionWithoutFace = !isMotionLess && !hasFaceCaptured;

                    videos.Add(new RemoteImaging.Core.Video { 
                                                            HasFaceCaptured = hasFaceCaptured, 
                                                            Path = path, 
                                                            IsMotionWithoutFace = isMotionWithoutFace,
                                                            IsMotionLess = isMotionLess,
                                                            });
                }

                startUTC = startUTC.AddMinutes(1);

            }

            return videos.ToArray();

        }

        private static string RelativePathNameForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }

        public static void DeleteVideoFileAt(DateTime time)
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

        private static string TheOldestSubDirectory(string root, string pattern)
        {
            string oldestName = (from y in System.IO.Directory.GetDirectories(root, pattern)
                                 orderby y
                                 select y).FirstOrDefault();

            if (string.IsNullOrEmpty(oldestName)) return string.Empty;

            return System.IO.Path.Combine(root, oldestName);

        }


        private static void DeleteVideoForDay(int y, int m, int d)
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


        public static void DeleteMostOutDatedDataForDay(int days)
        {
            string root = RootStoragePathForCamera(2);
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

        public static int GetTotalStorageMB()
        {
            int mb = (int) new System.IO.DriveInfo(Properties.Settings.Default.OutputPath).TotalSize / (1024 * 1024);
            return mb;

        }

    }
}
