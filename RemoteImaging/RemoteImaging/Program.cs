using System;
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
            faceSearch = new FaceSearchWrapper.FaceSearch();
            motionDetector = new MotionDetectWrapper.MotionDetector();
            ImageSampleCount = System.IO.Directory.GetFiles(Properties.Settings.Default.FaceSampleLib, "*.jpg").Length;

            SVMWrapper.InitSvmData(ImageLen, 20);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG

            if (!Util.VerifyKey())
            {
                RegisterForm form = new RegisterForm();
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Application.Restart();
                }
                
                return;
            }
#endif

            if (argv.Length > 0)
            {
                directory = argv[0];
            }

            string baseAddress = string.Format("net.tcp://{0}:8000", System.Net.IPAddress.Any);

            Uri netTcpBaseAddress = new Uri(baseAddress);
            ServiceHost host = new ServiceHost(typeof(Service.Service), netTcpBaseAddress);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            host.AddServiceEndpoint(typeof(RemoteControlService.IServiceFacade),
                tcpBinding, "TcpService");

            host.Open();

            Application.Run(new MainForm());

        }


        

        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
