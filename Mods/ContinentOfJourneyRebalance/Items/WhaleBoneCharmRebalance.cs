using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class ColdWhaleRework : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<ColdBlood>()
                || entity.type == ModContent.ItemType<WhaleBoneCharm>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            // Cold Blood
            if (item.type == ModContent.ItemType<ColdBlood>())
            {
                var homeRagPlayer = player.GetModPlayer<HomeRagPlayer>();
                var cojPlayer = player.GetModPlayer<TemplatePlayer>();

                cojPlayer.ColdBlood = false;
                homeRagPlayer.equippedColdBlood = true;
            }

            // Whale Bone Charm
            if (item.type == ModContent.ItemType<WhaleBoneCharm>())
            {
                bool bossIsAlive = NPC.AnyNPCs(ModContent.NPCType<WorldsEndEverlastingFallingWhale>()
                    /* || NPC.AnyNPCs(ModContent.NPCType<TheOverwatcher>()*/);

                if (!bossIsAlive && player.TryGetModPlayer(out TemplatePlayer cojPlayer))
                {
                    cojPlayer.WhaleBoneCharm = false;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            if (item.type == ModContent.ItemType<ColdBlood>())
            {
                tooltips.RemoveAll(line => line.Mod == "ContinentOfJourney" && line.Name == "CoJWBC");
                InsertTooltip(tooltips, new TooltipLine(Mod, "HRColdBlood",
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ColdBloodRework")));
            }

            if (item.type == ModContent.ItemType<WhaleBoneCharm>())
            {
                InsertTooltip(tooltips, new TooltipLine(Mod, "HRWhaleBoneCharm",
                            Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.WhaleBoneCharmRework")));
            }
        }
        private static void InsertTooltip(List<TooltipLine> tooltips, TooltipLine newLine)
        {
            int lastNativeIndex = tooltips.FindLastIndex(line =>
                line.Mod == "Terraria" || line.Mod == "ContinentOfJourney");

            if (lastNativeIndex == -1)
            {
                tooltips.Add(newLine);
            }
            else
            {
                tooltips.Insert(lastNativeIndex + 1, newLine);
            }
        }
    }
}