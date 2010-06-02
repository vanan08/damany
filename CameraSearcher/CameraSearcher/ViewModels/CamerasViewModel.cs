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
            _searchCamera.CameraFound += searchCamera_CameraFound;
        }

        public void Start()
        {
            _searchCamera.Search();
        }

        void searchCamera_CameraFound(object sender, SearchCamera.CameraFoundArgs e)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Action<object, SearchCamera.CameraFoundArgs> action = this.searchCamera_CameraFound;
                Application.Current.Dispatcher.BeginInvoke(action, sender, e);
                return;

            }

            var cameraQuery = (from c in Cameras
                               where c.Ip == e.CameraIp
                               select c).SingleOrDefault();

            if (cameraQuery == null)
            {
                var newCam = new Camera();
                newCam.Ip = e.CameraIp;
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
