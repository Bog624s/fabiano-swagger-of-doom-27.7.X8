﻿namespace wServer.networking.cliPackets
{
    public class UpdateAckPacket : ClientPacket
    {
        public override PacketID ID => PacketID.UPDATEACK;

        public override Packet CreateInstance()
        {
            return new UpdateAckPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
        }

        protected override void Write(Client psr, NWriter wtr)
        {
        }
    }
}