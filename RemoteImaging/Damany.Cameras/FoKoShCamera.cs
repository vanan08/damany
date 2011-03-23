using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Damany.Cameras
{
    public class FoKoShCamera : IDisposable
    {
        private IntPtr _camHandle;

        private System.IO.BinaryWriter _writer;
        private byte[] _header;
        private DateTime _lastRecordTime;

        private bool _started;
        private bool _disposed;
        private byte[] _buf = new byte[512 * 1024];
        private string _tempFile;

        private BkNetClientNative.StreamReadCallback _streamReadCallback;
        private BkNetClientNative.MessageCallback _messageCallback;


        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                BkNetClientNative.MP4_ClientSetConnectUser(_camHandle, value, Password);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                BkNetClientNative.MP4_ClientSetConnectUser(_camHandle, UserName, value);
            }
        }

        public System.Drawing.Rectangle DisplayPos
        {
            set
            {
                var tagRect = new tagRECT();
                tagRect.left = value.Left;
                tagRect.top = value.Top;
                tagRect.right = value.Right;
                tagRect.bottom = value.Bottom;

                BkNetClientNative.MP4_ClientSetDisPlayPos(_camHandle, ref tagRect);
            }
        }

        public string Ip { get; set; }
        public int Port { get; set; }
        public string SaveTo { get; set; }
        public int StreamId { get; set; }

        public FoKoShCamera(IntPtr hWnd)
        {
            _camHandle = BkNetClientNative.MP4_ClientInit(hWnd, 0xa0000, 0x200, 0);

            _messageCallback = MessageCallback;
            _streamReadCallback = StreamReadCallback;

            BkNetClientNative.MP4_ClientRegisterErrorCallBack(_camHandle, _messageCallback, IntPtr.Zero);
            BkNetClientNative.MP4_ClientRegisterStreamCallBack(_camHandle, _streamReadCallback, IntPtr.Zero);

            _tempFile = Path.GetTempFileName() + ".jpg";

            GC.KeepAlive(_messageCallback);
            GC.KeepAlive(_streamReadCallback);
        }

        ~FoKoShCamera()
        {
            Dispose(false);
        }

        public void Start()
        {
            BkNetClientNative.MP4_ClientConnectEx(_camHandle, Ip, (uint)Port, 0, (uint) StreamId, 0);
            _started = true;
        }

        public void StartRecord()
        {
            if (!Directory.Exists(SaveTo))
            {
                throw new InvalidOperationException("SaveTo must be set");
            }

            BkNetClientNative.MP4_ClientStartCapture(_camHandle);
        }

        public void StopRecord()
        {
            BkNetClientNative.MP4_ClientStopCapture(_camHandle);
        }

        public System.Drawing.Image CaptureImage()
        {
            if (!_started)
            {
                throw new InvalidOperationException("Start() must be called before calling CaptureImage()");
            }

            BkNetClientNative.MP4_ClientCapturePicturefile(_camHandle, _tempFile);
            return AForge.Imaging.Image.FromFile(_tempFile);
        }


        public void MessageCallback(System.IntPtr hClient, uint dwCode, System.IntPtr context)
        {

        }


        public int StreamReadCallback(System.IntPtr hClient, System.IntPtr context)
        {
            int type = 0;
            while (true)
            {
                var len = BkNetClientNative.MP4_ClientReadStreamData(_camHandle, _buf, _buf.Length, ref type);
                if (len <= 0) break;

                //视频文件头
                if (type == 0x80)
                {
                    if (_header == null)
                    {
                        _header = new byte[len];
                        Array.Copy(_buf, 0, _header, 0, len);
                    }
                }

                if (_header != null)
                {
                    var now = DateTime.Now;
                    if (now.Minute != _lastRecordTime.Minute)
                    {
                        DisposeWriter();
                        var file = now.ToShortTimeString().Replace(":", "-") + ".avi";
                        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
                        _writer = new BinaryWriter(File.OpenWrite(path));
                        _writer.Write(_header);
                        _lastRecordTime = now;
                    }
                }

                if (_writer != null && len > 0 && type != 0x80)
                {
                    _writer.Write(_buf, 0, len);
                }
            }

            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeWriter()
        {
            if (_writer != null)
            {
                _writer.Dispose();
                _writer = null;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                BkNetClientNative.MP4_ClientStopCapture(_camHandle);
                BkNetClientNative.MP4_ClientDisConnect(_camHandle);
                BkNetClientNative.MP4_ClientUInit(_camHandle);

                if (disposing)
                {
                    DisposeWriter();
                }

            }

            _disposed = true;
        }

    }
}
