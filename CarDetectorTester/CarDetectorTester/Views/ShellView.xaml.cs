﻿using System.Windows;
using DevExpress.Xpf.Charts;

namespace CarDetectorTester.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();


        }

        private void ComPort_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void realtimeChart_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.LastCommand = CommandToSend.Text;
            Properties.Settings.Default.Save();
        }

       
    }
}
