using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Windows.Form;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RemoteImaging.Core;
using Damany.Component;
using System.Threading;
using RemoteImaging.ImportPersonCompare;
using RemoteImaging.Query;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, IImageScreen
    {
        private const int GB = 1024 * 1024 * 1024;
        Configuration config = new Configuration();
        System.Windows.Forms.Timer time = null;
        public MainForm()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }

            diskSpaceCheckTimer.Interval = Properties.Settings.Default.FreeDiskspaceCheckIntervalMs;


            config.GetLineCameras();
            Properties.Settings setting = Properties.Settings.Default;

            cpuCounter = new PerformanceCounter();
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            //Camera[] cams = new Camera[Configuration.Instance.Cameras.Count];
            //Configuration.Instance.Cameras.CopyTo(cams, 0);
            //this.Cameras = cams;

            time = new System.Windows.Forms.Timer();
            time.Tick += time_Elapsed;
            time.Interval = 3000;
            time.Enabled = true;

            //FileHandle fh = new FileHandle();//删除无效视频
            //fh.DeleteInvalidVideo();

            //StartSetCam(setting);//根据光亮值设置相机

            SetMonitor();//启动布控
            Program.motionDetector.SetRectThr(setting.Thresholding, setting.ImageArr);//调用分组设置值

            InitStatusBar();

            Program.motionDetector.DrawMotionRect = setting.DrawMotionRect;

            var faceSearchConfig = new FaceSearchWrapper.FaceSearchConfiguration();

            faceSearchConfig.LeftRation = float.Parse(setting.IconLeftExtRatio);
            faceSearchConfig.TopRation = float.Parse(setting.IconTopExtRatio);
            faceSearchConfig.RightRation = float.Parse(setting.IconRightExtRatio);
            faceSearchConfig.BottomRation = float.Parse(setting.IconBottomExtRatio);

            faceSearchConfig.MinFaceWidth = int.Parse(setting.MinFaceWidth);
            int maxFaceWidth = int.Parse(setting.MaxFaceWidth);
            faceSearchConfig.FaceWidthRatio = (float)maxFaceWidth / faceSearchConfig.MinFaceWidth;

            faceSearchConfig.EnvironmentMode = setting.EnvMode;

            faceSearchConfig.SearchRectangle =
                new Rectangle(int.Parse(setting.SrchRegionLeft),
                              int.Parse(setting.SrchRegionTop),
                              int.Parse(setting.SrchRegionWidth),
                              int.Parse(setting.SrchRegionHeight));

            Program.faceSearch.Configuration = faceSearchConfig;
        }


        private void SetMonitor()
        {
            string point = Properties.Settings.Default.Point;
            if (point != "")
            {
                string[] strPoints = point.Split(' ');
                int oPointx = Convert.ToInt32(strPoints[0]);
                int oPointy = Convert.ToInt32(strPoints[1]);
                int tPointx = Convert.ToInt32(strPoints[2]);
                int tPointy = Convert.ToInt32(strPoints[3]);
                Program.motionDetector.SetAlarmArea(oPointx, oPointy, tPointx, tPointy, false);
            }
        }


        //根据光亮值修改摄像机   线程
        private void StartSetCam(Properties.Settings setting)
        {
            if (thread != null)
            {
                thread.Abort();
            }
            CameraUpdateSettings cus = new CameraUpdateSettings(setting.ComName, (BrightType)setting.BrightMode, setting.CurIp);
            thread = new Thread(new ParameterizedThreadStart(cus.ReadPort));
            thread.IsBackground = true;
            thread.Start();
        }


        delegate void DataCallBack();
        Camera[] cams = null;
        private void time_Elapsed(object source, EventArgs args)
        {
            if (config.Cameras != null)
            {
                cams = new Camera[config.Cameras.Count];
                config.Cameras.CopyTo(cams, 0);
                DataCallBack dcb = new DataCallBack(this.SetTreeNode);
                this.Invoke(dcb, null);
                time.Enabled = false;
            }
        }

        //动态 更新 Tree的方法
        private void SetTreeNode()
        {
            this.cameraTree.Nodes.Clear();

            TreeNode rootNode = new TreeNode()
            {
                Text = "所有摄像头",
                ImageIndex = 0,
                SelectedImageIndex = 0
            };

            Array.ForEach(cams, camera =>
            {

                TreeNode camNode = new TreeNode()
                {
                    Text = camera.Name,
                    ImageIndex = 1,
                    SelectedImageIndex = 1,
                    Tag = camera,
                };

                Action<string> setupCamera = (ip) =>
                {
                    using (FormConfigCamera form = new FormConfigCamera())
                    {
                        StringBuilder sb = new StringBuilder(form.Text);
                        sb.Append("-[");
                        sb.Append(ip);
                        sb.Append("]");

                        form.Navigate(ip);
                        form.Text = sb.ToString();
                        form.ShowDialog(this);
                    }
                };

                TreeNode setupNode = new TreeNode()
                {
                    Text = "设置",
                    ImageIndex = 2,
                    SelectedImageIndex = 2,
                    Tag = setupCamera,
                };
                TreeNode propertyNode = new TreeNode()
                {
                    Text = "属性",
                    ImageIndex = 3,
                    SelectedImageIndex = 3,
                };
                TreeNode ipNode = new TreeNode()
                {
                    Text = "IP地址:" + camera.IpAddress,
                    ImageIndex = 4,
                    SelectedImageIndex = 4
                };
                TreeNode idNode = new TreeNode()
                {
                    Text = "编号:" + camera.ID.ToString(),
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



        private Presenter presenter;
        Camera allCamera = new Camera() { ID = -1 };

        private TreeNode getTopCamera(TreeNode node)
        {
            if (node.Parent == null) return node;

            while (node.Tag == null || !(node.Tag is Camera))
            {
                node = node.Parent;
            }
            return node;
        }

        private Camera getSelCamera()
        {
            if (this.cameraTree.SelectedNode == null
                || this.cameraTree.SelectedNode.Level == 0)
            {
                return allCamera;
            }

            TreeNode nd = getTopCamera(this.cameraTree.SelectedNode);
            return nd.Tag as Camera;
        }


        #region IImageScreen Members

        public Camera SelectedCamera
        {
            get
            {
                if (this.InvokeRequired)
                {
                    System.Func<Camera> func = this.getSelCamera;
                    return this.Invoke(func) as Camera;
                }
                else
                {
                    return getSelCamera();
                }

            }

        }

        public ImageDetail SelectedImage
        {
            get
            {
                ImageDetail img = null;
                if (this.squareListView1.SelectedCell != null)
                {
                    Cell c = this.squareListView1.SelectedCell;
                    if (!string.IsNullOrEmpty(c.Path))
                    {
                        img = ImageDetail.FromPath(c.Path);
                    }

                }

                return img;
            }

        }

        public ImageDetail BigImage
        {
            set
            {
                try
                {
                    Image img = Image.FromFile(value.Path);
                    this.pictureEdit1.Image = img;
                    this.pictureEdit1.Tag = value;
                }
                catch (System.IO.IOException)
                {
                    string msg = string.Format("无法打开文件:\"{0}\"，请检查。", value.Path);

                    MessageBox.Show(this, msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        public IImageScreenObserver Observer { get; set; }

        private void ShowLiveFace(ImageDetail[] images)
        {
            if (images.Length == 0) return;

            Image oldFace = this.liveFace.Image;

            this.liveFace.Image = Image.FromFile(images.Last().Path);

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
                Image img = Image.FromFile(images[i].Path);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].Path, Text = text, Tag = null };
                cells[i] = newCell;
            }

            ShowLiveFace(images);

            this.squareListView1.ShowImages(cells);

        }

        #endregion

        Communication commucation;

        private Camera GetLastSelectedCamera()
        {
            Camera c = config.FindCameraByID(Properties.Settings.Default.LastSelCamID);
            return c;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            diskSpaceCheckTimer.Enabled = true;

            commucation = new Communication("224.0.0.23", 40001);
            commucation.Start();

            Camera c = GetLastSelectedCamera();
            if (c == null) return;

            if (FileSystemStorage.DriveRemoveable(Properties.Settings.Default.OutputPath))
            {
                DialogResult result = MessageBox.Show(this,
                    "输出目录位于可移动介质！继续吗?", "警告",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                {
                    return;
                }
            }


            this.StartCamera(c);


        }

        #region IImageScreen Members

        public Camera[] Cameras
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

                    Action<string> setupCamera = (ip) =>
                    {
                        using (FormConfigCamera form = new FormConfigCamera())
                        {
                            StringBuilder sb = new StringBuilder(form.Text);
                            sb.Append("-[");
                            sb.Append(ip);
                            sb.Append("]");

                            form.Navigate(ip);
                            form.Text = sb.ToString();
                            form.ShowDialog(this);
                        }
                    };

                    TreeNode setupNode = new TreeNode()
                    {
                        Text = "设置",
                        ImageIndex = 2,
                        SelectedImageIndex = 2,
                        Tag = setupCamera,
                    };
                    TreeNode propertyNode = new TreeNode()
                    {
                        Text = "属性",
                        ImageIndex = 3,
                        SelectedImageIndex = 3,
                    };
                    TreeNode ipNode = new TreeNode()
                    {
                        Text = "IP地址:" + camera.IpAddress,
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    };
                    TreeNode idNode = new TreeNode()
                    {
                        Text = "编号:" + camera.ID.ToString(),
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
            if (this.Observer != null)
            {
                this.Observer.SelectedImageChanged();
            }
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
            Program.faceSearch.SetExRatio(topRatio,
                                    bottomRatio,
                                    leftRatio,
                                    rightRatio);


            Program.faceSearch.SetROI(SearchRectangle.Left,
                SearchRectangle.Top,
                SearchRectangle.Width - 1,
                SearchRectangle.Height - 1
                );


            Program.faceSearch.SetFaceParas(minFaceWidth, maxFaceWidthRatio);


            Program.faceSearch.SetLightMode(envMode);
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }


        private void searchPic_Click(object sender, EventArgs e)
        {
            new RemoteImaging.Query.PicQueryForm().ShowDialog(this);
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
            new RemoteImaging.Query.VideoQueryForm().ShowDialog(this);
        }

        Thread thread = null;
        string tempComName = "";
        int tempModel = 0;
        private void options_Click(object sender, EventArgs e)
        {
            OptionsForm frm = OptionsForm.Instance;
            IList<Camera> camCopy = new List<Camera>();

            foreach (Camera item in Configuration.Instance.Cameras)
            {
                camCopy.Add(new Camera() { ID = item.ID, Name = item.Name, IpAddress = item.IpAddress, Mac = item.Mac, Status = item.Status });
            }


            frm.Cameras = camCopy;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {

                Properties.Settings setting = Properties.Settings.Default;

                Configuration.Instance.Cameras = frm.Cameras;//这里添加设置摄像机的 IP 和 ID 对应的设置类文件 ResetCameraInfo
                Configuration.Instance.Save();

                //setting.Save();

                InitStatusBar();

                this.Cameras = frm.Cameras.ToArray<Camera>();

                var minFaceWidth = int.Parse(setting.MinFaceWidth);
                float ratio = float.Parse(setting.MaxFaceWidth) / minFaceWidth;

                StartSetCam(setting);
            }

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

        private static long FreeDiskSpaceBytes()
        {
            return FileSystemStorage.GetFreeDiskSpaceBytes(Properties.Settings.Default.OutputPath);
        }

        private static string FormatBytesString(string name, long bytes)
        {
            float gb = (float)bytes / GB;

            if (gb < 1)
            {
                return string.Format(name + ": {0}MB", (int)(gb * 1024));
            }
            else
            {
                return string.Format(name + ": {0:F1}GB",   gb);
            }
        }

  

        private void realTimer_Tick(object sender, EventArgs e)
        {
            var reservedDiskSpaceBytes = (long)int.Parse(Properties.Settings.Default.ReservedDiskSpaceMB) * (1024 * 1024);
            var totalFreeDiskSpaceBytes = FreeDiskSpaceBytes();

            var availableBytes = totalFreeDiskSpaceBytes - reservedDiskSpaceBytes;

            string statusTxt = string.Format("CPU占用率: {0}, 可用内存: {1}, {2}, {3}, {4}",
                this.getCurrentCpuUsage(),
            this.getAvailableRAM(),
            FormatBytesString("空闲", totalFreeDiskSpaceBytes),
            FormatBytesString("保留", reservedDiskSpaceBytes),
            FormatBytesString("可用", availableBytes)
            );

            this.statusCPUMemUsage.Text = statusTxt;

            statusTime.Text = DateTime.Now.ToString();

         }

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
            Cell c = this.squareListView1.SelectedCell;
            if (c == null || c.Path == null) return;

            ImageDetail imgInfo = ImageDetail.FromPath(c.Path);

            string[] videos = FileSystemStorage.VideoFilesOfImage(imgInfo);

            if (videos.Length == 0)
            {
                MessageBox.Show(this, "没有找到相关视频", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            VideoPlayer.PlayVideosAsync(videos);
        }


        string videoPlayerPath;

        private void StartRecord(Camera cam)
        {
            this.axCamImgCtrl1.CamImgCtrlStop();

            this.axCamImgCtrl1.ImageFileURL = @"liveimg.cgi";
            this.axCamImgCtrl1.ImageType = @"MPEG";
            this.axCamImgCtrl1.CameraModel = 1;
            this.axCamImgCtrl1.CtlLocation = @"http://" + cam.IpAddress;
            this.axCamImgCtrl1.uid = "guest";
            this.axCamImgCtrl1.pwd = "guest";
            this.axCamImgCtrl1.RecordingFolderPath
                = Path.Combine(Properties.Settings.Default.OutputPath, cam.ID.ToString("D2"));
            this.axCamImgCtrl1.RecordingFormat = 0;
            this.axCamImgCtrl1.UniIP = this.axCamImgCtrl1.CtlLocation;
            this.axCamImgCtrl1.UnicastPort = 3939;
            this.axCamImgCtrl1.ComType = 0;

            if (Properties.Settings.Default.Live)
            {
                this.axCamImgCtrl1.CamImgCtrlStart();
                this.axCamImgCtrl1.CamImgRecStart();

            }

        }

        private void OnConnectionFinished(object ex)
        {
            if (ex != null)
                MessageBox.Show(this,
                                 "无法连接摄像头，请检查摄像头后重新连接",
                                 "连接错误",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            else
            {
                presenter.Start();
                this.faceRecognize.Enabled = true;
            }

        }


        int? lastSelCamID = null;

        private void StartCamera(Camera cam)
        {
            SynchronizationContext context = SynchronizationContext.Current;

            ICamera Icam = null;

            if (string.IsNullOrEmpty(Program.directory))
            {
                var camera = new Damany.Component.SanyoNetCamera();
                camera.IPAddress = cam.IpAddress;
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


            if (presenter == null)
                presenter = new Presenter(this, Icam);

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
                        lastSelCamID = cam.ID;
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
#if DEBUG
            testButton.Visible = true;
            testButton.Click += new EventHandler(testButton_Click);
#endif

            lastFreeSpaceBytes = FreeDiskSpaceBytes();

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
            if ((config.thread != null) && (config.thread.IsAlive))
            {
                config.thread.Abort();
            }
            if ((thread != null) && (thread.IsAlive))
            {
                thread.Abort();
            }

            if (lastSelCamID != null)
            {
                Properties.Settings.Default.LastSelCamID = (int) this.lastSelCamID;
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

            Camera cam = this.getTopCamera(this.cameraTree.SelectedNode).Tag as Camera;
            setupCamera(cam.IpAddress);
        }

        private void ViewCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.cameraTree.SelectedNode == null) return;

            Action<string> setupCamera = this.cameraTree.SelectedNode.Tag as Action<string>;
            if (setupCamera == null) return;

            Camera cam = this.getTopCamera(this.cameraTree.SelectedNode).Tag as Camera;
            StartCamera(cam);
        }

        bool isDeleting = false;

        private void diskSpaceCheckTimer_Tick(object sender, EventArgs e)
        {
            string drive = System.IO.Path.GetPathRoot(Properties.Settings.Default.OutputPath);

            var space = FileSystemStorage.GetFreeDiskSpaceBytes(drive);

            long diskQuota = long.Parse(Properties.Settings.Default.ReservedDiskSpaceMB) * (1024*1024);

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
            if (presenter == null) return;

            DialogResult res = MessageBox.Show("设置背景后将影响人脸识别准确度, 你确认要设置背景吗?", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res != DialogResult.Yes) return;

            presenter.UpdateBG();
        }


        #region IImageScreen Members

        private void ShowFaceRecognition(Image captured, Image fromLib, float similarity)
        {
            FormFaceRecognitionResult form = new FormFaceRecognitionResult();
            form.capturedFace.Image = captured;
            form.faceInLibrary.Image = fromLib;
            form.similarity.Text = similarity.ToString();

            form.Show(this);
        }
        public void ShowFaceRecognitionResult(Image captured, Image fromLib, float similarity)
        {
            if (InvokeRequired)
            {
                Action<Image, Image, float> show = ShowFaceRecognition;

                this.Invoke(show, captured, fromLib, similarity);
            }
            else
            {
                ShowFaceRecognition(captured, fromLib, similarity);
            }
        }

        public void ShowSuspectsInternal(ImportantPersonDetail[] suspects, Image captured)
        {

            ImportPersonCompare.ImmediatelyModel formAlert = new ImportPersonCompare.ImmediatelyModel();
            formAlert.ShowPersons = suspects.ToList();
            formAlert.PicCheckImg = captured;

            formAlert.ShowDialog(this);
        }


        public void ShowSuspects(ImportantPersonDetail[] suspects, Image captured)
        {
            if (InvokeRequired)
            {
                Action<ImportantPersonDetail[], Image> showSuspectsDel =
                    this.ShowSuspectsInternal;

                this.BeginInvoke(showSuspectsDel, suspects, captured);

            }
            else
            {
                ShowSuspectsInternal(suspects, captured);
            }

        }

        #endregion

        private void faceRecognize_CheckedChanged(object sender, EventArgs e)
        {
            if (this.presenter == null) return;

            this.presenter.FaceRecognize = faceRecognize.Checked;
        }
    }
}
