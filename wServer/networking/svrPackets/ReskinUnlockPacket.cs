namespace wServer.networking.svrPackets
{
    public class ReskinUnlockPacket : ServerPacket
    {
        public int SkinID { get; set; }

        public override PacketID ID => PacketID.RESKIN_UNLOCK;

        public override Packet CreateInstance()
        {
            return new ReskinUnlockPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            SkinID = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(SkinID);
        }
    }
}
