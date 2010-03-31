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
            this.cameraIdCombo = new System.Windows.Forms.ComboBox();
            this.timeFrom = new DevExpress.XtraEditors.TimeEdit();
            this.timeTo = new DevExpress.XtraEditors.TimeEdit();
            this.faceImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.facesListView = new Damany.RemoteImaging.Common.Controls.DoubleBufferedListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.machineCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.currentFace = new System.Windows.Forms.PictureBox();
            this.labelCaptureLoc = new System.Windows.Forms.Label();
            this.labelCaptureTime = new System.Windows.Forms.Label();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.wholePicture = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFirstPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrePage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelCurPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxPageSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlayVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTo.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentFace)).BeginInit();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // queryBtn
            // 
            this.queryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBtn.Location = new System.Drawing.Point(814, 26);
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
            this.label1.Location = new System.Drawing.Point(182, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "摄像机编号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "从：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "到：";
            // 
            // cameraIdCombo
            // 
            this.cameraIdCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraIdCombo.FormattingEnabled = true;
            this.cameraIdCombo.Location = new System.Drawing.Point(265, 27);
            this.cameraIdCombo.Name = "cameraIdCombo";
            this.cameraIdCombo.Size = new System.Drawing.Size(83, 20);
            this.cameraIdCombo.TabIndex = 4;
            // 
            // timeFrom
            // 
            this.timeFrom.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeFrom.Location = new System.Drawing.Point(400, 26);
            this.timeFrom.Name = "timeFrom";
            this.timeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeFrom.Properties.Mask.EditMask = "F";
            this.timeFrom.Size = new System.Drawing.Size(166, 21);
            this.timeFrom.TabIndex = 7;
            // 
            // timeTo
            // 
            this.timeTo.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeTo.Location = new System.Drawing.Point(624, 26);
            this.timeTo.Name = "timeTo";
            this.timeTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeTo.Properties.Mask.EditMask = "F";
            this.timeTo.Size = new System.Drawing.Size(170, 21);
            this.timeTo.TabIndex = 8;
            // 
            // faceImageList
            // 
            this.faceImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.faceImageList.ImageSize = new System.Drawing.Size(80, 60);
            this.faceImageList.TransparentColor = System.Drawing.Color.Transparent;
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
            this.facesListView.Size = new System.Drawing.Size(930, 251);
            this.facesListView.TabIndex = 10;
            this.facesListView.UseCompatibleStateImageBehavior = false;
            this.facesListView.ItemActivate += new System.EventHandler(this.secPicListView_ItemActive);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.machineCombo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cameraIdCombo);
            this.groupBox3.Controls.Add(this.queryBtn);
            this.groupBox3.Controls.Add(this.timeFrom);
            this.groupBox3.Controls.Add(this.timeTo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(930, 70);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // machineCombo
            // 
            this.machineCombo.FormattingEnabled = true;
            this.machineCombo.Location = new System.Drawing.Point(60, 27);
            this.machineCombo.Name = "machineCombo";
            this.machineCombo.Size = new System.Drawing.Size(105, 20);
            this.machineCombo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "主机：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.layoutControl1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 190);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "放大显示";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.currentFace);
            this.layoutControl1.Controls.Add(this.labelCaptureLoc);
            this.layoutControl1.Controls.Add(this.labelCaptureTime);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(3, 17);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(306, 170);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // currentFace
            // 
            this.currentFace.Location = new System.Drawing.Point(12, 12);
            this.currentFace.Name = "currentFace";
            this.currentFace.Size = new System.Drawing.Size(282, 81);
            this.currentFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentFace.TabIndex = 19;
            this.currentFace.TabStop = false;
            // 
            // labelCaptureLoc
            // 
            this.labelCaptureLoc.Location = new System.Drawing.Point(12, 97);
            this.labelCaptureLoc.Name = "labelCaptureLoc";
            this.labelCaptureLoc.Size = new System.Drawing.Size(282, 28);
            this.labelCaptureLoc.TabIndex = 17;
            this.labelCaptureLoc.Text = "抓拍地点：";
            this.labelCaptureLoc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCaptureTime
            // 
            this.labelCaptureTime.Location = new System.Drawing.Point(12, 129);
            this.labelCaptureTime.Name = "labelCaptureTime";
            this.labelCaptureTime.Size = new System.Drawing.Size(282, 29);
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(306, 170);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelCaptureTime;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 117);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 33);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(31, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(286, 33);
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
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 85);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(31, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(286, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.currentFace;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(286, 85);
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
            this.splitContainer1.Size = new System.Drawing.Size(930, 472);
            this.splitContainer1.SplitterDistance = 190;
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
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(930, 190);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.wholePicture);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 190);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "全身像";
            // 
            // wholePicture
            // 
            this.wholePicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wholePicture.Location = new System.Drawing.Point(3, 17);
            this.wholePicture.Name = "wholePicture";
            this.wholePicture.Size = new System.Drawing.Size(608, 170);
            this.wholePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.wholePicture.TabIndex = 16;
            this.wholePicture.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFirstPage,
            this.toolStripButtonPrePage,
            this.toolStripButtonNextPage,
            this.toolStripButtonLastPage,
            this.toolStripLabelCurPage,
            this.toolStripComboBoxPageSize,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButtonPlayVideo,
            this.toolStripSeparator1,
            this.saveToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(930, 27);
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
            // toolStripComboBoxPageSize
            // 
            this.toolStripComboBoxPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxPageSize.Items.AddRange(new object[] {
            "20",
            "30",
            "40",
            "50"});
            this.toolStripComboBoxPageSize.Name = "toolStripComboBoxPageSize";
            this.toolStripComboBoxPageSize.Size = new System.Drawing.Size(121, 27);
            this.toolStripComboBoxPageSize.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxPageSize_SelectedIndexChanged);
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
            this.toolStripButtonPlayVideo.Text = "相关视频";
            this.toolStripButtonPlayVideo.Click += new System.EventHandler(this.toolStripButtonPlayVideo_Click);
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
            // PicQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 542);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PicQueryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "搜索图片";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PicQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTo.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentFace)).EndInit();
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
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cameraIdCombo;
        private DevExpress.XtraEditors.TimeEdit timeFrom;
        private DevExpress.XtraEditors.TimeEdit timeTo;
        private System.Windows.Forms.ImageList faceImageList;
        private System.Windows.Forms.ImageList imageList2;
        private Damany.RemoteImaging.Common.Controls.DoubleBufferedListView facesListView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelCaptureTime;
        private System.Windows.Forms.Label labelCaptureLoc;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.PictureBox currentFace;
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
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxPageSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlayVideo;
        private System.Windows.Forms.PictureBox wholePicture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ComboBox machineCombo;
        private System.Windows.Forms.Label label4;
    }
}