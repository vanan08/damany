﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using NDepend.Helpers.FileDirectoryPath;

namespace Damany.PortraitCapturer.DAL.Providers
{

    public class LocalDb4oProvider : IRepository
    {
        public LocalDb4oProvider(string outputDirectory)
        {
            string reason;
            if (PathHelper.IsValidRelativePath(outputDirectory, out reason))
            {
                outputDirectory = 
                    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), outputDirectory);
            }

            root_dir = outputDirectory;
            image_dir = System.IO.Path.Combine(root_dir, "Images");

            this.FrameToPathConverter = this.ObjToPathMapper;
            this.PortraitToPathConverter = this.ObjToPathMapper;
   
        }


        public void Start()
        {
            if (!this.started)
            {
                InitAutoMapper();
                this.InitializeDatabase();
                this.started = true;
            }
        }

        public void Stop()
        {
            if (!this.started) return;

            if (this.dataProvider != null)
            {
                this.dataProvider.StopServer();
            }
        }


        public void SavePortrait(Portrait portrait)
        {
            this.CheckStarted();

            var dto = Mapper.Map<Portrait, DAL.DTO.Portrait>(portrait);
            dataProvider.SavePortrait(dto);
            var absolutePath = GetAbsolutePath(dto.Path);
            portrait.GetImage().SaveImage(absolutePath);

        }

        public void SaveFrame(Frame frame)
        {
            this.CheckStarted();
            var dto = Mapper.Map<Frame, DAL.DTO.Frame>(frame);
            dataProvider.SaveFrame(dto);

            var absolutePath = GetAbsolutePath(dto.Path);
            frame.GetImage().SaveImage(absolutePath);
        }

        public Frame GetFrame(System.Guid frameId)
        {
            this.CheckStarted();
            var dto = dataProvider.GetFrame(frameId);
            var frame = Mapper.Map<Damany.PortraitCapturer.DAL.DTO.Frame, Frame>(dto);

            return frame;
        }

        public Portrait GetPortrait(System.Guid portraitId)
        {
            this.CheckStarted();
            var dto = dataProvider.GetPortrait(portraitId);
            var p = Mapper.Map<Damany.PortraitCapturer.DAL.DTO.Portrait, Portrait>(dto);

            return p;
        }

        public IList<Frame> GetFrames(Damany.Util.DateTimeRange range)
        {
            this.CheckStarted();
            var dtos = dataProvider.GetFrames(range);
            var frames = dtos.ToList().ConvertAll( dto => Mapper.Map<DAL.DTO.Frame, Frame>(dto) );
            return frames;
        }

        public IList<Portrait> GetPortraits(Damany.Util.DateTimeRange range)
        {
            this.CheckStarted();
            var dtos = dataProvider.GetPortraits(range);
            var portraits = dtos.ToList().ConvertAll(dto => Mapper.Map<DAL.DTO.Portrait, Portrait>(dto));
            return portraits;

        }

        public void DeletePortrait(System.Guid portraitId)
        {
            this.CheckStarted();
            dataProvider.DeletePortrait(portraitId);

        }

        public void DeleteFrame(System.Guid frameId)
        {
            this.CheckStarted();
            dataProvider.DeleteFrame(frameId);
        }

       

        private string GetAbsolutePath(string relativePathOfImage)
        {
            var absoluteDirectory = System.IO.Path.Combine(image_dir, relativePathOfImage);

            var directory = System.IO.Path.GetDirectoryName(absoluteDirectory);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            return absoluteDirectory;
        }

 
        private void InitializeDatabase()
        {
            if (this.dataProvider == null)
            {
                System.IO.Directory.CreateDirectory(root_dir);
                System.IO.Directory.CreateDirectory(image_dir);

                var storePath = System.IO.Path.Combine(root_dir, "images.db4o");
                this.dataProvider = new Damany.PortraitCapturer.DAL.Providers.Db4oProvider(storePath);
                this.dataProvider.StartServer();
            }
        }

        private string ObjToPathMapper(Damany.Imaging.Common.CapturedObject obj)
        {
            var relativePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.jpg",
                obj.CapturedAt.Year,
                obj.CapturedAt.Month,
                obj.CapturedAt.Day,
                obj.CapturedAt.Hour,
                obj.Guid.ToString());


            return relativePath;
        }

        private void InitAutoMapper()
        {
            Mapper.CreateMap<Frame, Damany.PortraitCapturer.DAL.DTO.Frame>()
                     .ForMember("Path", opt => opt.MapFrom(this.FrameToPathConverter))
                     .ForMember("SourceId", opt => opt.MapFrom(frame => frame.CapturedFrom.Id));


            Mapper.CreateMap<Portrait, Damany.PortraitCapturer.DAL.DTO.Portrait>()
                .ForMember("Path", opt => opt.MapFrom(this.PortraitToPathConverter))
                .ForMember("SourceId", opt => opt.MapFrom(portrait => portrait.CapturedFrom.Id));


            Mapper.CreateMap<Damany.PortraitCapturer.DAL.DTO.Frame, Frame>()
                .ConstructUsing(dto => new Frame(System.IO.Path.Combine(image_dir, dto.Path)))
                .ForMember("MotionRectangles", opt => opt.Ignore())
                .ForMember("CapturedFrom", opt => opt.MapFrom(dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source; }));
            Mapper.CreateMap<Damany.PortraitCapturer.DAL.DTO.Portrait, Portrait>()
                .ConstructUsing(dto => new Portrait(System.IO.Path.Combine(image_dir, dto.Path)))
                .ForMember("CapturedFrom", opt => opt.MapFrom(dto => { var source = new MockFrameSource(); source.Id = dto.SourceId; return source; }));


            Mapper.AssertConfigurationIsValid();
        }

        private void CheckStarted()
        {
            if (!this.started)
            {
                throw new InvalidOperationException("Start must be called first");
            }
        }

       

        public Func<Damany.Imaging.Common.Frame, string> FrameToPathConverter { get; set; }
        public Func<Damany.Imaging.Common.Portrait, string> PortraitToPathConverter { get; set; }


        string root_dir = @".\Data";
        string image_dir = @".\Data\Images";
        bool started;

        Damany.PortraitCapturer.DAL.Providers.Db4oProvider dataProvider;

       
    }
}