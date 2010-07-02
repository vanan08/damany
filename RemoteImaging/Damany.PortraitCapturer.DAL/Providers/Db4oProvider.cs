using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Activation;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.TA;

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
                var config = Db4oClientServer.NewServerConfiguration();
                config.Common.Add(new TransparentActivationSupport());

                var embconfig = Db4oEmbedded.NewConfiguration();
                embconfig.Common.Add(new TransparentActivationSupport());
                server = Db4oClientServer.OpenServer(config, this.uriOfDb, 0);

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
            var container = OpenContainer();
            container.Store(portrait);
            container.Commit();
        }

        public void SaveFrame(DTO.Frame frame)
        {
            var container = OpenContainer();
            container.Store(frame);
            container.Commit();
        }

        public DTO.Frame GetFrame(Guid frameId)
        {
            var container = OpenContainer();
            return GetFrameInternal(frameId, container);
        }

        public DTO.Portrait GetPortrait(Guid portraitId)
        {
            var container = OpenContainer();
            return GetPortraitInternal(portraitId, container);
        }


        public IEnumerable<DTO.Frame> GetFramesQuery()
        {
            var frameQuery = from DTO.Frame frame in OpenContainer()
                             select frame;

            return frameQuery;
        }

        public IList<DTO.Frame> GetFrames(int cameraId, Damany.Util.DateTimeRange range)
        {
            var container = OpenContainer();
            var frames = container.Query<DTO.Frame>(frame =>
            {
                bool flag = frame.CapturedAt >= range.From && frame.CapturedAt <= range.To;

                if (cameraId != -1)
                {
                    flag = flag && frame.SourceId == cameraId;
                }

                return flag;
            });

            return frames;

        }

        public IList<DTO.Portrait> GetPortraits(int cameraId, Damany.Util.DateTimeRange range)
        {
            var container = OpenContainer();
            var portraits = container.Query<DTO.Portrait>(portrait =>
            {
                bool flag = portrait.CapturedAt >= range.From && portrait.CapturedAt <= range.To;
                if (cameraId != -1)
                {
                    flag = flag && portrait.SourceId == cameraId;
                }

                return flag;
            });


            return portraits;

        }

        public void DeletePortrait(Guid portraitId)
        {
            var container = OpenContainer();
            var p = GetPortraitInternal(portraitId, container);
            if (p != null)
            {
                container.Delete(p);
                container.Commit();
            }
        }

        public void DeletePortraits(IEnumerable<DTO.Portrait> portraits)
        {
            var c = OpenContainer();
            foreach (var portrait in portraits)
            {
                c.Delete(portrait);
            }

            c.Commit();


        }

        public void DeleteFrames(IEnumerable<DTO.Frame> frames)
        {
            var c = OpenContainer();
            foreach (var frame in frames)
            {
                c.Delete(frame);
            }

            c.Commit();
        }

        public void DeleteFrame(Guid frameId)
        {
            var container = OpenContainer();
            var p = GetFrameInternal(frameId, container);
            if (p != null)
            {
                container.Delete(p);
            }
        }

        public void DeleteFrame(DTO.Frame frame)
        {
            var container = OpenContainer();
            container.Delete(frame);
        }

        public void Commit()
        {
            OpenContainer().Commit();
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
            return _container ?? (_container = this.server.OpenClient());
        }


        private string uriOfDb;
        private Db4objects.Db4o.IObjectServer server;
        private Db4objects.Db4o.IObjectContainer _container;
    }
}
