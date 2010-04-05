using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Damany.PC.Domain;

namespace RemoteImaging
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        public Configuration()
        {
           
        }

       

        private FaceSearchWrapper.FaceSearchConfiguration faceSearchConfig;

        public FaceSearchWrapper.FaceSearchConfiguration FaceSearchConfig
        {
            get
            {
                if (this.faceSearchConfig == null)
                {
                    using (IObjectContainer db = OpenDb())
                    {
                        this.faceSearchConfig =
                            db.Query<FaceSearchWrapper.FaceSearchConfiguration>().FirstOrDefault();
                    }
                }

                return this.faceSearchConfig;
            }

        }

       
        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configuration();
                }
                return instance;
            }
        }

        public string fileName = Properties.Settings.Default.CamConfigFile;

        private static Configuration instance;

        public object GetStationID()
        {
            return int.Parse(Properties.Settings.Default.HostId);
        }

        public int GetReservedSpaceinMB()
        {
            return int.Parse(Properties.Settings.Default.ReservedDiskSpaceMB);
        }

        private Db4objects.Db4o.IObjectContainer OpenDb()
        {
            return Db4objects.Db4o.Db4oFactory.OpenFile("config.db4o");
        }

    }
}
