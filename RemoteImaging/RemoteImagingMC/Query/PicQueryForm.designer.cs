namespace RemoteImaging.Query
{
    partial class PicQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicQueryForm));
            this.queryBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hostsComboBox = new System.Windows.Forms.ComboBox();
            this.searchFromTime = new DevExpress.XtraEditors.TimeEdit();
            this.searchToTime = new DevExpress.XtraEditors.TimeEdit();
            this.facesList = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.facesListView = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.facePictureBox = new System.Windows.Forms.PictureBox();
            this.labelCaptureLoc = new System.Windows.Forms.Label();
            this.labelCaptureTime = new System.Windows.Forms.Label();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wholeImage = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirstPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrePage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelCurPage = new System.Windows.Forms.ToolStripLabel();
            this.pageSizeComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlayVideo = new System.Windows.Forms.ToolStripButton();
            this.downloadVideoFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveVideoFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.searchFromTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchToTime.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wholeImage)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // queryBtn
            // 
            this.queryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBtn.Location = new System.Drawing.Point(724, 26);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(86, 23);
            this.queryBtn.TabIndex = 0;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "监控点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "从:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "到:";
            // 
            // hostsComboBox
            // 
            this.hostsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hostsComboBox.FormattingEnabled = true;
            this.hostsComboBox.Location = new System.Drawing.Point(59, 27);
            this.hostsComboBox.Name = "hostsComboBox";
            this.hostsComboBox.Size = new System.Drawing.Size(119, 20);
            this.hostsComboBox.TabIndex = 4;
            // 
            // searchFromTime
            // 
            this.searchFromTime.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.searchFromTime.Location = new System.Drawing.Point(213, 27);
            this.searchFromTime.Name = "searchFromTime";
            this.searchFromTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchFromTime.Properties.Mask.EditMask = "f";
            this.searchFromTime.Size = new System.Drawing.Size(206, 23);
            this.searchFromTime.TabIndex = 7;
            // 
            // searchToTime
            // 
            this.searchToTime.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.searchToTime.Location = new System.Drawing.Point(499, 27);
            this.searchToTime.Name = "searchToTime";
            this.searchToTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchToTime.Properties.Mask.EditMask = "f";
            this.searchToTime.Size = new System.Drawing.Size(205, 23);
            this.searchToTime.TabIndex = 8;
            // 
            // facesList
            // 
            this.facesList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.facesList.ImageSize = new System.Drawing.Size(80, 60);
            this.facesList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(80, 60);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // facesListView
            // 
            this.facesListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.facesListView.AutoArrange = false;
            this.facesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.facesListView.HideSelection = false;
            this.facesListView.Location = new System.Drawing.Point(0, 27);
            this.facesListView.MultiSelect = false;
            this.facesListView.Name = "facesListView";
            this.facesListView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.facesListView.Size = new System.Drawing.Size(840, 256);
            this.facesListView.TabIndex = 10;
            this.facesListView.UseCompatibleStateImageBehavior = false;
            this.facesListView.ItemActivate += new System.EventHandler(this.bestPicListView_ItemActivate);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.hostsComboBox);
            this.groupBox3.Controls.Add(this.queryBtn);
            this.groupBox3.Controls.Add(this.searchFromTime);
            this.groupBox3.Controls.Add(this.searchToTime);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(840, 70);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.layoutControl1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(184, 185);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "放大显示";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.facePictureBox);
            this.layoutControl1.Controls.Add(this.labelCaptureLoc);
            this.layoutControl1.Controls.Add(this.labelCaptureTime);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(3, 17);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(178, 165);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // facePictureBox
            // 
            this.facePictureBox.Location = new System.Drawing.Point(7, 7);
            this.facePictureBox.Name = "facePictureBox";
            this.facePictureBox.Size = new System.Drawing.Size(164, 86);
            this.facePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.facePictureBox.TabIndex = 19;
            this.facePictureBox.TabStop = false;
            // 
            // labelCaptureLoc
            // 
            this.labelCaptureLoc.Location = new System.Drawing.Point(7, 103);
            this.labelCaptureLoc.Name = "labelCaptureLoc";
            this.labelCaptureLoc.Size = new System.Drawing.Size(164, 22);
            this.labelCaptureLoc.TabIndex = 17;
            this.labelCaptureLoc.Text = "抓拍地点：";
            this.labelCaptureLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCaptureTime
            // 
            this.labelCaptureTime.Location = new System.Drawing.Point(7, 135);
            this.labelCaptureTime.Name = "labelCaptureTime";
            this.labelCaptureTime.Size = new System.Drawing.Size(164, 23);
            this.labelCaptureTime.TabIndex = 18;
            this.labelCaptureTime.Text = "抓拍时间：";
            this.labelCaptureTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(178, 165);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelCaptureTime;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 33);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(31, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(174, 33);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.labelCaptureLoc;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(31, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(174, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.facePictureBox;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(174, 96);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 70);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.facesListView);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(840, 472);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 20;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel2.Controls.Add(this.splitter1);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(840, 185);
            this.splitContainer2.SplitterDistance = 184;
            this.splitContainer2.TabIndex = 21;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.axVLCPlugin21);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(442, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 185);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "视频";
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(3, 17);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(204, 165);
            this.axVLCPlugin21.TabIndex = 31;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(439, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 185);
            this.splitter1.TabIndex = 18;
            this.splitter1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wholeImage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 185);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "全身像";
            // 
            // wholeImage
            // 
            this.wholeImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wholeImage.Location = new System.Drawing.Point(3, 17);
            this.wholeImage.Name = "wholeImage";
            this.wholeImage.Size = new System.Drawing.Size(433, 165);
            this.wholeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wholeImage.TabIndex = 16;
            this.wholeImage.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFirstPage,
            this.toolStripButtonPrePage,
            this.toolStripButtonNextPage,
            this.toolStripButtonLastPage,
            this.toolStripLabelCurPage,
            this.pageSizeComboBox,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButtonPlayVideo,
            this.downloadVideoFile,
            this.toolStripSeparator1,
            this.saveToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(840, 27);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonFirstPage
            // 
            this.toolStripButtonFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFirstPage.Image")));
            this.toolStripButtonFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFirstPage.Name = "toolStripButtonFirstPage";
            this.toolStripButtonFirstPage.Size = new System.Drawing.Size(55, 24);
            this.toolStripButtonFirstPage.Text = "首页";
            this.toolStripButtonFirstPage.Click += new System.EventHandler(this.toolStripButtonFirstPage_Click);
            // 
            // toolStripButtonPrePage
            // 
            this.toolStripButtonPrePage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrePage.Image")));
            this.toolStripButtonPrePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrePage.Name = "toolStripButtonPrePage";
            this.toolStripButtonPrePage.Size = new System.Drawing.Size(55, 24);
            this.toolStripButtonPrePage.Text = "上页";
            this.toolStripButtonPrePage.Click += new System.EventHandler(this.toolStripButtonPrePage_Click);
            // 
            // toolStripButtonNextPage
            // 
            this.toolStripButtonNextPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNextPage.Image")));
            this.toolStripButtonNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNextPage.Name = "toolStripButtonNextPage";
            this.toolStripButtonNextPage.Size = new System.Drawing.Size(55, 24);
            this.toolStripButtonNextPage.Text = "下页";
            this.toolStripButtonNextPage.Click += new System.EventHandler(this.toolStripButtonNextPage_Click);
            // 
            // toolStripButtonLastPage
            // 
            this.toolStripButtonLastPage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLastPage.Image")));
            this.toolStripButtonLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLastPage.Name = "toolStripButtonLastPage";
            this.toolStripButtonLastPage.Size = new System.Drawing.Size(55, 24);
            this.toolStripButtonLastPage.Text = "末页";
            this.toolStripButtonLastPage.Click += new System.EventHandler(this.toolStripButtonLastPage_Click);
            // 
            // toolStripLabelCurPage
            // 
            this.toolStripLabelCurPage.Name = "toolStripLabelCurPage";
            this.toolStripLabelCurPage.Size = new System.Drawing.Size(57, 24);
            this.toolStripLabelCurPage.Text = "第1/1页";
            // 
            // pageSizeComboBox
            // 
            this.pageSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pageSizeComboBox.Items.AddRange(new object[] {
            "20",
            "30",
            "40",
            "50"});
            this.pageSizeComboBox.Name = "pageSizeComboBox";
            this.pageSizeComboBox.Size = new System.Drawing.Size(121, 27);
            this.pageSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPageSize_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 24);
            this.toolStripLabel1.Text = "张/页";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonPlayVideo
            // 
            this.toolStripButtonPlayVideo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlayVideo.Image")));
            this.toolStripButtonPlayVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlayVideo.Name = "toolStripButtonPlayVideo";
            this.toolStripButtonPlayVideo.Size = new System.Drawing.Size(81, 24);
            this.toolStripButtonPlayVideo.Text = "播放视频";
            this.toolStripButtonPlayVideo.Click += new System.EventHandler(this.toolStripButtonPlayVideo_Click);
            // 
            // downloadVideoFile
            // 
            this.downloadVideoFile.Image = ((System.Drawing.Image)(resources.GetObject("downloadVideoFile.Image")));
            this.downloadVideoFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.downloadVideoFile.Name = "downloadVideoFile";
            this.downloadVideoFile.Size = new System.Drawing.Size(81, 24);
            this.downloadVideoFile.Text = "保存视频";
            this.downloadVideoFile.Click += new System.EventHandler(this.downloadVideoFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(97, 24);
            this.saveToolStripButton.Text = "保存图片(&S)";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // saveVideoFileDialog
            // 
            this.saveVideoFileDialog.DefaultExt = "m4v";
            this.saveVideoFileDialog.Filter = "视频文件 (*.m4v)|*.m4v";
            this.saveVideoFileDialog.RestoreDirectory = true;
            // 
            // PicQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 542);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PicQueryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "搜索图片";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PicQueryForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PicQueryForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PicQueryForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.searchFromTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchToTime.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.facePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wholeImage)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox hostsComboBox;
        private DevExpress.XtraEditors.TimeEdit searchFromTime;
        private DevExpress.XtraEditors.TimeEdit searchToTime;
        private System.Windows.Forms.ImageList facesList;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ListView facesListView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelCaptureTime;
        private System.Windows.Forms.Label labelCaptureLoc;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.PictureBox facePictureBox;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonFirstPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrePage;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextPage;
        private System.Windows.Forms.ToolStripButton toolStripButtonLastPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabelCurPage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox pageSizeComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlayVideo;
        private System.Windows.Forms.PictureBox wholeImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Splitter splitter1;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private System.Windows.Forms.ToolStripButton downloadVideoFile;
        private System.Windows.Forms.SaveFileDialog saveVideoFileDialog;
    }
}