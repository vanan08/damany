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
            var files = System.IO.Directory.GetFiles(this._directoryPath, "*.jpg");

            var portraits = new List<Portrait>();

            foreach (var file in files)
            {
                var portrait = new Portrait(file);
                portrait.CapturedAt = DateTime.Now;
                portrait.CapturedFrom = new MockFrameSource();

                portraits.Add(portrait);
            }

            return portraits.ToList();
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