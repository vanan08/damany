using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using Emcaster.Sockets;
using Emcaster.Topics;
using System.IO;

namespace RemoteImaging
{
    public class Configuration
    {

        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        private Configuration(string brdcstip)
        {
            this.BroadcastIp = brdcstip;

        }

        public string BroadcastIp { get; set; }

        private void SendHostConfigQuery()
        {

        }

        public void StartDiscovery()
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
            //Cameras = lineCam;
        }

        public void Save()
        {
            XDocument doc = XDocument.Load(fileName);
            doc.Root.RemoveNodes();

            //             foreach (Camera cam in Cameras)
            //             {
            //                 doc.Root.Add(new XElement("cam",
            //                     new XAttribute("ip", cam.IpAddress),
            //                     new XAttribute("name", cam.Name),
            //                     new XAttribute("id", cam.ID)
            //                    ));
            //             }
            // 
            //             doc.Save(Properties.Settings.Default.CamConfigFile);
            //             LoadConfig();
        }


        public Host this[object id]
        {
            get
            {
                try
                {
                    return this.Hosts.First(h => h.Config.StationID.Equals(id));
                }
                catch (System.InvalidOperationException)
                {
                    return null;
                }

            }

        }

        public IList<Host> Hosts
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
                    //                     instance = new Configuration();
                    //                     instance.LoadConfig();
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

        }
    }
}
