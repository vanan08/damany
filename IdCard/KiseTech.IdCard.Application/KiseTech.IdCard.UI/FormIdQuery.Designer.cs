namespace Kise.IdCard.UI
{
    partial class FormIdQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIdQuery));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.idCardSource = new DevExpress.Xpo.XPCollection();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCopyOfImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSexName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdCardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinorityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBornDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGrantDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidateUntil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdStatusName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.printButton = new DevExpress.XtraBars.BarButtonItem();
            this.saveButton = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idCardSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.idCardSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 26);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageEdit1});
            this.gridControl1.Size = new System.Drawing.Size(871, 538);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // idCardSource
            // 
            this.idCardSource.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
            this.idCardSource.Sorting.AddRange(new DevExpress.Xpo.SortProperty[] {
            new DevExpress.Xpo.SortProperty("[CreationDate]", DevExpress.Xpo.DB.SortingDirection.Descending)});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCopyOfImage,
            this.colName,
            this.colSexName,
            this.colIdCardNo,
            this.colMinorityName,
            this.colBornDate,
            this.colGrantDept,
            this.colValidateFrom,
            this.colValidateUntil,
            this.colIdStatusName,
            this.colCreationDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.RowHeight = 64;
            // 
            // colCopyOfImage
            // 
            this.colCopyOfImage.Caption = "图片";
            this.colCopyOfImage.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colCopyOfImage.FieldName = "CopyOfImage";
            this.colCopyOfImage.Name = "colCopyOfImage";
            this.colCopyOfImage.SummaryItem.DisplayFormat = "{0} 条记录";
            this.colCopyOfImage.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.colCopyOfImage.Visible = true;
            this.colCopyOfImage.VisibleIndex = 0;
            this.colCopyOfImage.Width = 76;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // colName
            // 
            this.colName.Caption = "姓名";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 52;
            // 
            // colSexName
            // 
            this.colSexName.Caption = "性别";
            this.colSexName.FieldName = "SexName";
            this.colSexName.Name = "colSexName";
            this.colSexName.Visible = true;
            this.colSexName.VisibleIndex = 2;
            this.colSexName.Width = 45;
            // 
            // colIdCardNo
            // 
            this.colIdCardNo.Caption = "身份证号";
            this.colIdCardNo.FieldName = "IdCardNo";
            this.colIdCardNo.Name = "colIdCardNo";
            this.colIdCardNo.Visible = true;
            this.colIdCardNo.VisibleIndex = 4;
            this.colIdCardNo.Width = 96;
            // 
            // colMinorityName
            // 
            this.colMinorityName.Caption = "民族";
            this.colMinorityName.FieldName = "MinorityName";
            this.colMinorityName.Name = "colMinorityName";
            this.colMinorityName.Visible = true;
            this.colMinorityName.VisibleIndex = 3;
            this.colMinorityName.Width = 47;
            // 
            // colBornDate
            // 
            this.colBornDate.Caption = "出生日期";
            this.colBornDate.FieldName = "BornDate";
            this.colBornDate.Name = "colBornDate";
            this.colBornDate.Visible = true;
            this.colBornDate.VisibleIndex = 5;
            this.colBornDate.Width = 89;
            // 
            // colGrantDept
            // 
            this.colGrantDept.Caption = "发证机关";
            this.colGrantDept.FieldName = "GrantDept";
            this.colGrantDept.Name = "colGrantDept";
            this.colGrantDept.Visible = true;
            this.colGrantDept.VisibleIndex = 8;
            this.colGrantDept.Width = 85;
            // 
            // colValidateFrom
            // 
            this.colValidateFrom.Caption = "有效期自";
            this.colValidateFrom.FieldName = "ValidateFrom";
            this.colValidateFrom.Name = "colValidateFrom";
            this.colValidateFrom.Visible = true;
            this.colValidateFrom.VisibleIndex = 9;
            this.colValidateFrom.Width = 76;
            // 
            // colValidateUntil
            // 
            this.colValidateUntil.Caption = "有效期止";
            this.colValidateUntil.FieldName = "ValidateUntil";
            this.colValidateUntil.Name = "colValidateUntil";
            this.colValidateUntil.Visible = true;
            this.colValidateUntil.VisibleIndex = 10;
            this.colValidateUntil.Width = 81;
            // 
            // colIdStatusName
            // 
            this.colIdStatusName.Caption = "查询结果";
            this.colIdStatusName.FieldName = "IdStatusName";
            this.colIdStatusName.Name = "colIdStatusName";
            this.colIdStatusName.Visible = true;
            this.colIdStatusName.VisibleIndex = 7;
            // 
            // colCreationDate
            // 
            this.colCreationDate.Caption = "查询日期";
            this.colCreationDate.FieldName = "CreationDate";
            this.colCreationDate.Name = "colCreationDate";
            this.colCreationDate.Visible = true;
            this.colCreationDate.VisibleIndex = 6;
            this.colCreationDate.Width = 88;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.printButton,
            this.saveButton});
            this.barManager1.MaxItemId = 2;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.printButton, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.saveButton, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // printButton
            // 
            this.printButton.Caption = "打印";
            this.printButton.Glyph = ((System.Drawing.Image)(resources.GetObject("printButton.Glyph")));
            this.printButton.Id = 0;
            this.printButton.Name = "printButton";
            this.printButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.printButton_ItemClick);
            // 
            // saveButton
            // 
            this.saveButton.Caption = "导出Excel";
            this.saveButton.Glyph = ((System.Drawing.Image)(resources.GetObject("saveButton.Glyph")));
            this.saveButton.Id = 1;
            this.saveButton.Name = "saveButton";
            this.saveButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.saveButton_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(871, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 564);
            this.barDockControlBottom.Size = new System.Drawing.Size(871, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 538);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(871, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 538);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "xls";
            this.saveFileDialog.FileName = "身份证查询记录";
            this.saveFileDialog.Filter = "Excel 文档|*.xls";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // FormIdQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 564);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormIdQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "身份证记录历史查询";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idCardSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.Xpo.XPCollection idCardSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colCopyOfImage;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colIdCardNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBornDate;
        private DevExpress.XtraGrid.Columns.GridColumn colGrantDept;
        private DevExpress.XtraGrid.Columns.GridColumn colValidateFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colValidateUntil;
        private DevExpress.XtraGrid.Columns.GridColumn colCreationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSexName;
        private DevExpress.XtraGrid.Columns.GridColumn colMinorityName;
        private DevExpress.XtraGrid.Columns.GridColumn colIdStatusName;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem printButton;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}