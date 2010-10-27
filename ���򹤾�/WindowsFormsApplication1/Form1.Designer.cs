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
            this.applyButton = new DevExpress.XtraEditors.SimpleButton();
            this.peerAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.connectButton = new DevExpress.XtraEditors.SimpleButton();
            this.cameraIp = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.AllowDrop = true;
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.DrawRectangle = true;
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(7, 112);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(569, 353);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.FigureDrawn += new System.EventHandler<Damany.Windows.Form.DrawFigureEventArgs>(this.pictureBox1_FigureDrawn);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.pictureEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(584, 66);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(29, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(162, 33);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "设置虚拟线圈";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(509, 6);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(55, 55);
            this.pictureEdit1.TabIndex = 0;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.peerAddress);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.connectButton);
            this.panelControl3.Controls.Add(this.cameraIp);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 66);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(584, 40);
            this.panelControl3.TabIndex = 3;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(501, 5);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 25);
            this.applyButton.TabIndex = 5;
            this.applyButton.Text = "应用";
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // peerAddress
            // 
            this.peerAddress.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::WindowsFormsApplication1.Properties.Settings.Default, "PeerIpPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.peerAddress.EditValue = global::WindowsFormsApplication1.Properties.Settings.Default.PeerIpPort;
            this.peerAddress.Location = new System.Drawing.Point(408, 9);
            this.peerAddress.Name = "peerAddress";
            this.peerAddress.Size = new System.Drawing.Size(164, 21);
            this.peerAddress.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(331, 13);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "前端IP地址：";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(241, 9);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(58, 23);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "连接";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // cameraIp
            // 
            this.cameraIp.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::WindowsFormsApplication1.Properties.Settings.Default, "CameraIp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cameraIp.EditValue = global::WindowsFormsApplication1.Properties.Settings.Default.CameraIp;
            this.cameraIp.Location = new System.Drawing.Point(92, 10);
            this.cameraIp.Name = "cameraIp";
            this.cameraIp.Size = new System.Drawing.Size(143, 21);
            this.cameraIp.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "摄像机地址：";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.applyButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 471);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(584, 37);
            this.panelControl2.TabIndex = 4;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 508);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(600, 546);
            this.Name = "Form1";
            this.Text = "虚拟线圈标识器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.peerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Damany.Windows.Form.PictureBox pictureBox1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.TextEdit peerAddress;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton connectButton;
        private DevExpress.XtraEditors.TextEdit cameraIp;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton applyButton;
        private DevExpress.XtraEditors.PanelControl panelControl2;



    }
}

