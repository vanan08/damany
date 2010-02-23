using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    class SingleInstanceController : Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            this.Startup += new Microsoft.VisualBasic.ApplicationServices.StartupEventHandler(SingleInstanceController_Startup);
        }

        private static DialogResult RequestRegistrationKey()
        {
            using (var form = new RegisterForm())
            {
                return form.ShowDialog();
            }
        }



        private static void RequestRegistrationIfInvalidKey(out bool shouldReturn)
        {
            shouldReturn = false;
            if (!Util.IsKeyValid())
            {
                var result = RequestRegistrationKey();
                if (result == DialogResult.OK)
                {
                    Application.Restart();
                }
                shouldReturn = true;
                return;
            }
        }


        private static void InitProgram(string directory)
        {
            Program.directory = directory;
            Program.faceSearch = new FaceSearchWrapper.FaceSearch();
            Program.motionDetector = new MotionDetectWrapper.MotionDetector();
        }


        void SingleInstanceController_Startup(object sender, Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            bool shouldReturn;
            RequestRegistrationIfInvalidKey(out shouldReturn);
            if (shouldReturn)
            {
                e.Cancel = shouldReturn;
                return;
            }

            var directory = string.Empty;
            if (e.CommandLine.Count > 0)
            {
                directory = e.CommandLine[0];
            }

            InitProgram(directory);
        }


        protected override void OnCreateMainForm()
        {
            this.MainForm = new RealtimeDisplay.MainForm();
        }
    }
}
