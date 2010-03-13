using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Cameras
{
    public static class Factory
    {
        public static Damany.Imaging.Contracts.IFrameStream NewSanyoCamera(Uri uri)
        {
            var camera = new SanyoNetCamera();
            camera.Uri = uri;
            return camera;
        }

        public static Damany.Imaging.Contracts.IFrameStream NewAipStarCamera(Uri uri)
        {
            var aipstar = new Damany.Cameras.Wrappers.AipStarCamera(uri.Host, uri.Port, "", "");
            return aipstar;
        }
    }
}
