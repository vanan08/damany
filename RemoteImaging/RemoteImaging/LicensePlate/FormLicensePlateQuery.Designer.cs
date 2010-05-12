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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicensePlateQuery));
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.playVideo = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.licensePlateImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.licensePlateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.licensePlateList.FullRowSelect = true;
            this.licensePlateList.GridLines = true;
            this.licensePlateList.Location = new System.Drawing.Point(0, 25);
            this.licensePlateList.Name = "licensePlateList";
            this.licensePlateList.Size = new System.Drawing.Size(934, 159);
            this.licensePlateList.SmallImageList = this.licensePlateImageList;
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
            this.columnHeader2.Width = 142;
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
            this.pictureBox1.Location = new System.Drawing.Point(3, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(455, 318);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Image = global::RemoteImaging.Properties.Resources.Camera;
            this.groupControl1.AppearanceCaption.Options.UseImage = true;
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(470, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(461, 341);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "车牌照片";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupControl2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 81);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(934, 347);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.axVLCPlugin21);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(3, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(461, 341);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "视频";
            this.groupControl2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl2_Paint);
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(3, 20);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(455, 318);
            this.axVLCPlugin21.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.licensePlateList);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 428);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 184);
            this.panel1.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playVideo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(934, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // playVideo
            // 
            this.playVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playVideo.Image = ((System.Drawing.Image)(resources.GetObject("playVideo.Image")));
            this.playVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playVideo.Name = "playVideo";
            this.playVideo.Size = new System.Drawing.Size(23, 22);
            this.playVideo.Text = "toolStripButton1";
            this.playVideo.Click += new System.EventHandler(this.playVideo_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 425);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(934, 3);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // licensePlateImageList
            // 
            this.licensePlateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("licensePlateImageList.ImageStream")));
            this.licensePlateImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.licensePlateImageList.Images.SetKeyName(0, "BlueDot.gif");
            // 
            // FormLicensePlateQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 612);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormLicensePlateQuery";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车牌号查询";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLicensePlateQuery_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton playVideo;
        private System.Windows.Forms.Splitter splitter1;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private System.Windows.Forms.ImageList licensePlateImageList;
    }
}