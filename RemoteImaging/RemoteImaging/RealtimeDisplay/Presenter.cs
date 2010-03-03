using System;
using System.Collections.Generic;
using System.IO;
using ImageProcess;
using RemoteImaging.Core;
using Damany.Component;
using System.Drawing;
using OpenCvSharp;
using System.Threading;
using System.Diagnostics;
using System.Text;
using RemoteImaging.ImportPersonCompare;
using System.Linq;
using SuspectsRepository;
using System.Net.Sockets;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using FaceProcessingWrapper;


namespace RemoteImaging.RealtimeDisplay
{

    public class Presenter : IImageScreenObserver
    {
        private const string strTip = "提示信息";
        private TcpListener liveServer;
        IImageScreen screen;
        ICamera camera;
        System.ComponentModel.BackgroundWorker worker;

        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer reconnectTimer = new System.Timers.Timer();
        System.Timers.Timer videoFileCheckTimer = new System.Timers.Timer();

        Queue<Frame[]> framesArrayQueue = new Queue<Frame[]>();
        Queue<Frame> motionFrames = new Queue<Frame>();
        Queue<Frame> rawFrames = new Queue<Frame>();

        object framesArrayQueueLocker = new object();
        object rawFrameLocker = new object();
        object bgLocker = new object();

        AutoResetEvent goSearch = new AutoResetEvent(false);
        AutoResetEvent goDetectMotion = new AutoResetEvent(false);

        Thread motionDetectThread = null;

        SVM svm;
        PCA pca;
        FrontFaceChecker frontChecker;
        SuspectsRepository.SuspectsRepositoryManager suspectsMnger;

        System.Diagnostics.PerformanceCounter memCounter =
            Damany.Util.PerformanceCounterFactory.CreateMemoryCounter();

        private IplImage _BackGround;
        public IplImage BackGround
        {
            get
            {
                return _BackGround;
            }
            set
            {
                _BackGround = value;
                value.IsEnabledDispose = false;
            }
        }

        private void UpdateBGInternal(object sender, ImageCapturedEventArgs args)
        {
            this.ImageCaptured -= this.UpdateBGInternal;

            IplImage oldIpl = this.BackGround;

            lock (this.bgLocker)
                this.BackGround = BitmapConverter.ToIplImage((Bitmap)args.ImageCaptured);

            args.ImageCaptured.Save("BG.jpg");

            oldIpl.IsEnabledDispose = true;
            oldIpl.Dispose();
        }

        public void UpdateBG()
        {
            this.ImageCaptured += this.UpdateBGInternal;

        }


        public void RemoteListener(LiveServer s)
        {
            if (this.ImageCaptured != null)
            {
                //this.ImageCaptured -= s.ImageCaptured;
            }
        }



        /// <summary>
        /// Initializes a new instance of the Presenter class.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="uploadWatcher"></param>
        public Presenter(IImageScreen screen,
            ICamera camera)
        {
            this.screen = screen;
            this.camera = camera;

#if DEBUG
            Properties.Settings.Default.SearchSuspecious = true;
#endif


            if (Properties.Settings.Default.SearchSuspecious)
            {
                this.svm =
                    SVM.LoadFrom(Properties.Settings.Default.ImageRepositoryDirectory);
                this.pca =
                    PCA.LoadFrom(Properties.Settings.Default.ImageRepositoryDirectory);
                this.frontChecker =
                    FrontFaceChecker.FromFile(Properties.Settings.Default.FrontFaceTemplateFile);

                this.suspectsMnger = SuspectsRepository.SuspectsRepositoryManager.LoadFrom( Properties.Settings.Default.ImageRepositoryDirectory );
            }


            this.InitializeTrayIcon();


            motionDetectThread =
                Properties.Settings.Default.DetectMotion ?
                new Thread(this.DetectMotion) : new Thread(this.BypassDetectMotion);
            motionDetectThread.IsBackground = true;
            motionDetectThread.Name = "motion detect";


            this.screen.Observer = this;
            this.worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;

            this.timer.Interval = 1000 / float.Parse(Properties.Settings.Default.FPs);
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            this.reconnectTimer.Interval = 5000;
            this.reconnectTimer.Elapsed += new System.Timers.ElapsedEventHandler(reconnectTimer_Elapsed);

            videoFileCheckTimer.Interval = 1000 * 60;
            videoFileCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(videoFileCheckTimer_Elapsed);

            if (File.Exists("bg.jpg"))
                BackGround = OpenCvSharp.IplImage.FromFile(@"bg.jpg");

            new Service.ServiceProvider(Program.motionDetector, Program.faceSearch, this, camera).OpenServices();
        }


        private void NotifyUserError(string msg, string title)
        {
            NotifyUser(msg, title, ToolTipIcon.Error);
        }

        private void NotifyUserInfo(string msg, string title)
        {
            NotifyUser(msg, title, ToolTipIcon.Info);
        }

        private void NotifyUser(string msg, string title, ToolTipIcon icon)
        {
            this.notifyIcon.Visible = true;
            this.notifyIcon.ShowBalloonTip(3000,
                title, msg,
                icon);
        }

        void reconnectTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.reconnectTimer.Enabled = false;
            try
            {
                this.camera.Connect();
                NotifyUserInfo("重新连接摄像头成功！", strTip);
                System.Threading.Thread.Sleep(3000);
                this.notifyIcon.Visible = false;
                this.timer.Enabled = true;

            }
            catch (System.Net.WebException)
            {
                NotifyUserError("重新连接摄像头失败！系统将继续尝试。", strTip);
                System.Diagnostics.Debug.WriteLine("重连失败");
                this.reconnectTimer.Enabled = true;
                this.timer.Enabled = false;
            }

        }

        void videoFileCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Properties.Settings.Default.KeepMotionLessVideo) return;

            DateTime time = DateTime.Now.AddMinutes(-2);

            if (!FileSystemStorage.MotionImagesCapturedWhen(2, time))
                FileSystemStorage.DeleteVideoFileAt(time);


        }

        public void StartServer(object serverPort)
        {
            if (liveServer == null)
            {
                liveServer = new TcpListener((int)serverPort);
                liveServer.Start(1);
            }

            while (true)
            {
                TcpClient client = liveServer.AcceptTcpClient();

                System.Diagnostics.Debug.WriteLine("accept connection:" + client.Client.RemoteEndPoint);

                LiveServer ls = new LiveServer(client, this);
                ls.Start();

            }
        }

        System.Windows.Forms.NotifyIcon notifyIcon;

        private void InitializeTrayIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Icon = new System.Drawing.Icon("mainicon.ico");
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.timer.Enabled = false;
                this.QueryRawFrame();
                this.timer.Enabled = true;

            }
            catch (System.Net.WebException)
            {
                this.timer.Enabled = false;

                NotifyUserError("连接中断, 系统将自动重新连接摄像头。", strTip);
                System.Diagnostics.Debug.WriteLine("连接中断");

                this.reconnectTimer.Enabled = true;
            }

        }


        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (Properties.Settings.Default.SearchFace)
            {
                this.SearchFace();
            }
        }

        public event EventHandler<ImageCapturedEventArgs> ImageCaptured;
        int historyFramesQueueLength = 0;

        private bool cpuOverLoaded()
        {
            var memMB = memCounter.NextValue();
            return memMB <= 500.0f;
        }

        private void FireImageCapturedEvent(Bitmap bmp)
        {
            if (ImageCaptured != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs() { ImageCaptured = bmp };
                System.Diagnostics.Debug.WriteLine("fire ImageCaptured event");
                ImageCaptured(this, args);
            }
        }

        private void QueryRawFrame()
        {

            byte[] image = camera.CaptureImageBytes();

            Bitmap bmp = null;
            try
            {
                MemoryStream memStream = new MemoryStream(image);
                bmp = (Bitmap)Image.FromStream(memStream);
            }
            catch (System.ArgumentException ex)//图片格式出错
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionHandlingWrap);
                if (rethrow)
                {
                    throw;
                }
            }


            FireImageCapturedEvent(bmp);

            if (this.cpuOverLoaded())
            {
                Frame[][] frameArrayToDispose = null;
                lock (this.framesArrayQueueLocker)
                {
                    frameArrayToDispose = this.framesArrayQueue.ToArray();
                    this.framesArrayQueue.Clear();
                }

                Array.ForEach(frameArrayToDispose, ar =>
                {
                    Array.ForEach(ar, fr =>
                    {
                        fr.image.IsEnabledDispose = true;
                        fr.image.Dispose();
                    });
                });

                return;
            }

            Frame f = new Frame();
            f.timeStamp = DateTime.Now.Ticks;
            f.cameraID = 2;

            IplImage ipl = BitmapConverter.ToIplImage(bmp);
            ipl.IsEnabledDispose = false;
            f.image = ipl;


            lock (this.rawFrameLocker) rawFrames.Enqueue(f);

            goDetectMotion.Set();
        }


        private Frame GetNewFrame()
        {
            Frame newFrame = null;

            lock (this.rawFrameLocker)
            {
                if (rawFrames.Count > 0)
                {
                    newFrame = rawFrames.Dequeue();
                }
            }
            return newFrame;
        }


        private static bool IsStaticFrame(Frame aFrame)
        {
            return aFrame.image == null ||
                (aFrame.searchRect.Width == 0 || aFrame.searchRect.Height == 0);
        }

        private void DetectMotion()
        {
            int count = 0;
            while (true)
            {
                Frame nextFrame = GetNewFrame();
                if (nextFrame != null)
                {
                    Frame lastFrame = new Frame();

                    bool groupCaptured = Program.motionDetector.DetectFrame(nextFrame, lastFrame);


                    if (IsStaticFrame(lastFrame))
                    {
                        if (lastFrame.image != null)
                        {
                            lastFrame.image.IsEnabledDispose = true;
                            lastFrame.image.Dispose();
                        }

                    }
                    else
                    {
                        try
                        {
                            FileSystemStorage.SaveFrame(lastFrame);
                        }
                        catch (System.IO.IOException ex)
                        {
                            bool rethrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionHandlingLogging);
                            if (rethrow)
                            {
                                throw;
                            }
                        }

                        motionFrames.Enqueue(lastFrame);
                    }

                    if (groupCaptured)
                    {
                        Frame[] frames = motionFrames.ToArray();
                        motionFrames.Clear();

                        if (frames.Length <= 0) continue;

                        lock (framesArrayQueueLocker) framesArrayQueue.Enqueue(frames);

                        goSearch.Set();

                    }

                }
                else
                    goDetectMotion.WaitOne();

            }
        }

        void BypassDetectMotion()
        {
            while (true)
            {
                Frame f = this.GetNewFrame();

                if (f != null && f.image != null)
                {
                    IplImage ipl = f.image;
                    ipl.IsEnabledDispose = false;
                    f.searchRect.Width = ipl.Width;
                    f.searchRect.Height = ipl.Height;

                    motionFrames.Enqueue(f);
                    FileSystemStorage.SaveFrame(f);

                    if (motionFrames.Count == 6)
                    {
                        Frame[] frames = motionFrames.ToArray();
                        motionFrames.Clear();
                        lock (framesArrayQueueLocker) framesArrayQueue.Enqueue(frames);
                        goSearch.Set();
                    }
                }
                else
                    goDetectMotion.WaitOne();

            }

        }

        public void Start()
        {

            this.timer.Enabled = false;
            this.timer.Enabled = true;

            videoFileCheckTimer.Enabled = true;


            if (!motionDetectThread.IsAlive)
            {
                motionDetectThread.Start();
            }

            if (!this.worker.IsBusy)
            {
                this.worker.RunWorkerAsync();
            }

            if (this.liveServer == null)
                ThreadPool.QueueUserWorkItem(this.StartServer, 20000);



        }

        private string PrepareDestFolder(ImageDetail imgToProcess)
        {
            string parentOfBigPicFolder = Directory.GetParent(imgToProcess.ContainedBy).FullName;
            string destFolder = Path.Combine(parentOfBigPicFolder, Properties.Settings.Default.IconDirectoryName);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            return destFolder;
        }


        ImageDetail[] SaveImage(Target[] targets)
        {
            IList<ImageDetail> imgs = new List<ImageDetail>();

            foreach (Target t in targets)
            {
                Frame frame = t.BaseFrame;

                for (int j = 0; j < t.Faces.Length; ++j)
                {
                    string facePath = FileSystemStorage.PathForFaceImage(frame, j);
                    try
                    {
                        t.Faces[j].SaveImage(facePath);
                    }
                    catch (System.IO.IOException ex)
                    {
                        bool rethrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionHandlingLogging);
                        if (rethrow)
                        {
                            throw;
                        }
                    }

                    imgs.Add(ImageDetail.FromPath(facePath));
                }

            }

            ImageDetail[] details = new ImageDetail[imgs.Count];
            imgs.CopyTo(details, 0);

            return details;

        }


        void SearchFace()
        {
            while (true)
            {
                Frame[] frames = null;
                lock (framesArrayQueueLocker)
                {
                    if (framesArrayQueue.Count > 0)
                    {
                        frames = framesArrayQueue.Dequeue();
                    }
                }

                if (frames != null && frames.Length > 0)
                {
                    foreach (var f in frames)
                    {
                        Program.faceSearch.AddInFrame(f);
                    }

                    ImageProcess.Target[] targets = Program.faceSearch.SearchFaces();

                    ImageDetail[] imgs = this.SaveImage(targets);
                    this.screen.ShowImages(imgs);

                    if (Properties.Settings.Default.SearchSuspecious) DetectSuspecious(targets);

                    Array.ForEach(frames, f => { IntPtr cvPtr = f.image.CvPtr; OpenCvSharp.Cv.Release(ref cvPtr); f.image.Dispose(); });
                    Array.ForEach(targets, t =>
                    {
                        Array.ForEach(t.Faces, ipl => { ipl.IsEnabledDispose = true; ipl.Dispose(); });
                        t.BaseFrame.image.Dispose();
                    });
                }
                else
                    goSearch.WaitOne();

            }
        }


        private bool IsGoodGuy(float[] imgData)
        {

            double verdict = this.svm.Predict(imgData);

            System.Diagnostics.Debug.WriteLine(string.Format("=======verdict: {0}=======", verdict));

            return verdict == -1;
        }


        private void DetectSuspecious(Target[] targets)
        {
            foreach (var t in targets)
            {
                for (int i = 0; i < t.Faces.Length; ++i)
                {
                    IplImage normalized = Program.faceSearch.NormalizeImage(t.BaseFrame.image, t.FacesRectsForCompare[i]);

#if DEBUG
                    var bmp = BitmapConverter.ToBitmap(normalized);
#endif

                    if (!this.frontChecker.IsFront(normalized))
                    {
                        Debug.WriteLine("is not front face");

                        continue;
                    }
                    else
                    {
                        Debug.WriteLine("is front");
                    }


                    float[] imgData = NativeIconExtractor.ResizeIplTo(normalized, 100, 100);

                    if (IsGoodGuy(imgData))
                    {
                        Debug.WriteLine("is good guy");
                        continue;
                    }

                    var pcaRecognizeResult = this.pca.Recognize(imgData);

                    var filtered =
                        Array.FindAll(pcaRecognizeResult, r => r.Similarity > 0.85);

                    if (filtered.Length == 0) continue;

                    int j = 0;

                    IList<ImportantPersonDetail> details =
                        new List<ImportantPersonDetail>();

                    foreach (PersonInfo p in this.suspectsMnger.Peoples)
                    {
                        foreach (var result in filtered)
                        {
                            string fileName = System.IO.Path.GetFileName(this.pca.GetFileName(result.FileIndex));

                            int idx = fileName.IndexOf('_');
                            fileName = fileName.Remove(idx, 5);

                            if (p.FileName.Contains(fileName))
                            {
                                details.Add(new ImportantPersonDetail(p,
                                    new FaceRecognition.RecognizeResult { FileName = fileName, Similarity = result.Similarity }));
                            }
                        }
                    }

                    ImportantPersonDetail[] distinct = details.Distinct(new ImportantPersonComparer()).ToArray();

                    if (distinct.Length == 0) continue;

                    screen.ShowSuspects(distinct, t.Faces[i].ToBitmap());

                }
            }
        }

        public bool FaceRecognize { get; set; }

        #region IImageScreenObserver Members

        public void SelectedCameraChanged()
        {
            throw new NotImplementedException();
        }



        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.Path))
            {
                string bigPicPathName = FileSystemStorage.BigImgPathForFace(img);
                ImageDetail bigImageDetail = ImageDetail.FromPath(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }


    public class ImportantPersonComparer : System.Collections.Generic.IEqualityComparer<ImportantPersonDetail>
    {


        #region IEqualityComparer<ImportantPersonDetail> Members

        public bool Equals(ImportantPersonDetail x, ImportantPersonDetail y)
        {
            return string.Compare(x.Info.FileName, y.Info.FileName, true) == 0;
        }

        public int GetHashCode(ImportantPersonDetail obj)
        {
            return obj.Info.FileName.GetHashCode();
        }

        #endregion
    }

}
