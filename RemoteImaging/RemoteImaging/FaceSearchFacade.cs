using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AForge.Video;
using Damany.Imaging.Common;
using Damany.Imaging.Processors;
using Damany.PC.Domain;
using Damany.Cameras;
using Damany.PortraitCapturer.DAL;

namespace RemoteImaging
{
    public class FaceSearchFacade
    {
        private readonly MotionDetector _motionDetector;
        private readonly PortraitFinder _portraitFinder;
        private readonly IEnumerable<IFacePostFilter> _facePostFilters;
        private readonly IRepository _repository;
        private readonly IEventAggregator _eventAggregator;
        private AForge.Video.IVideoSource _jpegStream;
        private readonly ConcurrentQueue<List<Frame>> _motionFramesQueue = new ConcurrentQueue<List<Frame>>();
        private System.Threading.Thread _faceSearchThread;
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        private Damany.PC.Domain.CameraInfo _cameraInfo;
        private bool _run;


        public int MotionQueueSize { get; set; }

        public FaceSearchFacade(Damany.Imaging.Processors.MotionDetector motionDetector,
                                Damany.Imaging.Processors.PortraitFinder portraitFinder,
                                IEnumerable<Damany.Imaging.Common.IFacePostFilter> facePostFilters,
                                IRepository repository,
                                IEventAggregator eventAggregator)
        {
            _motionDetector = motionDetector;
            _portraitFinder = portraitFinder;
            _facePostFilters = facePostFilters;
            _repository = repository;
            _eventAggregator = eventAggregator;

            MotionQueueSize = 10;
            _run = true;

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

                _cameraInfo = cameraInfo;

                _jpegStream.NewFrame += JpegStreamNewFrame;
                _jpegStream.Start();

                if (_faceSearchThread == null)
                {
                    _faceSearchThread = new Thread(FaceSearchWorkerThread);
                    _faceSearchThread.IsBackground = true;
                    _faceSearchThread.Start();
                }




            }
        }

        public void SignalToStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.SignalToStop();

                _run = false;
                _signal.Set();

            }
        }

        public void WaitForStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.WaitForStop();
            }

            if (_faceSearchThread != null)
            {
                _faceSearchThread.Join();
            }

            foreach (List<Frame> frames in _motionFramesQueue)
            {
                frames.ForEach(f =>
                                   {
                                       f.Dispose();
                                       f = null;
                                   });
            }
        }

        void JpegStreamNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (_motionFramesQueue.Count > MotionQueueSize)
            {
                return;
            }

            var bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

            var ipl = OpenCvSharp.IplImage.FromBitmap(bmp);
            bmp.Dispose();

            var frame = new Frame(ipl);
            var grouped = _motionDetector.ProcessFrame(frame);

            if (grouped)
            {
                var motionFrames = _motionDetector.GetMotionFrames();

                foreach (var motionFrame in motionFrames)
                {
                    var source = new MockFrameSource();
                    source.Id = _cameraInfo.Id;
                    motionFrame.CapturedFrom = source;
                }

                motionFrames.ForEach(f => _repository.SaveFrame(f));

                _motionFramesQueue.Enqueue(motionFrames);
                _signal.Set();
            }
        }

        void FaceSearchWorkerThread()
        {
            while (_run)
            {
                List<Frame> frames = null;
                if (_motionFramesQueue.TryDequeue(out frames))
                {
                    var portraits = _portraitFinder.ProcessFrames(frames);

                    if (_repository != null)
                    {
                        portraits.ForEach(p => _repository.SavePortrait(p));
                    }

                    if (_eventAggregator != null)
                    {
                        portraits.ForEach(p =>
                                              {
                                                  _eventAggregator.Publish(p);
                                                  p.Dispose();
                                              });

                    }

                }
                else
                {
                    _signal.WaitOne();
                }
            }
        }
    }
}
