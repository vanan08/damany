﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using RemoteControlService;


namespace RemoteImaging
{
    using RealtimeDisplay;
    using System.Xml;

    static class Program
    {
        public static string directory;
        public static int ImageSampleCount = 2230;
        public static int ImageLen = 100*100;
        public static int EigenNum = 40;

        public static FaceSearchWrapper.FaceSearch faceSearch;
        public static MotionDetectWrapper.MotionDetector motionDetector;

  

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var controller
                = new SingleInstanceController();

            controller.Run(argv);

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(
                   e.ExceptionObject as Exception, Constants.ExceptionHandlingLogging
                   );

            }
            finally
            {
                Application.Exit();
            }
            
            
        }


        

        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
