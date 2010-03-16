using System;
using System.Collections.Generic;
using System.IO;
using Damany.Imaging.Common;
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
        MotionDetector motionDetector;
        SuspectsRepository.SuspectsRepositoryManager suspectsMnger;
        FaceSearchWrapper.FaceSearch faceSearcher;


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

        public object Tag { get; set; }

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
            Properties.Settings.Default.SearchSuspecious = false;
#endif

            this.faceSearcher = new FaceSearchWrapper.FaceSearch();

            LoadMotionDetector();


            this.InitializeTrayIcon();


            this.screen.Observer = this;

            this.reconnectTimer.Interval = 5000;
            this.reconnectTimer.Elapsed += new System.Timers.ElapsedEventHandler(reconnectTimer_Elapsed);

            videoFileCheckTimer.Interval = 1000 * 60;
            videoFileCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(videoFileCheckTimer_Elapsed);

            if (File.Exists("bg.jpg"))
                BackGround = OpenCvSharp.IplImage.FromFile(@"bg.jpg");

            //new Service.ServiceProvider(Program.faceSearch, this, camera).OpenServices();
        }

        private void LoadMotionDetector()
        {
            this.motionDetector = new MotionDetector();

            string point = Properties.Settings.Default.Point;
            if (point != "")
            {
                string[] strPoints = point.Split(' ');
                int oPointx = Convert.ToInt32(strPoints[0]);
                int oPointy = Convert.ToInt32(strPoints[1]);
                int tPointx = Convert.ToInt32(strPoints[2]);
                int tPointy = Convert.ToInt32(strPoints[3]);
                var rect = new Rectangle(oPointx, oPointy, oPointy + tPointx, oPointy + tPointy);
                this.motionDetector.ForbiddenRegion = rect;
            }

            this.motionDetector.SetRectThr(Properties.Settings.Default.Thresholding, Properties.Settings.Default.ImageArr);
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

//             if (this.liveServer == null)
//                 ThreadPool.QueueUserWorkItem(this.StartServer, 20000);

        }


        private bool IsGoodGuy(float[] imgData)
        {

            double verdict = this.svm.Predict(imgData);

            System.Diagnostics.Debug.WriteLine(string.Format("=======verdict: {0}=======", verdict));

            return verdict == -1;
        }


        public PersonInfo FindWanted(string fileName)
        {
            var wantedQuery = suspectsMnger.Peoples.Where(p =>
                {
                    int idx = fileName.IndexOf('_');
                    fileName = fileName.Remove(idx, 5);
                    return p.FileName.Contains(fileName);
                });

            return wantedQuery.FirstOrDefault();
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
