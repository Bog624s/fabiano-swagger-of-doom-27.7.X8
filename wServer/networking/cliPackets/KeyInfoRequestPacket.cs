namespace wServer.networking.cliPackets
{
	public class KeyInfoRequestPacket : ClientPacket
	{
		public int ItemType { get; set; }

		public override PacketID ID => PacketID.KEY_INFO_REQUEST;

		public override Packet CreateInstance()
		{
			return new KeyInfoRequestPacket();
		}

		protected override void Read(Client psr, NReader rdr)
		{
			ItemType = rdr.ReadInt32();
		}

		protected override void Write(Client psr, NWriter wtr)
		{
			wtr.Write(ItemType);
		}
	}
}