using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CarDetectorTester
{
    public class StreamMock : System.IO.Stream
    {
        private byte[] _buffer;

        public StreamMock()
        {
            _buffer = new byte[]
                          {
                              0x55, 0xAA, 0x12, 0x02,
                              0x01, 0xE4, 0x04, 0xC9, 0x00, 0x10, 0x55, 0xD8, 0x00, 0x13, 0x50, 0x9A, 0x00, 0x0E, 0x0F, 0x85,
                              0xA2
                          };
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        private int idx = 0;

        public override int Read(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[offset + i] = _buffer[idx++%_buffer.Length];
            }

            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {

        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
