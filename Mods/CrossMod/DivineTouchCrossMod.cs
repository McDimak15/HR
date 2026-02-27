using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class DivineTouchBuffs : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (IsTargetItem(item))
            {
                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
                modPlayer.DivineEmblem = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (IsTooltipTargetItem(item))
            {
                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                    coj.TryFind("DivineTouch", out ModItem divineTouch))
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
                    tooltips.Insert(insertAt, new TooltipLine(Mod, "DivineFireEffect", "Causes melee attacks to inflict the Divine Fire debuff") { OverrideColor = animatedColor });
                }
            }
        }

        private bool IsTargetItem(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                item.type == calamity.Find<ModItem>("ElementalGauntlet")?.Type)
                return true;

            return false;
        }

        private bool IsTooltipTargetItem(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                item.type == calamity.Find<ModItem>("ElementalGauntlet")?.Type)
                return true;

            return false;
        }
    }
}
