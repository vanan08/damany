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
using System.Windows.Shapes;

namespace CarDetectorTester.Views
{
    /// <summary>
    /// Interaction logic for WidthAndHeight.xaml
    /// </summary>
    public partial class WidthAndHeight : Window
    {
        public int RectWidth
        {
            get { return int.Parse((string) width.EditValue); }
        }

        public int RectLength
        {
            get { return int.Parse((string) length.EditValue); }
        }

        public WidthAndHeight()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
