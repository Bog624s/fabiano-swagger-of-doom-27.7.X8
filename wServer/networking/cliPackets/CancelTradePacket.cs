namespace wServer.networking.cliPackets
{
    public class CancelTradePacket : ClientPacket
    {
		public override PacketID ID => PacketID.CANCELTRADE;

        public override Packet CreateInstance()
        {
            return new CancelTradePacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
        }

        protected override void Write(Client psr, NWriter wtr)
        {
        }
    }
}