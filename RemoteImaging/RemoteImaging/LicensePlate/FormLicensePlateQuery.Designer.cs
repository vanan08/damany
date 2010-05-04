namespace RemoteImaging.LicensePlate
{
    partial class FormLicensePlateQuery
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mathTimeRange = new System.Windows.Forms.CheckBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.to = new DevExpress.XtraEditors.TimeEdit();
            this.matchLicenseNumber = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.from = new DevExpress.XtraEditors.TimeEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.licensePlateNumber = new System.Windows.Forms.TextBox();
            this.licensePlateList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mathTimeRange);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.to);
            this.groupBox1.Controls.Add(this.matchLicenseNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.from);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.licensePlateNumber);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(934, 81);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // mathTimeRange
            // 
            this.mathTimeRange.AutoSize = true;
            this.mathTimeRange.Checked = true;
            this.mathTimeRange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mathTimeRange.Location = new System.Drawing.Point(290, 29);
            this.mathTimeRange.Name = "mathTimeRange";
            this.mathTimeRange.Size = new System.Drawing.Size(60, 16);
            this.mathTimeRange.TabIndex = 6;
            this.mathTimeRange.Text = "时间段";
            this.mathTimeRange.UseVisualStyleBackColor = true;
            this.mathTimeRange.CheckedChanged += new System.EventHandler(this.mathTimeRange_CheckedChanged);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(847, 26);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 4;
            this.searchButton.Text = "查询";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // to
            // 
            this.to.EditValue = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            this.to.Location = new System.Drawing.Point(612, 25);
            this.to.Name = "to";
            this.to.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.to.Properties.Mask.EditMask = "F";
            this.to.Size = new System.Drawing.Size(196, 23);
            this.to.TabIndex = 4;
            // 
            // matchLicenseNumber
            // 
            this.matchLicenseNumber.AutoSize = true;
            this.matchLicenseNumber.Checked = true;
            this.matchLicenseNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.matchLicenseNumber.Location = new System.Drawing.Point(12, 29);
            this.matchLicenseNumber.Name = "matchLicenseNumber";
            this.matchLicenseNumber.Size = new System.Drawing.Size(60, 16);
            this.matchLicenseNumber.TabIndex = 5;
            this.matchLicenseNumber.Text = "车牌号";
            this.matchLicenseNumber.UseVisualStyleBackColor = true;
            this.matchLicenseNumber.CheckedChanged += new System.EventHandler(this.matchLicenseNumber_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(577, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "到：";
            // 
            // from
            // 
            this.from.EditValue = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            this.from.Location = new System.Drawing.Point(391, 24);
            this.from.Name = "from";
            this.from.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.from.Properties.Mask.EditMask = "F";
            this.from.Size = new System.Drawing.Size(158, 23);
            this.from.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(356, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "从：";
            // 
            // licensePlateNumber
            // 
            this.licensePlateNumber.Location = new System.Drawing.Point(78, 26);
            this.licensePlateNumber.Name = "licensePlateNumber";
            this.licensePlateNumber.Size = new System.Drawing.Size(161, 21);
            this.licensePlateNumber.TabIndex = 1;
            // 
            // licensePlateList
            // 
            this.licensePlateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.licensePlateList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.licensePlateList.FullRowSelect = true;
            this.licensePlateList.GridLines = true;
            this.licensePlateList.Location = new System.Drawing.Point(0, 265);
            this.licensePlateList.Name = "licensePlateList";
            this.licensePlateList.Size = new System.Drawing.Size(934, 198);
            this.licensePlateList.TabIndex = 1;
            this.licensePlateList.UseCompatibleStateImageBehavior = false;
            this.licensePlateList.View = System.Windows.Forms.View.Details;
            this.licensePlateList.SelectedIndexChanged += new System.EventHandler(this.licensePlateList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 27;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "车牌号码";
            this.columnHeader2.Width = 112;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "抓拍时间";
            this.columnHeader3.Width = 157;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "抓拍摄像头";
            this.columnHeader4.Width = 224;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(934, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 262);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(934, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // FormLicensePlateQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 463);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.licensePlateList);
            this.Name = "FormLicensePlateQuery";
            this.ShowInTaskbar = false;
            this.Text = "车牌号查询";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox licensePlateNumber;
        private DevExpress.XtraEditors.TimeEdit from;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TimeEdit to;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListView licensePlateList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox mathTimeRange;
        private System.Windows.Forms.CheckBox matchLicenseNumber;
        private System.Windows.Forms.Splitter splitter1;
    }
}