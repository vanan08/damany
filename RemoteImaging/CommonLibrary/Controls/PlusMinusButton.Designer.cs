namespace Damany.RemoteImaging.Common.Controls
{
    partial class PlusMinusButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlusMinusButton));
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.buttonsContainer1 = new DevExpress.ExpressApp.Win.Templates.ActionContainers.ButtonsContainer();
            ((System.ComponentModel.ISupportInitialize)(this.buttonsContainer1)).BeginInit();
            this.buttonsContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton3
            // 
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(3, 3);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(26, 22);
            this.simpleButton3.TabIndex = 10;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(131, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(26, 22);
            this.simpleButton1.TabIndex = 11;
            // 
            // buttonsContainer1
            // 
            this.buttonsContainer1.Controls.Add(this.simpleButton1);
            this.buttonsContainer1.Controls.Add(this.simpleButton3);
            this.buttonsContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsContainer1.Location = new System.Drawing.Point(0, 0);
            this.buttonsContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.buttonsContainer1.Name = "buttonsContainer1";
            this.buttonsContainer1.Size = new System.Drawing.Size(160, 28);
            this.buttonsContainer1.TabIndex = 11;
            // 
            // PlusMinusButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.buttonsContainer1);
            this.Name = "PlusMinusButton";
            this.Size = new System.Drawing.Size(160, 28);
            ((System.ComponentModel.ISupportInitialize)(this.buttonsContainer1)).EndInit();
            this.buttonsContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.ExpressApp.Win.Templates.ActionContainers.ButtonsContainer buttonsContainer1;


    }
}
