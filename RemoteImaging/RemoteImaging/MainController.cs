using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.PC.Domain;
using Damany.PortraitCapturer.DAL;
using RemoteImaging.Core;

namespace RemoteImaging
{
    public class MainController
    {
        public MainController(RealtimeDisplay.MainForm mainForm,
            Damany.RemoteImaging.Common.ConfigurationManager configManager,
            IRepository repository)
        {
            this._mainForm = mainForm;
            this._configManager = configManager;
            _repository = repository;
        }

        public void Start()
        {
            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();


            var camToStart = this._configManager.GetCameras();

            if (camToStart.Count == 1)
            {
                var single = this._configManager.GetCameras().Single();
                this.StartCameraInternal(single);
            }

        }

        public void StartCamera()
        {
            var selected = this._mainForm.GetSelectedCamera();
            if (selected == null) return;

            if (_currentController != null)
            {
                _currentController.Stop();
            }

            this.StartCameraInternal(selected);
        }

        public void SelectedPortraitChanged()
        {
            var p = _mainForm.SelectedPortrait;
            if (p == null) return;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
                                                              {
                                                                  var f = _repository.GetFrame(p.FrameId);
                                                                  _mainForm.BigImage = f;

                                                              });

        }

        public void PlayVideo()
        {
            var p = _mainForm.SelectedPortrait;
            if (p == null) return;

            VideoPlayer.PlayRelatedVideo(p);
        }



        private void StartCameraInternal(CameraInfo cam)
        {
            System.Threading.WaitCallback action = delegate
            {
                try
                {
                    var camController = Damany.RemoteImaging.Common.SearchLineBuilder.BuildNewSearchLine(cam);
                    camController.RegisterPortraitHandler(new Damany.Imaging.Handlers.PersistenceWriter(_repository));
                    camController.RegisterPortraitHandler(this._mainForm);

                    camController.Start();
                    _currentController = camController;
                    if (cam.Provider == CameraProvider.Sanyo)
                    {
                        this._mainForm.StartRecord(cam);
                    }

                }
                catch (Exception ex)
                {
                    var msg = string.Format("无法连接 {0}", cam.Location.Host);
                    _mainForm.ShowMessage(msg);
                }

            };
            System.Threading.ThreadPool.QueueUserWorkItem(action);
        }


        private RealtimeDisplay.MainForm _mainForm;
        private Damany.RemoteImaging.Common.ConfigurationManager _configManager;
        private readonly IRepository _repository;
        private Damany.Imaging.Processors.FaceSearchController _currentController;
    }
}
