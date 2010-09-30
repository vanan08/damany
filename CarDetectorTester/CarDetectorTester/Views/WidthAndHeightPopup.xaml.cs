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
using Cinch;

namespace CarDetectorTester.Views
{
    /// <summary>
    /// Interaction logic for WidthAndHeightPopup.xaml
    /// </summary>

    [PopupNameToViewLookupKeyMetadata("WidthAndHeightPopup", typeof(WidthAndHeightPopup))]
    public partial class WidthAndHeightPopup : Window
    {
        public int RectWidth
        {
            get { return int.Parse((string) width.EditValue); }
        }

        public int RectLength
        {
            get { return int.Parse((string) length.EditValue); }
        }

        public WidthAndHeightPopup()
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
