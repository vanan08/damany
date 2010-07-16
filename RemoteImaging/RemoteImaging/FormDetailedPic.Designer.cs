namespace RemoteImaging
{
    partial class FormDetailedPic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetailedPic));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.captureTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Sharpen = new System.Windows.Forms.ToolStripButton();
            this.Restore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.Contrast = new System.Windows.Forms.ToolStripButton();
            this.Brightness = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 25);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(382, 249);
            this.pictureEdit1.TabIndex = 0;
            // 
            // captureTime
            // 
            this.captureTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.captureTime.Location = new System.Drawing.Point(85, 22);
            this.captureTime.Name = "captureTime";
            this.captureTime.ReadOnly = true;
            this.captureTime.Size = new System.Drawing.Size(276, 21);
            this.captureTime.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "抓拍时间：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.captureTime);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 61);
            this.panel1.TabIndex = 25;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Brightness,
            this.Contrast,
            this.Sharpen,
            this.toolStripSeparator1,
            this.Restore,
            this.Save});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(382, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Sharpen
            // 
            this.Sharpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Sharpen.Name = "Sharpen";
            this.Sharpen.Size = new System.Drawing.Size(42, 22);
            this.Sharpen.Text = "锐化";
            this.Sharpen.Click += new System.EventHandler(this.Sharpen_Click);
            // 
            // Restore
            // 
            this.Restore.Enabled = false;
            this.Restore.Image = ((System.Drawing.Image)(resources.GetObject("Restore.Image")));
            this.Restore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Restore.Name = "Restore";
            this.Restore.Size = new System.Drawing.Size(58, 22);
            this.Restore.Text = "复原";
            this.Restore.Click += new System.EventHandler(this.Restore_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Save
            // 
            this.Save.Enabled = false;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(58, 22);
            this.Save.Text = "保存";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Contrast
            // 
            this.Contrast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Contrast.Name = "Contrast";
            this.Contrast.Size = new System.Drawing.Size(57, 22);
            this.Contrast.Text = "对比度";
            // 
            // Brightness
            // 
            this.Brightness.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Brightness.Name = "Brightness";
            this.Brightness.Size = new System.Drawing.Size(42, 22);
            this.Brightness.Text = "亮度";
            this.Brightness.Click += new System.EventHandler(this.Brightness_Click);
            // 
            // FormDetailedPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 335);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.MinimizeBox = false;
            this.Name = "FormDetailedPic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "图片详细信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDetailedPic_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.TextBox captureTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Sharpen;
        private System.Windows.Forms.ToolStripButton Restore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Brightness;
        private System.Windows.Forms.ToolStripButton Contrast;
        private System.Windows.Forms.ToolStripButton Save;
    }
}