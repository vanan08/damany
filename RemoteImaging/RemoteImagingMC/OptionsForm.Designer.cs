namespace RemoteImaging
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabelConfigCamera = new System.Windows.Forms.LinkLabel();
            this.dataGridCameras = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.deleteSelectedUser = new System.Windows.Forms.Button();
            this.addNewUserButton = new System.Windows.Forms.Button();
            this.usesList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Location = new System.Drawing.Point(-6, -24);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(574, 89);
            this.groupControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(110, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置系统参数";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(44, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 339);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabelConfigCamera);
            this.tabPage2.Controls.Add(this.dataGridCameras);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(486, 314);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "摄像头";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabelConfigCamera
            // 
            this.linkLabelConfigCamera.AutoSize = true;
            this.linkLabelConfigCamera.Location = new System.Drawing.Point(374, 15);
            this.linkLabelConfigCamera.Name = "linkLabelConfigCamera";
            this.linkLabelConfigCamera.Size = new System.Drawing.Size(89, 12);
            this.linkLabelConfigCamera.TabIndex = 1;
            this.linkLabelConfigCamera.TabStop = true;
            this.linkLabelConfigCamera.Text = "设置摄像头参数";
            this.linkLabelConfigCamera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelConfigCamera_LinkClicked);
            // 
            // dataGridCameras
            // 
            this.dataGridCameras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCameras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.ID,
            this.IP});
            this.dataGridCameras.Location = new System.Drawing.Point(16, 30);
            this.dataGridCameras.Name = "dataGridCameras";
            this.dataGridCameras.RowTemplate.Height = 23;
            this.dataGridCameras.Size = new System.Drawing.Size(449, 228);
            this.dataGridCameras.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            // 
            // ID
            // 
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.deleteSelectedUser);
            this.tabPage1.Controls.Add(this.addNewUserButton);
            this.tabPage1.Controls.Add(this.usesList);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(486, 314);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "用户管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // deleteSelectedUser
            // 
            this.deleteSelectedUser.Location = new System.Drawing.Point(110, 216);
            this.deleteSelectedUser.Name = "deleteSelectedUser";
            this.deleteSelectedUser.Size = new System.Drawing.Size(88, 23);
            this.deleteSelectedUser.TabIndex = 3;
            this.deleteSelectedUser.Text = "删除选定用户";
            this.deleteSelectedUser.UseVisualStyleBackColor = true;
            // 
            // addNewUserButton
            // 
            this.addNewUserButton.Location = new System.Drawing.Point(29, 216);
            this.addNewUserButton.Name = "addNewUserButton";
            this.addNewUserButton.Size = new System.Drawing.Size(75, 23);
            this.addNewUserButton.TabIndex = 2;
            this.addNewUserButton.Text = "添加新用户";
            this.addNewUserButton.UseVisualStyleBackColor = true;
            this.addNewUserButton.Click += new System.EventHandler(this.addNewUserButton_Click);
            // 
            // usesList
            // 
            this.usesList.FormattingEnabled = true;
            this.usesList.ItemHeight = 12;
            this.usesList.Location = new System.Drawing.Point(29, 50);
            this.usesList.Name = "usesList";
            this.usesList.Size = new System.Drawing.Size(417, 160);
            this.usesList.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户列表：";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(344, 416);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 21);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 416);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "人像截取阈值";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(93, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 2);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(370, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "下:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(400, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 21);
            this.textBox5.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(264, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "上:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(294, 49);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(51, 21);
            this.textBox6.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(150, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "右:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(180, 49);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(51, 21);
            this.textBox7.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(53, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "左:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "人像截取比例";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(93, 30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(380, 2);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(83, 49);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(51, 21);
            this.textBox8.TabIndex = 0;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(504, 440);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridView dataGridCameras;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.LinkLabel linkLabelConfigCamera;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button deleteSelectedUser;
        private System.Windows.Forms.Button addNewUserButton;
        private System.Windows.Forms.ListBox usesList;
    }
}