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
    public partial class MainForm : Form, IImageScreen, Damany.Imaging.Common.IPortraitHandler
    {
        private SplashForm splash = new SplashForm();
        public MainForm()
        {

//            splash.Show();
//            splash.Update();

            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }

            InitStatusBar();

            this.zoomPicBox.Visible = false;
            this.zoomPicBox.Dock = DockStyle.Fill;

            this.tableLayoutPanel1.Visible = true;
            this.tableLayoutPanel1.Dock = DockStyle.Fill;

            Application.Idle += new EventHandler(Application_Idle);
        }

        public void InitPips()
        {
            if (InvokeRequired)
            {
                Action action = InitPips;
                BeginInvoke(action, null);
                return;
            }


            for (int i = 0; i < Math.Min(tableLayoutPanel1.Controls.Count, Cameras.Length); i++)
            {
                var pip = (PipPictureBox) tableLayoutPanel1.Controls[i];
                pip.Tag = Cameras[i];
                
            }
        }


        public MainForm(Func<RemoteImaging.IPicQueryPresenter> picQueryPresenterCreator,
                        Func<Query.IVideoQueryPresenter> createVideoQueryPresenter,
                        Func<Damany.RemoteImaging.Common.Presenters.FaceComparePresenter> createFaceCompare,
                         Func<OptionsForm> createOptionsForm,
                         Func<OptionsPresenter> createOptionsPresenter)
            : this()
        {
            this.picPresenterCreator = picQueryPresenterCreator;
            _createVideoQueryPresenter = createVideoQueryPresenter;
            _createFaceCompare = createFaceCompare;
            this._createOptionsPresenter = createOptionsPresenter;
            this._createOptionsForm = createOptionsForm;

        }

        public int PipCount
        {
            get
            {
                return tableLayoutPanel1.Controls.Count;
            }
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


        public void ShowFrame(Frame frame)
        {
            foreach (var c in tableLayoutPanel1.Controls)
            {
                var pip = c as PipPictureBox;
                if (pip == null)
                {
                    continue;
                }

                var cam = pip.Tag as CameraInfo;
                if (cam == null)
                {
                    continue;
                }

                if (cam.Id == frame.CapturedFrom.Id)
                {
                    pip.Text = frame.CapturedFrom.Name;
                    pip.Image = frame.GetImage().ToBitmap();
                }
            }
        }


        CameraInfo allCamera = new CameraInfo() { Id = -1 };

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

            if (Cameras == null)
            {
                return null;
            }

            if (SelectedLiveCamera != null)
            {
                var c = Cameras.SingleOrDefault(cam => cam.Id == (int) SelectedLiveCamera.Tag);
                return c;
            }

            return null;
        }

        public Portrait SelectedPortrait
        {
            get
            {
                Portrait p = null;

                return p;
            }

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
            if (images.Length == 0) return;

            Image oldFace = this.liveFace.Image;

            this.liveFace.Image = Damany.Util.Extensions.MiscHelper.FromFileBuffered(images.Last().Path);

            if (oldFace != null)
            {
                oldFace.Dispose();
            }
        }


        public void ShowImages(ImageDetail[] images)
        {

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

        private CameraInfo[] _cameras = new CameraInfo[0];
        public CameraInfo[] Cameras
        {
            get
            {
                return _cameras;
            }
            set
            {
                _cameras = value;

                this.cameraTree.Nodes.Clear();

//                TreeNode rootNode = new TreeNode()
//                {
//                    Text = "所有摄像头",
//                    ImageIndex = 0,
//                    SelectedImageIndex = 0
//                };

                Array.ForEach(value, camera =>
                {
                    TreeNode camNode = new TreeNode
                    {
                        Text = camera.Name + @"-[" + camera.Location.ToString() + "]",
                        ImageIndex = 1,
                        SelectedImageIndex = 1,
                        Tag = camera,
                    };

//                    TreeNode setupNode = new TreeNode()
//                    {
//                        Text = "设置",
//                        ImageIndex = 2,
//                        SelectedImageIndex = 2,
//                    };
//                    TreeNode propertyNode = new TreeNode()
//                    {
//                        Text = "属性",
//                        ImageIndex = 3,
//                        SelectedImageIndex = 3,
//                    };
//                    TreeNode ipNode = new TreeNode()
//                    {
//                        Text = "地址:" + camera.Location.ToString(),
//                        ImageIndex = 4,
//                        SelectedImageIndex = 4
//                    };
                    TreeNode idNode = new TreeNode()
                    {
                        Text = "编号:" + camera.Id.ToString(),
                        ImageIndex = 5,
                        SelectedImageIndex = 5
                    };

                    camNode.Nodes.Add(idNode);


//                    propertyNode.Nodes.AddRange(new TreeNode[] { ipNode, idNode });
//                    camNode.Nodes.AddRange(new TreeNode[] { setupNode, propertyNode });)
                    this.cameraTree.Nodes.Add(camNode);

                });

//                this.cameraTree.Nodes.Add(rootNode);

                this.cameraTree.ExpandAll();
            }
        }

        #endregion


        private void squareListView1_SelectedCellChanged(object sender, EventArgs e)
        {
            controller.SelectedPortraitChanged();
        }


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (PicQueryForm form = new PicQueryForm())
            {
                form.ShowDialog(this);
            }
        }

        private static void SetupExtractor(int envMode, float leftRatio,
            float rightRatio,
            float topRatio,
            float bottomRatio,
            int minFaceWidth,
            float maxFaceWidthRatio,
            Rectangle SearchRectangle)
        {



        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {

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


        private void InitStatusBar()
        {
            statusOutputFolder.Text = "输出目录：" + Properties.Settings.Default.OutputPath;
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void ShowAbout()
        {
            AboutBox about = new AboutBox();
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
                if (this.InvokeRequired)
                {
                    Action ac = () => this.statusProgressBar.Visible = value;
                    //this.Invoke(ac);
                }
                else
                {
                    //this.statusProgressBar.Visible = value;
                }

            }
        }

        public void StepProgress()
        {
            if (InvokeRequired)
            {
                Action ac = () => this.statusProgressBar.PerformStep();

                this.Invoke(ac);
            }
            else
            {
                this.statusProgressBar.PerformStep();

            }

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
            FileSystemStorage.DeleteMostOutDatedDataForDay(1);
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

        }

        private void tsbMonitoring_Click(object sender, EventArgs e)
        {
            Monitoring monitoring = new Monitoring();
            monitoring.ShowDialog(this);
        }

        private void SetupCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.cameraTree.SelectedNode == null) return;

            Action<string> setupCamera = this.cameraTree.SelectedNode.Tag as Action<string>;
            if (setupCamera == null) return;

            var cam = this.getTopCamera(this.cameraTree.SelectedNode).Tag as CameraInfo;
            setupCamera(cam.Location.Host);
        }

        private void ViewCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        bool isDeleting = false;

        private void diskSpaceCheckTimer_Tick(object sender, EventArgs e)
        {
            string drive = System.IO.Path.GetPathRoot(Properties.Settings.Default.OutputPath);

            var space = FileSystemStorage.GetFreeDiskSpaceBytes(drive);

            long diskQuota = long.Parse(Properties.Settings.Default.ReservedDiskSpaceMB) * (1024 * 1024);

            if (space <= diskQuota && !isDeleting)
            {
                isDeleting = true;
                System.Threading.ThreadPool.QueueUserWorkItem((o) =>
                    {
                        try
                        {
                            FileSystemStorage.DeleteMostOutDatedDataForDay(1);
                        }
                        catch (System.IO.IOException ex)
                        {
                            bool rethrow = ExceptionPolicy.HandleException(ex, Constants.ExceptionHandlingLogging);
                            if (rethrow)
                            {
                                throw;
                            }
                        }
                        finally
                        {
                            isDeleting = false;
                        }

                    },
                    null
                    );
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("设置背景后将影响人脸识别准确度, 你确认要设置背景吗?", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res != DialogResult.Yes) return;

        }


        #region IImageScreen Members


        public void ShowSuspects(Damany.Imaging.PlugIns.PersonOfInterestDetectionResult result)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.PlugIns.PersonOfInterestDetectionResult> action = this.ShowSuspects;

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

        public void HandlePortraits(IList<Frame> motionFrames, IList<Portrait> portraits)
        {
            if (InvokeRequired)
            {
                Action<IList<Frame>, IList<Portrait>> action = HandlePortraits;
                BeginInvoke(action, motionFrames, portraits);
                return;
            }

            foreach (var portrait in portraits)
            {
                foreach (var control in tableLayoutPanel1.Controls)
                {
                    var p = control as PipPictureBox;
                    if (p == null) continue;
                    
                    var cam =  p.Tag as CameraInfo ;
                    if (cam == null)
                        continue;

                    if (cam.Id.Equals(portrait.CapturedFrom.Id))
                        {
                            p.SmallImage = portrait.GetIpl().ToBitmap();
                        }
                }


                var liveIdx = this.liveFace.Tag as CameraInfo;
                if (liveIdx == null)
                {
                    return;
                }

                if (liveIdx.Id == portrait.CapturedFrom.Id)
                {
                    liveFace.Image = portrait.GetIpl().ToBitmap();
                }


            }

        }

        public void Stop()
        {
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public string Author
        {
            get { throw new NotImplementedException(); }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantCopy
        {
            get { return true; }
        }

        public bool WantFrame
        {
            get { return false; }
        }

        public event EventHandler<EventArgs<Exception>> Stopped;

        private MainController controller;
        private Func<OptionsPresenter> _createOptionsPresenter;
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

       private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
       {
           this.ShowAbout();
       }

       private void pictureEdit2_DoubleClick(object sender, EventArgs e)
       {
           SwitchZoomPicbox();
       }

       private void zoomPicBox_DoubleClick(object sender, EventArgs e)
       {
           SwitchZoomPicbox();
       }

        private void SwitchZoomPicbox()
        {
            this.tableLayoutPanel1.Visible = !this.tableLayoutPanel1.Visible;
            this.zoomPicBox.Visible = !this.zoomPicBox.Visible;
        }

        private void cameraTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            
        }

        private void pipPictureBox_Click(object sender, EventArgs e)
        {
            var p = sender as PipPictureBox;
            if (p == null)
                return;

            if (SelectedLiveCamera != null && !object.ReferenceEquals(SelectedLiveCamera, sender))
            {
                SelectedLiveCamera.BorderStyle = BorderStyle.None;

            }

            p.BorderStyle = BorderStyle.FixedSingle;
            SelectedLiveCamera = (PipPictureBox)sender;

            if (p.SmallImage != null)
            {
                liveFace.Image = p.SmallImage;
            }


            liveFace.Tag = p.Tag;
        }

        private PipPictureBox SelectedLiveCamera { get; set; }

        public Image LiveFace
        {
            set
            {
                if (liveFace.Image != null)
                {
                    liveFace.Dispose();
                }

                liveFace.Image = value;
            }
        }

        private void contextMenuStripPip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextMenuStripPip.Items.Clear();

            foreach (var cam in Cameras)
            {
                var item = new ToolStripMenuItem(cam.Name);
                item.Tag = cam;

                item.Click += new EventHandler(item_Click);
                contextMenuStripPip.Items.Add(item);
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            var cam = (CameraInfo) ((ToolStripMenuItem) sender).Tag ;
            StartCam(cam);
        }

        private void StartCam(CameraInfo cam)
        {
            CameraInfo old = SelectedLiveCamera.Tag as CameraInfo;
            if (old != null)
            {
                controller.StopCamera(old);
            }

            controller.StartCamera(cam);
            SelectedLiveCamera.Tag = cam;
        }
    }
}
