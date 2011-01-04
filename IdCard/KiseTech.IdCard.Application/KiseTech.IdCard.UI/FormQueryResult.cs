using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kise.IdCard.UI
{
    public partial class FormQueryResult : Form
    {
        public FormQueryResult()
        {
            InitializeComponent();
        }

        public FormQueryResult(Image image, string normalResult, bool isSuspect)
            : this()
        {

            pictureBox1.Image = image;
            this.normalResult.Text = string.IsNullOrEmpty(normalResult) ? "正常" : normalResult;
            this.suspectResult.Text = isSuspect ? "网上追逃" : "正常";
        }
    }
}
