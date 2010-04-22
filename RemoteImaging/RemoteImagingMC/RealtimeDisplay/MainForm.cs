﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Windows.Form;
using System.IO;
using DevExpress.XtraNavBar;
using Damany.Imaging.Common;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RemoteImaging.Core;
using Microsoft.Win32;
using Damany.Component;
using System.Threading;
using RemoteImaging.Query;
using System.Net.Sockets;
using Damany.RemoteImaging.Common;
using Microsoft.Practices.EnterpriseLibrary.Security;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, IHostsPoolObserver, Damany.Imaging.Common.IPortraitHandler
    {
        private OptionsForm optionsForm;
        Configuration config = Configuration.Instance;
        System.Windows.Forms.Timer time = null;
        System.Collections.Generic.Dictionary<Cell, LiveClient> CellCameraMap =
            new System.Collections.Generic.Dictionary<Cell, LiveClient>();

        public BootLoader loader;


        public MainForm()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }

            for (int i = 0; i < 9; ++i)
            {
                var pip = new Damany.Windows.Form.PipPictureBox();
                pip.Text = (i + 1).ToString();
                pip.Tag = i+1;
                pip.Image = TestDataProvider.Data.GetFrame().ToBitmap();
                pip.SmallImage = TestDataProvider.Data.GetPortrait().ToBitmap();

                pip.Dock = DockStyle.Fill;

                this.tableLayoutPanel1.Controls.Add(pip);
                this.Pips.Add(pip);
            }



            //config.GetLineCameras();
            Properties.Settings setting = Properties.Settings.Default;


            InitStatusBar();

        }


        public Damany.Security.UsersAdmin.UsersManager UsersManager
        {
            get;
            set;
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


        //动态 更新 Tree的方法
        private void SetTreeNode()
        {
            this.hostsTree.Nodes.Clear();

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

            this.hostsTree.Nodes.Add(rootNode);

            this.hostsTree.ExpandAll();
        }



        Camera allCamera = new Camera() { ID = -1 };

        private TreeNode GetTopLevelNode(TreeNode childNode)
        {
            if (childNode == null)
                throw new ArgumentNullException("childNode", "childNode is null.");

            if (childNode.Parent == null) return childNode;

            TreeNode node = childNode;
            while (true)
            {
                if (node.Parent == null)
                {
                    return node.Parent;
                }
                node = node.Parent;
            }

        }

        private Host GetSelectedHost()
        {
            if (this.hostsTree.SelectedNode == null)
            {
                return null;
            }

            TreeNode nd = GetTopLevelNode(this.hostsTree.SelectedNode);
            return nd.Tag as Host;
        }


        #region IImageScreen Members

        public Host SelectedHost
        {
            get
            {
                return this.GetSelectedHost();
            }

        }

        public ImageDetail SelectedImage
        {
            get
            {
                ImageDetail img = null;
               
                return img;
            }

        }

        public ImageDetail BigImage
        {
            set { }

        }

        public IImageScreenObserver Observer { get; set; }

        public void ShowImages(ImageDetail[] images)
        {


        }

        #endregion



        #region IImageScreen Members

        public IList<Damany.PC.Domain.CameraInfo> Cameras
        {
            set
            {
                this.hostsTree.Nodes.Clear();

                TreeNode rootNode = new TreeNode()
                {
                    Text = "所有摄像头",
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                value.ToList().ForEach(camera =>
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
                        Text = "IP地址:" + camera.Location.Host,
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

                this.hostsTree.Nodes.Add(rootNode);

                this.hostsTree.ExpandAll();
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }


        private void searchPic_Click(object sender, EventArgs e)
        {
            var form = new RemoteImaging.Query.PicQueryForm();
            form.Cameras = ConfigurationManager.GetDefault().GetCameras().ToArray().ToList();
            var presenter = new PicQueryPresenter(form, loader.repository);
            form.Presenter = presenter;
            form.ShowDialog(this);
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
        }

        Thread thread = null;
        string tempComName = "";
        int tempModel = 0;
        private bool AuthorizeCurrentUser()
        {
            bool canProceed = AuthorizationManager.IsCurrentUserAuthorized();

            if (!canProceed)
            {
                MessageBox.Show(this,
                    "对不起，你没有权限执行本次操作！",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }

            return canProceed;
        }

        private void options_Click(object sender, EventArgs e)
        {
            if (!AuthorizeCurrentUser()) return;


            if (this.optionsForm == null)
            {
                this.optionsForm = new OptionsForm(this.UsersManager);
                this.optionsForm.Presenter =
                    new OptionPresenter(Damany.RemoteImaging.Common.ConfigurationManager.GetDefault(), this.optionsForm);
            }


            IList<Camera> camCopy = new List<Camera>();

            optionsForm.Cameras = camCopy;
            if (optionsForm.ShowDialog(this) == DialogResult.OK)
            {

                InitStatusBar();

                this.Cameras = ConfigurationManager.GetDefault().GetCameras();

                Properties.Settings setting = Properties.Settings.Default;
                var minFaceWidth = int.Parse(setting.MinFaceWidth);
                float ratio = float.Parse(setting.MaxFaceWidth) / minFaceWidth;
            }

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
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        private void realTimer_Tick(object sender, EventArgs e)
        {
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


        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
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
            
        }


        string videoPlayerPath;

        private void StartRecord(Camera cam)
        {

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

        private void tsbFileSet_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Properties.Settings.Default.Save();

        }

        private void tsbMonitoring_Click(object sender, EventArgs e)
        {
            Monitoring monitoring = new Monitoring();
            monitoring.ShowDialog(this);
        }

        private void SetupCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.hostsTree.SelectedNode == null) return;

            Action<string> setupCamera = this.hostsTree.SelectedNode.Tag as Action<string>;
            if (setupCamera == null) return;

            Camera cam = this.GetTopLevelNode(this.hostsTree.SelectedNode).Tag as Camera;
            setupCamera(cam.IpAddress);
        }

        private void ViewCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void diskSpaceCheckTimer_Tick(object sender, EventArgs e)
        {
            string drive = System.IO.Path.GetPathRoot(Properties.Settings.Default.OutputPath);

            int space = FileSystemStorage.GetFreeDiskSpaceMB(drive);

            int diskQuota = int.Parse(Properties.Settings.Default.DiskQuota);

            if (space <= diskQuota)
            {
                string msg = string.Format("\"{0}\" 盘空间仅剩余 {1} MB, 请尽快转存！", drive, space);
                alertControl1.Show(this, "警告", msg);
            }
        }


        private void AddLayoutMenuItem(string text, int i)
        {
           
        }
        private void squareViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
           

        }

        void layoutMode_Click(object sender, EventArgs e)
        {
            
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            LiveClient lc = ar.AsyncState as LiveClient;
            ConnectInfo info = lc.Tag as ConnectInfo;

            try
            {
                info.Socket.EndConnect(ar);
            }
            catch (System.Net.Sockets.SocketException)
            {
                string msg = string.Format("无法连接 {0}, 请检查设备", info.Source.Config.Name);

                Action showMsg = () => MessageBox.Show(this, msg, "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.BeginInvoke(showMsg);

                return;
            }

            if (CellCameraMap.ContainsKey(info.Target))
            {
                CellCameraMap[info.Target].ImageReceived -= this.lc_ImageReceived;

                CellCameraMap[info.Target].Stop();

                CellCameraMap.Remove(info.Target);
            }


            lc.ImageReceived += new EventHandler<ImageCapturedEventArgs>(lc_ImageReceived);
            lc.ConnectAborted += new EventHandler(lc_ConnectAborted);
            lc.Start();

            CellCameraMap.Add(info.Target, lc);

        }

        void mi_Click(object sender, EventArgs e)
        {

        }

        void lc_ConnectAborted(object sender, EventArgs e)
        {
            LiveClient lc = sender as LiveClient;

            this.CellCameraMap.Remove((lc.Tag as ConnectInfo).Target);
        }

        private static void UpdateCellProperty(Damany.RemoteImaging.Common.Frame frame, ConnectInfo connect)
        {
            connect.Target.Image = frame.image;
            connect.Target.OverlayText = connect.Source.Config.Name + " " + frame.timeStamp.ToString();
            connect.Target.EnableOverlayText = true;

        }

        void lc_ImageReceived(object sender, ImageCapturedEventArgs e)
        {
            
        }

        private void squareListView1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        public void AddHost(Host h)
        {
            var item = new TreeNode();
            item.Tag = h;

            UpdateNodeProperty(item, h);

            item.ExpandAll();

            this.hostsTree.Nodes.Add(item);

        }


        private static void UpdateNodeProperty(TreeNode node, Host h)
        {
            node.Text = h.Config.Name;
            node.ImageIndex = h.Status == HostStatus.OnLine ? 0 : 1;
            node.SelectedImageIndex = node.ImageIndex;

            //child nodes
            node.Nodes.Clear();
            node.Nodes.Add(
                new TreeNode(string.Format("摄像头编号: {0}", h.Config.CameraID), 2, 2));
        }

        public void UpdateHost(Host h)
        {
            var nodes = from TreeNode n in this.hostsTree.Nodes
                        where n.Tag == h
                        select n;

            if (nodes.Count() <= 0)
            {
                return;
            }

            TreeNode node = nodes.First();

            UpdateNodeProperty(node, h);

        }

        private void testButton_Click(object sender, EventArgs e)
        {
           

        }



        private void TcpConnected(IAsyncResult result)
        {
            TcpClient socket = result.AsyncState as TcpClient;

            if (socket.Connected)
            {
                var receiver = new ObjectReceiver(socket);
                receiver.Start();
            }

        }

        private void UpdateLiveImage(Image img, int cameraId)
        {
            if (this.InvokeRequired)
            {
                Action<Image, int> action = this.UpdateLiveImage;
                this.BeginInvoke(action, img, cameraId);
                return;
            }


            foreach (var pip in Pips)
            {
                int id = (int) pip.Tag;

                if (id == cameraId)
                {
                    pip.Image = (Image) img.Clone();
                }
            }

            img.Dispose();
            
        }

        HostsPool hostsPool;

        private void MainForm_Shown(object sender, EventArgs e)
        {
            hostsPool = new HostsPool("224.0.0.23", 40001);
            hostsPool.Observer = this;

            hostsPool.Start();

            foreach (var c in loader.controllers)
            {

                c.Start();

            }

            this.Cameras = ConfigurationManager.GetDefault().GetCameras();

        }

        private void HidePropertyForm(bool hide)
        {
            this.splitContainer1.Panel2Collapsed = hide;
        }

        private void propertyToolBar_Click(object sender, EventArgs e)
        {
            if (!AuthorizeCurrentUser()) return;

            this.HidePropertyForm(!this.splitContainer1.Panel2Collapsed);
        }

        private void hostConfig1_ApplyClick(object sender, EventArgs e)
        {
            bool shouldReturn = MakeSureHostIsSelected();
            if (shouldReturn) return;


            try
            {
                Gateways.HostConfig.Instance.SetHostName( this.SelectedHost.Ip , hostConfig1.HostName);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowInformationBox("通讯错误，请重试！");
            }


        }

        private void ShowInformationBox(string msg)
        {
            MessageBox.Show(this, msg, this.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private bool MakeSureHostIsSelected()
        {
            if (this.SelectedHost == null)
            {
                ShowInformationBox("请选择一台主机");
                return true;
            }

            return false;
        }


        private void sanyoNetCamera1_ApplyIrisClick(object sender, EventArgs e)
        {
            bool shouldReturn = MakeSureHostIsSelected();
            if (shouldReturn) return;

            try
            {
                Gateways.CameraConfig.Instance.SetIris(this.SelectedHost.Ip,
                    0,
                    this.sanyoNetCamera1.IrisMode,
                    this.sanyoNetCamera1.IrisLevel);

            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowInformationBox("通讯错误，请重试！");
            	
            }


        }

        private void sanyoNetCamera1_ApplyAgcClick(object sender, EventArgs e)
        {
            bool shouldReturn = MakeSureHostIsSelected();
            if (shouldReturn) return;

            try
            {
                Gateways.CameraConfig.Instance.SetAgc(this.SelectedHost.Ip,
                    0,
                   this.sanyoNetCamera1.AgcEnabled,
                   this.sanyoNetCamera1.DigitalGainEnabled);
            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowInformationBox("通讯错误，请重试！");
            }
            
        }

        private void sanyoNetCamera1_ApplyShutterClick(object sender, EventArgs e)
        {
            bool shouldReturn = MakeSureHostIsSelected();
            if (shouldReturn) return;

            try
            {
                Gateways.CameraConfig.Instance.SetShutter(this.SelectedHost.Ip,
                    0,
                    this.sanyoNetCamera1.ShutterMode,
                    this.sanyoNetCamera1.ShutterLevel);

            }
            catch (System.ServiceModel.CommunicationException)
            {
                this.ShowInformationBox("通讯错误，请重试！");
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        public void ShowPortrait(List<Damany.Imaging.Common.Portrait> portraits)
        {
            if (this.InvokeRequired)
            {
                Action<List<Damany.Imaging.Common.Portrait>> action = this.ShowPortrait;

                this.BeginInvoke(action, portraits);
                return;
            }

            if (portraits.Count > 0)
            {
                portraits.ToList().ForEach(p =>
                {
                    var img = p.GetIpl().ToBitmap();
                    foreach (var pip in this.Pips)
                    {
                        int index = (int) pip.Tag;
                        if (index == p.CapturedFrom.Id)
                        {
                            pip.SmallImage = (Image) img.Clone();
                        }
                    }

                    img.Dispose();
                    p.Dispose();
                });
            }

        }


        List<Damany.Windows.Form.PipPictureBox> Pips =
    new List<Damany.Windows.Form.PipPictureBox>();


        #region IPortraitHandler Members

        public void Initialize()
        {
        }

        public void Start()
        {
        }

        public void HandlePortraits(IList<Damany.Imaging.Common.Frame> motionFrames, 
            IList<Damany.Imaging.Common.Portrait> portraits)
        {
            this.ShowPortrait(portraits.ToList());
        }

        public void Stop()
        {
        }

        public string Description
        {
            get { return ""; }
        }

        public string Author
        {
            get { return ""; }
        }

        public Version Version
        {
            get { return new Version(); }
        }

        public bool WantCopy
        {
            get { return true; }
        }

        public bool WantFrame
        {
            get { return false; }
        }

        public event EventHandler<MiscUtil.EventArgs<Exception>> Stopped;

        #endregion
    }


    internal class ConnectInfo
    {
        public Cell Target { get; set; }
        public Host Source { get; set; }
        public TcpClient Socket { get; set; }
    }



}
