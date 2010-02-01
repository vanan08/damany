using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.RemoteImaging.Common.Forms;
using Damany.Security.UsersAdmin;

namespace RemoteImaging
{
    class SingleInstanceController : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            this.Startup += new Microsoft.VisualBasic.ApplicationServices.StartupEventHandler(SingleInstanceController_Startup);
        }

        void SingleInstanceController_Startup(object sender, Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            if (!Util.VerifyKey())
            {
                RegisterForm form = new RegisterForm();
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Application.Restart();
                }

                e.Cancel = true;
                return;
            }

            Login log = null;
            Program.usersManager = UsersManager.LoadUsers();

            User currentUser = null;

#if !DEBUG
            while (true)
            {
                log = new Login();
                log.LabelClicked += new EventHandler(log_LabelClicked);
                if (log.ShowDialog() != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                currentUser = Program.usersManager.GetUser(log.UserName, log.Password);
                if (currentUser != null)
                    break;
                else
                    Util.ShowErrorMessage(Properties.Resources.ErrorUserNameOrPassword);
            }
#endif

#if DEBUG
            currentUser = Program.usersManager.GetUser("admin", "admin");
#endif


            System.Threading.Thread.CurrentPrincipal = currentUser.ToPrincipal();

            if (e.CommandLine.Count > 0)
            {
                Program.directory = e.CommandLine[0];
            }

        }

        protected override void OnCreateMainForm()
        {
            var mainForm = new RealtimeDisplay.MainForm();
            mainForm.UsersManager = Program.usersManager;

            MainForm = mainForm;
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
                            Program.usersManager.ChangePassword(form.UserName, form.OldPassword, form.NewPassword);
                        if (succeed)
                        {
                            Program.usersManager.Save();
                            break;
                        }
                        else
                        {
                            Util.ShowErrorMessage(Properties.Resources.ErrorUserNameOrPassword);
                        }
                    }

                }
            }

        }


    }
}
