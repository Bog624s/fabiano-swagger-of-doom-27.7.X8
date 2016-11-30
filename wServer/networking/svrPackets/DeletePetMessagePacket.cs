namespace wServer.networking.svrPackets
{
    public class DeletePetMessagePacket : ServerPacket
    {
        public int PetId { get; set; }

        public override PacketID ID => PacketID.DELETE_PET;

        public override Packet CreateInstance()
        {
            return new DeletePetMessagePacket();
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