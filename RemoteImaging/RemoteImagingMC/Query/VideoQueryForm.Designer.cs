﻿namespace RemoteImaging.Query
{
    partial class VideoQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoQueryForm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.filePathToDownload = new System.Windows.Forms.TextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.allVideoRadioButton = new System.Windows.Forms.RadioButton();
            this.faceCapturedRadioButton = new System.Windows.Forms.RadioButton();
            this.queryBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit2 = new DevExpress.XtraEditors.TimeEdit();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.videoList = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.picList = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.downloadBmp = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.downloadBmp);
            this.groupBox3.Controls.Add(this.filePathToDownload);
            this.groupBox3.Controls.Add(this.testButton);
            this.groupBox3.Controls.Add(this.allVideoRadioButton);
            this.groupBox3.Controls.Add(this.faceCapturedRadioButton);
            this.groupBox3.Controls.Add(this.queryBtn);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.dateTimePicker2);
            this.groupBox3.Controls.Add(this.timeEdit1);
            this.groupBox3.Controls.Add(this.timeEdit2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(904, 99);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询条件";
            // 
            // filePathToDownload
            // 
            this.filePathToDownload.Location = new System.Drawing.Point(622, 13);
            this.filePathToDownload.Name = "filePathToDownload";
            this.filePathToDownload.Size = new System.Drawing.Size(256, 20);
            this.filePathToDownload.TabIndex = 21;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(643, 42);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 25);
            this.testButton.TabIndex = 20;
            this.testButton.Text = "stream";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // allVideoRadioButton
            // 
            this.allVideoRadioButton.AutoSize = true;
            this.allVideoRadioButton.Location = new System.Drawing.Point(532, 26);
            this.allVideoRadioButton.Name = "allVideoRadioButton";
            this.allVideoRadioButton.Size = new System.Drawing.Size(73, 17);
            this.allVideoRadioButton.TabIndex = 12;
            this.allVideoRadioButton.TabStop = true;
            this.allVideoRadioButton.Text = "全部查询";
            this.allVideoRadioButton.UseVisualStyleBackColor = true;
            // 
            // faceCapturedRadioButton
            // 
            this.faceCapturedRadioButton.AutoSize = true;
            this.faceCapturedRadioButton.Location = new System.Drawing.Point(397, 26);
            this.faceCapturedRadioButton.Name = "faceCapturedRadioButton";
            this.faceCapturedRadioButton.Size = new System.Drawing.Size(97, 17);
            this.faceCapturedRadioButton.TabIndex = 11;
            this.faceCapturedRadioButton.TabStop = true;
            this.faceCapturedRadioButton.Text = "查询有效视频";
            this.faceCapturedRadioButton.UseVisualStyleBackColor = true;
            // 
            // queryBtn
            // 
            this.queryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBtn.Location = new System.Drawing.Point(794, 42);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(84, 23);
            this.queryBtn.TabIndex = 19;
            this.queryBtn.Text = "查询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(338, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "查询范围";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "监控点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "时间起点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "时间终点";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(79, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(79, 61);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(129, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(397, 61);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(129, 20);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(217, 61);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit1.Size = new System.Drawing.Size(96, 22);
            this.timeEdit1.TabIndex = 7;
            // 
            // timeEdit2
            // 
            this.timeEdit2.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeEdit2.Location = new System.Drawing.Point(532, 61);
            this.timeEdit2.Name = "timeEdit2";
            this.timeEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit2.Size = new System.Drawing.Size(96, 22);
            this.timeEdit2.TabIndex = 8;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(80, 60);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // videoList
            // 
            this.videoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoList.FullRowSelect = true;
            this.videoList.GridLines = true;
            this.videoList.HideSelection = false;
            this.videoList.Location = new System.Drawing.Point(0, 13);
            this.videoList.Name = "videoList";
            this.videoList.Size = new System.Drawing.Size(154, 546);
            this.videoList.SmallImageList = this.imageList2;
            this.videoList.TabIndex = 25;
            this.videoList.UseCompatibleStateImageBehavior = false;
            this.videoList.View = System.Windows.Forms.View.Details;
            this.videoList.ItemActivate += new System.EventHandler(this.videoList_ItemActivate);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "png-0005.png");
            this.imageList2.Images.SetKeyName(1, "png-0652.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "视频列表：";
            // 
            // picList
            // 
            this.picList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picList.Location = new System.Drawing.Point(0, 13);
            this.picList.MultiSelect = false;
            this.picList.Name = "picList";
            this.picList.Size = new System.Drawing.Size(746, 126);
            this.picList.TabIndex = 22;
            this.picList.UseCompatibleStateImageBehavior = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "相关图片：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "视频播放：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 99);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.videoList);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(904, 559);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 30;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.axVLCPlugin21);
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.picList);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Size = new System.Drawing.Size(746, 559);
            this.splitContainer2.SplitterDistance = 416;
            this.splitContainer2.TabIndex = 0;
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(0, 13);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(746, 403);
            this.axVLCPlugin21.TabIndex = 30;
            // 
            // downloadBmp
            // 
            this.downloadBmp.Location = new System.Drawing.Point(643, 70);
            this.downloadBmp.Name = "downloadBmp";
            this.downloadBmp.Size = new System.Drawing.Size(75, 23);
            this.downloadBmp.TabIndex = 22;
            this.downloadBmp.Text = "bmp";
            this.downloadBmp.UseVisualStyleBackColor = true;
            this.downloadBmp.Click += new System.EventHandler(this.downloadBmp_Click);
            // 
            // VideoQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 658);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VideoQueryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "搜索视频";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VideoQueryForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoQueryForm_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit2.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private DevExpress.XtraEditors.TimeEdit timeEdit2;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView videoList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView picList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.RadioButton allVideoRadioButton;
        private System.Windows.Forms.RadioButton faceCapturedRadioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TextBox filePathToDownload;
        private System.Windows.Forms.Button downloadBmp;
    }
}