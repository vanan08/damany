using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using SuspectsRepository;

namespace RemoteImaging
{
    public class FaceProcessingModule : Autofac.Module
    {
        public bool EnableBackgroundComparer { get; set; }

        public bool EnableFrontFaceComparer { get; set; }
        public string FaceTemplatePath { get; set; }

        public bool EnableFaceComparer { get; set; }
        public string PersonOfInterestLibraryPath { get; set; }

        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<FaceComparer>()
                    .As<IOperation<Portrait>>()
                    .As<FaceComparer>()
                    .SingleInstance();


            if (EnableFaceComparer)
            {
                if (!System.IO.Directory.Exists(PersonOfInterestLibraryPath))
                    throw new System.IO.DirectoryNotFoundException("Persons of Interest library doesn't exist");

                var personRepository = SuspectsRepositoryManager.LoadFrom(PersonOfInterestLibraryPath);
                builder.RegisterInstance(personRepository.Peoples)
                    .As<IEnumerable<PersonOfInterest>>()
                    .ExternallyOwned();

                builder.RegisterType<LbpFaceComparer>()
                       .As<IRepositoryFaceComparer>();
            }
            else
            {
                builder.Register(c => new PersonOfInterest[0]).SingleInstance();
                builder.RegisterType<NullRepositoryFaceComparer>()
                       .As<IRepositoryFaceComparer>();
            }


            if (EnableFrontFaceComparer)
            {
                builder.RegisterType<Damany.Imaging.Handlers.FrontFaceVerifier>()
                        .WithParameter("template", FaceTemplatePath)
                        .As<IOperation<Portrait>>();
            }


            builder.RegisterType<RealtimeDisplay.MainForm>()
                    .As<IOperation<Portrait>>()
                    .As<RealtimeDisplay.MainForm>()
                    .PropertiesAutowired()
                    .SingleInstance();

            if (EnableBackgroundComparer)
            {
                builder.RegisterType<Damany.Imaging.Handlers.FaceVerifier>()
                .As<IOperation<Portrait>>();
            }

        }
    }
}
