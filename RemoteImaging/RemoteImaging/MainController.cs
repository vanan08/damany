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
            configManager.ConfigurationChanged += delegate
                                                      {
                                                          var cams = configManager.GetCameras();
                                                          mainForm.Cameras = cams.ToArray();
                                                      };

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
            var camsToStart = this._configManager.GetCameras();

            for (int i = 0; i < Math.Min(_mainForm.PipCount, camsToStart.Count); i++)
            {
                StartCameraInternal(camsToStart[i]);
            }

            _mainForm.InitPips();

        }

        public void SelectLiveCamera()
        {
            
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

        public void StartCamera(CameraInfo cam)
        {
            if (cam == null)
            {
                return;
            }

            if (HasStarted(cam))
            {
                camReferences[cam]++;
                return;
            }


            this.StartCameraInternal(cam);
        }

        public void StopCamera(CameraInfo cam)
        {
            if (camReferences.ContainsKey(cam))
            {
                camReferences[cam]--;
            }
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
                    camController.Worker.OnWorkItemIsDone += new Action<object>(Worker_OnWorkItemIsDone);
                    camController.Start();

                    if (!camReferences.ContainsKey(cam))
                    {
                        camReferences.Add(cam, 1);
                    }
                    else
                    {
                        camReferences[cam]++;
                        
                    }

                    
                    controllers[cam] = camController;
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

        void Worker_OnWorkItemIsDone(object obj)
        {
            Frame f = obj as Frame;

            if (f != null)
            {
                _mainForm.ShowFrame(f);
            }
        }

        private void RegisterHandlers(FaceSearchController camController)
        {
            foreach (var h in _handlers)
            {
                camController.RegisterPortraitHandler(h);
            }
        }

        private bool HasReference(CameraInfo cam)
        {
            if (camReferences.ContainsKey(cam))
            {
                return false;
            }

            return camReferences[cam] != 0;
        }

        private bool HasStarted(CameraInfo cam)
        {
            return controllers.ContainsKey(cam);
        }


        private RealtimeDisplay.MainForm _mainForm;
        private ConfigurationManager _configManager;
        private readonly IRepository _repository;
        private readonly IEnumerable<IPortraitHandler> _handlers;
        private readonly FaceComparer _comparer;

        private Dictionary<CameraInfo, int> camReferences = new Dictionary<CameraInfo, int>();

        private Dictionary<CameraInfo, FaceSearchController> controllers =
            new Dictionary<CameraInfo, FaceSearchController>();
    }
}
