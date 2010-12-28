//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.Composite.Events;

namespace StockTraderRI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;

            var cam = new Damany.Cameras.DirectoryFilesCamera(@"M:\≤‚ ‘Õº∆¨\Lb", "*.jpg");
            cam.NewFrame += new AForge.Video.NewFrameEventHandler(cam_NewFrame);



        }

        void cam_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {

        }

        private static void RunInDebugMode()
        {
            var mainScreen = new Shell();
            mainScreen.ShowView();

        }

        private static void RunInReleaseMode()
        {

        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show(StockTraderRI.Properties.Resources.UnhandledException);
            Environment.Exit(1);
        }
    }
}