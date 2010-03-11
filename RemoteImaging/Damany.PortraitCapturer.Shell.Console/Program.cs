using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Cameras;
using Damany.Imaging.Contracts;
using Damany.Imaging.Processors;
using Damany.Imaging.Handlers;

namespace Damany.PortraitCapturer.Shell.CmdLine
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate { Run(args[0]); });

            Console.ReadKey();

        }

        private static void Run(string uriString)
        {
            try
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
                        var sanyo  = new Damany.Cameras.SanyoNetCamera();
                        sanyo.UserName = "guest";
                        sanyo.Password = "guest";
                        sanyo.Uri = uri;
                        sanyo.Initialize();
                        sanyo.Connect();
                        source = sanyo;
                        break;

                    default:
                        break;
                }



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
                    list.ToList().ForEach( p => Console.Write(p.ToString() + ","));
                    Console.WriteLine("]");
                };

                var motionDetector = new MotionDetector(portraitFinder);

                while(true)
                {
                    var frame = source.RetrieveFrame();

                    System.Diagnostics.Debug.WriteLine("main thread id: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

                    //Console.WriteLine(frame.ToString());
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
