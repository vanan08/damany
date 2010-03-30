using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL.Providers;
using Autofac;

namespace RemoteImaging
{
    class StartUp
    {
        public event EventHandler StatusChanged;

        public void Start()
        {
            this.builder = new Autofac.ContainerBuilder();

            this.InitDataProvider();
            this.RegisterTypes();
        }


        public void CheckImportantFiles()
        {

        }

        public void InitDataProvider()
        {
            var repository = new Damany.PortraitCapturer.DAL.Providers.LocalDb4oProvider(@".\images.db4o");
            repository.Start();

            this.builder.RegisterInstance(repository).As<Damany.PortraitCapturer.DAL.IRepository>().ExternallyOwned();
        }

        public void RegisterTypes()
        {
            this.builder.RegisterType<RemoteImaging.Query.PicQueryForm>().As<RemoteImaging.IPicQueryScreen>();
            this.builder.RegisterType<RemoteImaging.PicQueryFormPresenter>().As<RemoteImaging.IPicQueryPresenter>();

            this.builder.RegisterType<RemoteImaging.RealtimeDisplay.MainForm>();

            this.Container = this.builder.Build();
        }


        public string Status { get; set; }
        public string OutputImagePath { get; set; }
        public Autofac.IContainer Container { get; private set; }

        private Autofac.ContainerBuilder builder;

        private Damany.PortraitCapturer.DAL.Providers.Db4oProvider dataProvider;
    }
}
