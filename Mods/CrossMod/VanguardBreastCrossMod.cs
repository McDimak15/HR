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
    public class AsgardianAegisBuffs : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (item.ModItem == null) return;

            string name = item.ModItem.Name;

            if (name == "AsgardianAegis" || name == "ColossusSoul" || name == "DimensionSoul" ||
                name == "EternitySoul" || name == "SupremeBarrier")
            {
                player.noKnockback = true;
                player.buffImmune[BuffID.Burning] = true;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[BuffID.OnFire3] = true;
                player.buffImmune[BuffID.Weak] = true;
                player.buffImmune[BuffID.Bleeding] = true;
                player.buffImmune[BuffID.Poisoned] = true;
                player.buffImmune[BuffID.BrokenArmor] = true;
                player.buffImmune[BuffID.Confused] = true;
                player.buffImmune[BuffID.Darkness] = true;
                player.buffImmune[BuffID.Silenced] = true;
                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Slow] = true;
                player.buffImmune[BuffID.Cursed] = true;
                player.buffImmune[BuffID.Stoned] = true;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[BuffID.CursedInferno] = true;
                player.buffImmune[BuffID.Ichor] = true;

                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod))
                {
                    int vulnerableBuff = ModContent.BuffType<ContinentOfJourney.Buffs.VulnerableBuff>();
                    player.buffImmune[vulnerableBuff] = true;
                }

                player.GetModPlayer<TemplatePlayer>().PinkPenny = true;
                player.GetModPlayer<TemplatePlayer>().TransactionCertificate = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (item.ModItem == null) return;

            string name = item.ModItem.Name;

            if (name == "AsgardianAegis" || name == "SupremeBarrier" || name == "ColossusSoul")
            {
                if (name == "SupremeBarrier" && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                {
                    return;
                }

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
                tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarok1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ImmunityVulnerable")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarok2", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.CommemorativeCoin")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarok3", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.TransactionCertificate")) { OverrideColor = animatedColor });
            }
        }
    }
}
