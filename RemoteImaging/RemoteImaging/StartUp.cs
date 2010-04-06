using System;
using System.Collections.Generic;
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

namespace RemoteImaging
{
    class StartUp
    {
        public event EventHandler StatusChanged;

        public void Start()
        {
            this.builder = new Autofac.ContainerBuilder();

            this.LoadPersonRepository();
            this.InitDataProvider();
            this.InitConfigManager();
            this.RegisterTypes();
        }

        private void CheckImportantFiles()
        {

        }

        private void LoadPersonRepository()
        {
            var personRepository = SuspectsRepositoryManager.LoadFrom(@"d:\imglib");
            this.builder.RegisterInstance(personRepository.Peoples).As
                <IEnumerable<PersonOfInterest>>().ExternallyOwned();
        }

        private void InitDataProvider()
        {
            var repository = new LocalDb4oProvider(@"D:\ImageOutput");
            repository.Start();

            this.builder.RegisterInstance(repository).As<Damany.PortraitCapturer.DAL.IRepository>().ExternallyOwned();
        }

        private void InitConfigManager()
        {
            var configManger = ConfigurationManager.GetDefault();

            this.builder.RegisterInstance(configManger).As<ConfigurationManager>().ExternallyOwned();
        }

        private void RegisterTypes()
        {
            this.builder.RegisterType<Query.PicQueryForm>().As<IPicQueryScreen>();
            this.builder.RegisterType<PicQueryFormPresenter>().As<IPicQueryPresenter>();


            this.builder.RegisterType<LbpFaceComparer>().As<IFaceComparer>();
            this.builder.RegisterType<FaceComparer>();

            this.builder.RegisterType<OptionsForm>().SingleInstance();
            this.builder.RegisterType<OptionsPresenter>();

            this.builder.RegisterType<MainController>();
            this.builder.RegisterType<RealtimeDisplay.MainForm>().SingleInstance();


            this.Container = this.builder.Build();
        }


        public string Status { get; set; }
        public string OutputImagePath { get; set; }
        public IContainer Container { get; private set; }

        private ContainerBuilder builder;

        private Db4oProvider dataProvider;
    }
}
