using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Damany.PC.Domain;
using Damany.PortraitCapturer.DAL.Providers;
using Autofac;
using Damany.Imaging.PlugIns;
using MiscUtil;
using SuspectsRepository;
using Damany.RemoteImaging.Common;
using Damany.RemoteImaging.Common;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging
{
    class StartUp
    {
        public event EventHandler StatusChanged;

        public void Start()
        {
            //CheckImportantFiles();

            this.builder = new Autofac.ContainerBuilder();

            this.InitDataProvider();
            this.InitConfigManager();
            this.RegisterTypes();
        }

        private void CheckImportantFiles()
        {
            var filesToCheck = new string[]
                                   {
                                       "cv100.dll",
                                       "cvaux100.dll",
                                       "cvcam100.dll",
                                       "cxcore100.dll",
                                       "cxts001.dll",
                                       "highgui100.dll",
                                       "libguide40.dll",
                                       "ml100.dll",
                                       "avdecoder.dll",
                                       "bk_netclientsdk.dll",
                                       "bkpostproc.dll",
                                       "transtclient.dll",
                                       "trclient.dll",
                                   };

            var dllsQuery = from f in System.IO.Directory.GetFiles(@".\", "*.dll")
                            select System.IO.Path.GetFileName(f).ToUpper();

            var dlls = dllsQuery.ToArray();


            foreach (var f in filesToCheck)
            {
                if (!dlls.Contains(f.ToUpper()))
                {
                    throw new System.IO.FileNotFoundException(f + " is missing");
                }
            }


        }

        private void InitDataProvider()
        {
            if (!System.IO.Directory.Exists(Properties.Settings.Default.OutputPath))
            {
                System.IO.Directory.CreateDirectory(Properties.Settings.Default.OutputPath);
            }

            if (Properties.Settings.Default.UseDirectoryRepository)
            {
                if (!System.IO.Directory.Exists(Properties.Settings.Default.DirectoryRepositoryPath))
                {
                    throw new DirectoryNotFoundException("Directory Repository Path not found");
                }

                var dirRepository = new Damany.PortraitCapturer.DAL.DirectoryRepository(@"M:\imageSearch");

                this.builder.RegisterInstance(dirRepository)
                            .As<IRepository>()
                            .ExternallyOwned();
            }
            else
            {
                var repository = new LocalDb4oProvider(Properties.Settings.Default.OutputPath);
                repository.Start();


                this.builder.RegisterInstance(repository)
                    .As<IRepository>()
                    .ExternallyOwned();
            }

        }

        private void InitConfigManager()
        {
            var configManger = ConfigurationManager.GetDefault();

            this.builder.RegisterInstance(configManger)
                .As<ConfigurationManager>()
                .ExternallyOwned();
        }

        private void RegisterTypes()
        {
            builder.RegisterType<FileSystemStorage>()
                .WithParameter("outputRoot", Properties.Settings.Default.OutputPath)
                .SingleInstance();

            builder.RegisterType<OutDatedDataRemover>()
                .WithParameter("outputDirectory", Properties.Settings.Default.OutputPath)
                .WithProperty("ReservedDiskSpaceInGb", Properties.Settings.Default.ReservedDiskSpaceGb);

            this.builder.RegisterType<Query.PicQueryForm>()
                .As<IPicQueryScreen>();
            this.builder.RegisterType<PicQueryFormPresenter>()
                .As<IPicQueryPresenter>();

            this.builder.RegisterType<Query.VideoQueryForm>()
                .As<Query.IVideoQueryScreen>();
            this.builder.RegisterType<Query.VideoQueryPresenter>()
                .As<Query.IVideoQueryPresenter>();

            this.builder.RegisterType<Damany.RemoteImaging.Common.Forms.FaceCompare>();
            this.builder.RegisterType<Damany.RemoteImaging.Common.Presenters.FaceComparePresenter>();

            builder.RegisterType<FaceProcessingWrapper.MotionDetector>()
                .As<IMotionDetector>();

            builder.RegisterType<Damany.Imaging.Processors.MotionDetector>();

            builder.RegisterType<Damany.Imaging.Processors.PortraitFinder>()
                   .PropertiesAutowired();

            this.builder.RegisterType<OptionsForm>().SingleInstance();
            this.builder.RegisterType<OptionsPresenter>();

            this.builder.RegisterType<MainController>();

            builder.RegisterType<Damany.Imaging.Common.EventAggregator>()
                   .As<IEventAggregator>().SingleInstance();

            builder.RegisterType<RealtimeDisplay.MainForm>().SingleInstance().PropertiesAutowired();

            builder.RegisterType<FaceSearchFacade>().WithProperty("MotionQueueSize", Properties.Settings.Default.MaxFrameQueueLength);


            builder.RegisterModule(new Autofac.Configuration.ConfigurationSettingsReader());

            this.Container = this.builder.Build();

        }


        public string Status { get; set; }
        public string OutputImagePath { get; set; }
        public IContainer Container { get; private set; }

        private ContainerBuilder builder;

        private Db4oProvider dataProvider;
    }
}
