namespace Damany.Windows.Form
{
    partial class SanyoNetCamera
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.applyShutter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.shutterLevel = new System.Windows.Forms.ComboBox();
            this.shutterShort = new System.Windows.Forms.RadioButton();
            this.shutterOff = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.applyIris = new System.Windows.Forms.Button();
            this.manualIrisLevel = new System.Windows.Forms.ComboBox();
            this.manualIris = new System.Windows.Forms.RadioButton();
            this.autoIris = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.applyAgc = new System.Windows.Forms.Button();
            this.digitalGain = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.agcOn = new System.Windows.Forms.RadioButton();
            this.agcOff = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.applyShutter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.shutterLevel);
            this.groupBox1.Controls.Add(this.shutterShort);
            this.groupBox1.Controls.Add(this.shutterOff);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "快门";
            // 
            // applyShutter
            // 
            this.applyShutter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.applyShutter.Image = global::Damany.Windows.Form.Properties.Resources.CheckMark;
            this.applyShutter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.applyShutter.Location = new System.Drawing.Point(195, 38);
            this.applyShutter.Name = "applyShutter";
            this.applyShutter.Size = new System.Drawing.Size(55, 29);
            this.applyShutter.TabIndex = 7;
            this.applyShutter.Text = "应用";
            this.applyShutter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.applyShutter.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "1/";
            // 
            // shutterLevel
            // 
            this.shutterLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shutterLevel.FormattingEnabled = true;
            this.shutterLevel.Location = new System.Drawing.Point(80, 52);
            this.shutterLevel.Name = "shutterLevel";
            this.shutterLevel.Size = new System.Drawing.Size(86, 20);
            this.shutterLevel.TabIndex = 2;
            // 
            // shutterShort
            // 
            this.shutterShort.AutoSize = true;
            this.shutterShort.Location = new System.Drawing.Point(20, 53);
            this.shutterShort.Name = "shutterShort";
            this.shutterShort.Size = new System.Drawing.Size(35, 16);
            this.shutterShort.TabIndex = 1;
            this.shutterShort.TabStop = true;
            this.shutterShort.Text = "短";
            this.shutterShort.UseVisualStyleBackColor = true;
            // 
            // shutterOff
            // 
            this.shutterOff.AutoSize = true;
            this.shutterOff.Location = new System.Drawing.Point(19, 22);
            this.shutterOff.Name = "shutterOff";
            this.shutterOff.Size = new System.Drawing.Size(47, 16);
            this.shutterOff.TabIndex = 0;
            this.shutterOff.TabStop = true;
            this.shutterOff.Text = "关闭";
            this.shutterOff.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.applyIris);
            this.groupBox2.Controls.Add(this.manualIrisLevel);
            this.groupBox2.Controls.Add(this.manualIris);
            this.groupBox2.Controls.Add(this.autoIris);
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "光圈";
            // 
            // applyIris
            // 
            this.applyIris.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.applyIris.Image = global::Damany.Windows.Form.Properties.Resources.CheckMark;
            this.applyIris.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.applyIris.Location = new System.Drawing.Point(195, 34);
            this.applyIris.Name = "applyIris";
            this.applyIris.Size = new System.Drawing.Size(55, 29);
            this.applyIris.TabIndex = 7;
            this.applyIris.Text = "应用";
            this.applyIris.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.applyIris.UseVisualStyleBackColor = true;
            // 
            // manualIrisLevel
            // 
            this.manualIrisLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manualIrisLevel.FormattingEnabled = true;
            this.manualIrisLevel.Location = new System.Drawing.Point(80, 55);
            this.manualIrisLevel.Name = "manualIrisLevel";
            this.manualIrisLevel.Size = new System.Drawing.Size(86, 20);
            this.manualIrisLevel.TabIndex = 2;
            // 
            // manualIris
            // 
            this.manualIris.AutoSize = true;
            this.manualIris.Location = new System.Drawing.Point(20, 56);
            this.manualIris.Name = "manualIris";
            this.manualIris.Size = new System.Drawing.Size(47, 16);
            this.manualIris.TabIndex = 1;
            this.manualIris.TabStop = true;
            this.manualIris.Text = "手动";
            this.manualIris.UseVisualStyleBackColor = true;
            // 
            // autoIris
            // 
            this.autoIris.AutoSize = true;
            this.autoIris.Location = new System.Drawing.Point(20, 20);
            this.autoIris.Name = "autoIris";
            this.autoIris.Size = new System.Drawing.Size(47, 16);
            this.autoIris.TabIndex = 0;
            this.autoIris.TabStop = true;
            this.autoIris.Text = "自动";
            this.autoIris.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.applyAgc);
            this.groupBox3.Controls.Add(this.digitalGain);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.agcOn);
            this.groupBox3.Controls.Add(this.agcOff);
            this.groupBox3.Location = new System.Drawing.Point(3, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 91);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "自动增益";
            // 
            // applyAgc
            // 
            this.applyAgc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.applyAgc.Image = global::Damany.Windows.Form.Properties.Resources.CheckMark;
            this.applyAgc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.applyAgc.Location = new System.Drawing.Point(195, 33);
            this.applyAgc.Name = "applyAgc";
            this.applyAgc.Size = new System.Drawing.Size(55, 29);
            this.applyAgc.TabIndex = 6;
            this.applyAgc.Text = "应用";
            this.applyAgc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.applyAgc.UseVisualStyleBackColor = true;
            // 
            // digitalGain
            // 
            this.digitalGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.digitalGain.FormattingEnabled = true;
            this.digitalGain.Location = new System.Drawing.Point(80, 57);
            this.digitalGain.Name = "digitalGain";
            this.digitalGain.Size = new System.Drawing.Size(86, 20);
            this.digitalGain.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "数字增益：";
            // 
            // agcOn
            // 
            this.agcOn.AutoSize = true;
            this.agcOn.Location = new System.Drawing.Point(20, 57);
            this.agcOn.Name = "agcOn";
            this.agcOn.Size = new System.Drawing.Size(47, 16);
            this.agcOn.TabIndex = 3;
            this.agcOn.TabStop = true;
            this.agcOn.Text = "打开";
            this.agcOn.UseVisualStyleBackColor = true;
            // 
            // agcOff
            // 
            this.agcOff.AutoSize = true;
            this.agcOff.Location = new System.Drawing.Point(20, 23);
            this.agcOff.Name = "agcOff";
            this.agcOff.Size = new System.Drawing.Size(47, 16);
            this.agcOff.TabIndex = 2;
            this.agcOff.TabStop = true;
            this.agcOff.Text = "关闭";
            this.agcOff.UseVisualStyleBackColor = true;
            // 
            // SanyoNetCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SanyoNetCamera";
            this.Size = new System.Drawing.Size(277, 288);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton shutterShort;
        private System.Windows.Forms.RadioButton shutterOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox shutterLevel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton manualIris;
        private System.Windows.Forms.RadioButton autoIris;
        private System.Windows.Forms.ComboBox manualIrisLevel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton agcOn;
        private System.Windows.Forms.RadioButton agcOff;
        private System.Windows.Forms.ComboBox digitalGain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button applyShutter;
        private System.Windows.Forms.Button applyIris;
        private System.Windows.Forms.Button applyAgc;
    }
}
