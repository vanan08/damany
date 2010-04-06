using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PC.Domain;

namespace RemoteImaging
{
    public class MainController
    {
        public MainController(RealtimeDisplay.MainForm mainForm, Damany.RemoteImaging.Common.ConfigurationManager configManager)
        {
            this._mainForm = mainForm;
            this._configManager = configManager;
            
        }

        public void Start()
        {
            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();

            var camToStart = this._configManager.GetCameras().SingleOrDefault();

            if (camToStart != null)
            {

                Action action = delegate
                                    {
                                        var camController = Damany.RemoteImaging.Common.SearchLineBuilder.BuildNewSearchLine(camToStart);
                                        camController.RegisterPortraitHandler(this._mainForm);

                                        camController.Start();
                                        if (camToStart.Provider == CameraProvider.Sanyo)
                                        {
                                            this._mainForm.StartRecord(camToStart);
                                        }
                                    };
                action.BeginInvoke(null, null);
            }
            
        }


        private RealtimeDisplay.MainForm _mainForm;
        private Damany.RemoteImaging.Common.ConfigurationManager _configManager;
    }
}
