using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;
using wServer.realm.entities;
using wServer.realm.worlds;

namespace wServer.networking.handlers
{
    internal class PetCommandHandler : PacketHandlerBase<ActivePetUpdateRequestPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.ACTIVE_PET_UPDATE_REQUEST; }
        }

        protected override void HandlePacket(Client client, ActivePetUpdateRequestPacket packet)
        {
            client.Manager.Logic.AddPendingAction(t =>
            {
                client.Manager.Database.AddDatabaseOperation(db =>
                {
                    if (!(client.Player.Owner is PetYard)) return;
                    var pet = ((PetYard)client.Player.Owner).FindPetById((int)packet.PetId);

                    switch (packet.CommandId)
                    {
                        case ActivePetUpdateRequestPacket.FOLLOW_PET:
                            if (client.Player.Pet != null) client.Player.Pet.PlayerOwner = null;
                            client.Player.Pet = pet;
                            pet.PlayerOwner = client.Player;
                            var cmd = db.CreateQuery();
                            cmd.CommandText = "UPDATE characters SET petId=@petId WHERE charId=@charId AND accId=@accId;";
                            cmd.Parameters.AddWithValue("@charId", client.Character.CharacterId);
                            cmd.Parameters.AddWithValue("@accId", client.Player.AccountId);
                            cmd.Parameters.AddWithValue("@petId", pet.PetId);
                            cmd.ExecuteNonQuery();
                            client.SendPacket(new ActivePetUpdatePacket
                            {
                                PetId = pet.PetId
                            });
                            client.Player.SaveToCharacter();
                            break;
                        case ActivePetUpdateRequestPacket.UNFOLLOW_PET:
                            cmd = db.CreateQuery();
                            cmd.CommandText = "UPDATE characters SET petId=-1 WHERE charId=@charId AND accId=@accId;";
                            cmd.Parameters.AddWithValue("@charId", client.Character.CharacterId);
                            cmd.Parameters.AddWithValue("@accId", client.Player.AccountId);
                            cmd.ExecuteNonQuery();
                            client.Player.Pet.PlayerOwner = null;
                            client.Player.Pet = null;
                            client.SendPacket(new ActivePetUpdatePacket
                            {
                                PetId = -1
                            });
                            break;
                        case ActivePetUpdateRequestPacket.RELEASE_PET:
                            cmd = db.CreateQuery();
                            cmd.CommandText = "DELETE FROM pets WHERE petId=@petId AND accId=@accId;";
                            cmd.Parameters.AddWithValue("@accId", client.Player.AccountId);
                            cmd.Parameters.AddWithValue("@petId", pet.PetId);
                            cmd.ExecuteNonQuery();
                            client.SendPacket(new DeletePetMessagePacket
                            {
                                PetId = pet.PetId
                            });
                            client.Player.SaveToCharacter();
                            client.Player.Owner.LeaveWorld(pet);
                            if (client.Player.Pet != null)
                                client.Player.Pet.PlayerOwner = client.Player;
                            break;
                        default:
                            client.Player.SendError("Unknown CommandId");
                            break;
                    }
                });
            });
        }
    }
}
