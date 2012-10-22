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
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.idCardControlLeft = new Kise.IdCard.UI.IdCardControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection(this.components);
            this.idCardControlRight = new Kise.IdCard.UI.IdCardControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.buttonEditManualQuery = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditManualQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            this.databaseQuery});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbon.MaxItemId = 16;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemMarqueeProgressBar1});
            this.ribbon.Size = new System.Drawing.Size(1164, 164);
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
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 647);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1164, 33);
            // 
            // idCardControlLeft
            // 
            this.idCardControlLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.idCardControlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idCardControlLeft.BirthDayFormat = "{0} 年 {1} 月 {2} 日";
            this.idCardControlLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.idCardControlLeft.IsSuspect = false;
            this.idCardControlLeft.Location = new System.Drawing.Point(12, 40);
            this.idCardControlLeft.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.idCardControlLeft.MinorityDictionary = null;
            this.idCardControlLeft.Name = "idCardControlLeft";
            this.idCardControlLeft.Size = new System.Drawing.Size(567, 431);
            this.idCardControlLeft.TabIndex = 11;
            // 
            // xpCollection1
            // 
            this.xpCollection1.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            // 
            // idCardControlRight
            // 
            this.idCardControlRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.idCardControlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.idCardControlRight.BirthDayFormat = "{0} 年 {1} 月 {2} 日";
            this.idCardControlRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.idCardControlRight.IsSuspect = false;
            this.idCardControlRight.Location = new System.Drawing.Point(583, 40);
            this.idCardControlRight.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.idCardControlRight.MinorityDictionary = null;
            this.idCardControlRight.Name = "idCardControlRight";
            this.idCardControlRight.Size = new System.Drawing.Size(569, 431);
            this.idCardControlRight.TabIndex = 11;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.buttonEditManualQuery);
            this.layoutControl1.Controls.Add(this.idCardControlRight);
            this.layoutControl1.Controls.Add(this.idCardControlLeft);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 164);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(756, 536, 395, 457);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1164, 483);
            this.layoutControl1.TabIndex = 23;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(569, 18);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "现场数据采集结果";
            // 
            // buttonEditManualQuery
            // 
            this.buttonEditManualQuery.Location = new System.Drawing.Point(916, 12);
            this.buttonEditManualQuery.MenuManager = this.ribbon;
            this.buttonEditManualQuery.Name = "buttonEditManualQuery";
            this.buttonEditManualQuery.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("buttonEditManualQuery.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.buttonEditManualQuery.Properties.Mask.EditMask = "(\\d{18,18}|\\d{15,15}|\\d{17,17}x)";
            this.buttonEditManualQuery.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.buttonEditManualQuery.Properties.Mask.ShowPlaceHolders = false;
            this.buttonEditManualQuery.Properties.NullText = "输入身份证号码手动查询";
            this.buttonEditManualQuery.Size = new System.Drawing.Size(236, 24);
            this.buttonEditManualQuery.StyleController = this.layoutControl1;
            this.buttonEditManualQuery.TabIndex = 12;
            this.buttonEditManualQuery.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            this.buttonEditManualQuery.Enter += new System.EventHandler(this.buttonEditManualQuery_Enter);
            this.buttonEditManualQuery.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buttonEditManualQuery_KeyPress);
            this.buttonEditManualQuery.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonEditManualQuery_KeyUp);
            this.buttonEditManualQuery.MouseEnter += new System.EventHandler(this.buttonEditManualQuery_MouseEnter);
            this.buttonEditManualQuery.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonEditManualQuery_MouseUp);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1164, 483);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.idCardControlLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(571, 435);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.idCardControlRight;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(571, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(573, 435);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEditManualQuery;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(796, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(348, 28);
            this.layoutControlItem3.Text = "数据库查询结果";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(105, 18);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(573, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(223, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem4.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem4.Control = this.labelControl1;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(573, 28);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 680);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "二代身份证自动查询系统";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMarqueeProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditManualQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
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
        private IdCardControl idCardControlLeft;
        private IdCardControl idCardControlRight;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ButtonEdit buttonEditManualQuery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}