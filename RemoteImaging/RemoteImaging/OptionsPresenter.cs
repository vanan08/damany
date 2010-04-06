using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.RemoteImaging.Common;

namespace RemoteImaging
{
    public class OptionsPresenter
    {
        public OptionsPresenter(ConfigurationManager manager, OptionsForm form )
        {
            this._manager = manager;
            this._form = form;
            
        }

        public void Start()
        {
            this._form.Cameras = _manager.GetCameras();
            this._form.AttachPresenter(this);
            this._form.ShowDialog();
        }

        public void UpdateConfig()
        {

            this._manager.ClearCameras();

            foreach (var cameraInfo in this._form.Cameras)
            {
                this._manager.AddCamera(cameraInfo);
            }
            
        }

        private ConfigurationManager _manager;
        private OptionsForm _form;
    }
}
