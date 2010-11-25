namespace Kise.IdCard.UI
{
    partial class IdCardControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdCardControl));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.issuedBy = new DevExpress.XtraEditors.LabelControl();
            this.idCardNo = new DevExpress.XtraEditors.LabelControl();
            this.expiry = new DevExpress.XtraEditors.LabelControl();
            this.idStatus = new DevExpress.XtraEditors.LabelControl();
            this.birthDay = new DevExpress.XtraEditors.LabelControl();
            this.image = new DevExpress.XtraEditors.PictureEdit();
            this.minority = new DevExpress.XtraEditors.LabelControl();
            this.sex = new DevExpress.XtraEditors.LabelControl();
            this.name = new DevExpress.XtraEditors.LabelControl();
            this.address = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.issuedBy);
            this.layoutControl1.Controls.Add(this.idCardNo);
            this.layoutControl1.Controls.Add(this.expiry);
            this.layoutControl1.Controls.Add(this.idStatus);
            this.layoutControl1.Controls.Add(this.birthDay);
            this.layoutControl1.Controls.Add(this.image);
            this.layoutControl1.Controls.Add(this.minority);
            this.layoutControl1.Controls.Add(this.sex);
            this.layoutControl1.Controls.Add(this.name);
            this.layoutControl1.Controls.Add(this.address);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(664, 431);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // issuedBy
            // 
            this.issuedBy.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.issuedBy.Appearance.Options.UseFont = true;
            this.issuedBy.Location = new System.Drawing.Point(76, 301);
            this.issuedBy.Name = "issuedBy";
            this.issuedBy.Size = new System.Drawing.Size(576, 44);
            this.issuedBy.StyleController = this.layoutControl1;
            this.issuedBy.TabIndex = 12;
            // 
            // idCardNo
            // 
            this.idCardNo.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.idCardNo.Appearance.Options.UseFont = true;
            this.idCardNo.Location = new System.Drawing.Point(76, 379);
            this.idCardNo.Name = "idCardNo";
            this.idCardNo.Size = new System.Drawing.Size(576, 40);
            this.idCardNo.StyleController = this.layoutControl1;
            this.idCardNo.TabIndex = 10;
            // 
            // expiry
            // 
            this.expiry.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.expiry.Appearance.Options.UseFont = true;
            this.expiry.Location = new System.Drawing.Point(76, 349);
            this.expiry.Name = "expiry";
            this.expiry.Size = new System.Drawing.Size(576, 26);
            this.expiry.StyleController = this.layoutControl1;
            this.expiry.TabIndex = 9;
            // 
            // idStatus
            // 
            this.idStatus.Appearance.Options.UseBackColor = true;
            this.idStatus.Appearance.Options.UseForeColor = true;
            this.idStatus.Appearance.Options.UseTextOptions = true;
            this.idStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.idStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.idStatus.Location = new System.Drawing.Point(474, 219);
            this.idStatus.Name = "idStatus";
            this.idStatus.Size = new System.Drawing.Size(178, 26);
            this.idStatus.StyleController = this.layoutControl1;
            this.idStatus.TabIndex = 13;
            // 
            // birthDay
            // 
            this.birthDay.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.birthDay.Appearance.Options.UseFont = true;
            this.birthDay.Location = new System.Drawing.Point(76, 137);
            this.birthDay.Name = "birthDay";
            this.birthDay.Size = new System.Drawing.Size(394, 56);
            this.birthDay.StyleController = this.layoutControl1;
            this.birthDay.TabIndex = 7;
            // 
            // image
            // 
            this.image.Location = new System.Drawing.Point(474, 12);
            this.image.Name = "image";
            this.image.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.image.Properties.Appearance.Options.UseBackColor = true;
            this.image.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.image.Size = new System.Drawing.Size(178, 203);
            this.image.StyleController = this.layoutControl1;
            this.image.TabIndex = 11;
            // 
            // minority
            // 
            this.minority.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.minority.Appearance.Options.UseFont = true;
            this.minority.Location = new System.Drawing.Point(293, 70);
            this.minority.Name = "minority";
            this.minority.Size = new System.Drawing.Size(177, 63);
            this.minority.StyleController = this.layoutControl1;
            this.minority.TabIndex = 6;
            // 
            // sex
            // 
            this.sex.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.sex.Appearance.Options.UseFont = true;
            this.sex.Location = new System.Drawing.Point(76, 70);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(149, 63);
            this.sex.StyleController = this.layoutControl1;
            this.sex.TabIndex = 5;
            // 
            // name
            // 
            this.name.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.name.Appearance.Options.UseFont = true;
            this.name.Location = new System.Drawing.Point(76, 12);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(394, 54);
            this.name.StyleController = this.layoutControl1;
            this.name.TabIndex = 4;
            // 
            // address
            // 
            this.address.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.address.Appearance.Options.UseFont = true;
            this.address.Location = new System.Drawing.Point(76, 197);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(394, 48);
            this.address.StyleController = this.layoutControl1;
            this.address.TabIndex = 8;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1,
            this.layoutControlItem10,
            this.layoutControlItem9,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem11});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(664, 431);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.expiry;
            this.layoutControlItem7.CustomizationFormText = "有效期限";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 337);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(644, 30);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "有效期限";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.idCardNo;
            this.layoutControlItem8.CustomizationFormText = "身份证号码";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 367);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(644, 44);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.Text = "身份证号码";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(60, 14);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 237);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(644, 52);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.issuedBy;
            this.layoutControlItem10.CustomizationFormText = "签发机关";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 289);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(644, 48);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "签发机关";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.image;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(462, 0);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(24, 24);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(182, 207);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.name;
            this.layoutControlItem1.CustomizationFormText = "姓名";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(462, 58);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "姓名";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sex;
            this.layoutControlItem2.CustomizationFormText = "性别";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 58);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(217, 67);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "性别";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.birthDay;
            this.layoutControlItem4.CustomizationFormText = "出生";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 125);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(462, 60);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "出生";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.address;
            this.layoutControlItem5.CustomizationFormText = "住址";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 185);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(462, 52);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "住址";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.minority;
            this.layoutControlItem6.CustomizationFormText = "民族";
            this.layoutControlItem6.Location = new System.Drawing.Point(217, 58);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(155, 17);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(245, 67);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "民族";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.idStatus;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(462, 207);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(74, 18);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(182, 30);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sex;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 17);
            this.layoutControlItem3.Name = "layoutControlItem2";
            this.layoutControlItem3.Size = new System.Drawing.Size(509, 302);
            this.layoutControlItem3.Text = "layoutControlItem2";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(93, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // IdCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.layoutControl1);
            this.Name = "IdCardControl";
            this.Size = new System.Drawing.Size(664, 431);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.image.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit image;
        private DevExpress.XtraEditors.LabelControl idCardNo;
        private DevExpress.XtraEditors.LabelControl expiry;
        private DevExpress.XtraEditors.LabelControl address;
        private DevExpress.XtraEditors.LabelControl birthDay;
        private DevExpress.XtraEditors.LabelControl minority;
        private DevExpress.XtraEditors.LabelControl sex;
        private DevExpress.XtraEditors.LabelControl name;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl issuedBy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.LabelControl idStatus;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
    }
}
