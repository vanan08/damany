using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Contracts;

namespace Damany.Cameras
{
    public static class Factory
    {
        public static Damany.Imaging.Contracts.IFrameStream NewFrameStream(Damany.PC.Domain.CameraInfo cameraInfo)
        {
            IFrameStream source = null;

            switch (cameraInfo.Provider)
            {
                case Damany.PC.Domain.CameraProvider.LocalDirectory:
                    var dir = new Damany.Cameras.DirectoryFilesCamera(cameraInfo.Location.LocalPath, "*.jpg");
                    source = dir;
                    break;
                case Damany.PC.Domain.CameraProvider.Sanyo:
                    var sanyo = new SanyoNetCamera();
                    sanyo.Uri = cameraInfo.Location;
                    sanyo.UserName = "guest";
                    sanyo.PassWord = "guest";
                    source = sanyo;
                    break;
                case Damany.PC.Domain.CameraProvider.AipStar:
                    var aip = new Damany.Cameras.Wrappers.AipStarCamera(cameraInfo.Location.Host, cameraInfo.Location.Port, "", "");
                    aip.UserName = "system";
                    aip.PassWord = "system";
                    source = aip;
                    break;
                default:
                    throw new NotSupportedException("camera type not supported");

                    break;
            }


            source.Id = cameraInfo.Id;

            return source;
        }

    }
}
