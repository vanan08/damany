using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.ServiceModel;
using Damany.RemoteImaging.Common.Forms;
using RemoteControlService;
using Autofac;
using RemoteImaging.ConfigurationSectionHandlers;


namespace RemoteImaging
{
    using RealtimeDisplay;
    using System.Xml;

    static class Program
    {
        public static string directory;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            try
            {
                var isFirst = false;

                using (var mutex = new Mutex(true, "5685FE28-6805-4F62-8851-F0DFDD2A9EBD", out isFirst))
                {
                    if (!isFirst)
                    {
                        MessageBox.Show("程序已经在运行中！ 点击确认后程序将退出。", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (!Util.VerifyKey())
                    {
                        RegisterForm form = new RegisterForm();
                        DialogResult res = form.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            Application.Restart();
                        }

                        return;
                    }


                    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                    Application.ThreadException += Application_ThreadException;


                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var strapper = new StartUp();
                    strapper.Start();

                    var remover = strapper.Container.Resolve<OutDatedDataRemover>();
                    System.GC.KeepAlive(remover);

                    var mainForm = strapper.Container.Resolve<RemoteImaging.RealtimeDisplay.MainForm>();
                    mainForm.Text = Properties.Settings.Default.ApplicationName;

                    var controller = strapper.Container.Resolve<MainController>();
                    mainForm.AttachController(controller);

                    mainForm.ButtonsVisible =
                        (ButtonsVisibleSectionHandler)System.Configuration.ConfigurationManager.GetSection("FaceDetector.ButtonsVisible");

                    Application.Run(mainForm);
                }


            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void HandleException(Exception e)
        {
            LogException(e);

            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write("--------\r\nException Occurred, restart application at " + DateTime.Now + "-----------\r\n");

            var form = new CountDownForm();
            form.Text = Properties.Settings.Default.ApplicationName;
            form.SecondsToCount = Properties.Settings.Default.RestartCountDown;
            form.Message = "系统发生了异常，系统已经将该异常记录在 exception.log 文件中，数秒后系统将自动重启。" +
                           "\r\n\r\n点击 \"确定\" 立即重启，点击 \"取消\" 将不重启。";

            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Restart();
            }

        }

        private static void ShowException(System.Exception e)
        {
            MessageBox.Show(e.ToString(), "发生异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void LogException(System.Exception e)
        {
            Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(
                e, Constants.ExceptionHandlingLogging
                );
        }

    }
}
