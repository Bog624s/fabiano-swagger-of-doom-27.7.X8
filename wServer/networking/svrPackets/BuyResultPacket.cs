namespace wServer.networking.svrPackets
{
    public class BuyResultPacket : ServerPacket
    {
		public const int UNKNOWN_ERROR = -1;
		public const int SUCCESS = 0;
		public const int INVALID_CHARACTER = 1;
		public const int ITEM_NOT_FOUND = 2;
		public const int NOT_ENOUGH_GOLD = 3;
		public const int INVENTORY_FULL = 4;
		public const int TOO_LOW_RANK = 5;
		public const int NOT_ENOUGH_FAME = 6;
		public const int PET_FEED_SUCCESS = 7;

        public int Result { get; set; }
        public string Message { get; set; }

        public override PacketID ID => PacketID.BUYRESULT;

        public override Packet CreateInstance()
        {
            return new BuyResultPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            Result = rdr.ReadInt32();
            Message = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(Result);
            wtr.WriteUTF(Message);
        }
    }
}