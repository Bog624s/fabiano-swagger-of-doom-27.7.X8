namespace wServer.networking.svrPackets
{
    public class DeathPacket : ServerPacket
    {
        public string AccountId { get; set; }
        public int CharId { get; set; }
        public string Killer { get; set; }
        public int ZombieId { get; set; }
        public int ZombieType { get; set; }

        public override PacketID ID => PacketID.DEATH;

        public override Packet CreateInstance()
        {
            return new DeathPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            AccountId = rdr.ReadUTF();
            CharId = rdr.ReadInt32();
            Killer = rdr.ReadUTF();
            ZombieId = rdr.ReadInt32();
            ZombieType = rdr.ReadInt32();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.WriteUTF(AccountId);
            wtr.Write(CharId);
            wtr.WriteUTF(Killer);
            wtr.Write(ZombieId);
            wtr.Write(ZombieType);
        }
    }
}