using System;
using System.Net;

namespace SearchCamera
{
    class DistributionRequestParser : IPacketParser<CameraInfo>
    {
        public ParseResult<CameraInfo> ParsePacket(System.Net.IPEndPoint sender, byte[] buffer)
        {
            var netOrder = BitConverter.ToUInt16(buffer, 0);

            var packetType = IPAddress.NetworkToHostOrder( (short) netOrder );
            if (packetType == PacketType.DistributionRequest)
            {
                var builder = new System.Text.StringBuilder();
                for (int i = 2; i < 6+2; i++)
                {
                    builder.Append( String.Format("{0:X2}", buffer[i]) );
                    builder.Append(':');
                }

                //remove last ':'
                if (builder.Length > 3)
                {
                    builder.Length -= 1;
                }

                var result = new CameraInfo() {CameraIp = sender.Address.ToString(), Mac = builder.ToString()};
                return new ParseResult<CameraInfo>(true, result);
            }

            return new ParseResult<CameraInfo>(false, null);
            
        }

      
    }
}