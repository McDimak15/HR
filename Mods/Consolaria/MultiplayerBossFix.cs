using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Consolaria.Content.Items.Summons;
using Consolaria.Content.NPCs.Bosses.Turkor;
using Consolaria.Content.NPCs.Bosses.Ocram;
using Consolaria.Content.Buffs;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("Consolaria")]
    [ExtendsFromMod("Consolaria")]
    public class ConsolariaMultiplayerPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<CursedStuffing>() ||
                   entity.type == ModContent.ItemType<SuspiciousLookingSkull>();
        }

        public override bool? UseItem(Item item, Player player)
        {
            if (player.whoAmI == Main.myPlayer && Main.netMode == NetmodeID.MultiplayerClient)
            {
                int bossType = -1;

                if (item.type == ModContent.ItemType<CursedStuffing>())
                {
                    bossType = ModContent.NPCType<TurkortheUngrateful>();

                    int turkeyBuff = ModContent.BuffType<PetTurkey>();
                    player.ClearBuff(turkeyBuff);
                }
                else if (item.type == ModContent.ItemType<SuspiciousLookingSkull>())
                {
                    bossType = ModContent.NPCType<Ocram>();
                }
                if (bossType != -1)
                {
                    NetMessage.SendData(61, -1, -1, null, player.whoAmI, (float)bossType);
                    return true;
                }
            }
            return null;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<CursedStuffing>())
            {
                return player.HasBuff(ModContent.BuffType<PetTurkey>()) && !NPC.AnyNPCs(ModContent.NPCType<TurkortheUngrateful>());
            }

            if (item.type == ModContent.ItemType<SuspiciousLookingSkull>())
            {
                return !Main.IsItDay() && !NPC.AnyNPCs(ModContent.NPCType<Ocram>());
            }

            return base.CanUseItem(item, player);
        }
    }
}