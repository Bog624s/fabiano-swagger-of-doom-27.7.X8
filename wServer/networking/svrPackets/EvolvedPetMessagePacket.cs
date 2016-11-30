namespace wServer.networking.svrPackets
{
    public class EvolvedPetMessagePacket : ServerPacket
    {
        public int PetId { get; set; }
        public int InitialSkin { get; set; }
        public int FinalSkin { get; set; }

        public override PacketID ID => PacketID.EVOLVE_PET;

        public override Packet CreateInstance()
        {
            return new EvolvedPetMessagePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            PetId = rdr.ReadInt32();
            InitialSkin = rdr.ReadInt32();
            FinalSkin = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(PetId);
            wtr.Write(InitialSkin);
            wtr.Write(FinalSkin);
        }
    }
}
