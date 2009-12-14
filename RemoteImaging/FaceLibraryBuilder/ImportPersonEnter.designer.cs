namespace FaceLibraryBuilder
{
    partial class ImportPersonEnter
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
            this.picTargetPerson = new System.Windows.Forms.PictureBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.rabMan = new System.Windows.Forms.RadioButton();
            this.rabWoman = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addFinished = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picTargetPerson)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // picTargetPerson
            // 
            this.picTargetPerson.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picTargetPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTargetPerson.Location = new System.Drawing.Point(3, 17);
            this.picTargetPerson.Name = "picTargetPerson";
            this.picTargetPerson.Size = new System.Drawing.Size(334, 376);
            this.picTargetPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTargetPerson.TabIndex = 10;
            this.picTargetPerson.TabStop = false;
            this.picTargetPerson.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTargetPerson_MouseMove);
            this.picTargetPerson.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTargetPerson_MouseDown);
            this.picTargetPerson.Paint += new System.Windows.Forms.PaintEventHandler(this.picTargetPerson_Paint);
            this.picTargetPerson.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTargetPerson_MouseUp);
            // 
            // lblAge
            // 
            this.lblAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(381, 174);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 12);
            this.lblAge.TabIndex = 13;
            this.lblAge.Text = "年龄";
            // 
            // s
            // 
            this.s.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s.AutoSize = true;
            this.s.Location = new System.Drawing.Point(381, 106);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(29, 12);
            this.s.TabIndex = 12;
            this.s.Text = "姓名";
            // 
            // lblId
            // 
            this.lblId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(381, 72);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(29, 12);
            this.lblId.TabIndex = 11;
            this.lblId.Text = "编号";
            // 
            // lblSex
            // 
            this.lblSex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(381, 140);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(29, 12);
            this.lblSex.TabIndex = 14;
            this.lblSex.Text = "性别";
            // 
            // lblCard
            // 
            this.lblCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCard.AutoSize = true;
            this.lblCard.Location = new System.Drawing.Point(381, 208);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(53, 12);
            this.lblCard.TabIndex = 15;
            this.lblCard.Text = "身份证号";
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtId.Location = new System.Drawing.Point(446, 69);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(167, 21);
            this.txtId.TabIndex = 17;
            this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(446, 103);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 21);
            this.txtName.TabIndex = 18;
            this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtAge
            // 
            this.txtAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAge.Location = new System.Drawing.Point(446, 171);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(167, 21);
            this.txtAge.TabIndex = 20;
            this.txtAge.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
            // 
            // txtCard
            // 
            this.txtCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCard.Location = new System.Drawing.Point(446, 205);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(167, 21);
            this.txtCard.TabIndex = 21;
            this.txtCard.Text = "12345678910111213";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(434, 406);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "添加";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rabMan
            // 
            this.rabMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rabMan.AutoSize = true;
            this.rabMan.Checked = true;
            this.rabMan.Location = new System.Drawing.Point(446, 138);
            this.rabMan.Name = "rabMan";
            this.rabMan.Size = new System.Drawing.Size(35, 16);
            this.rabMan.TabIndex = 24;
            this.rabMan.TabStop = true;
            this.rabMan.Text = "男";
            this.rabMan.UseVisualStyleBackColor = true;
            // 
            // rabWoman
            // 
            this.rabWoman.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rabWoman.AutoSize = true;
            this.rabWoman.Location = new System.Drawing.Point(532, 138);
            this.rabWoman.Name = "rabWoman";
            this.rabWoman.Size = new System.Drawing.Size(35, 16);
            this.rabWoman.TabIndex = 25;
            this.rabWoman.Text = "女";
            this.rabWoman.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.picTargetPerson);
            this.groupBox1.Location = new System.Drawing.Point(8, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 396);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重点人员图片显示";
            // 
            // addFinished
            // 
            this.addFinished.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addFinished.Location = new System.Drawing.Point(541, 406);
            this.addFinished.Name = "addFinished";
            this.addFinished.Size = new System.Drawing.Size(75, 23);
            this.addFinished.TabIndex = 30;
            this.addFinished.Text = "完成";
            this.addFinished.UseVisualStyleBackColor = true;
            this.addFinished.Click += new System.EventHandler(this.addFinished_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ImportPersonEnter
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 441);
            this.Controls.Add(this.addFinished);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rabWoman);
            this.Controls.Add(this.rabMan);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.s);
            this.Controls.Add(this.lblAge);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPersonEnter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "重点目标人信息录入";
            this.Load += new System.EventHandler(this.ImportPersonEnter_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImportPersonEnter_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.ImportPersonEnter_DragOver);
            ((System.ComponentModel.ISupportInitialize)(this.picTargetPerson)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picTargetPerson;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rabMan;
        private System.Windows.Forms.RadioButton rabWoman;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addFinished;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}