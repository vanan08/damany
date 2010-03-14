namespace Damany.PC.Shell.Winform
{
    partial class Form1
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
            this.frame = new System.Windows.Forms.PictureBox();
            this.portrait = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).BeginInit();
            this.SuspendLayout();
            // 
            // frame
            // 
            this.frame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame.Location = new System.Drawing.Point(0, 0);
            this.frame.Name = "frame";
            this.frame.Size = new System.Drawing.Size(618, 383);
            this.frame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.frame.TabIndex = 0;
            this.frame.TabStop = false;
            // 
            // portrait
            // 
            this.portrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.portrait.BackColor = System.Drawing.Color.Transparent;
            this.portrait.Location = new System.Drawing.Point(476, 280);
            this.portrait.Name = "portrait";
            this.portrait.Size = new System.Drawing.Size(142, 103);
            this.portrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.portrait.TabIndex = 1;
            this.portrait.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 383);
            this.Controls.Add(this.portrait);
            this.Controls.Add(this.frame);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox frame;
        private System.Windows.Forms.PictureBox portrait;
    }
}

