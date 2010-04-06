using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PC.Domain;

namespace RemoteImaging
{
    public class MainController
    {
        public MainController(RealtimeDisplay.MainForm mainForm, 
            Damany.RemoteImaging.Common.ConfigurationManager configManager)
        {
            this._mainForm = mainForm;
            this._configManager = configManager;
            
        }

        public void Start()
        {
            this._mainForm.Cameras = this._configManager.GetCameras().ToArray();


            var camToStart = this._configManager.GetCameras();

            if (camToStart.Count == 1)
            {
                var single = this._configManager.GetCameras().Single();

                System.Threading.WaitCallback action = delegate
                                    {
                                        var camController = Damany.RemoteImaging.Common.SearchLineBuilder.BuildNewSearchLine(single);
                                        camController.RegisterPortraitHandler(this._mainForm);

                                        camController.Start();
                                        if (single.Provider == CameraProvider.Sanyo)
                                        {
                                            this._mainForm.StartRecord(single);
                                        }
                                    };
                System.Threading.ThreadPool.QueueUserWorkItem(action);
            }
            
        }


        private RealtimeDisplay.MainForm _mainForm;
        private Damany.RemoteImaging.Common.ConfigurationManager _configManager;
    }
}
