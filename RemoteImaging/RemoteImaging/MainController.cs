using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.Imaging.Processors;
using Damany.PC.Domain;
using Damany.PortraitCapturer.DAL;
using RemoteImaging.Core;
using Damany.RemoteImaging.Common;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging
{
    public class MainController
    {
        private FaceSearchFacade _currentFaceSearchFacade;
        private Func<FaceSearchFacade> _faceSearchFacadeFactory;

        public MainController(RealtimeDisplay.MainForm mainForm,
                              ConfigurationManager configManager,
                              IRepository repository,
                              Func<FaceSearchFacade> faceSearchFacadeFactory)
        {
            this._mainForm = mainForm;
            this._configManager = configManager;
            _repository = repository;
            _faceSearchFacadeFactory = faceSearchFacadeFactory;
        }

        public void Start()
        {
            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();
            var camToStart = this._configManager.GetCameras();

            if (camToStart.Count == 1)
            {
                var single = this._configManager.GetCameras().Single();
                this.StartCamera(single);
            }

        }

        public void Stop()
        {
            StopSearchFaceFacade();
        }


        public void StartCamera(CameraInfo cameraInfo = null)
        {
            if (cameraInfo == null)
            {
                cameraInfo = _mainForm.GetSelectedCamera();
            }

            if (cameraInfo == null)
            {
                return;
            }

            StopSearchFaceFacade();

            var facade = _faceSearchFacadeFactory();
            facade.StartWith(cameraInfo);

            if (cameraInfo.Provider == CameraProvider.Sanyo)
            {
                _mainForm.StartRecord(cameraInfo);
            }

            _currentFaceSearchFacade = facade;

        }

        private void StopSearchFaceFacade()
        {
            if (_currentFaceSearchFacade != null)
            {
                _currentFaceSearchFacade.SignalToStop();
                _currentFaceSearchFacade.WaitForStop();
            }
        }

        public void SelectedPortraitChanged()
        {
            var p = _mainForm.SelectedPortrait;
            if (p == null) return;

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
                                                              {
                                                                  var f = p.Frame;
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
                    if (cam.Provider == CameraProvider.Sanyo)
                    {
                        this._mainForm.StartRecord(cam);
                    }

                }
                catch (System.Net.WebException ex)
                {
                    var msg = string.Format("无法连接 {0}", cam.Location.Host);
                    _mainForm.ShowMessage(msg);
                }

            };
            System.Threading.ThreadPool.QueueUserWorkItem(action);
        }



        private RealtimeDisplay.MainForm _mainForm;
        private ConfigurationManager _configManager;
        private readonly IRepository _repository;
        private readonly FaceSearchFacade _faceSearchFacade;
    }
}
