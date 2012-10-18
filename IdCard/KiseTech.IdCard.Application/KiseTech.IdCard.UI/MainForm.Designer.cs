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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.buttonQuery = new DevExpress.XtraBars.BarButtonItem();
            this.settingsButton = new DevExpress.XtraBars.BarButtonItem();
            this.progressBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            this.statusLabel = new DevExpress.XtraBars.BarStaticItem();
            this.startButton = new DevExpress.XtraBars.BarButtonItem();
            this.stopButton = new DevExpress.XtraBars.BarButtonItem();
            this.databaseQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItemManulQuery = new DevExpress.XtraBars.BarCheckItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.idCardControl1 = new Kise.IdCard.UI.IdCardControl();
            this.resultLabel = new DevExpress.XtraEditors.LabelControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection(this.components);
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageManualQuery = new DevExpress.XtraTab.XtraTabPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.idCardControl2 = new Kise.IdCard.UI.IdCardControl();
            this.xtraTabPageIdReader = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            this.clientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPageManualQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.xtraTabPageIdReader.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
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
            this.databaseQuery,
            this.barCheckItemManulQuery});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 16;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemMarqueeProgressBar1});
            this.ribbon.Size = new System.Drawing.Size(681, 147);
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
            // barCheckItemManulQuery
            // 
            this.barCheckItemManulQuery.Caption = "手动查询";
            this.barCheckItemManulQuery.Id = 15;
            this.barCheckItemManulQuery.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barCheckItemManulQuery.LargeGlyph")));
            this.barCheckItemManulQuery.Name = "barCheckItemManulQuery";
            this.barCheckItemManulQuery.DownChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemManulQuery_DownChanged);
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
            this.ribbonPageGroup3.ItemLinks.Add(this.barCheckItemManulQuery);
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
            this.clientPanel.Controls.Add(this.idCardControl1);
            this.clientPanel.Controls.Add(this.resultLabel);
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(0, 0);
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(675, 387);
            this.clientPanel.TabIndex = 2;
            // 
            // idCardControl1
            // 
            this.idCardControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idCardControl1.BirthDayFormat = "{0} 年 {1} 月 {2} 日";
            this.idCardControl1.IdCardInfo = null;
            this.idCardControl1.Location = new System.Drawing.Point(79, 36);
            this.idCardControl1.MinorityDictionary = null;
            this.idCardControl1.Name = "idCardControl1";
            this.idCardControl1.Size = new System.Drawing.Size(531, 323);
            this.idCardControl1.TabIndex = 11;
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
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Never;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 147);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageManualQuery;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Size = new System.Drawing.Size(681, 393);
            this.xtraTabControl1.TabIndex = 5;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageIdReader,
            this.xtraTabPageManualQuery});
            // 
            // xtraTabPageManualQuery
            // 
            this.xtraTabPageManualQuery.Controls.Add(this.panelControl1);
            this.xtraTabPageManualQuery.Name = "xtraTabPageManualQuery";
            this.xtraTabPageManualQuery.Size = new System.Drawing.Size(675, 387);
            this.xtraTabPageManualQuery.Text = "xtraTabPage2";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.buttonEdit1);
            this.panelControl1.Controls.Add(this.idCardControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(675, 387);
            this.panelControl1.TabIndex = 3;
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(11, 5);
            this.buttonEdit1.MenuManager = this.ribbon;
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "查询", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("buttonEdit1.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.buttonEdit1.Properties.NullText = "输入身份证号码";
            this.buttonEdit1.Size = new System.Drawing.Size(657, 22);
            this.buttonEdit1.TabIndex = 12;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // idCardControl2
            // 
            this.idCardControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idCardControl2.BirthDayFormat = "{0} 年 {1} 月 {2} 日";
            this.idCardControl2.IdCardInfo = null;
            this.idCardControl2.Location = new System.Drawing.Point(79, 36);
            this.idCardControl2.MinorityDictionary = null;
            this.idCardControl2.Name = "idCardControl2";
            this.idCardControl2.Size = new System.Drawing.Size(531, 323);
            this.idCardControl2.TabIndex = 11;
            // 
            // xtraTabPageIdReader
            // 
            this.xtraTabPageIdReader.Controls.Add(this.clientPanel);
            this.xtraTabPageIdReader.Name = "xtraTabPageIdReader";
            this.xtraTabPageIdReader.Size = new System.Drawing.Size(675, 387);
            this.xtraTabPageIdReader.Text = "xtraTabPage1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 571);
            this.Controls.Add(this.xtraTabControl1);
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
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPageManualQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.xtraTabPageIdReader.ResumeLayout(false);
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
        private DevExpress.XtraEditors.LabelControl resultLabel;
        private IdCardControl idCardControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageIdReader;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageManualQuery;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private IdCardControl idCardControl2;
        private DevExpress.XtraBars.BarCheckItem barCheckItemManulQuery;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
    }
}