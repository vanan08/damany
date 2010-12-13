using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Composite.Events;
using Autofac;

namespace StockTraderRI
{
    public class Bootstrapper
    {
        public void RegisterTypes()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType(typeof(EventAggregator)).As<IEventAggregator>().SingleInstance();

            var container = builder.Build();
        }

        protected DependencyObject CreateShell()
        {
            var shell = new Shell();
            shell.Show();

            return shell;


        }
    }
}
