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
            this.reportButton = new DevExpress.XtraBars.BarButtonItem();
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
            this.idCardStatus = new DevExpress.XtraEditors.LabelControl();
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
            this.reportButton,
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
            this.ribbon.Size = new System.Drawing.Size(511, 149);
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
            // reportButton
            // 
            this.reportButton.Caption = "报表";
            this.reportButton.Id = 9;
            this.reportButton.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("reportButton.LargeGlyph")));
            this.reportButton.Name = "reportButton";
            this.reportButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.reportButton_ItemClick);
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
            this.stopButton.Id = 11;
            this.stopButton.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("stopButton.LargeGlyph")));
            this.stopButton.Name = "stopButton";
            this.stopButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.stopButton_ItemClick);
            // 
            // databaseQuery
            // 
            this.databaseQuery.Caption = "真伪查询";
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
            this.ribbonPageGroup2.ItemLinks.Add(this.reportButton);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "工具";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.startButton);
            this.ribbonPageGroup3.ItemLinks.Add(this.stopButton);
            this.ribbonPageGroup3.ItemLinks.Add(this.databaseQuery);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 473);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(511, 23);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.ContentImage = ((System.Drawing.Image)(resources.GetObject("clientPanel.ContentImage")));
            this.clientPanel.Controls.Add(this.idCardStatus);
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
            this.clientPanel.Size = new System.Drawing.Size(511, 324);
            this.clientPanel.TabIndex = 2;
            // 
            // idCardStatus
            // 
            this.idCardStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idCardStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.idCardStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.idCardStatus.Location = new System.Drawing.Point(367, 163);
            this.idCardStatus.Name = "idCardStatus";
            this.idCardStatus.Size = new System.Drawing.Size(114, 20);
            this.idCardStatus.TabIndex = 9;
            this.idCardStatus.Text = "labelControl9";
            // 
            // image
            // 
            this.image.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.image.Location = new System.Drawing.Point(367, 25);
            this.image.MenuManager = this.ribbon;
            this.image.Name = "image";
            this.image.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.image.Size = new System.Drawing.Size(114, 132);
            this.image.TabIndex = 8;
            // 
            // idCardNo
            // 
            this.idCardNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idCardNo.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idCardNo.Location = new System.Drawing.Point(162, 263);
            this.idCardNo.Name = "idCardNo";
            this.idCardNo.Size = new System.Drawing.Size(40, 19);
            this.idCardNo.TabIndex = 7;
            this.idCardNo.Text = "idNo";
            // 
            // address
            // 
            this.address.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.address.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.address.Location = new System.Drawing.Point(94, 167);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(70, 19);
            this.address.TabIndex = 6;
            this.address.Text = "address";
            // 
            // day
            // 
            this.day.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.day.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.day.Location = new System.Drawing.Point(232, 126);
            this.day.Name = "day";
            this.day.Size = new System.Drawing.Size(10, 19);
            this.day.TabIndex = 5;
            this.day.Text = "d";
            // 
            // month
            // 
            this.month.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.month.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.month.Location = new System.Drawing.Point(177, 126);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(10, 19);
            this.month.TabIndex = 4;
            this.month.Text = "m";
            // 
            // year
            // 
            this.year.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.year.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.year.Location = new System.Drawing.Point(108, 126);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(40, 19);
            this.year.TabIndex = 3;
            this.year.Text = "year";
            // 
            // minority
            // 
            this.minority.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.minority.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.minority.Location = new System.Drawing.Point(207, 88);
            this.minority.Name = "minority";
            this.minority.Size = new System.Drawing.Size(80, 19);
            this.minority.TabIndex = 2;
            this.minority.Text = "minority";
            // 
            // sex
            // 
            this.sex.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sex.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sex.Location = new System.Drawing.Point(96, 87);
            this.sex.Name = "sex";
            this.sex.Size = new System.Drawing.Size(30, 19);
            this.sex.TabIndex = 1;
            this.sex.Text = "sex";
            // 
            // name
            // 
            this.name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.name.Appearance.Font = new System.Drawing.Font("SimHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(96, 48);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(40, 19);
            this.name.TabIndex = 0;
            this.name.Text = "name";
            // 
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 496);
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
        private DevExpress.XtraBars.BarButtonItem reportButton;
        private DevExpress.XtraBars.BarButtonItem startButton;
        private DevExpress.XtraBars.BarButtonItem stopButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem databaseQuery;
        private DevExpress.XtraEditors.LabelControl idCardStatus;
        private DevExpress.XtraEditors.PictureEdit image;
        private DevExpress.XtraEditors.LabelControl idCardNo;
        private DevExpress.XtraEditors.LabelControl address;
        private DevExpress.XtraEditors.LabelControl day;
        private DevExpress.XtraEditors.LabelControl month;
        private DevExpress.XtraEditors.LabelControl year;
        private DevExpress.XtraEditors.LabelControl minority;
        private DevExpress.XtraEditors.LabelControl sex;
        private DevExpress.XtraEditors.LabelControl name;
    }
}