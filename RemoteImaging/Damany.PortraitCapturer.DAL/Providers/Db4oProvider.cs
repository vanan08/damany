using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;

namespace Damany.PortraitCapturer.DAL.Providers
{
    public class Db4oProvider
    {
        public Db4oProvider(string dataBaseFile)
        {
            this.uriOfDb = dataBaseFile;
        }

        public void StartServer()
        {
            if (this.server == null)
            {
                this.server = Db4objects.Db4o.CS.Db4oClientServer.OpenServer(this.uriOfDb, 0);
            }
        }

        public void StopServer()
        {
            if (this.server != null)
            {
                this.server.Close();
            }
        }

        #region IDataProvider Members

        public void SavePortrait(DTO.Portrait portrait)
        {
            using (var container = OpenContainer())
            {
                container.Store(portrait);
            }
        }

        public void SaveFrame(DTO.Frame frame)
        {
            using (var container = OpenContainer())
            {
                container.Store(frame);
            }
        }

        public DTO.Frame GetFrame(Guid frameId)
        {
            using (var container = OpenContainer())
            {
                 return GetFrameInternal(frameId, container);
            }
        }

        public DTO.Portrait GetPortrait(Guid portraitId)
        {
            using (var container = OpenContainer())
            {
                return GetPortraitInternal(portraitId, container);
            }
        }

        public IList<DTO.Frame> GetFrames(Damany.Util.DateTimeRange range)
        {
            using (var container = OpenContainer())
            {
                return container.Query<DTO.Frame>(frame => {
                   return frame.CapturedAt >= range.From && frame.CapturedAt <= range.To;
                });
            }
            
        }

        public IList<DTO.Portrait> GetPortraits(int cameraId, Damany.Util.DateTimeRange range)
        {
            using (var container = OpenContainer())
            {
                return container.Query<DTO.Portrait>(portrait =>
                {
                    bool flag = portrait.CapturedAt >= range.From && portrait.CapturedAt <= range.To;
                    if (cameraId != -1)
                    {
                        flag = flag && portrait.SourceId == cameraId;
                    }

                    return flag;
                });
            }

        }

        public void DeletePortrait(Guid portraitId)
        {
            using (var container = OpenContainer())
            {
                var p = GetPortraitInternal(portraitId, container);
                if (p != null)
                {
                    container.Delete(p);
                }
            }
        }

        public void DeleteFrame(Guid frameId)
        {
            using (var container = OpenContainer())
            {
                var p = GetFrameInternal(frameId, container);
                if (p != null)
                {
                    container.Delete(p);
                }
            }
            
        }

        #endregion

        private static DTO.Portrait GetPortraitInternal(Guid portraitId, Db4objects.Db4o.IObjectContainer container)
        {
            return container.Query<DTO.Portrait>(portrait => portrait.Guid.Equals(portraitId)).SingleOrDefault();
        }

        private static DTO.Frame GetFrameInternal(Guid frameId, Db4objects.Db4o.IObjectContainer container)
        {
            return container.Query<DTO.Frame>(frame => frame.Guid.Equals(frameId)).SingleOrDefault();
        }

        private Db4objects.Db4o.IObjectContainer OpenContainer()
        {
            return this.server.OpenClient();
        }
        private string uriOfDb;
        private Db4objects.Db4o.IObjectServer server;
    }
}
