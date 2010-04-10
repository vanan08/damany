﻿namespace RemoteImaging.RealtimeDisplay
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.zoomPicBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.liveFace = new System.Windows.Forms.PictureBox();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
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
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
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
            this.tsbMonitoring = new System.Windows.Forms.ToolStripButton();
            this.faceLibBuilder = new System.Windows.Forms.ToolStripButton();
            this.faceCompare = new System.Windows.Forms.ToolStripButton();
            this.alermForm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.enhanceImg = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.diskSpaceCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相关视频ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.报警窗口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.工具栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人像栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备树ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人脸特征库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频检索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像检索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.比对查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.liveFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.mainToolStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusOutputFolder,
            this.statusCPUMemUsage,
            this.statusProgressBar,
            this.statusTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 581);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(948, 28);
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
            this.statusOutputFolder.Size = new System.Drawing.Size(161, 23);
            this.statusOutputFolder.Text = "toolStripStatusLabel2";
            this.statusOutputFolder.Click += new System.EventHandler(this.statusOutputFolder_Click);
            // 
            // statusCPUMemUsage
            // 
            this.statusCPUMemUsage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusCPUMemUsage.Name = "statusCPUMemUsage";
            this.statusCPUMemUsage.Size = new System.Drawing.Size(145, 23);
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
            this.statusTime.Size = new System.Drawing.Size(627, 23);
            this.statusTime.Spring = true;
            this.statusTime.Text = "toolStripStatusLabel1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.panel2);
            this.layoutControl1.Controls.Add(this.simpleButton2);
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3});
            this.layoutControl1.Location = new System.Drawing.Point(0, 50);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(948, 531);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.zoomPicBox);
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Location = new System.Drawing.Point(175, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(766, 517);
            this.panelControl1.TabIndex = 10;
            this.panelControl1.SizeChanged += new System.EventHandler(this.panelControl1_SizeChanged);
            // 
            // zoomPicBox
            // 
            this.zoomPicBox.BackColor = System.Drawing.SystemColors.Window;
            this.zoomPicBox.Location = new System.Drawing.Point(432, 74);
            this.zoomPicBox.Name = "zoomPicBox";
            this.zoomPicBox.Size = new System.Drawing.Size(180, 157);
            this.zoomPicBox.TabIndex = 7;
            this.zoomPicBox.TabStop = false;
            this.zoomPicBox.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox12, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox11, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox10, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox7, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(23, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(387, 327);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.liveFace);
            this.panel2.Controls.Add(this.panelControl6);
            this.panel2.Location = new System.Drawing.Point(7, 317);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 207);
            this.panel2.TabIndex = 13;
            // 
            // liveFace
            // 
            this.liveFace.BackColor = System.Drawing.SystemColors.Window;
            this.liveFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.liveFace.Location = new System.Drawing.Point(0, 24);
            this.liveFace.Name = "liveFace";
            this.liveFace.Size = new System.Drawing.Size(152, 183);
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
            this.panelControl6.Size = new System.Drawing.Size(152, 24);
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
            this.panelControl2.Size = new System.Drawing.Size(152, 294);
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
            this.cameraTree.Size = new System.Drawing.Size(152, 270);
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
            this.contextMenuStripForCamTreeView.Size = new System.Drawing.Size(105, 52);
            // 
            // ViewCameraToolStripMenuItem
            // 
            this.ViewCameraToolStripMenuItem.Name = "ViewCameraToolStripMenuItem";
            this.ViewCameraToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.ViewCameraToolStripMenuItem.Text = "查看";
            this.ViewCameraToolStripMenuItem.Click += new System.EventHandler(this.ViewCameraToolStripMenuItem_Click);
            // 
            // SetupCameraToolStripMenuItem
            // 
            this.SetupCameraToolStripMenuItem.Name = "SetupCameraToolStripMenuItem";
            this.SetupCameraToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
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
            this.panelControl4.Size = new System.Drawing.Size(152, 24);
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
            this.splitterItem2,
            this.splitterItem1,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(948, 531);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(168, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(776, 527);
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
            this.layoutControlItem2.Size = new System.Drawing.Size(162, 304);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.ContentVisible = false;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(162, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(6, 527);
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(0, 304);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.panel2;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 310);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(162, 217);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
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
            this.mainToolStrip.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchPic,
            this.toolStripSeparator2,
            this.videoSearch,
            this.playRelateVideo,
            this.toolStripSeparator1,
            this.options,
            this.toolStripSeparator3,
            this.tsbMonitoring,
            this.faceLibBuilder,
            this.faceCompare,
            this.alermForm,
            this.toolStripSeparator5,
            this.enhanceImg,
            this.toolStripButton1});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 25);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(948, 25);
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
            // tsbMonitoring
            // 
            this.tsbMonitoring.Image = ((System.Drawing.Image)(resources.GetObject("tsbMonitoring.Image")));
            this.tsbMonitoring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMonitoring.Name = "tsbMonitoring";
            this.tsbMonitoring.Size = new System.Drawing.Size(58, 22);
            this.tsbMonitoring.Text = "布控";
            this.tsbMonitoring.Click += new System.EventHandler(this.tsbMonitoring_Click);
            // 
            // faceLibBuilder
            // 
            this.faceLibBuilder.Image = ((System.Drawing.Image)(resources.GetObject("faceLibBuilder.Image")));
            this.faceLibBuilder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.faceLibBuilder.Name = "faceLibBuilder";
            this.faceLibBuilder.Size = new System.Drawing.Size(103, 22);
            this.faceLibBuilder.Text = "人脸特征库";
            this.faceLibBuilder.Click += new System.EventHandler(this.faceLibBuilder_Click);
            // 
            // faceCompare
            // 
            this.faceCompare.Image = ((System.Drawing.Image)(resources.GetObject("faceCompare.Image")));
            this.faceCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.faceCompare.Name = "faceCompare";
            this.faceCompare.Size = new System.Drawing.Size(118, 22);
            this.faceCompare.Text = "人脸比对查询";
            this.faceCompare.CheckedChanged += new System.EventHandler(this.faceRecognize_CheckedChanged);
            this.faceCompare.Click += new System.EventHandler(this.faceRecognize_Click);
            // 
            // alermForm
            // 
            this.alermForm.Image = ((System.Drawing.Image)(resources.GetObject("alermForm.Image")));
            this.alermForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alermForm.Name = "alermForm";
            this.alermForm.Size = new System.Drawing.Size(88, 22);
            this.alermForm.Text = "报警窗口";
            this.alermForm.Click += new System.EventHandler(this.alermForm_Click);
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
            this.toolStripButton1.Size = new System.Drawing.Size(88, 20);
            this.toolStripButton1.Text = "设置背景";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.检索ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相关视频ToolStripMenuItem,
            this.报警窗口ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.工具栏ToolStripMenuItem,
            this.人像栏ToolStripMenuItem,
            this.设备树ToolStripMenuItem});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.查看ToolStripMenuItem.Text = "查看";
            // 
            // 相关视频ToolStripMenuItem
            // 
            this.相关视频ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("相关视频ToolStripMenuItem.Image")));
            this.相关视频ToolStripMenuItem.Name = "相关视频ToolStripMenuItem";
            this.相关视频ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.相关视频ToolStripMenuItem.Text = "相关视频";
            // 
            // 报警窗口ToolStripMenuItem
            // 
            this.报警窗口ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("报警窗口ToolStripMenuItem.Image")));
            this.报警窗口ToolStripMenuItem.Name = "报警窗口ToolStripMenuItem";
            this.报警窗口ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.报警窗口ToolStripMenuItem.Text = "报警窗口";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 6);
            // 
            // 工具栏ToolStripMenuItem
            // 
            this.工具栏ToolStripMenuItem.Name = "工具栏ToolStripMenuItem";
            this.工具栏ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.工具栏ToolStripMenuItem.Text = "工具栏";
            // 
            // 人像栏ToolStripMenuItem
            // 
            this.人像栏ToolStripMenuItem.Name = "人像栏ToolStripMenuItem";
            this.人像栏ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.人像栏ToolStripMenuItem.Text = "人像栏";
            // 
            // 设备树ToolStripMenuItem
            // 
            this.设备树ToolStripMenuItem.Name = "设备树ToolStripMenuItem";
            this.设备树ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.设备树ToolStripMenuItem.Text = "设备树";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统设置ToolStripMenuItem,
            this.人脸特征库管理ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("系统设置ToolStripMenuItem.Image")));
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            // 
            // 人脸特征库管理ToolStripMenuItem
            // 
            this.人脸特征库管理ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("人脸特征库管理ToolStripMenuItem.Image")));
            this.人脸特征库管理ToolStripMenuItem.Name = "人脸特征库管理ToolStripMenuItem";
            this.人脸特征库管理ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.人脸特征库管理ToolStripMenuItem.Text = "人脸特征库管理";
            // 
            // 检索ToolStripMenuItem
            // 
            this.检索ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.视频检索ToolStripMenuItem,
            this.图像检索ToolStripMenuItem,
            this.比对查询ToolStripMenuItem});
            this.检索ToolStripMenuItem.Name = "检索ToolStripMenuItem";
            this.检索ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.检索ToolStripMenuItem.Text = "检索";
            // 
            // 视频检索ToolStripMenuItem
            // 
            this.视频检索ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("视频检索ToolStripMenuItem.Image")));
            this.视频检索ToolStripMenuItem.Name = "视频检索ToolStripMenuItem";
            this.视频检索ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.视频检索ToolStripMenuItem.Text = "视频检索";
            // 
            // 图像检索ToolStripMenuItem
            // 
            this.图像检索ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("图像检索ToolStripMenuItem.Image")));
            this.图像检索ToolStripMenuItem.Name = "图像检索ToolStripMenuItem";
            this.图像检索ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.图像检索ToolStripMenuItem.Text = "图像检索";
            // 
            // 比对查询ToolStripMenuItem
            // 
            this.比对查询ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("比对查询ToolStripMenuItem.Image")));
            this.比对查询ToolStripMenuItem.Name = "比对查询ToolStripMenuItem";
            this.比对查询ToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.比对查询ToolStripMenuItem.Text = "比对查询";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(48, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutToolStripMenuItem.Image")));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.aboutToolStripMenuItem.Text = "关于系统";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panelControl5
            // 
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl5.Controls.Add(this.pictureBox2);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl5.Location = new System.Drawing.Point(0, 0);
            this.panelControl5.Name = "panelControl5";
            this.panelControl5.Size = new System.Drawing.Size(172, 24);
            this.panelControl5.TabIndex = 1;
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
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(122, 103);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox5.Location = new System.Drawing.Point(260, 3);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(124, 103);
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox6.Location = new System.Drawing.Point(3, 112);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(122, 103);
            this.pictureBox6.TabIndex = 10;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox7.Location = new System.Drawing.Point(131, 112);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(123, 103);
            this.pictureBox7.TabIndex = 11;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox8.Location = new System.Drawing.Point(260, 112);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(124, 103);
            this.pictureBox8.TabIndex = 12;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox9.Location = new System.Drawing.Point(3, 221);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(122, 103);
            this.pictureBox9.TabIndex = 13;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox10.Location = new System.Drawing.Point(131, 221);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(123, 103);
            this.pictureBox10.TabIndex = 14;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox11.Location = new System.Drawing.Point(260, 221);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(124, 103);
            this.pictureBox11.TabIndex = 15;
            this.pictureBox11.TabStop = false;
            this.pictureBox11.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox12.Location = new System.Drawing.Point(131, 3);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(123, 103);
            this.pictureBox12.TabIndex = 16;
            this.pictureBox12.TabStop = false;
            this.pictureBox12.DoubleClick += new System.EventHandler(this.zoomPicBox_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 609);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zoomPicBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.liveFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
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
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList cameraImageList;
        private System.Windows.Forms.TreeView cameraTree;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel statusOutputFolder;
        private System.Windows.Forms.ToolStripStatusLabel statusTime;
        private System.Windows.Forms.Timer realTimer;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel statusCPUMemUsage;
        private System.Windows.Forms.ToolStripButton searchPic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton videoSearch;
        private System.Windows.Forms.ToolStripButton playRelateVideo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton options;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
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
        private System.Windows.Forms.ToolStripButton faceCompare;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.ToolStripButton alermForm;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PanelControl panelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.PictureBox liveFace;
        private System.Windows.Forms.ToolStripButton faceLibBuilder;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 检索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频检索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像检索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相关视频ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人脸特征库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 比对查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 报警窗口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 工具栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人像栏ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备树ToolStripMenuItem;
        private System.Windows.Forms.PictureBox zoomPicBox;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox3;

    }
}

