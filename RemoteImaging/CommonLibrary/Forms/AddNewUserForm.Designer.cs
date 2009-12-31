namespace Damany.RemoteImaging.Common.Forms
{
    partial class AddNewUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewUserForm));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lText = new System.Windows.Forms.Label();
            this.pHeader = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(222, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(123, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "确认密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(123, 163);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(217, 21);
            this.textBox2.TabIndex = 35;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // lPassword
            // 
            this.lPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassword.Location = new System.Drawing.Point(23, 125);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(80, 20);
            this.lPassword.TabIndex = 32;
            this.lPassword.Text = "密码:";
            this.lPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPassword.Location = new System.Drawing.Point(123, 125);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(217, 21);
            this.tbPassword.TabIndex = 30;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // pbImage
            // 
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(305, 7);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(48, 48);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbImage.TabIndex = 5;
            this.pbImage.TabStop = false;
            // 
            // lUsername
            // 
            this.lUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUsername.Location = new System.Drawing.Point(23, 80);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(80, 20);
            this.lUsername.TabIndex = 31;
            this.lUsername.Text = "用户名:";
            this.lUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Location = new System.Drawing.Point(123, 80);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(217, 21);
            this.tbUsername.TabIndex = 29;
            // 
            // lText
            // 
            this.lText.Location = new System.Drawing.Point(24, 20);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(133, 20);
            this.lText.TabIndex = 4;
            this.lText.Text = "请输入新用户名和密码";
            this.lText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pHeader.Controls.Add(this.pbImage);
            this.pHeader.Controls.Add(this.lText);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(407, 60);
            this.pHeader.TabIndex = 28;
            // 
            // AddNewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 248);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lUsername);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddNewUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加新用户";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pHeader.ResumeLayout(false);
            this.pHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.Panel pHeader;
    }
}