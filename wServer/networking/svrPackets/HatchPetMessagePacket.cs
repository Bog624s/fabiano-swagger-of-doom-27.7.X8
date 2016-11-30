namespace wServer.networking.svrPackets
{
    public class HatchPetMessagePacket : ServerPacket
    {
        public string PetName { get; set; }
        public int PetSkin { get; set; }

        public override PacketID ID => PacketID.HATCH_PET;

        public override Packet CreateInstance()
        {
            return new HatchPetMessagePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            PetName = rdr.ReadUTF();
            PetSkin = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.WriteUTF(PetName);
            wtr.Write(PetSkin);
        }
    }
}
