using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace FaceLibraryManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            base.OnStartup(e);

            try
            {
                var args = Environment.GetCommandLineArgs();
                if (args.Length == 1)
                {
                    MessageBox.Show("请指定人脸特征库目录!", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                    Application.Current.Shutdown(0);
                    return;
                }

                var repositoryDirectory = args[1];
                SuspectsRepository.SuspectsRepositoryManager repositoryManager;
                if (System.IO.Directory.Exists(repositoryDirectory))
                {
                    repositoryManager = SuspectsRepository.SuspectsRepositoryManager.LoadFrom(repositoryDirectory);
                }
                else
                {
                    repositoryManager = SuspectsRepository.SuspectsRepositoryManager.CreateNewIn(repositoryDirectory);
                }

                var vm = new ViewModel.SuspectsListViewModel(repositoryManager);

                var mainView = new MainView();
                mainView.DataContext = vm;
                mainView.Show();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }


        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ShowError(e.Exception);
            e.Handled = true;
        }

        private void ShowError(Exception e)
        {
            MessageBox.Show("更新特征库的过程中发生异常：\n\n" + e.Message, "异常", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
