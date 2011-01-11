using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Cinch;

namespace CarDetectorTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //CinchBootStrapper.Initialise(new List<Assembly> { typeof(App).Assembly });
            InitializeComponent();
        }
    }
}
