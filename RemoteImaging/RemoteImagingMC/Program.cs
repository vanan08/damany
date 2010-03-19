using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using Damany.RemoteImaging.Common.Forms;
using Damany.Security.UsersAdmin;
using System.IO;
using System.Security.Principal;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;


namespace RemoteImaging
{
    using RealtimeDisplay;
    using System.Xml;

    static class Program
    {
        public static UsersManager usersManager;
        public static string directory;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            var loader =
                new Damany.RemoteImaging.Common.BootLoader();

            try
            {
                loader.Load(@"d:\ImageOutput");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            	
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            mainForm.loader = loader;

            Application.Run(mainForm);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                ExceptionPolicy.HandleException((Exception)e.ExceptionObject, Constants.ExceptionPolicyLogging);
            }
            finally
            {
                Application.Exit();
            }

        }

       

        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
