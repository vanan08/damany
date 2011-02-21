namespace FaceSearcherTester
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.applyButton = new System.Windows.Forms.Button();
            this.h = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.w = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.x = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.applyROI = new System.Windows.Forms.CheckBox();
            this.drawFaceSize = new System.Windows.Forms.CheckBox();
            this.maxFaceWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.minFaceWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.applyButton);
            this.panel1.Controls.Add(this.h);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.w);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.y);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.x);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.applyROI);
            this.panel1.Controls.Add(this.drawFaceSize);
            this.panel1.Controls.Add(this.maxFaceWidth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.minFaceWidth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(141, 451);
            this.panel1.TabIndex = 10;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(31, 383);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 14;
            this.applyButton.Text = "应用";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // h
            // 
            this.h.Location = new System.Drawing.Point(62, 293);
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(42, 20);
            this.h.TabIndex = 13;
            this.h.Text = "0";
            this.h.Validating += new System.ComponentModel.CancelEventHandler(this.h_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "H:";
            // 
            // w
            // 
            this.w.Location = new System.Drawing.Point(62, 261);
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(42, 20);
            this.w.TabIndex = 11;
            this.w.Text = "0";
            this.w.Validating += new System.ComponentModel.CancelEventHandler(this.w_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "W:";
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(62, 230);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(42, 20);
            this.y.TabIndex = 9;
            this.y.Text = "0";
            this.y.Validating += new System.ComponentModel.CancelEventHandler(this.y_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y:";
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(62, 201);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(42, 20);
            this.x.TabIndex = 7;
            this.x.Text = "0";
            this.x.Validating += new System.ComponentModel.CancelEventHandler(this.x_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "X:";
            // 
            // applyROI
            // 
            this.applyROI.AutoSize = true;
            this.applyROI.Location = new System.Drawing.Point(20, 175);
            this.applyROI.Name = "applyROI";
            this.applyROI.Size = new System.Drawing.Size(98, 17);
            this.applyROI.TabIndex = 5;
            this.applyROI.Text = "应用搜索区域";
            this.applyROI.UseVisualStyleBackColor = true;
            // 
            // drawFaceSize
            // 
            this.drawFaceSize.AutoSize = true;
            this.drawFaceSize.Location = new System.Drawing.Point(20, 142);
            this.drawFaceSize.Name = "drawFaceSize";
            this.drawFaceSize.Size = new System.Drawing.Size(98, 17);
            this.drawFaceSize.TabIndex = 4;
            this.drawFaceSize.Text = "标记人像大小";
            this.drawFaceSize.UseVisualStyleBackColor = true;
            // 
            // maxFaceWidth
            // 
            this.maxFaceWidth.Location = new System.Drawing.Point(6, 96);
            this.maxFaceWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxFaceWidth.Name = "maxFaceWidth";
            this.maxFaceWidth.Size = new System.Drawing.Size(120, 20);
            this.maxFaceWidth.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "最小脸宽（像素）";
            // 
            // minFaceWidth
            // 
            this.minFaceWidth.Location = new System.Drawing.Point(7, 44);
            this.minFaceWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minFaceWidth.Name = "minFaceWidth";
            this.minFaceWidth.Size = new System.Drawing.Size(120, 20);
            this.minFaceWidth.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "最小脸宽（像素）";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(607, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(141, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(466, 451);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(38, 17);
            this.statusLabel.Text = "就绪";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 473);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "人脸搜索测试器";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxFaceWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFaceWidth)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown minFaceWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox h;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox w;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox applyROI;
        private System.Windows.Forms.CheckBox drawFaceSize;
        private System.Windows.Forms.NumericUpDown maxFaceWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}

