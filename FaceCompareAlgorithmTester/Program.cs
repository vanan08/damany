using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace FaceCompareAlgorithmTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var controller = LoadPlugins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            mainForm.FaceComparers = controller.FaceComparers;

            Application.Run(mainForm);
        }

        private static Controller LoadPlugins()
        {
            var catelog = new DirectoryCatalog(@".\");
            var container = new CompositionContainer(catelog);

            var controller = new Controller();
            container.ComposeParts(controller);

            return controller;
        }
    }
}
