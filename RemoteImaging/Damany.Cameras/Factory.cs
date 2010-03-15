using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;

namespace Damany.Cameras
{
    public static class Factory
    {
        public static Damany.Imaging.Contracts.IFrameStream NewFrameStream(Uri uri, string cameraType)
        {
            IFrameStream source = null;

            if (cameraType.ToUpper().Contains("AIP"))
            {
                var aip = new Damany.Cameras.Wrappers.AipStarCamera(uri.Host, uri.Port, "", "");
                aip.UserName = "system";
                aip.PassWord = "system";
                source = aip;
            }
            else if (cameraType.ToUpper().Contains("SANYO"))
            {
                var sanyo = new SanyoNetCamera ();
                sanyo.Uri = uri;
                sanyo.UserName = "guest";
                sanyo.PassWord = "guest";
                source = sanyo;
            }
            else if (cameraType.ToUpper().Contains("DIR"))
            {
                var dir = new Damany.Cameras.DirectoryFilesCamera(uri.LocalPath, "*.jpg");
                source = dir;
            }
            else
                throw new NotSupportedException("camera type not supported");

            return source;
        }

    }
}
