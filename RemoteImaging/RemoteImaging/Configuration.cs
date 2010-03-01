using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace RemoteImaging
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        public Configuration()
        {
            List<Camera> lineCam = new List<Camera>();
            XDocument camXMLDoc = XDocument.Load(fileName);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                lineCam.Add(new Camera()
                {
                    ID = id,
                    IpAddress = camElement.Attribute("ip").Value,
                    Name = camElement.Attribute("name").Value
                });
            }
            Cameras = lineCam;
        }

        public void Save()
        {
            XDocument doc = XDocument.Load(fileName);
            doc.Root.RemoveNodes();

            foreach (Camera cam in Cameras)
            {
                doc.Root.Add(new XElement("cam",
                    new XAttribute("ip", cam.IpAddress),
                    new XAttribute("name", cam.Name),
                    new XAttribute("id", cam.ID)));
            }

            doc.Save(Properties.Settings.Default.CamConfigFile);


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


        public Camera FindCameraByID(int ID)
        {
            try
            {
                return this.Cameras.First(c => c.ID == ID);
            }
            catch (System.InvalidOperationException)
            {
                return null;

            }
        }

        public IList<Camera> Cameras
        {
            get;
            set;

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
