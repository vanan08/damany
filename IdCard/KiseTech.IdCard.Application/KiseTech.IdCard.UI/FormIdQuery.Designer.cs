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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.idCardSource = new DevExpress.Xpo.XPCollection();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCopyOfImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdCardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBornDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGrantDept = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidateFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValidateUntil = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.colMinorityName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSexName = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idCardSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.idCardSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageEdit1});
            this.gridControl1.Size = new System.Drawing.Size(871, 564);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // idCardSource
            // 
            this.idCardSource.ObjectType = typeof(Kise.IdCard.Model.IdCardInfo);
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
            this.colIdStatus,
            this.colCreationDate});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.RowHeight = 64;
            // 
            // colCopyOfImage
            // 
            this.colCopyOfImage.Caption = "图片";
            this.colCopyOfImage.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colCopyOfImage.FieldName = "CopyOfImage";
            this.colCopyOfImage.Name = "colCopyOfImage";
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
            this.colName.Width = 77;
            // 
            // colIdCardNo
            // 
            this.colIdCardNo.Caption = "身份证号";
            this.colIdCardNo.FieldName = "IdCardNo";
            this.colIdCardNo.Name = "colIdCardNo";
            this.colIdCardNo.Visible = true;
            this.colIdCardNo.VisibleIndex = 4;
            this.colIdCardNo.Width = 102;
            // 
            // colBornDate
            // 
            this.colBornDate.Caption = "出生日期";
            this.colBornDate.FieldName = "BornDate";
            this.colBornDate.Name = "colBornDate";
            this.colBornDate.Visible = true;
            this.colBornDate.VisibleIndex = 5;
            this.colBornDate.Width = 66;
            // 
            // colGrantDept
            // 
            this.colGrantDept.Caption = "发证机关";
            this.colGrantDept.FieldName = "GrantDept";
            this.colGrantDept.Name = "colGrantDept";
            this.colGrantDept.Visible = true;
            this.colGrantDept.VisibleIndex = 8;
            this.colGrantDept.Width = 81;
            // 
            // colValidateFrom
            // 
            this.colValidateFrom.Caption = "有效期自";
            this.colValidateFrom.FieldName = "ValidateFrom";
            this.colValidateFrom.Name = "colValidateFrom";
            this.colValidateFrom.Visible = true;
            this.colValidateFrom.VisibleIndex = 9;
            this.colValidateFrom.Width = 55;
            // 
            // colValidateUntil
            // 
            this.colValidateUntil.Caption = "有效期止";
            this.colValidateUntil.FieldName = "ValidateUntil";
            this.colValidateUntil.Name = "colValidateUntil";
            this.colValidateUntil.Visible = true;
            this.colValidateUntil.VisibleIndex = 10;
            this.colValidateUntil.Width = 63;
            // 
            // colIdStatus
            // 
            this.colIdStatus.Caption = "查询结果";
            this.colIdStatus.FieldName = "IdStatus";
            this.colIdStatus.Name = "colIdStatus";
            this.colIdStatus.Visible = true;
            this.colIdStatus.VisibleIndex = 7;
            this.colIdStatus.Width = 61;
            // 
            // colCreationDate
            // 
            this.colCreationDate.Caption = "查询日期";
            this.colCreationDate.FieldName = "CreationDate";
            this.colCreationDate.Name = "colCreationDate";
            this.colCreationDate.Visible = true;
            this.colCreationDate.VisibleIndex = 6;
            this.colCreationDate.Width = 61;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // colMinorityName
            // 
            this.colMinorityName.Caption = "民族";
            this.colMinorityName.FieldName = "MinorityName";
            this.colMinorityName.Name = "colMinorityName";
            this.colMinorityName.Visible = true;
            this.colMinorityName.VisibleIndex = 3;
            // 
            // colSexName
            // 
            this.colSexName.Caption = "性别";
            this.colSexName.FieldName = "SexName";
            this.colSexName.Name = "colSexName";
            this.colSexName.Visible = true;
            this.colSexName.VisibleIndex = 2;
            // 
            // FormIdQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 564);
            this.Controls.Add(this.gridControl1);
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
        private DevExpress.XtraGrid.Columns.GridColumn colIdStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colCreationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSexName;
        private DevExpress.XtraGrid.Columns.GridColumn colMinorityName;
    }
}