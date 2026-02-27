using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.IO;
using Clamity.Content.Bosses.Clamitas;
using Clamity.Content.Bosses.Clamitas.NPCs;
using Clamity.Content.Bosses.WoB;
using Clamity.Content.Bosses.WoB.NPCs;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("Clamity")]
    [ExtendsFromMod("Clamity")]
    public class ClamityMultiplayerPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation) =>
            entity.type == ModContent.ItemType<ClamitasSummoningItem>() ||
            entity.type == ModContent.ItemType<WoBSummonItem>();

        public override bool? UseItem(Item item, Player player)
        {
            if (player.whoAmI == Main.myPlayer && Main.netMode == NetmodeID.MultiplayerClient)
            {
                int bossType = -1;
                if (item.type == ModContent.ItemType<ClamitasSummoningItem>()) bossType = ModContent.NPCType<ClamitasBoss>();
                else if (item.type == ModContent.ItemType<WoBSummonItem>()) bossType = ModContent.NPCType<WallOfBronze>();

                if (bossType != -1)
                {
                    ModPacket packet = Mod.GetPacket();
                    packet.Write((byte)1);
                    packet.Write(bossType);
                    packet.WriteVector2(player.Center);
                    packet.Send();

                    return true; 
                }
            }
            return null;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<ClamitasSummoningItem>()) return !NPC.AnyNPCs(ModContent.NPCType<ClamitasBoss>());
            if (item.type == ModContent.ItemType<WoBSummonItem>()) return !NPC.AnyNPCs(ModContent.NPCType<WallOfBronze>());
            return true;
        }
    }

    public class ClamityNetHandler : ModSystem
    {
        public override void PostSetupContent() { }

        public void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte messageType = reader.ReadByte();
            if (messageType == 1)
            {
                int bossType = reader.ReadInt32();
                Microsoft.Xna.Framework.Vector2 spawnPos = reader.ReadVector2();

                if (Main.netMode == NetmodeID.Server)
                {
                    int npcIndex = NPC.NewNPC(null, (int)spawnPos.X, (int)spawnPos.Y, bossType);
                    if (npcIndex < Main.maxNPCs)
                    {
                        Main.npc[npcIndex].netUpdate = true;
                        var text = Terraria.Localization.NetworkText.FromKey("Announcement.HasAwoken", Main.npc[npcIndex].GetTypeNetName());
                        NetMessage.SendData(25, -1, -1, text, 175, 75, 255);
                    }
                }
            }
        }
    }
}