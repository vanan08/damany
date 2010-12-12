namespace Damany.PC.Shell.Winform
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
            this.cameraConfigurer2 = new Damany.RemoteImaging.Common.Controls.CameraConfigurer();
            this.SuspendLayout();
            // 
            // cameraConfigurer2
            // 
            this.cameraConfigurer2.Location = new System.Drawing.Point(4, 12);
            this.cameraConfigurer2.Name = "cameraConfigurer2";
            this.cameraConfigurer2.Size = new System.Drawing.Size(498, 349);
            this.cameraConfigurer2.TabIndex = 0;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 374);
            this.Controls.Add(this.cameraConfigurer2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.Shown += new System.EventHandler(this.OptionsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Damany.RemoteImaging.Common.Controls.CameraConfigurer cameraConfigurer1;
        public Damany.RemoteImaging.Common.Controls.CameraConfigurer cameraConfigurer2;
    }
}