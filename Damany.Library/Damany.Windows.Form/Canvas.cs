using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Damany.Windows.Form
{
    public partial class Canvas : UserControl
    {
        public Canvas()
        {
            InitializeComponent();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.squareListView1.Visible =
                (e.Y > this.ClientRectangle.Height - this.squareListView1.Height) && e.Y < this.ClientRectangle.Height;

            this.toolStrip1.Visible =
                    (e.Y > 0) && e.Y < this.toolStrip1.Height;


            System.Diagnostics.Debug.WriteLine(this.squareListView1.Visible);
        }
    }
}
