using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AForge.Video;
using OpenCvSharp;

namespace Damany.Cameras
{

    public class DirectoryFilesCamera : AForge.Video.IVideoSource
    {
        private string directory;
        private string imagePattern;
        private string[] imageFiles;
        private int current;
        private bool _initialized;
        private Task _task;
        private CancellationTokenSource _cancellationSource;



        public event NewFrameEventHandler NewFrame;
        public event VideoSourceErrorEventHandler VideoSourceError;
        public event PlayingFinishedEventHandler PlayingFinished;



        public int FrameIntervalMs { get; set; }


        public DirectoryFilesCamera(string directory, string imagePattern)
        {
            if (String.IsNullOrEmpty(directory))
                throw new ArgumentException("directory is null or empty.", "directory");

            if (!System.IO.Directory.Exists(directory))
                throw new System.IO.DirectoryNotFoundException(GetDirectoryStringForDisplay(directory) + " doesn't exist");

            this.imagePattern = imagePattern ?? "*.*";


            this.directory = directory;
            this.Repeat = true;

            FrameIntervalMs = 2000;
        }

        public void Initialize()
        {
            if (!_initialized)
            {
                this.imageFiles = System.IO.Directory.GetFiles(this.directory, this.imagePattern);
                if (this.imageFiles.Length == 0)
                {
                    throw new InvalidOperationException(GetDirectoryStringForDisplay(this.directory) + " is empty");
                }

                _initialized = true;
            }
        }

        public System.Drawing.Bitmap RetrieveFrame()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("not initialized");
            }

            var img = AForge.Imaging.Image.FromFile(this.imageFiles[current++]);

            System.Diagnostics.Debug.WriteLine("read file: " + this.imageFiles[current - 1]);

            if (this.Repeat)
            {
                this.current = this.current % this.imageFiles.Length;
            }

            return img;
        }

        public int Id { get; set; }


        public bool Repeat { get; set; }


        public void Start()
        {
            if (_task == null)
            {
                _cancellationSource = new System.Threading.CancellationTokenSource();
                var token = _cancellationSource.Token;

                _task = Task.Factory.StartNew(() =>
                                                  {
                                                      Initialize();

                                                      while (true)
                                                      {
                                                          if (token.IsCancellationRequested) break;

                                                          var bmp = this.RetrieveFrame();

                                                          this.InvokeNewFrame(new NewFrameEventArgs(bmp));

                                                          Thread.Sleep(FrameIntervalMs);
                                                      }
                                                  }, _cancellationSource.Token);
            }

        }

        public void SignalToStop()
        {
            if (_cancellationSource != null)
            {
                _cancellationSource.Cancel();
            }
        }

        public void WaitForStop()
        {
            if (_task != null)
            {
                try
                {
                    _task.Wait();
                }
                catch (AggregateException)
                {
                }

            }
        }

        public void Stop()
        {

        }

        public string Source
        {
            get { return directory; }
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
            get { return false; }
        }




        private static string GetDirectoryStringForDisplay(string directory)
        {
            return "\"" + directory + "\"";
        }

        public void InvokeNewFrame(NewFrameEventArgs eventargs)
        {
            NewFrameEventHandler handler = NewFrame;
            if (handler != null) handler(this, eventargs);
        }

        public void InvokeVideoSourceError(VideoSourceErrorEventArgs eventargs)
        {
            VideoSourceErrorEventHandler handler = VideoSourceError;
            if (handler != null) handler(this, eventargs);
        }

        public void InvokePlayingFinished(ReasonToFinishPlaying reason)
        {
            PlayingFinishedEventHandler handler = PlayingFinished;
            if (handler != null) handler(this, reason);
        }



    }
}
