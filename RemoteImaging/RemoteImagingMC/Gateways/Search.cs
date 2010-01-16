using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Gateways
{
    public class Search : GateWayBase<RemoteControlService.ISearch>
    {

        public Search() : base(ServiceProxy.ProxyFactory.CreateSearchProxy){}

        static Search instance;

        public static Search Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Search();
                }

                return instance;
            }

        }

        public string[] SearchFaces(System.Net.IPAddress ip, int cameraID, DateTime from, DateTime to)
        {
            EnsureProxyCreated(ip);

            return proxies[ip].SearchFaces(cameraID, from, to);
        }

        public RemoteControlService.ImagePair GetFace(System.Net.IPAddress ip, string path)
        {
            EnsureProxyCreated(ip);

            return proxies[ip].GetFace(path);
        }

        public RemoteControlService.Video[] SearchVideos(System.Net.IPAddress ip, int cameraID, DateTime from, DateTime to)
        {
            EnsureProxyCreated(ip);

            return proxies[ip].SearchVideos(cameraID, from, to);
        }

        public RemoteControlService.ImagePair[] FacesCapturedAt(System.Net.IPAddress ip, int cameraID, DateTime time)
        {
            EnsureProxyCreated(ip);

            return proxies[ip].FacesCapturedAt(cameraID, time);
        }

        public string VideoFilePathRecordedAt(System.Net.IPAddress ip, DateTime time, int camID)
        {
            EnsureProxyCreated(ip);

            return proxies[ip].VideoFilePathRecordedAt(time, camID);
        }

        public System.IO.Stream DownloadFile(System.Net.IPAddress ip, string file, string uselessString)
        {
            EnsureProxyCreated(ip);
            return proxies[ip].DownloadFile(file, uselessString);
        }

        public System.Drawing.Bitmap DownloadBitmap(System.Net.IPAddress ip, string file)
        {
            EnsureProxyCreated(ip);
            return proxies[ip].DownloadBitmap(file);
        }

    }
}
