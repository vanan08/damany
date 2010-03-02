using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;

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
        public Thread thread = null;
        //获得在线摄像机 
        public void GetLineCameras()
        {
            List<Camera> lineCam = new List<Camera>();
            List<Camera> trueLineCamera = new List<Camera>();
            XDocument camXMLDoc = XDocument.Load(fileName);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                lineCam.Add(new Camera()
                {
                    ID = id,
                    IpAddress = camElement.Attribute("ip").Value,
                    Name = camElement.Attribute("name").Value,

                });
            }

        }

        public object GetStationID()
        {
            return int.Parse(Properties.Settings.Default.HostId);
        }

        public int GetReservedSpaceinMB()
        {
            return int.Parse(Properties.Settings.Default.ReservedDiskSpaceMB);
        }

    }
}
