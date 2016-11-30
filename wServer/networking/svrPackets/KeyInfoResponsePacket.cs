namespace wServer.networking.svrPackets
{
	public class KeyInfoResponsePacket : ServerPacket
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Creator { get; set; }

		public override PacketID ID => PacketID.KEY_INFO_RESPONSE;

		public override Packet CreateInstance()
		{
			return new KeyInfoResponsePacket();
		}

		protected override void Read(Client psr, NReader rdr)
		{
			Name = rdr.ReadUTF();
			Description = rdr.ReadUTF();
			Creator = rdr.ReadUTF();
		}

		protected override void Write(Client psr, NWriter wtr)
		{
			wtr.WriteUTF(Name);
			wtr.WriteUTF(Description);
			wtr.WriteUTF(Creator);
		}
	}
}