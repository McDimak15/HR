using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class ElementalQuiverAndSnipersSoulCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("ElementalQuiver", out ModItem eq) &&
                entity.type == eq.Type)
                return true;

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.GooglesOn = true;
            modPlayer.StarQuiver = true;

            player.magicQuiver = true;
            player.aggro -= 400;

            if (player.HeldItem.useAmmo == AmmoID.Bullet)
                player.scope = true;

            player.GetDamage(DamageClass.Ranged) += 0.05f;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.arrowDamage *= 1.15f;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

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
            tooltips.Insert(insertAt, new TooltipLine(Mod, "HR_Bonus1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.CrossbowScope")) { OverrideColor = animatedColor });
        }
    }
}
