using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using AForge.Video;

namespace Damany.Cameras
{
    public class FoKoShVideoSourceAdapter : IVideoSource
    {
        private System.Threading.Thread _worker;
        private bool _exit;


        public FoKoShCamera Camera;
        public int FrameInterval;


        public event VideoSourceErrorEventHandler VideoSourceError;
        public event PlayingFinishedEventHandler PlayingFinished;
        public event NewFrameEventHandler NewFrame;

        public FoKoShVideoSourceAdapter(IntPtr hwnd)
        {
            Camera = new FoKoShCamera(hwnd);
        }


        public void Start()
        {
            if (!Camera.Started)
            {
                Camera.Start();
            }

            if (_worker == null)
            {
                _worker = new Thread(this.WorkerThread);
                _worker.IsBackground = true;
                _worker.Start();
            }

        }

        public void SignalToStop()
        {
            _exit = true;
        }

        public void WaitForStop()
        {
            if (_worker != null)
            {
                _worker.Join();
            }
        }

        public void Stop()
        {
            _exit = true;
        }

        public string Source
        {
            get { return Camera.Ip + ":" + Camera.Port; }
        }

        public int FramesReceived
        {
            get { return 0; }
        }

        public int BytesReceived
        {
            get { return 0; }
        }

        public bool IsRunning
        {
            get { return Camera.Started; }
        }


        public void FireNewFrameEvent(NewFrameEventArgs eventargs)
        {
            NewFrameEventHandler handler = NewFrame;
            if (handler != null) handler(this, eventargs);
        }



        private void WorkerThread()
        {
            while (!_exit)
            {
                if (Camera != null && Camera.Started)
                {
                    var img = Camera.CaptureImage();
                    var arg = new NewFrameEventArgs((Bitmap)img);
                    FireNewFrameEvent(arg);
                }

                Thread.Sleep(FrameInterval);
            }


        }
    }
}
