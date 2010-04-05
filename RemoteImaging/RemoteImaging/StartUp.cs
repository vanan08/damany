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
            this.RegisterTypes();
        }

        public void Run()
        {
            var camInfo = new Damany.PC.Domain.CameraInfo();
            camInfo.Name = "sanyo";
            camInfo.Provider = CameraProvider.Sanyo;
            camInfo.Location = new Uri(@"http://192.168.1.201");
            camInfo.Id = 1;
            camInfo.Description = "";

            var camController = Damany.RemoteImaging.Common.SearchLineBuilder.BuildNewSearchLine(camInfo);
            var faceComparer = this.Container.Resolve<Damany.Imaging.PlugIns.FaceComparer>();
            camController.RegisterPortraitHandler(faceComparer);

            var persistenWriter =
                new Damany.Imaging.Handlers.PersistenceWriter(
                    this.Container.Resolve<Damany.PortraitCapturer.DAL.IRepository>());

            camController.RegisterPortraitHandler(persistenWriter);

            var mainForm = this.Container.Resolve<RemoteImaging.RealtimeDisplay.MainForm>();
            faceComparer.PersonOfInterestDected += delegate(object sender, EventArgs<PersonOfInterestDetectionResult> e)
            {
                mainForm.ShowSuspects(e.Value);
            };

            camController.RegisterPortraitHandler(mainForm);

            camController.Start();
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

        private void RegisterTypes()
        {
            this.builder.RegisterType<Query.PicQueryForm>().As<IPicQueryScreen>();
            this.builder.RegisterType<PicQueryFormPresenter>().As<IPicQueryPresenter>();

            this.builder.RegisterType<RealtimeDisplay.MainForm>().SingleInstance();

            this.builder.RegisterType<LbpFaceComparer>().As<IFaceComparer>();
            this.builder.RegisterType<FaceComparer>();

            this.Container = this.builder.Build();
        }


        public string Status { get; set; }
        public string OutputImagePath { get; set; }
        public Autofac.IContainer Container { get; private set; }

        private Autofac.ContainerBuilder builder;

        private Damany.PortraitCapturer.DAL.Providers.Db4oProvider dataProvider;
    }
}
