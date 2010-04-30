using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.RemoteImaging.Common;
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
using RemoteImaging.LicensePlate;
using RemoteImaging.Query;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Damany.PC.Domain;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, 
        IImageScreen, 
        IOperation<Portrait>,
        LicensePlate.ILicensePlateObserver
    {
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
                        ConfigurationManager configurationManager,
						FileSystemStorage videoRepository
                        )
            : this()
        {
            CreateFaceCompare = createFaceCompare;
            this.picPresenterCreator = picQueryPresenterCreator;
            _createVideoQueryPresenter = createVideoQueryPresenter;
            _createFaceCompare = createFaceCompare;
            this._createOptionsPresenter = createOptionsPresenter;
            _configurationManager = configurationManager;
            this._createOptionsForm = createOptionsForm;
			_videoRepository = videoRepository;

        }

        public LicensePlate.ILicensePlateEventPublisher LicensePlateEventPublisher
        {
            set
            {
                _licensePlateEventPublisher = value;
                _licensePlateEventPublisher.RegisterForLicensePlateEvent(this);
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


            if (this.cameraTree.SelectedNode == null
                || this.cameraTree.SelectedNode.Level == 0)
            {
                return allCamera;
            }

            TreeNode nd = getTopCamera(this.cameraTree.SelectedNode);
            return nd.Tag as CameraInfo;
        }

        public Portrait SelectedPortrait
        {
            get
            {
                Portrait p = null;
                if (this.squareListView1.SelectedCell != null)
                {
                    p = (Portrait)this.squareListView1.SelectedCell.Tag;
                }

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

                this.pictureEdit1.Image = value.GetImage().ToBitmap();
                this.pictureEdit1.Tag = value;

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
            ImageCell[] cells = new ImageCell[images.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(images[i].Path);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].Path, Text = text, Tag = null };
                cells[i] = newCell;
            }

            ShowLiveFace(images);

            this.squareListView1.ShowImages(cells);

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
            set
            {
                this.cameraTree.Nodes.Clear();

                TreeNode rootNode = new TreeNode()
                {
                    Text = "所有摄像头",
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                Array.ForEach(value, camera =>
                {
                    TreeNode camNode = new TreeNode()
                    {
                        Text = camera.Name,
                        ImageIndex = 1,
                        SelectedImageIndex = 1,
                        Tag = camera,
                    };

                    TreeNode setupNode = new TreeNode()
                    {
                        Text = "设置",
                        ImageIndex = 2,
                        SelectedImageIndex = 2,
                    };
                    TreeNode propertyNode = new TreeNode()
                    {
                        Text = "属性",
                        ImageIndex = 3,
                        SelectedImageIndex = 3,
                    };
                    TreeNode ipNode = new TreeNode()
                    {
                        Text = "地址:" + camera.Location.ToString(),
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    };
                    TreeNode idNode = new TreeNode()
                    {
                        Text = "编号:" + camera.Id.ToString(),
                        ImageIndex = 5,
                        SelectedImageIndex = 5
                    };


                    propertyNode.Nodes.AddRange(new TreeNode[] { ipNode, idNode });
                    camNode.Nodes.AddRange(new TreeNode[] { setupNode, propertyNode });
                    rootNode.Nodes.Add(camNode);

                });

                this.cameraTree.Nodes.Add(rootNode);

                this.cameraTree.ExpandAll();
            }
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
            this.squareListView1.NumberOfColumns = 1;
        }

        private void column2by2_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 2;
        }

        private void column3by3_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 3;
        }

        private void column4by4_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 4;
        }

        private void column5by5_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 5;
        }

        private void InitStatusBar()
        {
            statusOutputFolder.Text = "输出目录：" + Properties.Settings.Default.OutputPath;
        }

        private void aboutButton_Click(object sender, EventArgs e)
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
            if (this.pictureEdit1.Tag == null)
            {
                return;
            }

            ImageDetail img = this.pictureEdit1.Tag as ImageDetail;

            ShowDetailPic(img);

        }

        private void ShowPic()
        {
            if (this.squareListView1.SelectedCell == null)
                return;
            string p = this.squareListView1.SelectedCell.Path;
            if (p == null) return;

            this.ShowDetailPic(ImageDetail.FromPath(p));
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
            var axCamImgCtrl = cam.Id == 1 ? this.axCamImgCtrl1 : this.axCamImgCtrl2;

            if (InvokeRequired)
            {
                Action<CameraInfo, AxIMGCTRLLib.AxCamImgCtrl> action = StartRecordInternal;

                this.BeginInvoke(action, cam, axCamImgCtrl);
                return;
            }

            StartRecordInternal(cam, axCamImgCtrl);
        }

        private void StartRecordInternal(CameraInfo cam, AxIMGCTRLLib.AxCamImgCtrl axCamImgCtrl)
        {
            axCamImgCtrl.CamImgCtrlStop();

            System.Diagnostics.Debug.WriteLine(axCamImgCtrl.Tag);

            axCamImgCtrl.ImageFileURL = @"liveimg.cgi";
            axCamImgCtrl.ImageType = @"MPEG";
            axCamImgCtrl.CameraModel = 1;
            axCamImgCtrl.CtlLocation = @"http://" + cam.Location.Host;
            axCamImgCtrl.uid = "guest";
            axCamImgCtrl.pwd = "guest";
            axCamImgCtrl.RecordingFolderPath
                = Path.Combine(Properties.Settings.Default.OutputPath, cam.Id.ToString("D2"));
            axCamImgCtrl.RecordingFormat = 0;
            axCamImgCtrl.UniIP = this.axCamImgCtrl1.CtlLocation;
            axCamImgCtrl.UnicastPort = 3939;
            axCamImgCtrl.ComType = 0;

            if (Properties.Settings.Default.Live)
            {
                axCamImgCtrl.CamImgCtrlStart();
                axCamImgCtrl.CamImgRecStart();

            }
        }

        private void OnConnectionFinished(object ex)
        {


        }


        int? lastSelCamID = null;

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
            int height = this.panelControl1.Height - this.axCamImgCtrl1.Height;
            int x = (this.panelControl1.Width - this.axCamImgCtrl1.Width) / 2;
            this.axCamImgCtrl1.Left = x;
            this.squareListView1.Height = height - 15;
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
            this.controller.Start();
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

        public void HandlePortraits(IEnumerable<Portrait> portraits)
        {
            var imgCells = portraits.Select(p => new ImageCell()
                                                     {
                                                         Image = p.GetIpl().ToBitmap(),
                                                         Text = p.CapturedAt.ToString(),
                                                         Tag = p
                                                     }).ToArray();

            this.squareListView1.ShowImages(imgCells);
            var last = portraits.LastOrDefault();
            if (last != null)
            {
                this.liveFace.Image = last.GetIpl().ToBitmap();
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
        private readonly ConfigurationManager _configurationManager;
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

        public IEnumerable<Portrait> Execute(IEnumerable<Portrait> inputs)
        {
            this.HandlePortraits(inputs);

            return inputs;
        }
		
		
	    public ConfigurationSectionHandlers.ButtonsVisibleSectionHandler ButtonsVisible
        {
            set
            {
                this.faceLibBuilder.Visible = value.HumanFaceLibraryButtonVisible;
                this.faceCompare.Visible = value.CompareFaceButtonVisible;
                this.alermForm.Visible = value.ShowAlermFormButtonVisible;
            }
        }


        private void axCamImgCtrl1_Enter(object sender, EventArgs e)
        {

        }

        private void licensePlateList_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {

        }

        public void LicensePlateCaptured(LicensePlateInfo licensePlateInfo)
        {
            if (InvokeRequired)
            {
                Action<LicensePlateInfo> action = LicensePlateCaptured;
                this.Invoke(action, licensePlateInfo);
                return;
            }

            var item = new ListViewItem(string.Empty, 0);
            item.SubItems.Add(licensePlateInfo.LicensePlateNumber);
            item.SubItems.Add(licensePlateInfo.CaptureTime.ToString());

            var camInfo = _configurationManager.GetCameraById(licensePlateInfo.CapturedFrom);
            var cameraName = "未知摄像头";
            if (camInfo != null)
            {
                cameraName = camInfo.Name ?? "未知摄像头";
                
            }
            item.SubItems.Add(cameraName);

            licensePlateList.Items.Add(item);
            licensePlateList.EnsureVisible(licensePlateList.Items.Count-1);
        }

        private LicensePlate.ILicensePlateEventPublisher _licensePlateEventPublisher;
    }
}
