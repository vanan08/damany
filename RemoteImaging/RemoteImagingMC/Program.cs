using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using Damany.RemoteImaging.Common.Forms;
using Damany.Security.UsersAdmin;
using System.IO;
using System.Security.Principal;


namespace RemoteImaging
{
    using RealtimeDisplay;
    using System.Xml;

    static class Program
    {
        private static UsersManager usersManager;
        public static string directory;

        public static void ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, RemoteImaging.Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            string fullQualifiedName = typeof(string).AssemblyQualifiedName;


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
            usersManager = UsersManager.LoadUsers();

            User currentUser = null;

#if !DEBUG
            while (true)
            {
                log = new Login();
                log.LabelClicked += new EventHandler(log_LabelClicked);
                if (log.ShowDialog() != DialogResult.OK)
                    return;

                currentUser = usersManager.GetUser(log.UserName, log.Password);
                if (currentUser != null)
                    break;
                else
                    ShowErrorMessage(Properties.Resources.ErrorUserNameOrPassword);
            }
#endif

#if DEBUG
            currentUser = usersManager.GetUser("admin", "admin");
#endif


            System.Threading.Thread.CurrentPrincipal = currentUser.ToPrincipal();

            if (argv.Length > 0)
            {
                directory = argv[0];
            }

            var mainForm = new MainForm();
            mainForm.UsersManager = usersManager;

            Application.Run(mainForm);

        }

        static void log_LabelClicked(object sender, EventArgs e)
        {
            while (true)
            {
                using (var form = new ChangePasswordForm())
                {
                    DialogResult result = form.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        break;
                    }
                    else if (result == DialogResult.OK)
                    {
                        bool succeed = 
                            usersManager.ChangePassword(form.UserName, form.OldPassword, form.NewPassword);
                        if (succeed)
                        {
                            usersManager.Save();
                            break;
                        }
                        else
                        {
                            ShowErrorMessage(Properties.Resources.ErrorUserNameOrPassword);
                        }
                    }
                    
                }
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
