using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Damany.Imaging.Contracts;
using Damany.Imaging.Processors;
using Damany.PortraitCapturer.Repository;
using Damany.Imaging.Handlers;
using Damany.Cameras.Wrappers;
using Damany.Cameras;

namespace Damany.PC.Shell.Winform
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var driver = BootStrapper.BootStrap(@"D:\20090505", "dir");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainForm = new Form1();
            mainForm.driver = driver;
            Application.Run(mainForm);
        }
    }

    static class BootStrapper
    {
        static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);
        private const string root_dir = @".\DataNew";
        private const string image_dir = @".\DataNew\Images";


        private static IFrameStream NewCamera(string cameraType, Uri uri)
        {
            IFrameStream source = null;

            if (cameraType.ToUpper().Contains("AIP"))
            {
                var aip = (AipStarCamera)Damany.Cameras.Factory.NewAipStarCamera(uri);
                aip.UserName = "system";
                aip.PassWord = "system";
                source = aip;
            }
            else if (cameraType.ToUpper().Contains("SANYO"))
            {
                var sanyo = (SanyoNetCamera)Damany.Cameras.Factory.NewSanyoCamera(uri);
                sanyo.UserName = "guest";
                sanyo.PassWord = "guest";
                source = sanyo;
            }
            else if (cameraType.ToUpper().Contains("DIR"))
            {
                var dir = new Damany.Cameras.DirectoryFilesCamera(uri.AbsolutePath, "*.jpg");
                source = dir;
            }
            else
                throw new NotSupportedException("camera type not supported");

            return source;
        }


        private static Damany.Util.PersistentWorker InitDriver(IFrameStream source, MotionDetector motionDetector)
        {
            var retriever = new Damany.Util.PersistentWorker();
            retriever.OnWorkItemIsDone += item =>
            {
                Console.Write("\r");
                Frame f = item as Frame;
                Console.Write(f.ToString());
            };

            retriever.DoWork = delegate
            {
                var frame = source.RetrieveFrame();
                retriever.ReportWorkItem(frame);
                motionDetector.DetectMotion(frame);
            };

            retriever.OnExceptionRetry = delegate { source.Connect(); };
            return retriever;
        }


        private static void HandleInput(PersistenceService persistenceService,
            PersistenceWriter portraitFileSystemWriter,
            Damany.Util.PersistentWorker retriever)
        {
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        retriever.WorkFrequency *= 2;
                        break;
                    case ConsoleKey.DownArrow:
                        retriever.WorkFrequency /= 2;
                        break;
                    case ConsoleKey.F:
                        var query =
                            persistenceService.GetPortraits(new Damany.Util.DateTimeRange(DateTime.Now.AddHours(-1), DateTime.Now));

                        Console.WriteLine("query hit: " + query.Count);

                        break;
                    default:
                        retriever.Stop();
                        portraitFileSystemWriter.Stop();
                        exit.Set();
                        return;
                        break;
                }
            }
        }

        public static Damany.Util.PersistentWorker BootStrap( string url, string cameraType )
        {
                Uri uri = new Uri(url);

                IFrameStream source = NewCamera(cameraType, uri);
                source.Initialize();
                source.Connect();

                var persistenceService = GetPersistenceService();
                var writer = new PersistenceWriter(persistenceService);
                writer.Initialize();


                finder = new PortraitFinder();
                finder.AddListener(writer);

                MotionDetector motionDetector = new MotionDetector();
                motionDetector.MotionFrameCaptured += finder.HandleMotionFrame;

                return InitDriver(source, motionDetector);
           

        }

        public static PortraitFinder finder { get; set; }

        private static string ObjToPathMapper(Damany.Imaging.Contracts.CapturedObject obj)
        {
            var relativePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.jpg",
                obj.CapturedAt.Year,
                obj.CapturedAt.Month,
                obj.CapturedAt.Day,
                obj.CapturedAt.Hour,
                obj.Guid.ToString());

            return System.IO.Path.Combine(image_dir, relativePath);
        }


        private static IFrameStream FrameStreamFromUri(string uriString)
        {
            System.Uri uri = new System.Uri(uriString);

            IFrameStream source = null;

            switch (uri.Scheme)
            {
                case "file":
                    var directory = new Damany.Cameras.DirectoryFilesCamera(uri.AbsolutePath, "*.jpg");
                    directory.Initialize();
                    source = directory;
                    break;
                case "http":
                    var sanyo = new Damany.Cameras.SanyoNetCamera();
                    sanyo.UserName = "guest";
                    sanyo.PassWord = "guest";
                    sanyo.Uri = uri;
                    sanyo.Initialize();
                    sanyo.Connect();
                    source = sanyo;
                    break;

                default:
                    break;
            }
            return source;
        }


        private static PersistenceService GetPersistenceService()
        {
            var dataProvider = InitializeDatabase();
            var persistenceService =
                new PersistenceService(dataProvider, ObjToPathMapper, ObjToPathMapper);
            return persistenceService;
        }

        private static Damany.PortraitCapturer.DAL.IDataProvider InitializeDatabase()
        {
            System.IO.Directory.CreateDirectory(root_dir);
            System.IO.Directory.CreateDirectory(image_dir);

            var storePath = System.IO.Path.Combine(root_dir, "images.db4o");
            var store = new Damany.PortraitCapturer.DAL.Providers.Db4oProvider(storePath);
            store.StartServer();

            return store;
        }

        private static void Run(string uriString)
        {
            try
            {
                IFrameStream source = FrameStreamFromUri(uriString);


                var frameWritter = new FrameWritter();

                var portraitWriter = new PortraitsLogger(@".\Portrait");
                portraitWriter.Initialize();

                var asyncPortraitWriter = new AsyncPortraitLogger(@".\AsyncPortrait1");
                asyncPortraitWriter.Stopped += (o, e) => System.Diagnostics.Debug.WriteLine(e.Value.Message);
                asyncPortraitWriter.Initialize();

                var asyncWriter1 = new AsyncPortraitLogger(@".\asyncport2");
                asyncWriter1.Initialize();
                asyncWriter1.Start();

                var portraitFinder = new PortraitFinder();
                portraitFinder.AddListener(asyncPortraitWriter);
                //              portraitFinder.AddListener(portraitWriter);
                //              portraitFinder.AddListener(asyncWriter1);

                asyncPortraitWriter.Start();


                portraitFinder.PortraitCaptured += list =>
                {
                    Console.Write("[");
                    list.ToList().ForEach(p => Console.Write(p.ToString() + ","));
                    Console.WriteLine("]");
                };

                var motionDetector = new MotionDetector();
                motionDetector.MotionFrameCaptured += portraitFinder.HandleMotionFrame;

                while (true)
                {
                    var frame = source.RetrieveFrame();

                    System.Diagnostics.Debug.WriteLine("main thread id: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

                    Console.WriteLine(frame.ToString());
                    motionDetector.DetectMotion(frame);

                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }

        }
    }
}
