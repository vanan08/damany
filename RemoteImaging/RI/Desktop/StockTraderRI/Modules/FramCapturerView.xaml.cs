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
using Microsoft.Practices.Composite.Events;

namespace StockTraderRI.Modules
{
    /// <summary>
    /// Interaction logic for FramCapturerView.xaml
    /// </summary>
    public partial class FramCapturerView : UserControl
    {
        private readonly IEventAggregator _eventAggregator;


        public FramCapturerView()
        {
            InitializeComponent();
        }

        public FramCapturerView(Microsoft.Practices.Composite.Events.IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
    }
}
