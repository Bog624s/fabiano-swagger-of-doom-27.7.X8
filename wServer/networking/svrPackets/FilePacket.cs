namespace wServer.networking.svrPackets
{
    public class FilePacket : ServerPacket
    {
        public string FileName { get; set; }
        public byte[] Bytes { get; set; }

        public override PacketID ID => PacketID.FILE;

        public override Packet CreateInstance()
        {
            return new FilePacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            FileName = rdr.ReadUTF();
            Bytes = new byte[rdr.ReadInt32()];
            Bytes = rdr.ReadBytes(Bytes.Length);
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.WriteUTF(FileName);
            wtr.Write(Bytes.Length);
            wtr.Write(Bytes);
        }
    }
}