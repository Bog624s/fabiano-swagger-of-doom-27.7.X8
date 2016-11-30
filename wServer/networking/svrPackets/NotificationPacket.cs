namespace wServer.networking.svrPackets
{
    public class NotificationPacket : ServerPacket
    {
        public int ObjectId { get; set; }
        public string Message { get; set; }
        public ARGB Color { get; set; }

        public override PacketID ID => PacketID.NOTIFICATION;

        public override Packet CreateInstance()
        {
            return new NotificationPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ObjectId = rdr.ReadInt32();
            Message = rdr.ReadUTF();
            Color = ARGB.Read(psr, rdr);
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(ObjectId);
            wtr.WriteUTF(Message);
            Color.Write(psr, wtr);
        }
    }
}