using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.EPolice.Networking
{
    using Common.Util;

    class PacketSplitter : Parsers.ParserBase
    {
        public event EventHandler<MiscUtil.EventArgs<Packets.Raw>> PacketCaptured;
        public event EventHandler<MiscUtil.EventArgs<Exception>> ExceptionOccurred;

        public PacketSplitter(System.IO.Stream stream)
        {
            this.reader = new MiscUtil.IO.EndianBinaryReader(BitConverter, stream, Encoding);
        }

        public void Start()
        {
            if (this.started)
            {
                throw new InvalidOperationException("Already started");
            }

            exit = false;
            worker = new System.Threading.Thread(this.StartInternal);
            worker.IsBackground = true;
            worker.Start();
            started = true;

        }

        public void Stop()
        {
            if (!this.started)
            {
                throw new InvalidOperationException("Not started yet");
            }

            this.exit = false;
            if (this.worker.IsAlive)
            {
                worker.Join();
            }
            this.started = false;
        }


        private void StartInternal()
        {
            while (!this.exit)
            {
                try
                {
                    var type = reader.ReadInt32();

                    var bufferLen = reader.ReadInt32();
                    var buffer = reader.ReadBytes(bufferLen);

                    var packet = new Packets.Raw();
                    packet.Type = (uint)type;
                    packet.Buffer = buffer;

                    var args = new MiscUtil.EventArgs<Packets.Raw>(packet);

                    this.PacketCaptured.Raise(this, args);

                }
                catch (System.Exception ex)
                {
                    this.ExceptionOccurred.Raise(this, new MiscUtil.EventArgs<Exception>(ex));
                }
            }

        }



        private System.Threading.Thread worker;
        MiscUtil.IO.EndianBinaryReader reader;
        bool started;
        bool exit;
    }
}
