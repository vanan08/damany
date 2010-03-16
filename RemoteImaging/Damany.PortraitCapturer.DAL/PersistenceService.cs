using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using NDepend.Helpers.FileDirectoryPath;

namespace Damany.PortraitCapturer.DAL
{
    using Damany.PortraitCapturer.DAL.Providers;

    public class PersistenceService
    {
        public static PersistenceService CreateDefault(string root)
        {
            string reason;
            if (PathHelper.IsValidRelativePath(root, out reason))
            {
                root = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), root);
            }

            root_dir = root;
            image_dir = System.IO.Path.Combine(root_dir, "Images");
           
            return GetPersistenceService();

        }

        public PersistenceService(IDataProvider dataProvider,
                                  Func<Frame, string> frameToPathConverter,
                                  Func<Portrait, string> portraitToPathConverter )
        {
            Mapper.CreateMap<Frame, Damany.PortraitCapturer.DAL.DTO.Frame>()
                .ForMember("Path", opt => opt.MapFrom(frameToPathConverter))
                .ForMember("SourceId", opt => opt.MapFrom( frame => frame.CapturedFrom.Id ));


            Mapper.CreateMap<Portrait, Damany.PortraitCapturer.DAL.DTO.Portrait>()
                .ForMember("Path", opt => opt.MapFrom(portraitToPathConverter))
                .ForMember("SourceId", opt => opt.MapFrom (portrait => portrait.CapturedFrom.Id ) );



            Mapper.CreateMap<Damany.PortraitCapturer.DAL.DTO.Frame, Frame>()
                .ConstructUsing( dto => new Frame( System.IO.Path.Combine(image_dir, dto.Path) ) )
                .ForMember("MotionRectangles", opt => opt.Ignore())
                .ForMember("CapturedFrom", opt => opt.MapFrom( dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source;} ));
            Mapper.CreateMap<Damany.PortraitCapturer.DAL.DTO.Portrait, Portrait>()
                .ConstructUsing(dto => new Portrait( System.IO.Path.Combine(image_dir, dto.Path) ) )
                .ForMember("CapturedFrom", opt => opt.MapFrom(dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source; }));


            Mapper.AssertConfigurationIsValid();

            this.dataProvider = dataProvider;
        }

        public void SavePortrait(Portrait portrait)
        {
            var dto = Mapper.Map<Portrait, DAL.DTO.Portrait>(portrait);
            dataProvider.SavePortrait(dto);
            var absolutePath = GetAbsolutePath(dto.Path);
            portrait.GetImage().SaveImage(absolutePath);

        }

        public void SaveFrame(Frame frame)
        {
            var dto = Mapper.Map<Frame, DAL.DTO.Frame>(frame);
            dataProvider.SaveFrame(dto);

            var absolutePath = GetAbsolutePath(dto.Path);
            frame.GetImage().SaveImage(absolutePath);
        }

        public Frame GetFrame(System.Guid frameId)
        {
            var dto = dataProvider.GetFrame(frameId);
            var frame = Mapper.Map<Damany.PortraitCapturer.DAL.DTO.Frame, Frame>(dto);

            return frame;
        }

        public Portrait GetPortrait(System.Guid portraitId)
        {
            var dto = dataProvider.GetPortrait(portraitId);
            var p = Mapper.Map<Damany.PortraitCapturer.DAL.DTO.Portrait, Portrait>(dto);

            return p;
        }

        public IList<Frame> GetFrames(Damany.Util.DateTimeRange range)
        {
            var dtos = dataProvider.GetFrames(range);
            var frames = dtos.ToList().ConvertAll( dto => Mapper.Map<DAL.DTO.Frame, Frame>(dto) );
            return frames;
        }

        public IList<Portrait> GetPortraits(Damany.Util.DateTimeRange range)
        {
            var dtos = dataProvider.GetPortraits(range);
            var portraits = dtos.ToList().ConvertAll(dto => Mapper.Map<DAL.DTO.Portrait, Portrait>(dto));
            return portraits;

        }

        void DeletePortrait(System.Guid portraitId)
        {
            dataProvider.DeletePortrait(portraitId);

        }
        void DeleteFrame(System.Guid frameId)
        {
            dataProvider.DeleteFrame(frameId);
        }

        void Stop()
        {
            if (storageProvider != null)
            {
                Damany.PortraitCapturer.DAL.Providers.Db4oProvider db4o
                    = storageProvider as Damany.PortraitCapturer.DAL.Providers.Db4oProvider;
                if (db4o != null)
                {
                    db4o.StopServer();
                }
            }
        }

        private static string GetAbsolutePath(string relativePathOfImage)
        {
            var absoluteDirectory = System.IO.Path.Combine(image_dir, relativePathOfImage);

            var directory = System.IO.Path.GetDirectoryName(absoluteDirectory);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            return absoluteDirectory;
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
            storageProvider = new Damany.PortraitCapturer.DAL.Providers.Db4oProvider(storePath);
            storageProvider.StartServer();

            return storageProvider;
        }

        private static string ObjToPathMapper(Damany.Imaging.Common.CapturedObject obj)
        {
            var relativePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.jpg",
                obj.CapturedAt.Year,
                obj.CapturedAt.Month,
                obj.CapturedAt.Day,
                obj.CapturedAt.Hour,
                obj.Guid.ToString());


            return relativePath;
        }

        private static Db4oProvider storageProvider;
        static string root_dir = @".\Data";
        static string image_dir = @".\Data\Images";

        IDataProvider dataProvider;
    }
}
