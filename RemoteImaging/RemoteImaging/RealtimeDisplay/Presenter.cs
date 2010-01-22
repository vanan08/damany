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
        object camLocker = new object();

        AutoResetEvent goSearch = new AutoResetEvent(false);
        AutoResetEvent goDetectMotion = new AutoResetEvent(false);

        Thread motionDetectThread = null;

        FaceSVMWrapper.SVM svm;

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

        public void UpdateBG()
        {
            try
            {
                byte[] imgData = null;
                lock (this.camLocker)
                    imgData = this.camera.CaptureImageBytes();

                Image img = Image.FromStream(new MemoryStream(imgData));

                lock (this.bgLocker)
                    this.BackGround = BitmapConverter.ToIplImage((Bitmap)img);

                img.Save("BG.jpg");
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("获取背景图片错误，请重试！");
                return;
            }
            catch(System.ArgumentException)
            {
                MessageBox.Show("获取背景图片错误，请重试！");
                return;
            }
            
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

            this.svm = FaceSVMWrapper.SVM.LoadFrom(Properties.Settings.Default.ImageRepositoryDirectory);

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

            this.timer.Interval = 1000 / int.Parse(Properties.Settings.Default.FPs);
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
                System.Diagnostics.Debug.WriteLine("capture frame begin");
                this.timer.Enabled = false;
                this.QueryRawFrame();
                System.Diagnostics.Debug.WriteLine("capture frame after");
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
            lock (this.framesArrayQueueLocker)
            {

                bool result = this.framesArrayQueue.Count >= historyFramesQueueLength
                        && this.framesArrayQueue.Count >= Properties.Settings.Default.MaxFrameQueueLength;
                historyFramesQueueLength = this.framesArrayQueue.Count;

                return result;
            }

        }

        private void QueryRawFrame()
        {

            byte[] image = null;
            lock (this.camLocker)
            {
                image = camera.CaptureImageBytes();
            }

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


            if (ImageCaptured != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs() { ImageCaptured = bmp };
                System.Diagnostics.Debug.WriteLine("fire ImageCaptured event");
                ImageCaptured(this, args);
            }


            if (this.cpuOverLoaded()) return;

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
                try
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

                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("exception");
                }
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

                    float[] imgData = NativeIconExtractor.ResizeIplTo(normalized, 100, 100);

                    if (IsGoodGuy(imgData)) return;

                    FaceRecognition.RecognizeResult[] results = new
                         FaceRecognition.RecognizeResult[Program.ImageSampleCount];


                    FaceRecognition.FaceRecognizer.Recognize(
                                                            imgData,
                                                            Program.ImageSampleCount,
                                                            results,
                                                            Program.ImageLen, Program.EigenNum);


                    FaceRecognition.RecognizeResult[] filtered =
                        Array.FindAll(results, r => r.similarity > 0.85);


                    if (filtered.Length == 0) return;

                    int j = 0;

                    IList<ImportantPersonDetail> details =
                        new List<ImportantPersonDetail>();

                    foreach (PersonInfo p in SuspectsRepositoryManager.Instance.Peoples)
                    {
                        foreach (FaceRecognition.RecognizeResult result in filtered)
                        {
                            string fileName = System.IO.Path.GetFileName(result.fileName);

                            int idx = fileName.IndexOf('_');
                            fileName = fileName.Remove(idx, 5);

                            if (string.Compare(fileName, p.FileName, true) == 0)
                            {
                                details.Add(new ImportantPersonDetail(p, result));
                            }
                        }
                    }

                    ImportantPersonDetail[] distinct = details.Distinct(new ImportantPersonComparer()).ToArray();

                    if (distinct.Length == 0) return;

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
