using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CameraSearcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchCamera.CameraSearcher searcher = new SearchCamera.CameraSearcher();

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            searcher.CameraFound += (o, args) => this.AddToList(args.CameraIp);

            try
            {
                searcher.Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void AddToList(string cameraIp)
        {
            if (!CheckAccess())
            {
                Action<string> action = this.AddToList;
                this.Dispatcher.BeginInvoke(action, cameraIp);
                return;
            }

            this.listBoxCamera.Items.Insert(0, cameraIp);
        }
    }
}
