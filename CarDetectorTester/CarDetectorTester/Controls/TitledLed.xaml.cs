using System;
using System.Collections.Generic;
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

namespace CarDetectorTester
{
	/// <summary>
	/// Interaction logic for TitledLed.xaml
	/// </summary>
	public partial class TitledLed : UserControl
	{

        public string Speed
        {
            get { return (string)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(string), typeof(TitledLed), new UIPropertyMetadata("LED"));

       

		public TitledLed()
		{
			this.InitializeComponent();
		}
	}
}