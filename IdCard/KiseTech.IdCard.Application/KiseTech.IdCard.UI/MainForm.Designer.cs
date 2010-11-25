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
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.idCardControl1 = new Kise.IdCard.UI.IdCardControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
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
            this.ribbon.ExpandCollapseItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.buttonQuery,
            this.settingsButton,
            this.progressBar,
            this.statusLabel,
            this.reportButton,
            this.startButton,
            this.stopButton});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 12;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemMarqueeProgressBar1});
            this.ribbon.SelectedPage = this.ribbonPage1;
            this.ribbon.Size = new System.Drawing.Size(612, 148);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // buttonQuery
            // 
            this.buttonQuery.Caption = "查询";
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 511);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(612, 23);
            // 
            // clientPanel
            // 
            this.clientPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.clientPanel.Controls.Add(this.idCardControl1);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 148);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(612, 363);
            this.clientPanel.TabIndex = 2;
            // 
            // idCardControl1
            // 
            this.idCardControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("idCardControl1.BackgroundImage")));
            this.idCardControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idCardControl1.BirthDayFormat = "{0} 年 {1} 月 {2} 日";
            this.idCardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idCardControl1.IdCardInfo = null;
            this.idCardControl1.Location = new System.Drawing.Point(0, 0);
            this.idCardControl1.MinorityDictionary = null;
            this.idCardControl1.Name = "idCardControl1";
            this.idCardControl1.Size = new System.Drawing.Size(612, 363);
            this.idCardControl1.TabIndex = 0;
            // 
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 534);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
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
        private IdCardControl idCardControl1;
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
    }
}