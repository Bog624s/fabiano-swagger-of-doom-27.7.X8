namespace wServer.networking.cliPackets
{
    public class QuestRedeemPacket : ClientPacket
    {
        public ObjectSlot Object { get; set; }

        public override PacketID ID => PacketID.QUEST_REDEEM;

        public override Packet CreateInstance()
        {
            return new QuestRedeemPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Object = ObjectSlot.Read(client, rdr);
        }

        protected override void Write(Client client, NWriter wtr)
        {
            Object.Write(client, wtr);
        }
    }
}