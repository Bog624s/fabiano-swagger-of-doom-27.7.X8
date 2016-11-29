#region

using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class AOEAckHandler : PacketHandlerBase<AoeAckPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.AOEACK; }
        }

        protected override void HandlePacket(Client client, AoeAckPacket packet)
        {
            //TODO: Implement something.
        }
    }
}