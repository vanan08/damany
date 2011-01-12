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
            this.lastPicture = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelCurrentIni = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
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
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "目标目录：";
            // 
            // srcDir
            // 
            this.srcDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceAppender.Properties.Settings.Default, "SourceDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.srcDir.Location = new System.Drawing.Point(86, 12);
            this.srcDir.Name = "srcDir";
            this.srcDir.Size = new System.Drawing.Size(264, 21);
            this.srcDir.TabIndex = 2;
            this.srcDir.Text = global::FaceAppender.Properties.Settings.Default.SourceDir;
            // 
            // dstDir
            // 
            this.dstDir.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FaceAppender.Properties.Settings.Default, "DestDir", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dstDir.Location = new System.Drawing.Point(86, 39);
            this.dstDir.Name = "dstDir";
            this.dstDir.Size = new System.Drawing.Size(264, 21);
            this.dstDir.TabIndex = 3;
            this.dstDir.Text = global::FaceAppender.Properties.Settings.Default.DestDir;
            // 
            // browseForSrcDir
            // 
            this.browseForSrcDir.Location = new System.Drawing.Point(356, 10);
            this.browseForSrcDir.Name = "browseForSrcDir";
            this.browseForSrcDir.Size = new System.Drawing.Size(75, 23);
            this.browseForSrcDir.TabIndex = 4;
            this.browseForSrcDir.Text = "选择目录";
            this.browseForSrcDir.UseVisualStyleBackColor = true;
            this.browseForSrcDir.Click += new System.EventHandler(this.browseForSrcDir_Click);
            // 
            // browseForDstDir
            // 
            this.browseForDstDir.Location = new System.Drawing.Point(356, 37);
            this.browseForDstDir.Name = "browseForDstDir";
            this.browseForDstDir.Size = new System.Drawing.Size(75, 23);
            this.browseForDstDir.TabIndex = 5;
            this.browseForDstDir.Text = "选择目录";
            this.browseForDstDir.UseVisualStyleBackColor = true;
            this.browseForDstDir.Click += new System.EventHandler(this.browseForDstDir_Click);
            // 
            // lastPicture
            // 
            this.lastPicture.Location = new System.Drawing.Point(12, 87);
            this.lastPicture.Name = "lastPicture";
            this.lastPicture.Size = new System.Drawing.Size(424, 250);
            this.lastPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.lastPicture.TabIndex = 7;
            this.lastPicture.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelCurrentIni,
            this.labelCounter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 340);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(448, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelCurrentIni
            // 
            this.labelCurrentIni.Name = "labelCurrentIni";
            this.labelCurrentIni.Size = new System.Drawing.Size(370, 17);
            this.labelCurrentIni.Spring = true;
            // 
            // labelCounter
            // 
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(63, 17);
            this.labelCounter.Text = "已处理：0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 362);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lastPicture);
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
        private System.Windows.Forms.PictureBox lastPicture;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelCurrentIni;
        private System.Windows.Forms.ToolStripStatusLabel labelCounter;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

