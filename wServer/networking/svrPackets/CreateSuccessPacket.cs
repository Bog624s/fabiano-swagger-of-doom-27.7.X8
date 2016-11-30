namespace wServer.networking.svrPackets
{
    public class CreateSuccessPacket : ServerPacket
    {
        public int ObjectID { get; set; }
        public int CharacterID { get; set; }

        public override PacketID ID => PacketID.CREATE_SUCCESS;

        public override Packet CreateInstance()
        {
            return new CreateSuccessPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ObjectID = rdr.ReadInt32();
            CharacterID = rdr.ReadInt32();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(ObjectID);
            wtr.Write(CharacterID);
        }
    }
}