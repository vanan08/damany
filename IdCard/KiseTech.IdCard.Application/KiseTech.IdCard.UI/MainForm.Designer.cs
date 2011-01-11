namespace Kise.IdCard.UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.buttonQuery = new DevExpress.XtraBars.BarButtonItem();
            this.settingsButton = new DevExpress.XtraBars.BarButtonItem();
            this.progressBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.statusLabel = new DevExpress.XtraBars.BarStaticItem();
            this.startButton = new DevExpress.XtraBars.BarButtonItem();
            this.stopButton = new DevExpress.XtraBars.BarButtonItem();
            this.databaseQuery = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.resultLabel = new DevExpress.XtraEditors.LabelControl();
            this.image = new DevExpress.XtraEditors.PictureEdit();
            this.idCardNo = new DevExpress.XtraEditors.LabelControl();
            this.address = new DevExpress.XtraEditors.LabelControl();
            this.day = new DevExpress.XtraEditors.LabelControl();
            this.month = new DevExpress.XtraEditors.LabelControl();
            this.year = new DevExpress.XtraEditors.LabelControl();
            this.minority = new DevExpress.XtraEditors.LabelControl();
            this.sex = new DevExpress.XtraEditors.LabelControl();
            this.name = new DevExpress.XtraEditors.LabelControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
            // 
            // 
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.buttonQuery,
            this.settingsButton,
            this.progressBar,
            this.statusLabel,
            this.startButton,
            this.stopButton,
            this.databaseQuery});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 13;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemMarqueeProgressBar1});
            this.ribbon.SelectedPage = this.ribbonPage1;
            this.ribbon.Size = new System.Drawing.Size(681, 149);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // buttonQuery
            // 
            this.buttonQuery.Caption = "历史记录";
            this.buttonQuery.Id = 1;
            this.buttonQuery.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("buttonQuery.LargeGlyph")));
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.buttonQuery_ItemClick);
            // 
            // settingsButton
            // 
            this.settingsButton.Caption = "设置";
            this.settingsButton.Id = 5;
            this.settingsButton.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("settingsButton.LargeGlyph")));
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.settingsButton_ItemClick);
            // 
            // progressBar
            // 
            this.progressBar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.progressBar.Edit = this.repositoryItemMarqueeProgressBar1;
            this.progressBar.Id = 7;
            this.progressBar.Name = "progressBar";
            this.progressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBar.Width = 100;
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            this.repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // statusLabel
            // 
            this.statusLabel.Caption = "就绪";
            this.statusLabel.Id = 8;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // startButton
            // 
            this.startButton.Caption = "启动";
            this.startButton.Id = 10;
            this.startButton.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("startButton.LargeGlyph")));
            this.startButton.Name = "startButton";
            this.startButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.startButton_ItemClick);
            // 
            // stopButton
            // 
            this.stopButton.Caption = "停止";
            this.stopButton.Enabled = false;
            this.stopButton.Id = 11;
            this.stopButton.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("stopButton.LargeGlyph")));
            this.stopButton.Name = "stopButton";
            this.stopButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.stopButton_ItemClick);
            // 
            // databaseQuery
            // 
            this.databaseQuery.Caption = "数据库查询";
            this.databaseQuery.Enabled = false;
            this.databaseQuery.Id = 12;
            this.databaseQuery.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("databaseQuery.LargeGlyph")));
            this.databaseQuery.Name = "databaseQuery";
            this.databaseQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.databaseQuery_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "控制面板";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.settingsButton);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "系统";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.buttonQuery);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "工具";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.startButton);
            this.ribbonPageGroup3.ItemLinks.Add(this.stopButton);
            this.ribbonPageGroup3.ItemLinks.Add(this.databaseQuery);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.ShowCaptionButton = false;
            this.ribbonPageGroup3.Text = "控制";
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.progressBar);
            this.ribbonStatusBar.ItemLinks.Add(this.statusLabel);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 540);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(681, 31);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.ContentImage = ((System.Drawing.Image)(resources.GetObject("clientPanel.ContentImage")));
            this.clientPanel.Controls.Add(this.resultLabel);
            this.clientPanel.Controls.Add(this.image);
            this.clientPanel.Controls.Add(this.idCardNo);
            this.clientPanel.Controls.Add(this.address);
            this.clientPanel.Controls.Add(this.day);
            this.clientPanel.Controls.Add(this.month);
            this.clientPanel.Controls.Add(this.year);
            this.clientPanel.Controls.Add(this.minority);
            this.clientPanel.Controls.Add(this.sex);
            this.clientPanel.Controls.Add(this.name);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 149);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(681, 391);
            this.clientPanel.TabIndex = 2;
            // 
            // resultLabel
            // 
            this.resultLabel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.resultLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.resultLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.resultLabel.Location = new System.Drawing.Point(3, 3);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(675, 27);
            this.resultLabel.TabIndex = 10;
            this.resultLabel.Text = "labelControl1";
            this.resultLabel.Visible = false;
            // 
            // image
            // 
            this.image.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.image.Location = new System.Drawing.Point(427, 58);
            this.image.MenuManager = this.ribbon;
            this.image.Name = "image";
            this.image.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.image.Size = new System.Drawing.Size(133, 142);
            this.image.TabIndex = 8;
            // 
            // idCardNo
            // 
            this.idCardNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idCardNo.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idCardNo.Location = new System.Drawing.Point(252, 297);
            this.idCardNo.Name = "idCardNo";
            this.idCardNo.Size = new System.Drawing.Size(0, 19);
            this.idCardNo.TabIndex = 7;
            // 
            // address
            // 
            this.address.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.address.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.address.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.address.Location = new System.Drawing.Point(182, 202);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(224, 0);
            this.address.TabIndex = 6;
            // 
            // day
            // 
            this.day.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.day.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.day.Location = new System.Drawing.Point(316, 160);
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(0, 19);
            this.day.TabIndex = 5;
            // 
            // month
            // 
            this.month.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.month.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.month.Location = new System.Drawing.Point(269, 160);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(0, 19);
            this.month.TabIndex = 4;
            // 
            // year
            // 
            this.year.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.year.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.year.Location = new System.Drawing.Point(182, 160);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(0, 19);
            this.year.TabIndex = 3;
            // 
            // minority
            // 
            this.minority.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minority.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.minority.Location = new System.Drawing.Point(293, 122);
            this.minority.Name = "minority";
            this.minority.Size = new System.Drawing.Size(0, 19);
            this.minority.TabIndex = 2;
            // 
            // sex
            // 
            this.sex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sex.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sex.Location = new System.Drawing.Point(182, 122);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(0, 19);
            this.sex.TabIndex = 1;
            // 
            // name
            // 
            this.name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.name.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(182, 83);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(0, 19);
            this.name.TabIndex = 0;
            // 
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 571);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "二代身份证自动查询系统";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            this.clientPanel.ResumeLayout(false);
            this.clientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.image.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.BarButtonItem buttonQuery;
        private DevExpress.XtraBars.BarButtonItem settingsButton;
        private DevExpress.XtraBars.BarEditItem progressBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarStaticItem statusLabel;
        private DevExpress.Xpo.XPCollection xpCollection1;
        private DevExpress.XtraBars.BarButtonItem startButton;
        private DevExpress.XtraBars.BarButtonItem stopButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem databaseQuery;
        private DevExpress.XtraEditors.PictureEdit image;
        private DevExpress.XtraEditors.LabelControl idCardNo;
        private DevExpress.XtraEditors.LabelControl address;
        private DevExpress.XtraEditors.LabelControl day;
        private DevExpress.XtraEditors.LabelControl month;
        private DevExpress.XtraEditors.LabelControl year;
        private DevExpress.XtraEditors.LabelControl minority;
        private DevExpress.XtraEditors.LabelControl sex;
        private DevExpress.XtraEditors.LabelControl name;
        private DevExpress.XtraEditors.LabelControl resultLabel;
    }
}