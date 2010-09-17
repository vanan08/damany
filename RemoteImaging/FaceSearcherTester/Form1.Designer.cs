namespace FaceSearcherTester
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.maxFaceWidth = new DevExpress.XtraEditors.TextEdit();
            this.minFaceWidth = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.faceCount = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.drawFaceSizeMark = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawFaceSizeMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.drawFaceSizeMark);
            this.layoutControl1.Controls.Add(this.faceCount);
            this.layoutControl1.Controls.Add(this.maxFaceWidth);
            this.layoutControl1.Controls.Add(this.minFaceWidth);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl1.Location = new System.Drawing.Point(0, 292);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(708, 134);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // maxFaceWidth
            // 
            this.maxFaceWidth.Location = new System.Drawing.Point(468, 38);
            this.maxFaceWidth.Name = "maxFaceWidth";
            this.maxFaceWidth.Size = new System.Drawing.Size(223, 21);
            this.maxFaceWidth.StyleController = this.layoutControl1;
            this.maxFaceWidth.TabIndex = 5;
            this.maxFaceWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.minFaceWidth_KeyDown);
            // 
            // minFaceWidth
            // 
            this.minFaceWidth.Location = new System.Drawing.Point(129, 38);
            this.minFaceWidth.Name = "minFaceWidth";
            this.minFaceWidth.Size = new System.Drawing.Size(223, 21);
            this.minFaceWidth.StyleController = this.layoutControl1;
            this.minFaceWidth.TabIndex = 4;
            this.minFaceWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.minFaceWidth_KeyDown);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup5,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(708, 134);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(708, 292);
            this.pictureEdit1.TabIndex = 1;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "参数设置";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignModeGroup.AutoSize;
            this.layoutControlGroup5.Size = new System.Drawing.Size(702, 93);
            this.layoutControlGroup5.Text = "参数设置";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.minFaceWidth;
            this.layoutControlItem1.CustomizationFormText = "最小脸宽（像素）：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(339, 25);
            this.layoutControlItem1.Text = "最小脸宽（像素）：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(108, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.maxFaceWidth;
            this.layoutControlItem2.CustomizationFormText = "最大脸宽（像素）：";
            this.layoutControlItem2.Location = new System.Drawing.Point(339, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(339, 25);
            this.layoutControlItem2.Text = "最大脸宽（像素）：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(108, 14);
            // 
            // faceCount
            // 
            this.faceCount.Location = new System.Drawing.Point(45, 98);
            this.faceCount.Name = "faceCount";
            this.faceCount.Properties.DisplayFormat.FormatString = "{0} 张";
            this.faceCount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.faceCount.Properties.ReadOnly = true;
            this.faceCount.Size = new System.Drawing.Size(658, 21);
            this.faceCount.StyleController = this.layoutControl1;
            this.faceCount.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.faceCount;
            this.layoutControlItem3.CustomizationFormText = "人像：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(702, 35);
            this.layoutControlItem3.Text = "人像：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(36, 14);
            // 
            // drawFaceSizeMark
            // 
            this.drawFaceSizeMark.Location = new System.Drawing.Point(17, 63);
            this.drawFaceSizeMark.Name = "drawFaceSizeMark";
            this.drawFaceSizeMark.Properties.Caption = "标记人像大小";
            this.drawFaceSizeMark.Size = new System.Drawing.Size(674, 19);
            this.drawFaceSizeMark.StyleController = this.layoutControl1;
            this.drawFaceSizeMark.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.drawFaceSizeMark;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(678, 23);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 426);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Form1";
            this.Text = "人脸搜索测试器";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drawFaceSizeMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit maxFaceWidth;
        private DevExpress.XtraEditors.TextEdit minFaceWidth;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit faceCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit drawFaceSizeMark;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}

