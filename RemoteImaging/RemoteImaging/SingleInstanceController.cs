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

        void SingleInstanceController_Startup(object sender, Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {

#if !DEBUG

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
#endif

            if (e.CommandLine.Count  > 0)
            {
                Program.directory = e.CommandLine[0];
            }


            Program.faceSearch = new FaceSearchWrapper.FaceSearch();
            Program.motionDetector = new MotionDetectWrapper.MotionDetector();

        }


        protected override void OnCreateMainForm()
        {
            this.MainForm = new RealtimeDisplay.MainForm();
        }
    }
}
