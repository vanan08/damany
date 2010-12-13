namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new Damany.Windows.Form.PictureBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.Utils.Frames.NotePanel8_1();
            this.captureImage = new DevExpress.XtraEditors.SimpleButton();
            this.applyButton = new DevExpress.XtraEditors.SimpleButton();
            this.liveImg = new DevExpress.XtraEditors.PictureEdit();
            this.connectButton = new DevExpress.XtraEditors.SimpleButton();
            this.cameraIp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.snapShots = new Leadtools.WinForms.RasterImageList();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liveImg.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIp.Properties)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.AllowDrop = true;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.DrawRectangle = true;
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(2, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(529, 476);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.FigureDrawn += new System.EventHandler<Damany.Windows.Form.DrawFigureEventArgs>(this.pictureBox1_FigureDrawn);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(529, 66);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl3.Location = new System.Drawing.Point(31, 16);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(162, 33);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "设置虚拟线圈";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(454, 6);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(55, 55);
            this.pictureEdit1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.spinEdit2);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.panelControl2);
            this.panelControl3.Controls.Add(this.captureImage);
            this.panelControl3.Controls.Add(this.applyButton);
            this.panelControl3.Controls.Add(this.liveImg);
            this.panelControl3.Controls.Add(this.connectButton);
            this.panelControl3.Controls.Add(this.cameraIp);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.MinimumSize = new System.Drawing.Size(273, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(273, 551);
            this.panelControl3.TabIndex = 3;
            // 
            // spinEdit2
            // 
            this.spinEdit2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spinEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::WindowsFormsApplication1.Properties.Settings.Default, "SnapCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.spinEdit2.EditValue = global::WindowsFormsApplication1.Properties.Settings.Default.SnapCount;
            this.spinEdit2.Location = new System.Drawing.Point(103, 291);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(46, 21);
            this.spinEdit2.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Location = new System.Drawing.Point(49, 294);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "抓拍张数";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.panelControl2.Location = new System.Drawing.Point(7, 412);
            this.panelControl2.MaxRows = 10;
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(261, 141);
            this.panelControl2.TabIndex = 7;
            this.panelControl2.TabStop = false;
            this.panelControl2.Text = "设置好摄像头IP地址后，点击“连接“按钮。  当实时画面出现后，点击”抓拍“按钮，当前抓拍的图片会出现在大图中。在大图中用鼠标左键单击并拖动，定义一个矩形框。设定" +
                "好矩形框后，点击”应用”按钮，稍候片刻，如果弹出对话框“设置成功\"，则该矩形框设置就被成功应用到系统中。";
            // 
            // captureImage
            // 
            this.captureImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.captureImage.Enabled = false;
            this.captureImage.Location = new System.Drawing.Point(31, 321);
            this.captureImage.Name = "captureImage";
            this.captureImage.Size = new System.Drawing.Size(201, 32);
            this.captureImage.TabIndex = 6;
            this.captureImage.Text = "抓拍";
            this.captureImage.Click += new System.EventHandler(this.captureImage_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(31, 362);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(201, 32);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "应用";
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // liveImg
            // 
            this.liveImg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.liveImg.Location = new System.Drawing.Point(7, 5);
            this.liveImg.Name = "liveImg";
            this.liveImg.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.liveImg.Size = new System.Drawing.Size(261, 235);
            this.liveImg.TabIndex = 3;
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.Location = new System.Drawing.Point(210, 246);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(58, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "连接";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // cameraIp
            // 
            this.cameraIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraIp.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::WindowsFormsApplication1.Properties.Settings.Default, "CameraIp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cameraIp.EditValue = global::WindowsFormsApplication1.Properties.Settings.Default.CameraIp;
            this.cameraIp.Location = new System.Drawing.Point(87, 247);
            this.cameraIp.Name = "cameraIp";
            this.cameraIp.Size = new System.Drawing.Size(117, 21);
            this.cameraIp.TabIndex = 1;
            this.cameraIp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cameraIp_KeyUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 250);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "摄像机地址：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(811, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(32, 17);
            this.statusLabel.Text = "就绪";
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.pictureBox1);
            this.panelControl4.Controls.Add(this.panelControl1);
            this.panelControl4.Controls.Add(this.splitterControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(278, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(533, 551);
            this.panelControl4.TabIndex = 7;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterControl1.Location = new System.Drawing.Point(2, 544);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(529, 5);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // snapShots
            // 
            this.snapShots.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.snapShots.ItemImageSize = new System.Drawing.Size(128, 128);
            this.snapShots.ItemSize = new System.Drawing.Size(150, 150);
            this.snapShots.Location = new System.Drawing.Point(0, 0);
            this.snapShots.Name = "rasterImageList1";
            this.snapShots.Size = new System.Drawing.Size(136, 160);
            this.snapShots.TabIndex = 0;
            this.snapShots.SelectedIndexChanged += new System.EventHandler(this.snapShots_SelectedIndexChanged);
            // 
            // splitterControl2
            // 
            this.splitterControl2.Location = new System.Drawing.Point(273, 0);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(5, 551);
            this.splitterControl2.TabIndex = 8;
            this.splitterControl2.TabStop = false;
            // 
            // Form1
            // 
            this.AcceptButton = this.connectButton;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 573);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 546);
            this.Name = "Form1";
            this.Text = "虚拟线圈标识器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liveImg.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIp.Properties)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Damany.Windows.Form.PictureBox pictureBox1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton connectButton;
        private DevExpress.XtraEditors.TextEdit cameraIp;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton applyButton;
        private DevExpress.XtraEditors.SimpleButton captureImage;
        private DevExpress.XtraEditors.PictureEdit liveImg;
        private DevExpress.Utils.Frames.NotePanel8_1 panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinEdit2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private Leadtools.WinForms.RasterImageList snapShots;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;



    }
}

