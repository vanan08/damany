using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Damany.RemoteImaging.Common
{
    public static class ControlHelper
    {
        public static void SetControlProperty(Control c, string propertyName, object value)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(Control).InvokeMember(propertyName,
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, c, new object[] { value });

        }
    }
}
