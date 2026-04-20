using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ContinentOfJourney.Buffs;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance
{
    public class CoJGlobalBuff : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (type == ModContent.BuffType<BattaliansBackupBuff>())
            {
                player.statDefense -= 15;
                player.endurance -= 0.50f;
            }
            if (type == ModContent.BuffType<SolarBurntBuff>())
            {
                player.lifeRegen += 120;
            }
            if (type == ModContent.BuffType<SlimeOceanDrownBuff>())
            {
                player.lifeRegen += 40;
            }
            if (type == ModContent.BuffType<UnexistBuff>())
            {
                player.lifeRegen += 350;
            }
        }

        public override void ModifyBuffText(int type, ref string displayName, ref string tooltip, ref int drawOffset)
        {
            if (type == ModContent.BuffType<GrayRookBuff>())
            {
                tooltip = tooltip.Replace(
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.OrigTooltip1"),
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.Replace1"));

                tooltip = tooltip.Replace(
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.OrigTooltip2"),
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.Replace2"));

                tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.MeleeRebalance");

                if (ModLoader.HasMod("ThoriumMod"))
                {
                    tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.HealerRebalance");
                    tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.BardRebalance");
                }
            }

            if (type == ModContent.BuffType<BattaliansBackupBuff>())
            {
                tooltip = tooltip.Replace("70%", "20%").Replace("30", "15");
            }

            if (type == ModContent.BuffType<Flask_DivineFireBuff>() || 
                type == ModContent.BuffType<Flask_SteelBuff>() || 
                type == ModContent.BuffType<Flask_PlagueBuff>() ||
                type == ModContent.BuffType<Flask_ForceBreakBuff>() )
            {
                tooltip = tooltip.Replace(
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.BuffOrigTooltip"),
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.Replace"));
            }
        }
    }
}