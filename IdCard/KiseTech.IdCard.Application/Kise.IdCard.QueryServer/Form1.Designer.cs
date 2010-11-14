namespace Kise.IdCard.QueryServer
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
            this.serverText = new System.Windows.Forms.TextBox();
            this.clientText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serverSend = new System.Windows.Forms.Button();
            this.clientSend = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // serverText
            // 
            this.serverText.Location = new System.Drawing.Point(94, 287);
            this.serverText.Name = "serverText";
            this.serverText.Size = new System.Drawing.Size(145, 20);
            this.serverText.TabIndex = 1;
            // 
            // clientText
            // 
            this.clientText.Location = new System.Drawing.Point(345, 288);
            this.clientText.Name = "clientText";
            this.clientText.Size = new System.Drawing.Size(145, 20);
            this.clientText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 290);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Client";
            // 
            // serverSend
            // 
            this.serverSend.Location = new System.Drawing.Point(74, 320);
            this.serverSend.Name = "serverSend";
            this.serverSend.Size = new System.Drawing.Size(75, 23);
            this.serverSend.TabIndex = 5;
            this.serverSend.Text = "Send";
            this.serverSend.UseVisualStyleBackColor = true;
            this.serverSend.Click += new System.EventHandler(this.serverSend_Click);
            // 
            // clientSend
            // 
            this.clientSend.Location = new System.Drawing.Point(382, 320);
            this.clientSend.Name = "clientSend";
            this.clientSend.Size = new System.Drawing.Size(75, 23);
            this.clientSend.TabIndex = 6;
            this.clientSend.Text = "Send";
            this.clientSend.UseVisualStyleBackColor = true;
            this.clientSend.Click += new System.EventHandler(this.clientSend_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(28, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(514, 269);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 104;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 355);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.clientSend);
            this.Controls.Add(this.serverSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientText);
            this.Controls.Add(this.serverText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverText;
        private System.Windows.Forms.TextBox clientText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button serverSend;
        private System.Windows.Forms.Button clientSend;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

