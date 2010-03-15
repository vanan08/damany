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
                var args = Environment.GetCommandLineArgs();

                var controller = BootStrapper.BootStrap(args[1], args[2]);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var mainForm = new Form1();
                mainForm.controller = controller;
                mainForm.repository = BootStrapper.PersistenceService;
                Application.Run(mainForm);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }

    static class BootStrapper
    {
        private static PersistenceService persistenceService = null;
        static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);


        public static PersistenceService PersistenceService
        {
            get
            {
                return persistenceService;
            }
        }
        public static Damany.Imaging.Processors.FaceSearchController BootStrap(string url, string cameraType)
        {
            Uri uri = new Uri(url);

            var source = Damany.Cameras.Factory.NewFrameStream(uri, cameraType);
            source.Initialize();
            source.Connect();

            var controller = FaceSearchFactory.CreateNewController(source);

            if (PersistenceService == null)
            {
                persistenceService = PersistenceService.CreateDefault(@".\");
            }

            var writer = new PersistenceWriter(PersistenceService);

            controller.RegisterPortraitHandler(writer);

            return controller;

        }
    }
       
}
