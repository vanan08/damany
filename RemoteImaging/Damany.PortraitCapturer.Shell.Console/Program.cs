﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Cameras;
using Damany.Imaging.Common;
using Damany.Imaging.Processors;
using Damany.Imaging.Handlers;
using Damany.Cameras.Wrappers;
using Damany.PortraitCapturer.DAL.Providers;
using Damany.Imaging.Handlers;
using Damany.PortraitCapturer.DAL;

namespace Damany.PortraitCapturer.Shell.CmdLine
{
    class Program
    {
        static System.Threading.AutoResetEvent exit = new System.Threading.AutoResetEvent(false);
        private const string root_dir = @".\DataNew";
        private const string image_dir = @".\DataNew\Images";

        static void Main(string[] args)
        {

            try
            {
                //System.Threading.ThreadPool.QueueUserWorkItem(uri => RunPumper(uri), args);

                exit.WaitOne();

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private static void HandleInput(
            LocalDb4oProvider persistenceService,
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
                            persistenceService.GetPortraits(-1, new Damany.Util.DateTimeRange(DateTime.Now.AddHours(-1), DateTime.Now));

                        Console.WriteLine("query hit: " + query.Count);

                        break;
                    default:
                        retriever.Stop();
                        exit.Set();
                        return;
                        break;
                }
            }
        }

    }
}
