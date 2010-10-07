using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.RemoteImaging.Common.Presenters;
using Damany.Windows.Form;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MiscUtil;
using RemoteImaging.Core;
using Damany.Component;
using System.Threading;
using RemoteImaging.ImportPersonCompare;
using RemoteImaging.Query;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Damany.PC.Domain;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, IImageScreen
    {
        public Func<FaceComparePresenter> CreateFaceCompare { get; set; }

        private IEventAggregator _eventAggregator;
        public IEventAggregator EventAggregator
        {
            get
            {
                return _eventAggregator;
            }
            set
            {
                _eventAggregator = value;
                _eventAggregator.PortraitFound += HandlePortrait;
                _eventAggregator.FaceMatchFound += EventAggregatorFaceMatchFound;
            }
        }

        void EventAggregatorFaceMatchFound(object sender, EventArgs<PersonOfInterestDetectionResult> e)
        {
            if (InvokeRequired)
            {
                Action<PersonOfInterestDetectionResult> ac = ShowSuspects;
                this.Invoke(ac, e.Value);
                return;
            }

            this.ShowSuspects(e.Value);
        }

        private SplashForm splash = new SplashForm();
        public MainForm()
        {
            splash.TopMost = false;
            splash.Show();
            splash.Update();

            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }

            InitStatusBar();

            Application.Idle += new EventHandler(Application_Idle);
        }


        public MainForm(Func<IPicQueryPresenter> picQueryPresenterCreator,
                        Func<IVideoQueryPresenter> createVideoQueryPresenter,
                        Func<FaceComparePresenter> createFaceCompare,
                        Func<OptionsForm> createOptionsForm,
                        Func<OptionsPresenter> createOptionsPresenter,
                        FileSystemStorage videoRepository)
            : this()
        {
            CreateFaceCompare = createFaceCompare;
            this.picPresenterCreator = picQueryPresenterCreator;
            _createVideoQueryPresenter = createVideoQueryPresenter;
            _createFaceCompare = createFaceCompare;
            this._createOptionsPresenter = createOptionsPresenter;
            _videoRepository = videoRepository;
            this._createOptionsForm = createOptionsForm;

            Damany.RemoteImaging.Common.ConfigurationManager.GetDefault().ConfigurationChanged += MainForm_ConfigurationChanged;

        }

        void MainForm_ConfigurationChanged(object sender, EventArgs e)
        {
            this.Cameras = Damany.RemoteImaging.Common.ConfigurationManager.GetDefault().GetCameras().ToArray();
        }

        public void ShowMessage(string msg)
        {
            if (InvokeRequired)
            {
                Action<string> action = this.ShowMessage;
                this.BeginInvoke(action, msg);
                return;
            }

            MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void AttachController(MainController controller)
        {
            this.controller = controller;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (this.splash != null)
            {
                this.splash.Dispose();
            }
        }


        private TreeNode getTopCamera(TreeNode node)
        {
            if (node.Parent == null) return node;

            while (node.Tag == null || !(node.Tag is CameraInfo))
            {
                node = node.Parent;
            }
            return node;
        }


        #region IImageScreen Members

        public CameraInfo GetSelectedCamera()
        {
            if (this.InvokeRequired)
            {
                return (CameraInfo)this.Invoke(new Func<CameraInfo>(() => this.GetSelectedCamera()));
            }

            return null;
        }

        public Portrait SelectedPortrait
        {
            get { return null; }

        }

        public Frame BigImage
        {
            set
            {
                if (InvokeRequired)
                {
                    Action action = delegate
                                        {
                                            this.BigImage = value;
                                        };
                    this.BeginInvoke(action);
                    return;
                }


            }
        }

        public IImageScreenObserver Observer { get; set; }

        private void ShowLiveFace(ImageDetail[] images)
        {
           
        }


        public void ShowImages(ImageDetail[] images)
        {
            ImageCell[] cells = new ImageCell[images.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(images[i].Path);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].Path, Text = text, Tag = null };
                cells[i] = newCell;
            }

            ShowLiveFace(images);


        }

        #endregion


        private CameraInfo GetLastSelectedCamera()
        {
            int lastId = Properties.Settings.Default.LastSelCamID;
            return Damany.RemoteImaging.Common.ConfigurationManager.GetDefault().GetCameraById(lastId);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

            this.controller.Start();

        }

        #region IImageScreen Members

        public CameraInfo[] Cameras
        {
            set { }
        }

        #endregion


        private void squareListView1_SelectedCellChanged(object sender, EventArgs e)
        {
            controller.SelectedPortraitChanged();
        }


        private void searchPic_Click(object sender, EventArgs e)
        {
            var p = this.picPresenterCreator();
            p.Start();
        }


        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        [DllImport("user32.dll", EntryPoint = "BringWindowToTop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BringWindowToTop([In()] IntPtr hWnd);

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///nCmdShow: int
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow([In()] IntPtr hWnd, int nCmdShow);

        System.Diagnostics.Process videoDnTool;

        private void dnloadVideo_Click(object sender, EventArgs e)
        {
            if (videoDnTool != null && !videoDnTool.HasExited)
            {
                //restore window and bring it to top
                ShowWindow(videoDnTool.MainWindowHandle, 9);
                BringWindowToTop(videoDnTool.MainWindowHandle);
                return;
            }

            videoDnTool = System.Diagnostics.Process.Start(Properties.Settings.Default.VideoDnTool);
            videoDnTool.EnableRaisingEvents = true;
            videoDnTool.Exited += videoDnTool_Exited;

        }

        void videoDnTool_Exited(object sender, EventArgs e)
        {
            videoDnTool = null;
        }

        private void videoSearch_Click(object sender, EventArgs e)
        {
            var p = _createVideoQueryPresenter();
            p.Start();
        }

        Thread thread = null;
        string tempComName = "";
        int tempModel = 0;
        private void options_Click(object sender, EventArgs e)
        {
            var p = this._createOptionsPresenter();
            p.Start();
        }

        private void column1by1_Click(object sender, EventArgs e)
        {
        }

        private void column2by2_Click(object sender, EventArgs e)
        {
            
        }

        private void column3by3_Click(object sender, EventArgs e)
        {
            
        }

        private void column4by4_Click(object sender, EventArgs e)
        {
        }

        private void column5by5_Click(object sender, EventArgs e)
        {
        }

        private void InitStatusBar()
        {
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        long lastFreeSpaceBytes;


        private void statusOutputFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"explorer.exe",
                Properties.Settings.Default.OutputPath);
        }

        private void statusUploadFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"explorer.exe",
               Properties.Settings.Default.ImageUploadPool);
        }

        private void cameraTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }


        #region IImageScreen Members


        public bool ShowProgress
        {
            set
            {


            }
        }

        public void StepProgress()
        {


        }

        #endregion

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;

        private string getCurrentCpuUsage()
        {
            return String.Format("{0:F0}%", cpuCounter.NextValue());
        }

        private string getAvailableRAM()
        {
            return String.Format("{0}MB", ramCounter.NextValue());
        }

        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void ShowPic()
        {
        }


        private void squareListView1_CellDoubleClick(object sender, CellDoubleClickEventArgs args)
        {
            ShowPic();
        }




        private void playRelateVideo_Click(object sender, EventArgs e)
        {
            controller.PlayVideo();
        }


        string videoPlayerPath;

        public void StartRecord(CameraInfo cam)
        {
            if (InvokeRequired)
            {
                Action<CameraInfo> action = StartRecord;

                this.BeginInvoke(action, cam);
                return;
            }


        }

        private void OnConnectionFinished(object ex)
        {


        }


        int? lastSelCamID = null;



        private void StartCamera(CameraInfo cam)
        {


            SynchronizationContext context = SynchronizationContext.Current;

            ICamera Icam = null;

            if (string.IsNullOrEmpty(Program.directory))
            {
                var camera = new Damany.Component.SanyoNetCamera();
                camera.IPAddress = cam.Location.Host.ToString();
                camera.UserName = "guest";
                camera.Password = "guest";

                Icam = camera;

            }
            else
            {
                MockCamera mc = new MockCamera(Program.directory);
                mc.Repeat = true;
                Icam = mc;
            }



            System.Threading.ThreadPool.QueueUserWorkItem((object o) =>
                {
                    System.Exception error = null;
                    try
                    {
                        Icam.Connect();
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        error = ex;
                    }
                    catch (System.Net.WebException ex)
                    {
                        error = ex;
                    }

                    context.Post(OnConnectionFinished, error);

                    if (error == null)
                    {
                        lastSelCamID = cam.Id;
                        this.StartRecord(cam);
                    }

                });

        }
        private void cameraTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {


        }

        private void axCamImgCtrl1_InfoChanged(object sender, AxIMGCTRLLib._ICamImgCtrlEvents_InfoChangedEvent e)
        {
            Debug.WriteLine("========info changed" + e.infoConn.AlarmInfo);
        }

        private void enhanceImg_Click(object sender, EventArgs e)
        {
            this.ShowPic();
        }

        private void CenterLiveControl()
        {
        }
        private void panelControl1_SizeChanged(object sender, EventArgs e)
        {
            CenterLiveControl();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            CenterLiveControl();
        }

        void testButton_Click(object sender, EventArgs e)
        {
            _videoRepository.DeleteMostOutDatedDataForDay(0, 1);
        }

        private void tsbFileSet_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((thread != null) && (thread.IsAlive))
            {
                thread.Abort();
            }

            if (lastSelCamID != null)
            {
                Properties.Settings.Default.LastSelCamID = (int)this.lastSelCamID;
            }

            Properties.Settings.Default.Save();

            if (this.controller != null)
            {
                this.controller.Stop();
            }

        }

        private void tsbMonitoring_Click(object sender, EventArgs e)
        {
            Monitoring monitoring = new Monitoring();
            monitoring.ShowDialog(this);
        }

        private void SetupCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ViewCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.StartCamera();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("设置背景后将影响人脸识别准确度, 你确认要设置背景吗?", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res != DialogResult.Yes) return;

        }


        #region IImageScreen Members


        public void ShowSuspects(PersonOfInterestDetectionResult result)
        {
            if (InvokeRequired)
            {
                Action<PersonOfInterestDetectionResult> action = this.ShowSuspects;

                this.BeginInvoke(action, result);
                return;
            }

            alertForm.AddSuspects(result);

        }


        #endregion

        private void faceRecognize_CheckedChanged(object sender, EventArgs e)
        {
        }

        private Func<RemoteImaging.IPicQueryPresenter> picPresenterCreator;
        private readonly Func<IVideoQueryPresenter> _createVideoQueryPresenter;
        private readonly Func<FaceComparePresenter> _createFaceCompare;
        private Func<RemoteImaging.IPicQueryScreen> picScreenCreator;
        public void Initialize()
        {
        }

        public void Start()
        {
        }

        public void HandlePortrait(object sender, EventArgs<Portrait> e)
        {
            Portrait clone = e.Value.Clone();

            if (InvokeRequired)
            {
                Action<Portrait> ac = ShowPortrait;

                this.BeginInvoke(ac, clone);
                return;
            }

            ShowPortrait(clone);
        }

        private void ShowPortrait(Portrait clone)
        {
        }

        private MainController controller;
        private Func<OptionsPresenter> _createOptionsPresenter;
        private readonly FileSystemStorage _videoRepository;
        private Func<OptionsForm> _createOptionsForm;

        private void faceRecognize_Click(object sender, EventArgs e)
        {
            var p = _createFaceCompare();
            p.ComparerSensitivity = Properties.Settings.Default.LbpThreshold;

            var thresholds = new float[]
                                 {
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityLow,
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityMiddle,
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityHi
                                 };

            p.Thresholds = thresholds;
            p.Start();

        }

        private void faceLibBuilder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("FaceLibraryBuilder.exe");
        }

        ImportPersonCompare.ImmediatelyModel alertForm = new ImportPersonCompare.ImmediatelyModel();

        private void alermForm_Click(object sender, EventArgs e)
        {
            this.alertForm.Show(this);
        }


        public ConfigurationSectionHandlers.ButtonsVisibleSectionHandler ButtonsVisible
        {
            set
            {
            }
        }

        private void realTimer_Tick(object sender, EventArgs e)
        {
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
