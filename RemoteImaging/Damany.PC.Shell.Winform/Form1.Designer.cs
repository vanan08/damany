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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.frame = new System.Windows.Forms.PictureBox();
            this.portrait = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.options = new System.Windows.Forms.ToolStripButton();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.slowDown = new System.Windows.Forms.ToolStripButton();
            this.speedUp = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.frame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.portrait.Location = new System.Drawing.Point(476, 255);
            this.portrait.Name = "portrait";
            this.portrait.Size = new System.Drawing.Size(142, 128);
            this.portrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.portrait.TabIndex = 1;
            this.portrait.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options,
            this.startButton,
            this.slowDown,
            this.speedUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(618, 26);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // options
            // 
            this.options.Image = ((System.Drawing.Image)(resources.GetObject("options.Image")));
            this.options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(55, 23);
            this.options.Text = "选项";
            this.options.Click += new System.EventHandler(this.options_Click);
            // 
            // startButton
            // 
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(55, 23);
            this.startButton.Text = "启动";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // slowDown
            // 
            this.slowDown.Image = ((System.Drawing.Image)(resources.GetObject("slowDown.Image")));
            this.slowDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.slowDown.Name = "slowDown";
            this.slowDown.Size = new System.Drawing.Size(81, 23);
            this.slowDown.Text = "降低速度";
            this.slowDown.Click += new System.EventHandler(this.slowDown_Click);
            // 
            // speedUp
            // 
            this.speedUp.Image = ((System.Drawing.Image)(resources.GetObject("speedUp.Image")));
            this.speedUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.speedUp.Name = "speedUp";
            this.speedUp.Size = new System.Drawing.Size(81, 23);
            this.speedUp.Text = "加快速度";
            this.speedUp.Click += new System.EventHandler(this.speedUp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 383);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.portrait);
            this.Controls.Add(this.frame);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.frame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portrait)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox frame;
        private System.Windows.Forms.PictureBox portrait;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton options;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton slowDown;
        private System.Windows.Forms.ToolStripButton speedUp;
    }
}

