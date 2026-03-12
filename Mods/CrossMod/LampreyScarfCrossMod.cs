using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using CalamityMod.Items.Accessories;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class LampreyScarfCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ModContent.ItemType<StatisCurse>() ||
                   item.type == ModContent.ItemType<Nucleogenesis>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            player.dd2Accessory = true;
            player.whipRangeMultiplier += 0.10f;
            player.GetModPlayer<TemplatePlayer>().Lamprey = true;
            player.AddBuff(ModContent.BuffType<ContinentOfJourney.Buffs.LampreyBuff>(), 2);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (item.type == ModContent.ItemType<StatisCurse>() || item.type == ModContent.ItemType<Nucleogenesis>())
            {
                AddLampreyTooltips(tooltips);
            }
        }

        private void AddLampreyTooltips(List<TooltipLine> tooltips)
        {
            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            int maxTooltipIndex = -1;
            int maxNumber = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        maxTooltipIndex = i;
                    }
                }
                else if (tooltips[i].Mod == "InfernalEclipseAPI")
                {
                    maxTooltipIndex = i;
                }
            }
            int insertAt = maxTooltipIndex != -1 ? maxTooltipIndex + 1 : tooltips.Count;
            tooltips.Insert(insertAt, new TooltipLine(Mod, "LampreyScarfBuff1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.LampreyScarf")) { OverrideColor = animatedColor });
        }
    }
}
