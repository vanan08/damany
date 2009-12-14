using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;
using RemoteImaging.Query;
using System.Drawing;
using System.Diagnostics;


namespace RemoteImaging.Service
{
    [ServiceKnownType(typeof(System.Drawing.Bitmap))]
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    class Service : IServiceFacade
    {
        string[] FaceFiles;

        #region IServiceFacade Members

        public string[] SearchFaces(int cameraID, DateTime beginTime, DateTime endTime)
        {
            ImageDirSys start =
                new ImageDirSys(cameraID.ToString("d2"), DateTimeInString.FromDateTime(beginTime));
            ImageDirSys end =
                 new ImageDirSys(cameraID.ToString("d2"), DateTimeInString.FromDateTime(endTime));

            FaceFiles = ImageSearch.SearchImages(start, end, ImageDirSys.SearchType.PicType);

            return FaceFiles;
        }

        public ImagePair GetFace(string path)
        {
            Bitmap face = (Bitmap)Image.FromFile(path);

            string bigImgPath = FileSystemStorage.BigImgPathForFace(Core.ImageDetail.FromPath(path));

            Bitmap big = (Bitmap)Image.FromFile(bigImgPath);

            ImagePair ip = new ImagePair();
            ip.Face = face;
            ip.FacePath = path;

            ip.BigImage = big;
            ip.BigImagePath = bigImgPath;

            return ip;
        }

        public Video[] SearchVideos(int cameraID, DateTime from, DateTime to)
        {
            RemoteImaging.Core.Video[] videos = FileSystemStorage.VideoFilesBetween(cameraID, from, to);

            Video[] serviceVideos = new Video[videos.Length];

            for (int i = 0; i < videos.Length; ++i)
            {
                serviceVideos[i] = new Video()
                {
                    HasFaceCaptured = videos[i].HasFaceCaptured,
                    Path = videos[i].Path,
                };
            }

            return serviceVideos;
        }

        public string VideoFilePathRecordedAt(DateTime time, int camID)
        {
            return FileSystemStorage.VideoFilePathNameIfExists(time, camID);
        }


        public ImagePair[] FacesCapturedAt(int cameraID, DateTime time)
        {
            string[] files = ImageSearch.FacesCapturedAt(time, cameraID, true);
            ImagePair[] bmps = new ImagePair[files.Length];

            for (int i = 0; i < files.Length ; i++)
            {
                ImagePair ip = new ImagePair();
                ip.Face = (Bitmap) Bitmap.FromFile(files[i]);
                ip.FacePath = files[i];

                bmps[i] = ip;
            }

            return bmps;
            

        }


   

        #endregion
    }
}
