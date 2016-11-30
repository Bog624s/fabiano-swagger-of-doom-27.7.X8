namespace wServer.networking.svrPackets
{
    public class PetUpgradeRequestPacket : ServerPacket
    {
		public const int GOLD_PAYMENT_TYPE = 0;
		public const int FAME_PAYMENT_TYPE = 1;

        public int Type { get; set; }

        public override PacketID ID => PacketID.PETYARDUPDATE;

        public override Packet CreateInstance()
        {
            return new PetUpgradeRequestPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Type = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Type);
        }
    }
}