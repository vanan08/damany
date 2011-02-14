namespace RemoteImaging
{
    partial class FormFaceRecognitionResult
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
            this.capturedFace = new System.Windows.Forms.PictureBox();
            this.faceInLibrary = new System.Windows.Forms.PictureBox();
            this.similarity = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.capturedFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceInLibrary)).BeginInit();
            this.SuspendLayout();
            // 
            // capturedFace
            // 
            this.capturedFace.Location = new System.Drawing.Point(30, 24);
            this.capturedFace.Name = "capturedFace";
            this.capturedFace.Size = new System.Drawing.Size(188, 227);
            this.capturedFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.capturedFace.TabIndex = 0;
            this.capturedFace.TabStop = false;
            // 
            // faceInLibrary
            // 
            this.faceInLibrary.Location = new System.Drawing.Point(310, 24);
            this.faceInLibrary.Name = "faceInLibrary";
            this.faceInLibrary.Size = new System.Drawing.Size(190, 225);
            this.faceInLibrary.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.faceInLibrary.TabIndex = 1;
            this.faceInLibrary.TabStop = false;
            // 
            // similarity
            // 
            this.similarity.AutoSize = true;
            this.similarity.Location = new System.Drawing.Point(243, 118);
            this.similarity.Name = "similarity";
            this.similarity.Size = new System.Drawing.Size(41, 12);
            this.similarity.TabIndex = 2;
            this.similarity.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // FaceRecognitionResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 288);
            this.Controls.Add(this.similarity);
            this.Controls.Add(this.faceInLibrary);
            this.Controls.Add(this.capturedFace);
            this.Name = "FaceRecognitionResult";
            this.ShowInTaskbar = false;
            this.Text = "FaceRecognitionResult";
            this.Load += new System.EventHandler(this.FaceRecognitionResult_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceRecognitionResult_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.capturedFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceInLibrary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox capturedFace;
        public System.Windows.Forms.PictureBox faceInLibrary;
        public System.Windows.Forms.Label similarity;
        private System.Windows.Forms.Timer timer1;

    }
}