using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Damany.Windows.Form
{
    public static class FormHelper
    {
        /*code needed to disable start menu*/
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;


        public static void ShowStartMenu(bool show)
        {
            int hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, show ? SW_SHOW : SW_HIDE);
        }
    }
}
