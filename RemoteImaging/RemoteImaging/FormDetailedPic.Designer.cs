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
            this.Image = new DevExpress.XtraEditors.PictureEdit();
            this.captureTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Image.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image.Location = new System.Drawing.Point(0, 0);
            this.Image.Name = "Image";
            this.Image.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.Image.Size = new System.Drawing.Size(382, 274);
            this.Image.TabIndex = 0;
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
            // FormDetailedPic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 335);
            this.Controls.Add(this.Image);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "FormDetailedPic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "图片详细信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDetailedPic_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.Image.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.PictureEdit Image;
        public System.Windows.Forms.TextBox captureTime;
    }
}