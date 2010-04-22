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

namespace RemoteImaging
{
    public class MainController
    {
        public MainController(RealtimeDisplay.MainForm mainForm,
                              ConfigurationManager configManager,
                              IRepository repository,
                              IEnumerable<IPortraitHandler> handlers,
                              FaceComparer comparer)
        {
            this._mainForm = mainForm;
            this._configManager = configManager;
            _repository = repository;
            _handlers = handlers;
            _comparer = comparer;

        }

        public void Start()
        {
            InitializeHandlers();

            this._comparer.PersonOfInterestDected += _comparer_PersonOfInterestDected;
            this._comparer.Threshold = Properties.Settings.Default.RealTimeFaceCompareSensitivity;
            this._comparer.Comparer.SetSensitivity( Properties.Settings.Default.LbpThreshold );

            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();
            var camToStart = this._configManager.GetCameras();

            if (camToStart.Count == 1)
            {
                var single = this._configManager.GetCameras().Single();
                this.StartCameraInternal(single);
            }

        }

        private void InitializeHandlers()
        {
            foreach (var portraitHandler in _handlers)
            {
                portraitHandler.Initialize();
                portraitHandler.Start();
            }
        }

        void _comparer_PersonOfInterestDected(object sender, MiscUtil.EventArgs<PersonOfInterestDetectionResult> e)
        {
            this._mainForm.ShowSuspects(e.Value);
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
                    var camController = SearchLineBuilder.BuildNewSearchLine(cam);

                    RegisterHandlers(camController);

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

        private void RegisterHandlers(FaceSearchController camController)
        {
            foreach (var h in _handlers)
            {
            }
        }


        private RealtimeDisplay.MainForm _mainForm;
        private ConfigurationManager _configManager;
        private readonly IRepository _repository;
        private readonly IEnumerable<IPortraitHandler> _handlers;
        private readonly FaceComparer _comparer;
        private Damany.Imaging.Processors.FaceSearchController _currentController;
    }
}
