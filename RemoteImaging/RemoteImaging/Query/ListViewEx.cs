using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging.Query
{
    public class ListViewEx : ListView
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);

        const int SB_HORZ = 0;
        const int SB_VERT = 1;
        protected override void WndProc(ref Message m)
        {
            if (this.View == View.LargeIcon)
            {
                ShowScrollBar(this.Handle, SB_HORZ, 1);
                ShowScrollBar(this.Handle, SB_VERT, 1);
            }
            base.WndProc(ref m);
        }
    }
}
