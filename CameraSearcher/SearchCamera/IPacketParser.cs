namespace SearchCamera
{
    public interface IPacketParser<T>
    {
        ParseResult<T> ParsePacket(System.Net.IPEndPoint sender, byte[] buffer);
    }
}