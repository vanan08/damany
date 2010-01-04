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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewUserForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.repeatPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.passWord = new System.Windows.Forms.TextBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.lText = new System.Windows.Forms.Label();
            this.pHeader = new System.Windows.Forms.Panel();
            this.userValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(222, 213);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 38;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(123, 213);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 37;
            this.okButton.Text = "确认";
            this.okButton.UseVisualStyleBackColor = true;
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
            // repeatPassword
            // 
            this.repeatPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.repeatPassword.Location = new System.Drawing.Point(123, 163);
            this.repeatPassword.Name = "repeatPassword";
            this.repeatPassword.Size = new System.Drawing.Size(217, 21);
            this.repeatPassword.TabIndex = 35;
            this.repeatPassword.UseSystemPasswordChar = true;
            this.repeatPassword.Validating += new System.ComponentModel.CancelEventHandler(this.repeatPassword_Validating);
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
            // passWord
            // 
            this.passWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passWord.Location = new System.Drawing.Point(123, 125);
            this.passWord.Name = "passWord";
            this.userValidationProvider.SetPerformValidation(this.passWord, true);
            this.passWord.Size = new System.Drawing.Size(217, 21);
            this.userValidationProvider.SetSourcePropertyName(this.passWord, "Password");
            this.passWord.TabIndex = 30;
            this.passWord.UseSystemPasswordChar = true;
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
            // userName
            // 
            this.userName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userName.Location = new System.Drawing.Point(123, 80);
            this.userName.Name = "userName";
            this.userValidationProvider.SetPerformValidation(this.userName, true);
            this.userName.Size = new System.Drawing.Size(217, 21);
            this.userValidationProvider.SetSourcePropertyName(this.userName, "Name");
            this.userName.TabIndex = 29;
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
            // userValidationProvider
            // 
            this.userValidationProvider.ErrorProvider = this.errorProvider;
            this.userValidationProvider.RulesetName = "Rule Set";
            this.userValidationProvider.SourceTypeName = "Damany.Security.UsersAdmin.User,Damany.Security";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // AddNewUserForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(407, 248);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.repeatPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.passWord);
            this.Controls.Add(this.lUsername);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddNewUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加新用户";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pHeader.ResumeLayout(false);
            this.pHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox repeatPassword;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.Panel pHeader;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider userValidationProvider;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}