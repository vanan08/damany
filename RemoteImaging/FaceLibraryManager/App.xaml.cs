using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace FaceLibraryManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var manager = SuspectsRepository.SuspectsRepositoryManager.LoadFrom(@"d:\imglib");
            var vm = new ViewModel.SuspectsListViewModel(manager);

            var mainView = new MainView();
            mainView.DataContext = vm;
            mainView.Show();
        }
    }
}
