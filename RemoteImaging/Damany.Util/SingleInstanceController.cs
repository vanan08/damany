using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.ApplicationServices;

namespace Damany.Util
{
    public class SingleInstanceController<T> : WindowsFormsApplicationBase where T : System.Windows.Forms.Form, new()
    {

        public SingleInstanceController()
        {
            IsSingleInstance = true;

        }

        protected override void OnCreateMainForm()
        {
            MainForm = new T();
            
            int i = 0;
        }
    }
}
