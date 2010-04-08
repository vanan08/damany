namespace Damany.RemoteImaging.Common.Forms
{
    partial class FaceCompare
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.compareButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.currentPic = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTo = new DevExpress.XtraEditors.TimeEdit();
            this.searchFrom = new DevExpress.XtraEditors.TimeEdit();
            this.choosePic = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.targetPic = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.faceList = new Damany.RemoteImaging.Common.Controls.DoubleBufferedListView();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.compareButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.choosePic);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 830);
            this.panel1.TabIndex = 0;
            // 
            // compareButton
            // 
            this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.compareButton.Enabled = false;
            this.compareButton.Location = new System.Drawing.Point(63, 502);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 21);
            this.compareButton.TabIndex = 5;
            this.compareButton.Text = "比对";
            this.compareButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 58);
            this.label3.TabIndex = 4;
            this.label3.Text = "选定一张照片后,从数据库中比较与该照片相似的图片";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.currentPic);
            this.groupBox3.Location = new System.Drawing.Point(3, 539);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 288);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "待比较图片";
            // 
            // currentPic
            // 
            this.currentPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPic.Location = new System.Drawing.Point(3, 17);
            this.currentPic.Name = "currentPic";
            this.currentPic.Size = new System.Drawing.Size(188, 268);
            this.currentPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentPic.TabIndex = 0;
            this.currentPic.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.searchTo);
            this.groupBox2.Controls.Add(this.searchFrom);
            this.groupBox2.Location = new System.Drawing.Point(3, 291);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 191);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "比对参数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始时间:";
            // 
            // searchTo
            // 
            this.searchTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTo.EditValue = new System.DateTime(2010, 3, 19, 0, 0, 0, 0);
            this.searchTo.Location = new System.Drawing.Point(9, 84);
            this.searchTo.Name = "searchTo";
            this.searchTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchTo.Properties.Mask.EditMask = "f";
            this.searchTo.Size = new System.Drawing.Size(179, 21);
            this.searchTo.TabIndex = 1;
            // 
            // searchFrom
            // 
            this.searchFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchFrom.EditValue = new System.DateTime(2010, 3, 19, 0, 0, 0, 0);
            this.searchFrom.Location = new System.Drawing.Point(9, 37);
            this.searchFrom.Name = "searchFrom";
            this.searchFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchFrom.Properties.Mask.EditMask = "f";
            this.searchFrom.Size = new System.Drawing.Size(179, 21);
            this.searchFrom.TabIndex = 0;
            // 
            // choosePic
            // 
            this.choosePic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.choosePic.Location = new System.Drawing.Point(63, 245);
            this.choosePic.Name = "choosePic";
            this.choosePic.Size = new System.Drawing.Size(75, 21);
            this.choosePic.TabIndex = 1;
            this.choosePic.Text = "浏览";
            this.choosePic.UseVisualStyleBackColor = true;
            this.choosePic.Click += new System.EventHandler(this.choosePic_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.targetPic);
            this.groupBox1.Location = new System.Drawing.Point(0, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选定目标";
            // 
            // targetPic
            // 
            this.targetPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetPic.Location = new System.Drawing.Point(3, 17);
            this.targetPic.Name = "targetPic";
            this.targetPic.Size = new System.Drawing.Size(194, 150);
            this.targetPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetPic.TabIndex = 0;
            this.targetPic.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 830);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.SteelBlue;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(203, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(561, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "比较结果";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Jpeg 文件|*.jpg";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(203, 806);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(561, 24);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 19);
            this.toolStripStatusLabel1.Text = "就绪";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(22, 17);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "低"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "中"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "高")});
            this.radioGroup1.Size = new System.Drawing.Size(138, 37);
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioGroup1);
            this.groupBox4.Location = new System.Drawing.Point(6, 125);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(179, 60);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "准确度";
            // 
            // faceList
            // 
            this.faceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faceList.LargeImageList = this.imageList1;
            this.faceList.Location = new System.Drawing.Point(203, 22);
            this.faceList.Name = "faceList";
            this.faceList.Size = new System.Drawing.Size(561, 784);
            this.faceList.TabIndex = 2;
            this.faceList.UseCompatibleStateImageBehavior = false;
            // 
            // FaceCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 830);
            this.Controls.Add(this.faceList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "FaceCompare";
            this.ShowInTaskbar = false;
            this.Text = "人脸比对查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceCompare_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button choosePic;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox targetPic;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox currentPic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TimeEdit searchTo;
        private DevExpress.XtraEditors.TimeEdit searchFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Splitter splitter1;
        private Damany.RemoteImaging.Common.Controls.DoubleBufferedListView faceList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
    }
}