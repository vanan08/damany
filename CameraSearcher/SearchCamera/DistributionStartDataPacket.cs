namespace SearchCamera
{
    internal class DistributionStartDataPacket : Packet<short>
    {

        public override void AppendData(PacketAssembler assembler)
        {
            assembler.AppendData(this.Data);
        }

    }
}