using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class EdgewalkerCrossMod : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (item.ModItem == null) return;

            string name = item.ModItem.Name;

            if (name == "SeraphTracers")
            {
                player.GetCritChance(DamageClass.Generic) += 6;
                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
                modPlayer.DemolitionistsLuckyClover = true;
                modPlayer.RocketJump = true;
                modPlayer.SpecialRocket = 0;
                modPlayer.RocketCooldown = 480;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (item.ModItem == null) return;

            string name = item.ModItem.Name;

            if (name == "SeraphTracers")
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
                tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarok1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Edgewalker.EdgeCrit")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarok2", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Edgewalker.EdgeRocket")) { OverrideColor = animatedColor });
            }
        }
    }
}
