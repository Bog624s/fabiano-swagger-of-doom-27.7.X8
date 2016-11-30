namespace wServer.networking.svrPackets
{
    public class GuildResultPacket : ServerPacket
    {
        public bool Success { get; set; }
        public string LineBuilderJSON { get; set; }

        public override PacketID ID => PacketID.GUILDRESULT;

        public override Packet CreateInstance()
        {
            return new GuildResultPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            Success = rdr.ReadBoolean();
            LineBuilderJSON = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(Success);
            wtr.WriteUTF(LineBuilderJSON);
        }
    }
}