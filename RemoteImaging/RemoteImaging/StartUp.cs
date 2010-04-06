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
            var personRepository = SuspectsRepository.SuspectsRepositoryManager.LoadFrom(@"d:\imglib");
            this.builder.RegisterInstance(personRepository.Peoples).As
                <IEnumerable<Damany.Imaging.PlugIns.PersonOfInterest>>().ExternallyOwned();
        }

        private void InitDataProvider()
        {
            var repository = new Damany.PortraitCapturer.DAL.Providers.LocalDb4oProvider(@"D:\ImageOutput");
            repository.Start();

            this.builder.RegisterInstance(repository).As<Damany.PortraitCapturer.DAL.IRepository>().ExternallyOwned();
        }

        private void InitConfigManager()
        {
            var configManger = Damany.RemoteImaging.Common.ConfigurationManager.GetDefault();

            this.builder.RegisterInstance(configManger).As<Damany.RemoteImaging.Common.ConfigurationManager>().ExternallyOwned();
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
        public Autofac.IContainer Container { get; private set; }

        private Autofac.ContainerBuilder builder;

        private Damany.PortraitCapturer.DAL.Providers.Db4oProvider dataProvider;
    }
}
