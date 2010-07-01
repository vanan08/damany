using System;
using System.Collections.Generic;
using System.Linq;
using Damany.Imaging.Common;
using Damany.Util;

namespace Damany.PortraitCapturer.DAL
{
    public class DirectoryRepository : IRepository
    {
        private readonly string _directoryPath;

        public DirectoryRepository(string directoryPath)
        {
            _directoryPath = directoryPath;
        }

        public void SavePortrait(Portrait portrait)
        {
            
        }

        public void SaveFrame(Frame frame)
        {
            
        }

        public Frame GetFrame(Guid frameId)
        {
            return null;
        }

        public Portrait GetPortrait(Guid portraitId)
        {
            return null;
        }

        public IList<Frame> GetFrames(int cameraId, DateTimeRange range)
        {
            return new List<Frame>();
        }

        public IList<Portrait> GetPortraits(int cameraId, DateTimeRange range)
        {
            var portraits = from f in System.IO.Directory.GetFiles(this._directoryPath, "*.jpg")
                            select new Portrait(f);

            return portraits.ToList();
        }

        public bool FrameExists(int cameraId, DateTime time)
        {
            throw new NotImplementedException();
        }

        public bool PortraitExists(int cameraId, DateTime time)
        {
            throw new NotImplementedException();
        }

        public void DeletePortrait(Guid portraitId)
        {
        }

        public void DeleteFrame(Guid frameId)
        {
        }

        public void DeletePortraits(int cameraId, DateTimeRange range)
        {
        }

        public void DeleteFrames(int cameraId, DateTimeRange range)
        {
        }
    }
}