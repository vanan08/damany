using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using Damany.RemoteImaging.Common.Forms;
using Damany.Security.UsersAdmin;
using System.IO;


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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG

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


            Login log = null;
            UsersManager manager = null;
            using (var stream = Configuration.getUsersSettingReadStream())
            {
                manager = UsersManager.LoadUsers(stream);
            }

            do
            {
                log = new Login();
                log.LabelClicked += new EventHandler(log_LabelClicked);
                if (log.ShowDialog() != DialogResult.OK)
                    return;

            } while (manager.GetUser(log.UserName, log.Password) == null);
#endif


            if (argv.Length > 0)
            {
                directory = argv[0];
            }

            var mainForm = new MainForm();
            mainForm.UsersManager = manager;

            Application.Run(mainForm);

        }

        static void log_LabelClicked(object sender, EventArgs e)
        {
            using (var form = new ChangePasswordForm())
            {
                form.ShowDialog();
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
