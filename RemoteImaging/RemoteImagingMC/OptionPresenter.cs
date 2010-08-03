using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.RemoteImaging.Common;
using Damany.PC.Domain;

namespace RemoteImaging
{
    public class OptionPresenter
    {
        public OptionPresenter(ConfigurationManager manager, OptionsForm form)
        {
            this.manager = manager;
            this.form = form;

        }

        public void Start()
        {
            this.form.cameraConfigurer1.Cameras = this.manager.GetCameras();

        }


        public void AddCamera(CameraInfo cam)
        {
            this.manager.AddCamera(cam);
            this.form.cameraConfigurer1.Cameras = this.manager.GetCameras();
        }

        public void DeleteCamera(CameraInfo cam)
        {
            this.manager.DeleteCamera(cam);
            this.form.cameraConfigurer1.Cameras = this.manager.GetCameras();
        }

        ConfigurationManager manager;
        OptionsForm form;
    }
}
