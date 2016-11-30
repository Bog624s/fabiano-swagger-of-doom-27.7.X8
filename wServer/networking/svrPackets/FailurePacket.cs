namespace wServer.networking.svrPackets
{
    public class FailurePacket : ServerPacket
    {
		public const int INCORRECT_VERSION = 4;
		public const int BAD_KEY = 5;
		public const int INVALID_TELEPORT_TARGET = 6;
		public const int EMAIL_VERIFICATION_NEEDED = 7;

        public int ErrorId { get; set; }
        public string ErrorDescription { get; set; }

        public override PacketID ID => PacketID.FAILURE;

        public override Packet CreateInstance()
        {
            return new FailurePacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ErrorId = rdr.ReadInt32();
            ErrorDescription = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(ErrorId);
            wtr.WriteUTF(ErrorDescription);
        }
    }
}