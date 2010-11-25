using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;
using System.IO;
using Damany.Imaging.Common;
using OpenCvSharp;

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


        public static int GetFreeDiskSpaceMB(string drive)
        {
            DriveInfo driveInfo = new DriveInfo(drive);
            long FreeSpace = driveInfo.AvailableFreeSpace;

            FreeSpace /= 1024 * 1024;
            return (int)FreeSpace;
        }

        public static bool DriveRemoveable(string drive)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo(Properties.Settings.Default.OutputPath);
            return (di.DriveType == DriveType.Removable);
        }

        private static string StorageRootPathForCamera(int cameraID)
        {
            string root = Path.Combine(Properties.Settings.Default.OutputPath,
                                  cameraID.ToString("d2"));
            return root;
        }

        public static void SaveFrame(Frame frame)
        {
            IplImage ipl = frame.GetImage();

            string path = frame.GetFileName();
            DateTime dt = frame.CapturedAt;

            string root = StorageRootPathForCamera(frame.CapturedFrom.Id);
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
            DateTime dt = frame.CapturedAt;

            string folderFace = FileSystemStorage.EnsureFolderForFacesAt(frame.CapturedFrom.Id, dt);

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
            string root = StorageRootPathForCamera(camID);
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


        public static string VideoFilePathNameAt(DateTime time, int camID)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath,
                            camID.ToString("D2"));

            DateTime utcTime = time.ToUniversalTime();
            string relPath = RelativePathNameForVideoFile(utcTime);

            string videoFilePath = Path.Combine(rootFolder, relPath);
            return videoFilePath;
        }


        public static string[] VideoFilesOfImage(ImageDetail img)
        {
            string videoFilePath = VideoFilePathNameAt(img.CaptureTime, img.FromCamera);
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

        public static string[] VideoFilesBetween(int cameraID, DateTime startLocalTime, DateTime endLocalTime)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));

            DateTime startUTC = startLocalTime.ToUniversalTime();
            DateTime endUTC = endLocalTime.ToUniversalTime();
            List<string> files = new List<string>();

            while (startUTC <= endUTC)
            {
                string relativePath = RelativePathNameForVideoFile(startUTC);
                string path = Path.Combine(rootFolder, relativePath);
                if (File.Exists(path))
                {
                    files.Add(path);
                }

                startUTC = startUTC.AddMinutes(1);

            }

            return files.ToArray();

        }

        private static string RelativePathNameForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }
    }
}
