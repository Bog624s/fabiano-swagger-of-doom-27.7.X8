﻿namespace wServer.networking.svrPackets
{
    public class AoePacket : ServerPacket
    {
        public Position Position { get; set; }
        public float Radius { get; set; }
        public ushort Damage { get; set; }
        public ConditionEffectIndex Effects { get; set; }
        public float EffectDuration { get; set; }
        public short OriginType { get; set; }

        public override PacketID ID => PacketID.AOE;

        public override Packet CreateInstance()
        {
            return new AoePacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            Position = Position.Read(psr, rdr);
            Radius = rdr.ReadSingle();
            Damage = rdr.ReadUInt16();
            Effects = (ConditionEffectIndex)rdr.ReadByte();
            EffectDuration = rdr.ReadSingle();
            OriginType = rdr.ReadInt16();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            Position.Write(psr, wtr);
            wtr.Write(Radius);
            wtr.Write(Damage);
            wtr.Write((byte)Effects);
            wtr.Write(EffectDuration);
            wtr.Write(OriginType);
        }
    }
}