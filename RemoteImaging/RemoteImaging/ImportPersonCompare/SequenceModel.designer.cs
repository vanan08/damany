namespace RemoteImaging.ImportPersonCompare
{
    partial class SequenceModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceModel));
            this.lvInfoList = new System.Windows.Forms.ListView();
            this.cWId = new System.Windows.Forms.ColumnHeader();
            this.cWarnDate = new System.Windows.Forms.ColumnHeader();
            this.cWarnTime = new System.Windows.Forms.ColumnHeader();
            this.cWarnAddress = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.grbTargetImg = new System.Windows.Forms.GroupBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSimilarity = new System.Windows.Forms.Label();
            this.lblTextSim = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.picCheck = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.v = new System.Windows.Forms.ListView();
            this.cId = new System.Windows.Forms.ColumnHeader();
            this.cName = new System.Windows.Forms.ColumnHeader();
            this.cSex = new System.Windows.Forms.ColumnHeader();
            this.cAge = new System.Windows.Forms.ColumnHeader();
            this.cCard = new System.Windows.Forms.ColumnHeader();
            this.cSimilarity = new System.Windows.Forms.ColumnHeader();
            this.picStandard = new System.Windows.Forms.PictureBox();
            this.grbWarnList = new System.Windows.Forms.GroupBox();
            this.grbTargetImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStandard)).BeginInit();
            this.grbWarnList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvInfoList
            // 
            this.lvInfoList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cWId,
            this.cWarnDate,
            this.cWarnTime,
            this.cWarnAddress,
            this.columnHeader5});
            this.lvInfoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInfoList.FullRowSelect = true;
            this.lvInfoList.GridLines = true;
            this.lvInfoList.Location = new System.Drawing.Point(3, 17);
            this.lvInfoList.MultiSelect = false;
            this.lvInfoList.Name = "lvInfoList";
            this.lvInfoList.Size = new System.Drawing.Size(730, 193);
            this.lvInfoList.TabIndex = 2;
            this.lvInfoList.UseCompatibleStateImageBehavior = false;
            this.lvInfoList.View = System.Windows.Forms.View.Details;
            // 
            // cWId
            // 
            this.cWId.Text = "";
            // 
            // cWarnDate
            // 
            this.cWarnDate.Text = "报警时间";
            this.cWarnDate.Width = 128;
            // 
            // cWarnTime
            // 
            this.cWarnTime.Text = "报警时间";
            this.cWarnTime.Width = 117;
            // 
            // cWarnAddress
            // 
            this.cWarnAddress.Text = "报警地点";
            this.cWarnAddress.Width = 191;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 137;
            // 
            // grbTargetImg
            // 
            this.grbTargetImg.Controls.Add(this.lblAddress);
            this.grbTargetImg.Controls.Add(this.lblTime);
            this.grbTargetImg.Controls.Add(this.lblDate);
            this.grbTargetImg.Controls.Add(this.lblSimilarity);
            this.grbTargetImg.Controls.Add(this.lblTextSim);
            this.grbTargetImg.Controls.Add(this.btnCancel);
            this.grbTargetImg.Controls.Add(this.btnOK);
            this.grbTargetImg.Controls.Add(this.picCheck);
            this.grbTargetImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbTargetImg.Location = new System.Drawing.Point(0, 0);
            this.grbTargetImg.Name = "grbTargetImg";
            this.grbTargetImg.Size = new System.Drawing.Size(736, 202);
            this.grbTargetImg.TabIndex = 21;
            this.grbTargetImg.TabStop = false;
            this.grbTargetImg.Text = "待识别图片";
            // 
            // lblAddress
            // 
            this.lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(192, 125);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(155, 12);
            this.lblAddress.TabIndex = 21;
            this.lblAddress.Text = "地址： **市**区**号**南门";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(193, 81);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(95, 12);
            this.lblTime.TabIndex = 20;
            this.lblTime.Text = "时间： 11:02:38";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(192, 38);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(113, 12);
            this.lblDate.TabIndex = 19;
            this.lblDate.Text = "日期： 2009-10-28 ";
            // 
            // lblSimilarity
            // 
            this.lblSimilarity.AutoSize = true;
            this.lblSimilarity.Font = new System.Drawing.Font("NSimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSimilarity.ForeColor = System.Drawing.Color.Red;
            this.lblSimilarity.Location = new System.Drawing.Point(429, 23);
            this.lblSimilarity.Name = "lblSimilarity";
            this.lblSimilarity.Size = new System.Drawing.Size(72, 19);
            this.lblSimilarity.TabIndex = 17;
            this.lblSimilarity.Text = "相似度";
            // 
            // lblTextSim
            // 
            this.lblTextSim.AutoSize = true;
            this.lblTextSim.Location = new System.Drawing.Point(507, 29);
            this.lblTextSim.Name = "lblTextSim";
            this.lblTextSim.Size = new System.Drawing.Size(179, 12);
            this.lblTextSim.TabIndex = 18;
            this.lblTextSim.Text = "预计: 50%-58%  检测结果 55.9%";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(635, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(541, 168);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "处理";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // picCheck
            // 
            this.picCheck.Image = ((System.Drawing.Image)(resources.GetObject("picCheck.Image")));
            this.picCheck.Location = new System.Drawing.Point(6, 16);
            this.picCheck.Name = "picCheck";
            this.picCheck.Size = new System.Drawing.Size(158, 179);
            this.picCheck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCheck.TabIndex = 1;
            this.picCheck.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.v);
            this.groupBox1.Controls.Add(this.picStandard);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 205);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "犯罪分子";
            // 
            // v
            // 
            this.v.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cId,
            this.cName,
            this.cSex,
            this.cAge,
            this.cCard,
            this.cSimilarity});
            this.v.FullRowSelect = true;
            this.v.GridLines = true;
            this.v.Location = new System.Drawing.Point(182, 20);
            this.v.Name = "v";
            this.v.Size = new System.Drawing.Size(551, 179);
            this.v.TabIndex = 16;
            this.v.UseCompatibleStateImageBehavior = false;
            this.v.View = System.Windows.Forms.View.Details;
            this.v.SelectedIndexChanged += new System.EventHandler(this.v_SelectedIndexChanged);
            // 
            // cId
            // 
            this.cId.Text = "";
            this.cId.Width = 26;
            // 
            // cName
            // 
            this.cName.Text = "姓名";
            this.cName.Width = 89;
            // 
            // cSex
            // 
            this.cSex.Text = "性别";
            this.cSex.Width = 63;
            // 
            // cAge
            // 
            this.cAge.Text = "年龄";
            this.cAge.Width = 59;
            // 
            // cCard
            // 
            this.cCard.Text = "身份证号";
            this.cCard.Width = 220;
            // 
            // cSimilarity
            // 
            this.cSimilarity.Text = "相似度范围值";
            this.cSimilarity.Width = 89;
            // 
            // picStandard
            // 
            this.picStandard.Image = ((System.Drawing.Image)(resources.GetObject("picStandard.Image")));
            this.picStandard.Location = new System.Drawing.Point(6, 20);
            this.picStandard.Name = "picStandard";
            this.picStandard.Size = new System.Drawing.Size(158, 179);
            this.picStandard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picStandard.TabIndex = 0;
            this.picStandard.TabStop = false;
            // 
            // grbWarnList
            // 
            this.grbWarnList.Controls.Add(this.lvInfoList);
            this.grbWarnList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbWarnList.Location = new System.Drawing.Point(0, 407);
            this.grbWarnList.Name = "grbWarnList";
            this.grbWarnList.Size = new System.Drawing.Size(736, 213);
            this.grbWarnList.TabIndex = 23;
            this.grbWarnList.TabStop = false;
            this.grbWarnList.Text = "报警信息列表";
            // 
            // SequenceModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 620);
            this.Controls.Add(this.grbWarnList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbTargetImg);
            this.Name = "SequenceModel";
            this.Text = "报警 顺序处理模式";
            this.Load += new System.EventHandler(this.SequenceModel_Load);
            this.grbTargetImg.ResumeLayout(false);
            this.grbTargetImg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCheck)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStandard)).EndInit();
            this.grbWarnList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvInfoList;
        private System.Windows.Forms.GroupBox grbTargetImg;
        private System.Windows.Forms.PictureBox picCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picStandard;
        private System.Windows.Forms.GroupBox grbWarnList;
        private System.Windows.Forms.ColumnHeader cWId;
        private System.Windows.Forms.ColumnHeader cWarnDate;
        private System.Windows.Forms.ColumnHeader cWarnTime;
        private System.Windows.Forms.ColumnHeader cWarnAddress;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSimilarity;
        private System.Windows.Forms.Label lblTextSim;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ListView v;
        private System.Windows.Forms.ColumnHeader cId;
        private System.Windows.Forms.ColumnHeader cName;
        private System.Windows.Forms.ColumnHeader cSex;
        private System.Windows.Forms.ColumnHeader cAge;
        private System.Windows.Forms.ColumnHeader cCard;
        private System.Windows.Forms.ColumnHeader cSimilarity;
    }
}