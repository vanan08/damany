using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CameraSearcher.ViewModels
{
    using Models;

    public class CamerasViewModel
    {
        private readonly SearchCamera.CameraSearcher _searchCamera;

        public CamerasViewModel()
        {
            Cameras = new ObservableCollection<Camera>();
            _searchCamera = new SearchCamera.CameraSearcher();
            _searchCamera.NewCamera += SearchNewCameraNewCamera;
        }

        public void Start()
        {
            _searchCamera.Search();
        }

        void SearchNewCameraNewCamera(object sender, MiscUtil.EventArgs<SearchCamera.CameraInfo> e)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Action<object, MiscUtil.EventArgs<SearchCamera.CameraInfo> > action = this.SearchNewCameraNewCamera;
                Application.Current.Dispatcher.BeginInvoke(action, sender, e);
                return;

            }

            var cameraQuery = (from c in Cameras
                               where c.Mac == e.Value.Mac
                               select c).SingleOrDefault();

            if (cameraQuery == null)
            {
                var newCam = new Camera();
                newCam.Ip = e.Value.CameraIp;
                newCam.Mac = e.Value.Mac;
                Cameras.Add(newCam);
            }
            else
            {
                cameraQuery.LastSeenActive = DateTime.Now;
            }
        }

        public ObservableCollection<Camera> Cameras { get; set; }
    }
}
