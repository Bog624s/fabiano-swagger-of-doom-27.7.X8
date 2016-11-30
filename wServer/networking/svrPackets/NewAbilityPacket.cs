namespace wServer.networking.svrPackets
{
    public class NewAbilityPacket : ServerPacket
    {
        public Ability Type { get; set; }

        public override PacketID ID => PacketID.NEW_ABILITY;

        public override Packet CreateInstance()
        {
            return new NewAbilityPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Type = (Ability)rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write((int)Type);
        }
    }
}
