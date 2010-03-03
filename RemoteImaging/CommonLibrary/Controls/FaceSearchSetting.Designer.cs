namespace RemoteImaging.Controls
{
    partial class CameraSetting
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
            this.label41 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.enableMotionDetect = new System.Windows.Forms.CheckBox();
            this.drawMotionRect = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bottomExtRatio = new DevExpress.XtraEditors.TextEdit();
            this.topExtRatio = new DevExpress.XtraEditors.TextEdit();
            this.rightExtRatio = new DevExpress.XtraEditors.TextEdit();
            this.leftExtRatio = new DevExpress.XtraEditors.TextEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.maxFaceWidth = new DevExpress.XtraEditors.TextEdit();
            this.minFaceWidth = new DevExpress.XtraEditors.TextEdit();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.motionRegionAreaLimit = new DevExpress.XtraEditors.SpinEdit();
            this.imageGroupLength = new DevExpress.XtraEditors.SpinEdit();
            this.framesPerSec = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomExtRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topExtRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightExtRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftExtRatio.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionRegionAreaLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageGroupLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.framesPerSec.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(24, 105);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(89, 12);
            this.label41.TabIndex = 69;
            this.label41.Text = "图片数(每组)：";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(23, 79);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(65, 12);
            this.label32.TabIndex = 68;
            this.label32.Text = "画框域值：";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 306);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(143, 12);
            this.label25.TabIndex = 66;
            this.label25.Text = "摄像头获取频率(帧/秒)：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(185, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 58;
            this.label19.Text = "最大脸宽：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 54;
            this.label18.Text = "最小脸宽：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(331, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 51;
            this.label10.Text = "下：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(224, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 49;
            this.label9.Text = "上：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(111, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 47;
            this.label8.Text = "右：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "左：";
            // 
            // enableMotionDetect
            // 
            this.enableMotionDetect.AutoSize = true;
            this.enableMotionDetect.Location = new System.Drawing.Point(16, 20);
            this.enableMotionDetect.Name = "enableMotionDetect";
            this.enableMotionDetect.Size = new System.Drawing.Size(72, 16);
            this.enableMotionDetect.TabIndex = 59;
            this.enableMotionDetect.Text = "运动检测";
            this.enableMotionDetect.UseVisualStyleBackColor = true;
            this.enableMotionDetect.CheckedChanged += new System.EventHandler(this.enableMotionDetecto_CheckedChanged);
            // 
            // drawMotionRect
            // 
            this.drawMotionRect.AutoSize = true;
            this.drawMotionRect.Enabled = false;
            this.drawMotionRect.Location = new System.Drawing.Point(35, 42);
            this.drawMotionRect.Name = "drawMotionRect";
            this.drawMotionRect.Size = new System.Drawing.Size(120, 16);
            this.drawMotionRect.TabIndex = 61;
            this.drawMotionRect.Text = "标识运动检测结果";
            this.drawMotionRect.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bottomExtRatio);
            this.groupBox1.Controls.Add(this.topExtRatio);
            this.groupBox1.Controls.Add(this.rightExtRatio);
            this.groupBox1.Controls.Add(this.leftExtRatio);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 67);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人像扩展比例";
            // 
            // bottomExtRatio
            // 
            this.bottomExtRatio.EditValue = 0.5F;
            this.bottomExtRatio.Location = new System.Drawing.Point(361, 26);
            this.bottomExtRatio.Name = "bottomExtRatio";
            this.bottomExtRatio.Properties.Mask.EditMask = "f";
            this.bottomExtRatio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.bottomExtRatio.Size = new System.Drawing.Size(66, 23);
            this.bottomExtRatio.TabIndex = 55;
            // 
            // topExtRatio
            // 
            this.topExtRatio.EditValue = 0.5F;
            this.topExtRatio.Location = new System.Drawing.Point(255, 25);
            this.topExtRatio.Name = "topExtRatio";
            this.topExtRatio.Properties.Mask.EditMask = "f";
            this.topExtRatio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.topExtRatio.Size = new System.Drawing.Size(66, 23);
            this.topExtRatio.TabIndex = 54;
            // 
            // rightExtRatio
            // 
            this.rightExtRatio.EditValue = 0.5F;
            this.rightExtRatio.Location = new System.Drawing.Point(146, 26);
            this.rightExtRatio.Name = "rightExtRatio";
            this.rightExtRatio.Properties.Mask.EditMask = "f";
            this.rightExtRatio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rightExtRatio.Size = new System.Drawing.Size(65, 23);
            this.rightExtRatio.TabIndex = 53;
            // 
            // leftExtRatio
            // 
            this.leftExtRatio.EditValue = 0.5F;
            this.leftExtRatio.Location = new System.Drawing.Point(49, 26);
            this.leftExtRatio.Name = "leftExtRatio";
            this.leftExtRatio.Properties.Mask.EditMask = "f";
            this.leftExtRatio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.leftExtRatio.Size = new System.Drawing.Size(54, 23);
            this.leftExtRatio.TabIndex = 52;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.maxFaceWidth);
            this.groupBox2.Controls.Add(this.minFaceWidth);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Location = new System.Drawing.Point(3, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 68);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人像截取阈值(像素)";
            // 
            // maxFaceWidth
            // 
            this.maxFaceWidth.EditValue = 300;
            this.maxFaceWidth.Location = new System.Drawing.Point(255, 24);
            this.maxFaceWidth.Name = "maxFaceWidth";
            this.maxFaceWidth.Properties.Mask.EditMask = "d";
            this.maxFaceWidth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.maxFaceWidth.Size = new System.Drawing.Size(80, 23);
            this.maxFaceWidth.TabIndex = 60;
            // 
            // minFaceWidth
            // 
            this.minFaceWidth.EditValue = 60;
            this.minFaceWidth.Location = new System.Drawing.Point(85, 24);
            this.minFaceWidth.Name = "minFaceWidth";
            this.minFaceWidth.Properties.Mask.EditMask = "d";
            this.minFaceWidth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.minFaceWidth.Size = new System.Drawing.Size(72, 23);
            this.minFaceWidth.TabIndex = 59;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.motionRegionAreaLimit);
            this.groupBox4.Controls.Add(this.imageGroupLength);
            this.groupBox4.Controls.Add(this.enableMotionDetect);
            this.groupBox4.Controls.Add(this.drawMotionRect);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.label41);
            this.groupBox4.Location = new System.Drawing.Point(3, 150);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(455, 135);
            this.groupBox4.TabIndex = 76;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运动检测";
            // 
            // motionRegionAreaLimit
            // 
            this.motionRegionAreaLimit.EditValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.motionRegionAreaLimit.Enabled = false;
            this.motionRegionAreaLimit.Location = new System.Drawing.Point(113, 73);
            this.motionRegionAreaLimit.Name = "motionRegionAreaLimit";
            this.motionRegionAreaLimit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.motionRegionAreaLimit.Properties.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.motionRegionAreaLimit.Properties.IsFloatValue = false;
            this.motionRegionAreaLimit.Properties.Mask.EditMask = "N00";
            this.motionRegionAreaLimit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.motionRegionAreaLimit.Properties.MinValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.motionRegionAreaLimit.Size = new System.Drawing.Size(100, 23);
            this.motionRegionAreaLimit.TabIndex = 75;
            this.motionRegionAreaLimit.EditValueChanged += new System.EventHandler(this.motionRegionAreaLimit_EditValueChanged);
            // 
            // imageGroupLength
            // 
            this.imageGroupLength.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.imageGroupLength.Enabled = false;
            this.imageGroupLength.Location = new System.Drawing.Point(113, 99);
            this.imageGroupLength.Name = "imageGroupLength";
            this.imageGroupLength.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.imageGroupLength.Properties.IsFloatValue = false;
            this.imageGroupLength.Properties.Mask.EditMask = "N00";
            this.imageGroupLength.Properties.MaxValue = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.imageGroupLength.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.imageGroupLength.Size = new System.Drawing.Size(100, 23);
            this.imageGroupLength.TabIndex = 74;
            // 
            // framesPerSec
            // 
            this.framesPerSec.EditValue = 2F;
            this.framesPerSec.Location = new System.Drawing.Point(149, 300);
            this.framesPerSec.Name = "framesPerSec";
            this.framesPerSec.Properties.Mask.EditMask = "f";
            this.framesPerSec.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.framesPerSec.Size = new System.Drawing.Size(46, 23);
            this.framesPerSec.TabIndex = 77;
            // 
            // CameraSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.framesPerSec);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label25);
            this.Name = "CameraSetting";
            this.Size = new System.Drawing.Size(464, 333);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomExtRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topExtRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightExtRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftExtRatio.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.motionRegionAreaLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageGroupLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.framesPerSec.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox enableMotionDetect;
        private System.Windows.Forms.CheckBox drawMotionRect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.TextEdit leftExtRatio;
        private DevExpress.XtraEditors.TextEdit bottomExtRatio;
        private DevExpress.XtraEditors.TextEdit topExtRatio;
        private DevExpress.XtraEditors.TextEdit rightExtRatio;
        private DevExpress.XtraEditors.TextEdit framesPerSec;
        private DevExpress.XtraEditors.TextEdit maxFaceWidth;
        private DevExpress.XtraEditors.TextEdit minFaceWidth;
        private DevExpress.XtraEditors.SpinEdit motionRegionAreaLimit;
        private DevExpress.XtraEditors.SpinEdit imageGroupLength;
    }
}
