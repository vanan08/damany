using System;
using System.Globalization;
using System.Threading;

namespace Kise.IdCard.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");

            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Black");

            if (args.Length > 0)
            {
                IsDebug = true;
            }

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show((e.ExceptionObject as Exception).Message);
        }

        public static bool IsDebug = false;
    }
}
