#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace wServer.networking.cliPackets
{
    public class ActivePetUpdateRequestPacket : ClientPacket
    {
        public const int FOLLOW_PET = 1;
        public const int UNFOLLOW_PET = 2;
        public const int RELEASE_PET = 3;

        public int CommandId { get; set; }
        public uint PetId { get; set; }

		public override PacketID ID => PacketID.ACTIVE_PET_UPDATE_REQUEST;

        public override Packet CreateInstance()
        {
            return new ActivePetUpdateRequestPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            CommandId = rdr.ReadByte();
            PetId = (uint)rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write((byte)CommandId);
            wtr.Write((int)PetId);
        }
    }
}
