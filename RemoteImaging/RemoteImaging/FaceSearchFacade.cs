using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video;
using Damany.Imaging.Common;
using Damany.Imaging.Processors;
using Damany.PC.Domain;
using Damany.Cameras;

namespace RemoteImaging
{
    public class FaceSearchFacade
    {
        private readonly MotionDetector _motionDetector;
        private readonly PortraitFinder _portraitFinder;
        private readonly IEnumerable<IFacePostFilter> _facePostFilters;
        private AForge.Video.IVideoSource _jpegStream;

        public FaceSearchFacade(Damany.Imaging.Processors.MotionDetector motionDetector,
                                 Damany.Imaging.Processors.PortraitFinder portraitFinder,
                                 IEnumerable<Damany.Imaging.Common.IFacePostFilter> facePostFilters)
        {
            _motionDetector = motionDetector;
            _portraitFinder = portraitFinder;
            _facePostFilters = facePostFilters;
        }


        public void StartWith(Damany.PC.Domain.CameraInfo cameraInfo)
        {
            if (_jpegStream == null)
            {
                switch (cameraInfo.Provider)
                {
                    case CameraProvider.LocalDirectory:
                        var dir = new Damany.Cameras.DirectoryFilesCamera(cameraInfo.Location.LocalPath, "*.jpg");
                        dir.Repeat = true;
                        _jpegStream = dir;
                        break;
                    case CameraProvider.Sanyo:
                        var sanyo = new JPEGExtendStream(cameraInfo.Location.ToString());
                        sanyo.Login = "guest";
                        sanyo.Password = "guest";
                        sanyo.FrameInterval = 500;

                        sanyo.RequireCookie = cameraInfo.Provider == CameraProvider.Sanyo;
                        _portraitFinder.PostFilters = _facePostFilters;

                        _jpegStream = sanyo;
                        break;
                    case CameraProvider.AipStar:
                        throw new NotSupportedException();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _jpegStream.NewFrame += JpegStreamNewFrame;
                _jpegStream.Start();
            }
        }

        public void SignalToStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.SignalToStop();
            }
        }

        public void WaitForStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.WaitForStop();
            }
        }

        void JpegStreamNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

            var ipl = OpenCvSharp.IplImage.FromBitmap(bmp);
            bmp.Dispose();

            var frame = new Frame(ipl);
            var grouped = _motionDetector.ProcessFrame(frame);

            if (grouped)
            {
                var motionFrames = _motionDetector.GetMotionFrames();
                _portraitFinder.ProcessFrames(motionFrames);
            }
        }
    }
}
