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
            Application.Run(new MainForm());
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
