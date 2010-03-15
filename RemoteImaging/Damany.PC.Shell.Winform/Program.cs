using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Damany.Imaging.Contracts;
using Damany.Imaging.Processors;
using Damany.PortraitCapturer.Repository;
using Damany.Imaging.Handlers;
using Damany.Cameras.Wrappers;
using Damany.Cameras;
using Damany.RemoteImaging.Common;

namespace Damany.PC.Shell.Winform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }

    
       
}
