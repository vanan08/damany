namespace Kise.IdCard.UI
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.okButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.smsNo = new DevExpress.XtraEditors.TextEdit();
            this.smsComPort = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smsNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smsComPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.okButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 311);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(537, 41);
            this.panelControl1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(454, 9);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "确定";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 98);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "短信中心号码";
            // 
            // smsNo
            // 
            this.smsNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::Kise.IdCard.UI.Properties.Settings.Default, "smsCenterNo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.smsNo.EditValue = global::Kise.IdCard.UI.Properties.Settings.Default.smsCenterNo;
            this.smsNo.Location = new System.Drawing.Point(130, 95);
            this.smsNo.Name = "smsNo";
            this.smsNo.Properties.EditFormat.FormatString = "d";
            this.smsNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.smsNo.Properties.Mask.BeepOnError = true;
            this.smsNo.Properties.Mask.EditMask = "d";
            this.smsNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.smsNo.Size = new System.Drawing.Size(330, 21);
            this.smsNo.TabIndex = 2;
            // 
            // smsComPort
            // 
            this.smsComPort.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::Kise.IdCard.UI.Properties.Settings.Default, "smsModemComPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.smsComPort.EditValue = global::Kise.IdCard.UI.Properties.Settings.Default.smsModemComPort;
            this.smsComPort.Location = new System.Drawing.Point(130, 138);
            this.smsComPort.Name = "smsComPort";
            this.smsComPort.Properties.Mask.EditMask = "COM\\d\\d?";
            this.smsComPort.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.smsComPort.Size = new System.Drawing.Size(330, 21);
            this.smsComPort.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 141);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "短信Modem串口";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.pictureEdit1);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(537, 64);
            this.panelControl2.TabIndex = 6;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(467, 5);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(58, 52);
            this.pictureEdit1.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 22);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "参数设置";
            // 
            // checkEdit1
            // 
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::Kise.IdCard.UI.Properties.Settings.Default, "AutoStart", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkEdit1.EditValue = global::Kise.IdCard.UI.Properties.Settings.Default.AutoStart;
            this.checkEdit1.Location = new System.Drawing.Point(32, 277);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "系统启动后自动开始";
            this.checkEdit1.Size = new System.Drawing.Size(147, 19);
            this.checkEdit1.TabIndex = 3;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 352);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.smsComPort);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.smsNo);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项设置";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smsNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smsComPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton okButton;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit smsNo;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.TextEdit smsComPort;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}