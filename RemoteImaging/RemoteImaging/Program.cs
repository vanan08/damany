using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.ServiceModel;
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
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ThreadException += Application_ThreadException;


                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var strapper = new StartUp();
                strapper.Start();

                var remover = strapper.Container.Resolve<OutDatedDataRemover>();
                System.GC.KeepAlive(remover);

                var mainForm = strapper.Container.Resolve<RemoteImaging.RealtimeDisplay.MainForm>();
                var controller = strapper.Container.Resolve<MainController>();
                mainForm.AttachController(controller);

                mainForm.ButtonsVisible =
                    (ButtonsVisibleSectionHandler) System.Configuration.ConfigurationManager.GetSection("FaceDetector.ButtonsVisible");

                Application.Run(mainForm);

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
            ShowException(e);
        }

        private static void ShowException(System.Exception e)
        {
            MessageBox.Show(e.Message, "发生异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
