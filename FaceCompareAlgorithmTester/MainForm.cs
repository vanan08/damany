using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;

namespace FaceCompareAlgorithmTester
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }



        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public IEnumerable<ISimpleFaceComparer> FaceComparers
        {
            set
            {
                var list = value.ToList();
                this.faceComparersComboBox.DataSource = list;
                this.faceComparersComboBox.DisplayMember = "Name";
            }
        }

        public ISimpleFaceComparer SelectedComparer
        {
            get
            {
                return (ISimpleFaceComparer) this.faceComparersComboBox.SelectedValue;
            }

        }


        private void faceComparersComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var comparer = this.faceComparersComboBox.SelectedValue as Damany.Imaging.Common.ISimpleFaceComparer;
            this.descOfCurFaceComparer.Text = comparer.Description;
            this.configComparer.Enabled = comparer is IConfigurable;
        }

        private void configComparer_Click(object sender, EventArgs e)
        {
            var configurable = this.SelectedComparer as IConfigurable;

            var form = new OptionsForm();
            form.propertyGrid1.SelectedObject = configurable.GetConfig();
            form.ShowDialog(this);

            configurable.SetConfig(form.propertyGrid1.SelectedObject);

        }
    }
}
