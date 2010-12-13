using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;

namespace Kise.IdCard.UI
{
    public partial class FormIdQuery : DevExpress.XtraEditors.XtraForm
    {
        public FormIdQuery()
        {
            InitializeComponent();
        }

        private void printButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IdReport report = GetReport();
            report.ShowPreviewDialog();
        }

        private IdReport GetReport()
        {
            var report = new IdReport();
            report.DataSource = idCardSource;

            var col = new DevExpress.Xpo.XPCollection(typeof(Model.IdCardInfo));
            col.LoadingEnabled = false;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                var row = gridView1.GetRow(i) as Model.IdCardInfo;
                col.Add(row);
            }

            report.DataSource = col;
            return report;
        }

        private void saveButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = saveFileDialog.ShowDialog(this);
            if (dr != DialogResult.OK) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                var report = GetReport();
                report.ExportToXls(saveFileDialog.FileName, new XlsExportOptions(TextExportMode.Text, false, true));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}