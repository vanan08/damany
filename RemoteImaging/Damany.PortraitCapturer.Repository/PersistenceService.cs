using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Damany.Imaging.Contracts;
using Damany.PortraitCapturer.DAL;

namespace Damany.PortraitCapturer.Repository
{
    using DAL;

    public class PersistenceService
    {
        public static PersistenceService CreateDefault(string root)
        {
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
                .ConstructUsing(dto => new Frame(dto.Path))
                .ForMember("MotionRectangles", opt => opt.Ignore())
                .ForMember("CapturedFrom", opt => opt.MapFrom( dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source;} ));
            Mapper.CreateMap<Damany.PortraitCapturer.DAL.DTO.Portrait, Portrait>()
                .ConstructUsing(dto => new Portrait(dto.Path))
                .ForMember("CapturedFrom", opt => opt.MapFrom(dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source; }));


            Mapper.AssertConfigurationIsValid();

            this.dataProvider = dataProvider;
        }

        public void SavePortrait(Portrait portrait)
        {
            var dto = Mapper.Map<Portrait, DAL.DTO.Portrait>(portrait);
            dataProvider.SavePortrait(dto);
            CreateDirectory(dto.Path);
            portrait.GetImage().SaveImage(dto.Path);

        }

        public void SaveFrame(Frame frame)
        {
            var dto = Mapper.Map<Frame, DAL.DTO.Frame>(frame);
            dataProvider.SaveFrame(dto);

            CreateDirectory(dto.Path);
            frame.GetImage().SaveImage(dto.Path);
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

        private static void CreateDirectory(string path)
        {
            var directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }
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
            var store = new Damany.PortraitCapturer.DAL.Providers.Db4oProvider(storePath);
            store.StartServer();

            return store;
        }

        private static string ObjToPathMapper(Damany.Imaging.Contracts.CapturedObject obj)
        {
            var relativePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.jpg",
                obj.CapturedAt.Year,
                obj.CapturedAt.Month,
                obj.CapturedAt.Day,
                obj.CapturedAt.Hour,
                obj.Guid.ToString());

            return System.IO.Path.Combine(image_dir, relativePath);
        }

        static string root_dir = @".\Data";
        static string image_dir = @".\Data\Images";

        IDataProvider dataProvider;
    }
}
