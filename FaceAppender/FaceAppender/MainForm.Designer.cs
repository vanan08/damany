namespace FaceAppender
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
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.srcDir = new System.Windows.Forms.TextBox();
            this.dstDir = new System.Windows.Forms.TextBox();
            this.browseForSrcDir = new System.Windows.Forms.Button();
            this.browseForDstDir = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.lastPicture = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelCurrentIni = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastPicture)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "车牌人像融合工具";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "源目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "目标目录：";
            // 
            // srcDir
            // 
            this.srcDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceAppender.Properties.Settings.Default, "SourceDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.srcDir.Location = new System.Drawing.Point(89, 47);
            this.srcDir.Name = "srcDir";
            this.srcDir.ReadOnly = true;
            this.srcDir.Size = new System.Drawing.Size(264, 20);
            this.srcDir.TabIndex = 2;
            this.srcDir.Text = global::FaceAppender.Properties.Settings.Default.SourceDir;
            // 
            // dstDir
            // 
            this.dstDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceAppender.Properties.Settings.Default, "DestDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dstDir.Location = new System.Drawing.Point(89, 76);
            this.dstDir.Name = "dstDir";
            this.dstDir.ReadOnly = true;
            this.dstDir.Size = new System.Drawing.Size(264, 20);
            this.dstDir.TabIndex = 3;
            this.dstDir.Text = global::FaceAppender.Properties.Settings.Default.DestDir;
            // 
            // browseForSrcDir
            // 
            this.browseForSrcDir.Location = new System.Drawing.Point(359, 44);
            this.browseForSrcDir.Name = "browseForSrcDir";
            this.browseForSrcDir.Size = new System.Drawing.Size(75, 25);
            this.browseForSrcDir.TabIndex = 4;
            this.browseForSrcDir.Text = "选择目录";
            this.browseForSrcDir.UseVisualStyleBackColor = true;
            this.browseForSrcDir.Click += new System.EventHandler(this.browseForSrcDir_Click);
            // 
            // browseForDstDir
            // 
            this.browseForDstDir.Location = new System.Drawing.Point(359, 74);
            this.browseForDstDir.Name = "browseForDstDir";
            this.browseForDstDir.Size = new System.Drawing.Size(75, 25);
            this.browseForDstDir.TabIndex = 5;
            this.browseForDstDir.Text = "选择目录";
            this.browseForDstDir.UseVisualStyleBackColor = true;
            this.browseForDstDir.Click += new System.EventHandler(this.browseForDstDir_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(448, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(58, 22);
            this.startButton.Text = "启动";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(58, 22);
            this.stopButton.Text = "停止";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(103, 22);
            this.toolStripButton1.Text = "重置计数器";
            // 
            // lastPicture
            // 
            this.lastPicture.Location = new System.Drawing.Point(12, 105);
            this.lastPicture.Name = "lastPicture";
            this.lastPicture.Size = new System.Drawing.Size(424, 260);
            this.lastPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.lastPicture.TabIndex = 7;
            this.lastPicture.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelCurrentIni,
            this.labelCounter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 370);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(448, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelCurrentIni
            // 
            this.labelCurrentIni.Name = "labelCurrentIni";
            this.labelCurrentIni.Size = new System.Drawing.Size(358, 17);
            this.labelCurrentIni.Spring = true;
            // 
            // labelCounter
            // 
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(75, 17);
            this.labelCounter.Text = "已处理：0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 392);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lastPicture);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.browseForDstDir);
            this.Controls.Add(this.browseForSrcDir);
            this.Controls.Add(this.dstDir);
            this.Controls.Add(this.srcDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "车牌人像融合器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastPicture)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox srcDir;
        private System.Windows.Forms.TextBox dstDir;
        private System.Windows.Forms.Button browseForSrcDir;
        private System.Windows.Forms.Button browseForDstDir;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.PictureBox lastPicture;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelCurrentIni;
        private System.Windows.Forms.ToolStripStatusLabel labelCounter;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

