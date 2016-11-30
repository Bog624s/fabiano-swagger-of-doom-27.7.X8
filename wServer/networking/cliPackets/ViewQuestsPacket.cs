namespace wServer.networking.cliPackets
{
    public class ViewQuestsPacket : ClientPacket
    {
        public override PacketID ID => PacketID.QUEST_FETCH_ASK;

        public override Packet CreateInstance()
        {
            return new ViewQuestsPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
        }

        protected override void Write(Client client, NWriter wtr)
        {            
        }
    }
}
