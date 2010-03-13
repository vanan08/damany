using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Cameras;
using Damany.Imaging.Contracts;
using Damany.Imaging.Processors;
using Damany.Imaging.Handlers;
using Damany.Cameras.Wrappers;

namespace Damany.PortraitCapturer.Shell.CmdLine
{
    class Program
    {
        static FramePumper pumper;
        static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);

        static void Main(string[] args)
        {
            try
            {
                System.Threading.ThreadPool.QueueUserWorkItem(uri => RunPumper(uri), args[0]);

                exit.WaitOne();

                pumper.Stop();

            }
            catch (System.Exception ex)
            {

            }

        }

        private static void RunPumper(object uriString)
        {
            try
            {
                Uri uri = new Uri(uriString as string);

                var source = (AipStarCamera)Damany.Cameras.Factory.NewAipStarCamera(uri);
                source.UserName = "system";
                source.PassWord = "system";

                source.Initialize();
                source.Connect();

                AsyncPortraitLogger writer = new AsyncPortraitLogger("portraits captured");
                writer.Initialize();
                writer.Start();

                PortraitFinder finder = new PortraitFinder();
                finder.AddListener(writer);

                MotionDetector motionDetector = new MotionDetector();
                motionDetector.MotionFrameCaptured += finder.HandleMotionFrame;

                var retriever = new Damany.Util.PersistentWorker();
                retriever.OnWorkItemIsDone += item =>
                {
                    Frame f = item as Frame;
                    Console.WriteLine(f.Ipl.Size.ToString() + " " + f.CapturedAt.ToString() + " " + f.CapturedFrom.Description);
                };

                retriever.DoWork = delegate
                {
                    var frame = source.RetrieveFrame();
                    retriever.ReportWorkItem(frame);
                    motionDetector.DetectMotion(frame);
                };
                retriever.OnExceptionRetry = delegate { source.Connect(); };
                retriever.Start();


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
                        default:
                            exit.Set();
                            return;
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

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
                    sanyo.Password = "guest";
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
