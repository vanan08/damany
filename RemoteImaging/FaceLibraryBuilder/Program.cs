using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FaceLibraryBuilder
{
    static class Program
    {
        public static FaceSearchWrapper.FaceSearch searcher;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            searcher = new FaceSearchWrapper.FaceSearch();
            searcher.SetFaceParas(50, 6);

            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImportPersonEnter());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
