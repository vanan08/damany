namespace KiseTech.IdCard.UI
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
            this.destinationNo = new System.Windows.Forms.TextBox();
            this.msgToSend = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.response = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // destinationNo
            // 
            this.destinationNo.Location = new System.Drawing.Point(55, 22);
            this.destinationNo.Name = "destinationNo";
            this.destinationNo.Size = new System.Drawing.Size(100, 21);
            this.destinationNo.TabIndex = 0;
            this.destinationNo.Text = "10086";
            // 
            // msgToSend
            // 
            this.msgToSend.Location = new System.Drawing.Point(55, 61);
            this.msgToSend.Name = "msgToSend";
            this.msgToSend.Size = new System.Drawing.Size(274, 21);
            this.msgToSend.TabIndex = 1;
            this.msgToSend.Text = "0000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(361, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // response
            // 
            this.response.Location = new System.Drawing.Point(55, 109);
            this.response.Multiline = true;
            this.response.Name = "response";
            this.response.Size = new System.Drawing.Size(274, 184);
            this.response.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 321);
            this.Controls.Add(this.response);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.msgToSend);
            this.Controls.Add(this.destinationNo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox destinationNo;
        private System.Windows.Forms.TextBox msgToSend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox response;
    }
}

