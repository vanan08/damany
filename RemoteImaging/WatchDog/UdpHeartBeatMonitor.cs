using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace WatchDog
{
    public class UdpHeartBeatMonitor : HeartBeatMonitorBase
    {
        private readonly int _portNumber;
        private System.Net.Sockets.UdpClient _udpSocket;

        private DateTime? _lastActive;
        private object _locker;


        private const int CheckInterval = 5000;

        public UdpHeartBeatMonitor(int portNumber)
        {
            _portNumber = portNumber;
            _locker = new object();

            OnStart += MsmqHeartBeatMonitor_OnStart;
        }

        private DateTime? LastActive
        {
            get
            {
                lock (_locker)
                {
                    return _lastActive;
                }

            }
            set
            {
                lock (_locker)
                {
                    _lastActive = value;
                }

            }
        }

        void MsmqHeartBeatMonitor_OnStart(object sender, EventArgs e)
        {
            var ipendpoint = new IPEndPoint(IPAddress.Any, _portNumber);
            _udpSocket = new UdpClient(ipendpoint);
            _udpSocket.BeginReceive(Receive, null);

        }

        protected override void Work(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                //程序是否曾经被启动过
                if (LastActive != null)
                {
                    if (DateTime.Now - LastActive > TimeToReport)
                    {
                        InvokeHeartBeatStopped(EventArgs.Empty);
                    }
                }

                Thread.Sleep(CheckInterval);
            }
        }

        void Receive(IAsyncResult ar)
        {
            try
            {
                var endpoint = new IPEndPoint(IPAddress.Any, 0);
                var buffer = _udpSocket.EndReceive(ar, ref endpoint);

                System.Diagnostics.Debug.WriteLine(buffer.Length);

                LastActive = DateTime.Now;

                Thread.Sleep(CheckInterval);
                _udpSocket.BeginReceive(Receive, null);

            }
            catch
            {
            }

        }

    }
}