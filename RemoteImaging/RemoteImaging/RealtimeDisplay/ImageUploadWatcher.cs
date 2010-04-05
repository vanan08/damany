using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RemoteImaging.Core;

namespace RemoteImaging.RealtimeDisplay
{
    public class ImageUploadWatcher
    {
        public event ImageUploadHandler ImagesUploaded;

        private FileSystemWatcher _Watcher;

        public string PathToWatch { get; set; }

        public void Start()
        {
            this._Watcher = new FileSystemWatcher();
            this._Watcher.IncludeSubdirectories = true;

            if (!Directory.Exists(this.PathToWatch))
            {
                Directory.CreateDirectory(this.PathToWatch);
            }

            this._Watcher.Path = this.PathToWatch;
            this._Watcher.Filter = "*.jpg";
            this._Watcher.Created += File_Created;
            this._Watcher.EnableRaisingEvents = true;
        }

        IDictionary<int, IList<ImageDetail>> cameraImagesQueue
            = new Dictionary<int, IList<ImageDetail>>();

        private void InitCameraQueue(int cameraID)
        {
            if (!cameraImagesQueue.ContainsKey(cameraID))
            {
                cameraImagesQueue[cameraID] = new List<ImageDetail>();
            }
        }

        private void FireUploadEvent(ImageDetail incomingImg)
        {
            IList<ImageDetail> imgs = this.cameraImagesQueue[incomingImg.FromCamera];

            if (imgs.Count == 0)
            {
                imgs.Add(incomingImg);
                return;
            }

            int idxOfLastImg = imgs.Count - 1;

            bool sameBatch = incomingImg.CaptureTime == imgs[idxOfLastImg].CaptureTime;

            //已经是新的一组图片
            if (!(sameBatch))
            {
                ImageDetail[] imgsToFire = imgs.ToArray();
                imgs.Clear();
                imgs.Add(incomingImg);
                FireEvent(imgsToFire);
            }
            else
            {
                imgs.Add(incomingImg);
                //一组图片已经收集齐全
                if (imgs.Count == Properties.Settings.Default.LengthOfImageGroup)
                {
                    ImageDetail[] imgsToFire = imgs.ToArray();
                    imgs.Clear();
                    FireEvent(imgsToFire);
                }
            }
        }



        private void FireEvent(ImageDetail[] imgs)
        {
            if (imgs.Length <= 0)
                return;

            if (this.ImagesUploaded != null)
            {
                ImageUploadEventArgs args =
                    new ImageUploadEventArgs { CameraID = imgs[0].FromCamera, Images = imgs };
                this.ImagesUploaded(this, args);
            }

        }

        void File_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                ImageDetail img = ImageDetail.FromPath(e.FullPath);

                System.Diagnostics.Debug.WriteLine("file created");
                System.Diagnostics.Debug.WriteLine(e.FullPath);

                InitCameraQueue(img.FromCamera);
                FireUploadEvent(img);
            }
        }
    }
}
