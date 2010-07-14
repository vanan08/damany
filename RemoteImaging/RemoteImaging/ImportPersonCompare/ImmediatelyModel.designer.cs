namespace RemoteImaging.ImportPersonCompare
{
    partial class ImmediatelyModel
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
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.suspectImage = new System.Windows.Forms.PictureBox();
            this.lblTextSim = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbTargetImg = new System.Windows.Forms.GroupBox();
            this.clearAll = new System.Windows.Forms.Button();
            this.personOfInterestImage = new System.Windows.Forms.PictureBox();
            this.suspectsList = new System.Windows.Forms.ListView();
            this.cId = new System.Windows.Forms.ColumnHeader();
            this.cName = new System.Windows.Forms.ColumnHeader();
            this.cSex = new System.Windows.Forms.ColumnHeader();
            this.cAge = new System.Windows.Forms.ColumnHeader();
            this.cCard = new System.Windows.Forms.ColumnHeader();
            this.cSimilarity = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.suspectImage)).BeginInit();
            this.grbTargetImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personOfInterestImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(186, 29);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(113, 12);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "日期： 2009-10-28 ";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(187, 72);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(95, 12);
            this.lblTime.TabIndex = 8;
            this.lblTime.Text = "时间： 11:02:38";
            // 
            // lblAddress
            // 
            this.lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(186, 116);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(155, 12);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "地址： **市**区**号**南门";
            // 
            // suspectImage
            // 
            this.suspectImage.Location = new System.Drawing.Point(6, 16);
            this.suspectImage.Name = "suspectImage";
            this.suspectImage.Size = new System.Drawing.Size(158, 179);
            this.suspectImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.suspectImage.TabIndex = 1;
            this.suspectImage.TabStop = false;
            // 
            // lblTextSim
            // 
            this.lblTextSim.AutoSize = true;
            this.lblTextSim.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTextSim.Location = new System.Drawing.Point(489, 29);
            this.lblTextSim.Name = "lblTextSim";
            this.lblTextSim.Size = new System.Drawing.Size(130, 16);
            this.lblTextSim.TabIndex = 16;
            this.lblTextSim.Text = "检测结果 55.9%";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(476, 172);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "处理";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(557, 172);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "消警";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grbTargetImg
            // 
            this.grbTargetImg.Controls.Add(this.clearAll);
            this.grbTargetImg.Controls.Add(this.btnCancel);
            this.grbTargetImg.Controls.Add(this.btnOK);
            this.grbTargetImg.Controls.Add(this.lblTextSim);
            this.grbTargetImg.Controls.Add(this.suspectImage);
            this.grbTargetImg.Controls.Add(this.lblAddress);
            this.grbTargetImg.Controls.Add(this.lblTime);
            this.grbTargetImg.Controls.Add(this.lblDate);
            this.grbTargetImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbTargetImg.Location = new System.Drawing.Point(0, 0);
            this.grbTargetImg.Name = "grbTargetImg";
            this.grbTargetImg.Size = new System.Drawing.Size(736, 202);
            this.grbTargetImg.TabIndex = 14;
            this.grbTargetImg.TabStop = false;
            this.grbTargetImg.Text = "待识别图片";
            // 
            // clearAll
            // 
            this.clearAll.Location = new System.Drawing.Point(638, 173);
            this.clearAll.Name = "clearAll";
            this.clearAll.Size = new System.Drawing.Size(75, 23);
            this.clearAll.TabIndex = 17;
            this.clearAll.Text = "全部消警";
            this.clearAll.UseVisualStyleBackColor = true;
            this.clearAll.Click += new System.EventHandler(this.clearAll_Click);
            // 
            // personOfInterestImage
            // 
            this.personOfInterestImage.Location = new System.Drawing.Point(6, 20);
            this.personOfInterestImage.Name = "personOfInterestImage";
            this.personOfInterestImage.Size = new System.Drawing.Size(158, 179);
            this.personOfInterestImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.personOfInterestImage.TabIndex = 0;
            this.personOfInterestImage.TabStop = false;
            // 
            // suspectsList
            // 
            this.suspectsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.suspectsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cId,
            this.cName,
            this.cSex,
            this.cAge,
            this.cCard,
            this.cSimilarity});
            this.suspectsList.FullRowSelect = true;
            this.suspectsList.GridLines = true;
            this.suspectsList.Location = new System.Drawing.Point(185, 20);
            this.suspectsList.Name = "suspectsList";
            this.suspectsList.Size = new System.Drawing.Size(545, 179);
            this.suspectsList.TabIndex = 15;
            this.suspectsList.UseCompatibleStateImageBehavior = false;
            this.suspectsList.View = System.Windows.Forms.View.Details;
            this.suspectsList.SelectedIndexChanged += new System.EventHandler(this.lvPersonInfo_SelectedIndexChanged);
            // 
            // cId
            // 
            this.cId.Text = "";
            this.cId.Width = 26;
            // 
            // cName
            // 
            this.cName.Text = "姓名";
            this.cName.Width = 62;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.suspectsList);
            this.groupBox1.Controls.Add(this.personOfInterestImage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 202);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 205);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预设目标";
            // 
            // ImmediatelyModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 410);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbTargetImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImmediatelyModel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "警报";
            this.Shown += new System.EventHandler(this.ImmediatelyModel_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImmediatelyModel_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.suspectImage)).EndInit();
            this.grbTargetImg.ResumeLayout(false);
            this.grbTargetImg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.personOfInterestImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.PictureBox suspectImage;
        private System.Windows.Forms.Label lblTextSim;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grbTargetImg;
        private System.Windows.Forms.PictureBox personOfInterestImage;
        private System.Windows.Forms.ListView suspectsList;
        private System.Windows.Forms.ColumnHeader cId;
        private System.Windows.Forms.ColumnHeader cName;
        private System.Windows.Forms.ColumnHeader cSex;
        private System.Windows.Forms.ColumnHeader cAge;
        private System.Windows.Forms.ColumnHeader cCard;
        private System.Windows.Forms.ColumnHeader cSimilarity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button clearAll;


    }
}