namespace wServer.networking.svrPackets
{
    public class ActivePetUpdatePacket : ServerPacket
    {
        public int PetId { get; set; }

        public override PacketID ID => PacketID.ACTIVEPETUPDATE;

        public override Packet CreateInstance()
        {
            return new ActivePetUpdatePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            PetId = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(PetId);
        }
    }
}