using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using HomewardRagnarok;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class AccsReworkGlobalItem : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var homeRagPlayer = player.GetModPlayer<HomeRagPlayer>();
            var cojPlayer = player.GetModPlayer<TemplatePlayer>();

            // Cold Blood
            if (item.type == ModContent.ItemType<ColdBlood>())
            {
                cojPlayer.ColdBlood = false;
                homeRagPlayer.equippedColdBlood = true;
            }

            // Whale Bone Charm
            if (item.type == ModContent.ItemType<WhaleBoneCharm>())
            {
                cojPlayer.WhaleBoneCharm = false;
                homeRagPlayer.equippedWhaleBoneCharm = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<ColdBlood>())
            {
                tooltips.RemoveAll(line => line.Mod == "ContinentOfJourney" && line.Name == "CoJWBC");
                tooltips.Add(new TooltipLine(Mod, "HRColdBlood", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ColdBloodRework")));
            }
            if (item.type == ModContent.ItemType<WhaleBoneCharm>())
            {
                tooltips.RemoveAll(line => line.Mod == "ContinentOfJourney" && line.Name == "CoJWBC");
                tooltips.Add(new TooltipLine(Mod, "HRWhaleBoneCharm", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.WhaleBoneCharmRework")));
            }
        }
    }
}