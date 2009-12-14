﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Windows.Form;
using System.IO;
using DevExpress.XtraNavBar;
using ImageProcess;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RemoteImaging.Core;
using Microsoft.Win32;
using Damany.Component;
using System.Threading;
using MotionDetectWrapper;
using RemoteImaging.Query;
using System.Net.Sockets;
using Damany.RemoteImaging.Net.Discovery;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form
    {
        Configuration config = Configuration.Instance;
        System.Windows.Forms.Timer time = null;
        System.Collections.Generic.Dictionary<Cell, LiveClient> CellCameraMap =
            new System.Collections.Generic.Dictionary<Cell, LiveClient>();


        public MainForm()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }



            //config.GetLineCameras();
            Properties.Settings setting = Properties.Settings.Default;

            InitStatusBar();

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
                if (this.squareListView1.LastSelectedCell != null)
                {
                    Cell c = this.squareListView1.LastSelectedCell;
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
            set { }

        }

        public IImageScreenObserver Observer { get; set; }

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


            this.squareListView1.ShowImages(cells);

        }

        #endregion



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



            frm.Cameras = camCopy;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {

                Properties.Settings setting = Properties.Settings.Default;


                InitStatusBar();

                this.Cameras = frm.Cameras.ToArray<Camera>();

                var minFaceWidth = int.Parse(setting.MinFaceWidth);
                float ratio = float.Parse(setting.MaxFaceWidth) / minFaceWidth;
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

        private void realTimer_Tick(object sender, EventArgs e)
        {

            statusTime.Text = DateTime.Now.ToString();
            this.StepProgress();
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



        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
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
            if ((config.thread != null) && (config.thread.IsAlive))
            {
                config.thread.Abort();
            }
            if ((thread != null) && (thread.IsAlive))
            {
                thread.Abort();
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


        private void squareViewContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.squareViewContextMenu.Items.Clear();
            Cell c = this.squareListView1.SelectedCell;

            foreach (var h in config.Hosts)
            {
                ToolStripMenuItem mi = new ToolStripMenuItem(h.Name);
                mi.Tag = h;
                mi.Click += new EventHandler(mi_Click);

                if (CellCameraMap.ContainsKey(c))
                {
                    if ((CellCameraMap[c].Tag as ConnectInfo).Source == h)
                    {
                        mi.Enabled = false;
                    }

                }

                this.squareViewContextMenu.Items.Add(mi);
            }

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
                string msg = string.Format("无法连接 {0}, 请检查设备", info.Source.Name);
                
                Action showMsg = ()=> MessageBox.Show(this, msg, "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            Cell c = this.squareListView1.SelectedCell;

            if (c == null) return;

            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            HostConfiguration host = menuItem.Tag as HostConfiguration;

            TcpClient tcp = new TcpClient();
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(host.ip);
            System.Net.IPEndPoint ep = new System.Net.IPEndPoint(ip, 20000);
            try
            {
                ConnectInfo info = new ConnectInfo() { Socket = tcp, Target = c, Source = host };

                LiveClient lc = new LiveClient(tcp);
                lc.Tag = info;

                tcp.BeginConnect(ip, 20000, this.ConnectCallback, lc);

               
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show(this, "无法连接, 请检查设备", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            



        }

        void lc_ConnectAborted(object sender, EventArgs e)
        {
            LiveClient lc = sender as LiveClient;

            this.CellCameraMap.Remove((lc.Tag as ConnectInfo).Target);
        }

        void lc_ImageReceived(object sender, ImageCapturedEventArgs e)
        {
            ConnectInfo c = (sender as LiveClient).Tag as ConnectInfo;

            if (this.InvokeRequired)
            {
                Action<Image> updateImage = img => c.Target.Image = img;
                this.BeginInvoke(updateImage, e.ImageCaptured);
            }
            else
            {
                c.Target.Image = e.ImageCaptured;
            }

            this.squareListView1.Invalidate(c.Target.Rec);
        }

        private void squareListView1_MouseDown(object sender, MouseEventArgs e)
        {
            var cell = this.squareListView1.HitTest(e.Location);
            if (cell != null)
            {
                this.squareListView1.SelectedCell = cell;
            }
        }


        public void AddOrUpdateHost(HostConfiguration hostInfo)
        {

        }
    }


    internal class ConnectInfo
    {
        public Cell Target { get; set; }
        public HostConfiguration Source { get; set; }
        public TcpClient Socket { get; set; }
    }



}
