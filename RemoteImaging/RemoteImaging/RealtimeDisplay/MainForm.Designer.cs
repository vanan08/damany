namespace RemoteImaging.RealtimeDisplay
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("192.168.1.2", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("2", 2, 2);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("南门", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("192.168.1.1", 1, 1);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("3", 2, 2);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("北门", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusOutputFolder = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCPUMemUsage = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.liveFace = new System.Windows.Forms.PictureBox();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.axCamImgCtrl1 = new AxIMGCTRLLib.AxCamImgCtrl();
            this.squareListView1 = new Damany.Windows.Form.SquareListView();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cameraTree = new System.Windows.Forms.TreeView();
            this.contextMenuStripForCamTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetupCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraImageList = new System.Windows.Forms.ImageList(this.components);
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.realTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.searchPic = new System.Windows.Forms.ToolStripButton();
            this.videoSearch = new System.Windows.Forms.ToolStripButton();
            this.playRelateVideo = new System.Windows.Forms.ToolStripButton();
            this.options = new System.Windows.Forms.ToolStripButton();
            this.aboutButton = new System.Windows.Forms.ToolStripLabel();
            this.tsbMonitoring = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.enhanceImg = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.faceRecognize = new System.Windows.Forms.ToolStripButton();
            this.testButton = new System.Windows.Forms.ToolStripButton();
            this.diskSpaceCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liveFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axCamImgCtrl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.contextMenuStripForCamTreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusOutputFolder,
            this.statusCPUMemUsage,
            this.statusProgressBar,
            this.statusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 584);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(897, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusOutputFolder
            // 
            this.statusOutputFolder.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusOutputFolder.Image = ((System.Drawing.Image)(resources.GetObject("statusOutputFolder.Image")));
            this.statusOutputFolder.Name = "statusOutputFolder";
            this.statusOutputFolder.Size = new System.Drawing.Size(149, 20);
            this.statusOutputFolder.Text = "toolStripStatusLabel2";
            this.statusOutputFolder.Click += new System.EventHandler(this.statusOutputFolder_Click);
            // 
            // statusCPUMemUsage
            // 
            this.statusCPUMemUsage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusCPUMemUsage.Name = "statusCPUMemUsage";
            this.statusCPUMemUsage.Size = new System.Drawing.Size(133, 20);
            this.statusCPUMemUsage.Text = "toolStripStatusLabel1";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(200, 22);
            this.statusProgressBar.Step = 1;
            this.statusProgressBar.Visible = false;
            // 
            // statusTime
            // 
            this.statusTime.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTime.Image = ((System.Drawing.Image)(resources.GetObject("statusTime.Image")));
            this.statusTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statusTime.Name = "statusTime";
            this.statusTime.Size = new System.Drawing.Size(600, 20);
            this.statusTime.Spring = true;
            this.statusTime.Text = "toolStripStatusLabel1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel2);
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.layoutControl1.Location = new System.Drawing.Point(0, 89);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(897, 495);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.liveFace);
            this.panel2.Controls.Add(this.panelControl6);
            this.panel2.Location = new System.Drawing.Point(7, 154);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 150);
            this.panel2.TabIndex = 13;
            // 
            // liveFace
            // 
            this.liveFace.BackColor = System.Drawing.SystemColors.Window;
            this.liveFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liveFace.Location = new System.Drawing.Point(0, 24);
            this.liveFace.Name = "liveFace";
            this.liveFace.Size = new System.Drawing.Size(167, 126);
            this.liveFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.liveFace.TabIndex = 3;
            this.liveFace.TabStop = false;
            // 
            // panelControl6
            // 
            this.panelControl6.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.panelControl6.Appearance.Options.UseBackColor = true;
            this.panelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl6.Controls.Add(this.labelControl1);
            this.panelControl6.Controls.Add(this.pictureBox4);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl6.Location = new System.Drawing.Point(0, 0);
            this.panelControl6.Name = "panelControl6";
            this.panelControl6.Size = new System.Drawing.Size(167, 24);
            this.panelControl6.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(38, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(133, 24);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "实时人像";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox4.Image = global::RemoteImaging.Properties.Resources.FaceIcon32;
            this.pictureBox4.Location = new System.Drawing.Point(0, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 24);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 1;
            this.pictureBox4.TabStop = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.pictureEdit1);
            this.panelControl3.Controls.Add(this.panelControl5);
            this.panelControl3.Location = new System.Drawing.Point(7, 320);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(167, 168);
            this.panelControl3.TabIndex = 12;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 24);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(167, 144);
            this.pictureEdit1.TabIndex = 2;
            this.pictureEdit1.DoubleClick += new System.EventHandler(this.pictureEdit1_DoubleClick);
            // 
            // panelControl5
            // 
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.labelControl2);
            this.panelControl5.Controls.Add(this.pictureBox2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(167, 24);
            this.panelControl5.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(38, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(133, 24);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "全身像";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::RemoteImaging.Properties.Resources.Peoples32;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 24);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.axCamImgCtrl1);
            this.panelControl1.Controls.Add(this.squareListView1);
            this.panelControl1.Location = new System.Drawing.Point(190, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(700, 481);
            this.panelControl1.TabIndex = 10;
            this.panelControl1.SizeChanged += new System.EventHandler(this.panelControl1_SizeChanged);
            // 
            // axCamImgCtrl1
            // 
            this.axCamImgCtrl1.Enabled = true;
            this.axCamImgCtrl1.Location = new System.Drawing.Point(82, 6);
            this.axCamImgCtrl1.Name = "axCamImgCtrl1";
            this.axCamImgCtrl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCamImgCtrl1.OcxState")));
            this.axCamImgCtrl1.Size = new System.Drawing.Size(704, 353);
            this.axCamImgCtrl1.TabIndex = 3;
            this.axCamImgCtrl1.InfoChanged += new AxIMGCTRLLib._ICamImgCtrlEvents_InfoChangedEventHandler(this.axCamImgCtrl1_InfoChanged);
            // 
            // squareListView1
            // 
            this.squareListView1.AutoDisposeImage = true;
            this.squareListView1.BackColor = System.Drawing.SystemColors.Control;
            this.squareListView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.squareListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.squareListView1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.squareListView1.Location = new System.Drawing.Point(3, 329);
            this.squareListView1.MaxCountOfCells = 25;
            this.squareListView1.Name = "squareListView1";
            this.squareListView1.NumberOfColumns = 6;
            this.squareListView1.NumberofRows = 3;
            this.squareListView1.Padding = new System.Windows.Forms.Padding(3);
            this.squareListView1.SelectedCell = null;
            this.squareListView1.Size = new System.Drawing.Size(694, 149);
            this.squareListView1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.squareListView1, "双击查看全身图片");
            this.squareListView1.SelectedCellChanged += new System.EventHandler(this.squareListView1_SelectedCellChanged);
            this.squareListView1.CellDoubleClick += new Damany.Windows.Form.CellDoubleClickHandler(this.squareListView1_CellDoubleClick);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(7, 38);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(306, 22);
            this.simpleButton2.StyleController = this.layoutControl1;
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "simpleButton2";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(7, 38);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(147, 22);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "simpleButton1";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.cameraTree);
            this.panelControl2.Controls.Add(this.panelControl4);
            this.panelControl2.Location = new System.Drawing.Point(7, 7);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(167, 131);
            this.panelControl2.TabIndex = 11;
            // 
            // cameraTree
            // 
            this.cameraTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cameraTree.ContextMenuStrip = this.contextMenuStripForCamTreeView;
            this.cameraTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraTree.FullRowSelect = true;
            this.cameraTree.HideSelection = false;
            this.cameraTree.ImageIndex = 0;
            this.cameraTree.ImageList = this.cameraImageList;
            this.cameraTree.Location = new System.Drawing.Point(0, 24);
            this.cameraTree.Name = "cameraTree";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "Node1";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "192.168.1.2";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "Node4";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "2";
            treeNode3.ImageIndex = 0;
            treeNode3.Name = "Node0";
            treeNode3.Text = "南门";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "Node3";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "192.168.1.1";
            treeNode5.ImageIndex = 2;
            treeNode5.Name = "Node5";
            treeNode5.SelectedImageIndex = 2;
            treeNode5.Text = "3";
            treeNode6.Name = "Node2";
            treeNode6.Text = "北门";
            this.cameraTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6});
            this.cameraTree.SelectedImageIndex = 0;
            this.cameraTree.Size = new System.Drawing.Size(167, 107);
            this.cameraTree.TabIndex = 1;
            this.cameraTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.cameraTree_NodeMouseDoubleClick);
            this.cameraTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.cameraTree_NodeMouseClick);
            // 
            // contextMenuStripForCamTreeView
            // 
            this.contextMenuStripForCamTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewCameraToolStripMenuItem,
            this.SetupCameraToolStripMenuItem});
            this.contextMenuStripForCamTreeView.Name = "contextMenuStripForCamTreeView";
            this.contextMenuStripForCamTreeView.Size = new System.Drawing.Size(107, 48);
            // 
            // ViewCameraToolStripMenuItem
            // 
            this.ViewCameraToolStripMenuItem.Name = "ViewCameraToolStripMenuItem";
            this.ViewCameraToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.ViewCameraToolStripMenuItem.Text = "查看";
            this.ViewCameraToolStripMenuItem.Click += new System.EventHandler(this.ViewCameraToolStripMenuItem_Click);
            // 
            // SetupCameraToolStripMenuItem
            // 
            this.SetupCameraToolStripMenuItem.Name = "SetupCameraToolStripMenuItem";
            this.SetupCameraToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.SetupCameraToolStripMenuItem.Text = "设置";
            this.SetupCameraToolStripMenuItem.Click += new System.EventHandler(this.SetupCameraToolStripMenuItem_Click);
            // 
            // cameraImageList
            // 
            this.cameraImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("cameraImageList.ImageStream")));
            this.cameraImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.cameraImageList.Images.SetKeyName(0, "Cameras16.gif");
            this.cameraImageList.Images.SetKeyName(1, "Camera16.gif");
            this.cameraImageList.Images.SetKeyName(2, "spanner16.gif");
            this.cameraImageList.Images.SetKeyName(3, "property.gif");
            this.cameraImageList.Images.SetKeyName(4, "ip16.gif");
            this.cameraImageList.Images.SetKeyName(5, "id16.gif");
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.labelControl3);
            this.panelControl4.Controls.Add(this.pictureBox1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(167, 24);
            this.panelControl4.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(43, 0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(133, 24);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "摄像头列表";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::RemoteImaging.Properties.Resources.Camera32;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 24);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.simpleButton1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(158, 320);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.simpleButton2;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(317, 320);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.splitterItem2,
            this.splitterItem1,
            this.layoutControlItem6,
            this.splitterItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(897, 495);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(183, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(710, 491);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl2;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(177, 141);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl3;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 313);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(177, 178);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.ContentVisible = false;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(177, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(6, 491);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 141);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.panel2;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 147);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(177, 160);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // splitterItem3
            // 
            this.splitterItem3.AllowHotTrack = true;
            this.splitterItem3.CustomizationFormText = "splitterItem3";
            this.splitterItem3.Location = new System.Drawing.Point(0, 307);
            this.splitterItem3.Name = "splitterItem3";
            this.splitterItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 31);
            this.label1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(109, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 17);
            this.label3.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 64);
            this.panel1.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(85, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "智能人像抓拍系统";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(24, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(55, 47);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 0;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.UseFading = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchPic,
            this.toolStripSeparator2,
            this.videoSearch,
            this.playRelateVideo,
            this.toolStripSeparator1,
            this.options,
            this.toolStripSeparator3,
            this.aboutButton,
            this.tsbMonitoring,
            this.toolStripSeparator5,
            this.enhanceImg,
            this.toolStripButton1,
            this.faceRecognize,
            this.testButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 64);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(897, 25);
            this.mainToolStrip.TabIndex = 4;
            this.mainToolStrip.Text = "toolStrip2";
            // 
            // searchPic
            // 
            this.searchPic.Image = ((System.Drawing.Image)(resources.GetObject("searchPic.Image")));
            this.searchPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.searchPic.Name = "searchPic";
            this.searchPic.Size = new System.Drawing.Size(88, 22);
            this.searchPic.Text = "图像检索";
            this.searchPic.Click += new System.EventHandler(this.searchPic_Click);
            // 
            // videoSearch
            // 
            this.videoSearch.Image = ((System.Drawing.Image)(resources.GetObject("videoSearch.Image")));
            this.videoSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.videoSearch.Name = "videoSearch";
            this.videoSearch.Size = new System.Drawing.Size(88, 22);
            this.videoSearch.Text = "视频检索";
            this.videoSearch.Click += new System.EventHandler(this.videoSearch_Click);
            // 
            // playRelateVideo
            // 
            this.playRelateVideo.Image = ((System.Drawing.Image)(resources.GetObject("playRelateVideo.Image")));
            this.playRelateVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playRelateVideo.Name = "playRelateVideo";
            this.playRelateVideo.Size = new System.Drawing.Size(118, 22);
            this.playRelateVideo.Text = "播放相关视频";
            this.playRelateVideo.Click += new System.EventHandler(this.playRelateVideo_Click);
            // 
            // options
            // 
            this.options.Image = ((System.Drawing.Image)(resources.GetObject("options.Image")));
            this.options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(88, 22);
            this.options.Text = "系统设置";
            this.options.Click += new System.EventHandler(this.options_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
            this.aboutButton.IsLink = true;
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(84, 22);
            this.aboutButton.Text = "关于系统";
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // tsbMonitoring
            // 
            this.tsbMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tsbMonitoring.Image")));
            this.tsbMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMonitoring.Name = "tsbMonitoring";
            this.tsbMonitoring.Size = new System.Drawing.Size(58, 22);
            this.tsbMonitoring.Text = "布控";
            this.tsbMonitoring.Click += new System.EventHandler(this.tsbMonitoring_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // enhanceImg
            // 
            this.enhanceImg.Image = ((System.Drawing.Image)(resources.GetObject("enhanceImg.Image")));
            this.enhanceImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enhanceImg.Name = "enhanceImg";
            this.enhanceImg.Size = new System.Drawing.Size(88, 22);
            this.enhanceImg.Text = "图片增强";
            this.enhanceImg.Click += new System.EventHandler(this.enhanceImg_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton1.Text = "设置背景";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // faceRecognize
            // 
            this.faceRecognize.CheckOnClick = true;
            this.faceRecognize.Enabled = false;
            this.faceRecognize.Image = ((System.Drawing.Image)(resources.GetObject("faceRecognize.Image")));
            this.faceRecognize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.faceRecognize.Name = "faceRecognize";
            this.faceRecognize.Size = new System.Drawing.Size(88, 22);
            this.faceRecognize.Text = "人脸比对";
            this.faceRecognize.Visible = false;
            this.faceRecognize.CheckedChanged += new System.EventHandler(this.faceRecognize_CheckedChanged);
            // 
            // testButton
            // 
            this.testButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.testButton.Image = ((System.Drawing.Image)(resources.GetObject("testButton.Image")));
            this.testButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(23, 22);
            this.testButton.Text = "test";
            this.testButton.Visible = false;
            // 
            // diskSpaceCheckTimer
            // 
            this.diskSpaceCheckTimer.Interval = 600000;
            this.diskSpaceCheckTimer.Tick += new System.EventHandler(this.diskSpaceCheckTimer_Tick);
            // 
            // alertControl1
            // 
            this.alertControl1.ShowPinButton = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 609);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "智能人像抓拍系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.liveFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axCamImgCtrl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.contextMenuStripForCamTreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private Damany.Windows.Form.SquareListView squareListView1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ImageList cameraImageList;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView cameraTree;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel statusOutputFolder;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.Timer realTimer;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusCPUMemUsage;
        private AxIMGCTRLLib.AxCamImgCtrl axCamImgCtrl1;
        private System.Windows.Forms.ToolStripButton searchPic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton videoSearch;
        private System.Windows.Forms.ToolStripButton playRelateVideo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton options;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel aboutButton;
        private System.Windows.Forms.ToolStripButton enhanceImg;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbMonitoring;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForCamTreeView;
        private System.Windows.Forms.ToolStripMenuItem SetupCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewCameraToolStripMenuItem;
        private System.Windows.Forms.Timer diskSpaceCheckTimer;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton faceRecognize;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.ToolStripButton testButton;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private System.Windows.Forms.PictureBox liveFace;

    }
}

