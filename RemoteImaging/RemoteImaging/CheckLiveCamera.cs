using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Timers;
using System.Net;
using System.Net.Sockets;

namespace RemoteImaging
{
    public class CheckLiveCamera
    {
        Socket sock = null;
        IPEndPoint iep1 = null;
        IPEndPoint iep = null;
        public CheckLiveCamera(List<Camera> tt, Configuration configd)
        {
            listCamera = new List<Camera>();
            listCamera = tt;

            trueCamera = new Camera[listCamera.Count];
            listCamera.CopyTo(trueCamera);

            config = new Configuration();
            config = configd;

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            iep1 = new IPEndPoint(IPAddress.Broadcast, 10001);//255.255.255.255
            iep = new IPEndPoint(IPAddress.Any, 10000);
            //sock.Bind(iep);
        }

        private Camera[] trueCamera;
        private List<Camera> listCamera;

        Configuration config = null;

        //发送
        public void send()
        {
            byte[] buffer = new byte[512];

            buffer[0] = 0x40;
            buffer[1] = 0x31;
            buffer[2] = 0x00;
            buffer[3] = 0x00;
            buffer[4] = 0x00;
            buffer[5] = 0x00;
            buffer[6] = 0x00;
            buffer[7] = 0x00;
            buffer[8] = 0x00;
            buffer[9] = 0x01;
            buffer[10] = 0x00;
            buffer[11] = 0x00;
            buffer[12] = 0x00;
            buffer[13] = 0x01;
            buffer[14] = 0x00;
            buffer[15] = 0x01;

            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            int ByteNumber = 0;

            try
            {
                ByteNumber = sock.SendTo(buffer, iep1);
            }
            catch (Exception ex)
            {
                return;
            }
        }

        //接收
        public byte[] recive()
        {
            EndPoint ep = (EndPoint)iep;
            byte[] resBuffer = new byte[512];

            try
            {
                int resInt = sock.ReceiveFrom(resBuffer, ref ep);
                return resBuffer;
            }
            catch (Exception ex)
            {
                return resBuffer;
            }
        }

        private void elapsedMethod(object sender, ElapsedEventArgs eargs)
        {
            send();
        }

        public void Run(object obj)
        {
            System.Timers.Timer time = new System.Timers.Timer();
            time.Elapsed += new ElapsedEventHandler(elapsedMethod);
            time.Interval = 2000;
            time.Enabled = true;
            string strMAC = "";
            string strIP = "";
            byte[] resBuffer = new byte[512];
            int count = 0;
            while (true)
            {
                resBuffer = recive();

                strMAC = string.Format("{0:x000}.{1:x000}.{2:x000}.{3:x000}.{4:x000}.{5:x000}", resBuffer[2].ToString("X"), resBuffer[3].ToString("X"), resBuffer[4].ToString("X"), resBuffer[5].ToString("X"), resBuffer[6].ToString("X"), resBuffer[7].ToString("X"));
                strIP = string.Format("{0:x2}.{1:x000}.{2:x000}.{3:x000}", resBuffer[32].ToString(), resBuffer[33].ToString(), resBuffer[34].ToString(), resBuffer[35].ToString());
                for (int i = 0; i < listCamera.Count; i++)
                {
                    Camera cam = new Camera();
                    cam = listCamera[i];
                    if (cam.Mac.Equals(strMAC))
                    {
                        Camera resCam = new Camera();
                        resCam.ID = cam.ID;
                        resCam.IpAddress = strIP;
                        resCam.Name = cam.Name;
                        resCam.Status = true;
                        resCam.Mac = cam.Mac;
                        trueCamera[i] = resCam;
                        config.Cameras = trueCamera.ToList();
                    }
                }
            }
        }
    }
}