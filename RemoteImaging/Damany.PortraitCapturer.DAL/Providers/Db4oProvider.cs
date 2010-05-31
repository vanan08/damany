using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o;

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

            var portraitQuery = from DTO.Portrait p in container
                                where p.CapturedAt >= range.From && p.CapturedAt <= range.To
                                select p;

            if (cameraId != -1)
            {
                portraitQuery = from p in portraitQuery
                                where p.SourceId == cameraId
                                select p;
            }

            return portraitQuery.ToList();

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

        #endregion

        private static DTO.Portrait GetPortraitInternal(Guid portraitId, Db4objects.Db4o.IObjectContainer container)
        {
            var portraitQuery = from DTO.Portrait p in container
                                where p.Guid.Equals(portraitId)
                                select p;
            return portraitQuery.SingleOrDefault();
        }

        private static DTO.Frame GetFrameInternal(Guid frameId, Db4objects.Db4o.IObjectContainer container)
        {
            var frameQuery = from DTO.Frame f in container
                             where f.Guid.Equals(frameId)
                             select f;

            return frameQuery.SingleOrDefault();
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
