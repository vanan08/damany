namespace Damany.RemoteImaging.Common.Forms
{
    partial class ChangePasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePasswordForm));
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.pHeader = new System.Windows.Forms.Panel();
            this.lText = new System.Windows.Forms.Label();
            this.lPassword = new System.Windows.Forms.Label();
            this.lUsername = new System.Windows.Forms.Label();
            this.oldPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.repeatedPassword = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.userValidationProvider = new Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.pHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
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
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pHeader.Controls.Add(this.pbImage);
            this.pHeader.Controls.Add(this.lText);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(385, 60);
            this.pHeader.TabIndex = 15;
            // 
            // lText
            // 
            this.lText.Location = new System.Drawing.Point(24, 20);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(241, 20);
            this.lText.TabIndex = 4;
            this.lText.Text = "请输入你的用户名和旧密码";
            this.lText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lPassword
            // 
            this.lPassword.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lPassword.Location = new System.Drawing.Point(23, 132);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(80, 20);
            this.lPassword.TabIndex = 21;
            this.lPassword.Text = "旧密码:";
            this.lPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lUsername
            // 
            this.lUsername.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUsername.Location = new System.Drawing.Point(23, 92);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(80, 20);
            this.lUsername.TabIndex = 20;
            this.lUsername.Text = "用户名:";
            this.lUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // oldPassword
            // 
            this.oldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.oldPassword.Location = new System.Drawing.Point(123, 132);
            this.oldPassword.Name = "oldPassword";
            this.oldPassword.Size = new System.Drawing.Size(217, 21);
            this.oldPassword.TabIndex = 19;
            this.oldPassword.UseSystemPasswordChar = true;
            // 
            // tbUsername
            // 
            this.tbUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbUsername.Location = new System.Drawing.Point(123, 92);
            this.tbUsername.Name = "tbUsername";
            this.userValidationProvider.SetPerformValidation(this.tbUsername, true);
            this.tbUsername.Size = new System.Drawing.Size(217, 21);
            this.userValidationProvider.SetSourcePropertyName(this.tbUsername, "Name");
            this.tbUsername.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "新密码:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // newPassword
            // 
            this.newPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newPassword.Location = new System.Drawing.Point(123, 191);
            this.newPassword.Name = "newPassword";
            this.userValidationProvider.SetPerformValidation(this.newPassword, true);
            this.newPassword.Size = new System.Drawing.Size(217, 21);
            this.userValidationProvider.SetSourcePropertyName(this.newPassword, "Password");
            this.newPassword.TabIndex = 22;
            this.newPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "确认新密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // repeatedPassword
            // 
            this.repeatedPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.errorProvider.SetError(this.repeatedPassword, "两次输入的密码不一致。");
            this.repeatedPassword.Location = new System.Drawing.Point(123, 231);
            this.repeatedPassword.Name = "repeatedPassword";
            this.repeatedPassword.Size = new System.Drawing.Size(217, 21);
            this.userValidationProvider.SetSourcePropertyName(this.repeatedPassword, "RepeatedPassword");
            this.repeatedPassword.TabIndex = 24;
            this.repeatedPassword.UseSystemPasswordChar = true;
            this.repeatedPassword.Validating += new System.ComponentModel.CancelEventHandler(this.repeatedPassword_Validating);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(107, 287);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 26;
            this.okButton.Text = "确认";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(204, 287);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 27;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
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
            // ChangePasswordForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(385, 322);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.repeatedPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPassword);
            this.Controls.Add(this.lPassword);
            this.Controls.Add(this.lUsername);
            this.Controls.Add(this.oldPassword);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ChangePasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.pHeader.ResumeLayout(false);
            this.pHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.TextBox oldPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox repeatedPassword;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WinForms.ValidationProvider userValidationProvider;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}