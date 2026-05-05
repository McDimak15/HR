using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance
{
    public class COJGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem == null || item.ModItem.Mod.Name != "ContinentOfJourney") return;

            switch (item.ModItem.Name)
            {
                case "BatNecklace":
                case "DivineNecklace":
                    player.GetKnockback<SummonDamageClass>() += 2.5f;
                    player.GetDamage<SummonDamageClass>() += 0.12f;
                    player.maxTurrets += 2;
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem == null || item.ModItem.Mod.Name != "ContinentOfJourney") return;

            switch (item.ModItem.Name)
            {
                case "MapleMushroom":
                    foreach (var line in tooltips)
                    {
                        if (line.Text.Contains("1"))
                            line.Text = line.Text.Replace("1", "2");
                    }
                    break;
                
                case "SteelFlask":
                case "PlagueFlask":
                case "ForceBreakFlask":
                case "DivineFireFlask":
                    foreach (var line in tooltips)
                    {
                        if (line.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.ItemOrigTooltip")))
                            line.Text = line.Text.Replace(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.ItemOrigTooltip"), Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.Replace"));
                    }
                    break;

                case "DebtTicket":
                case "WindfallTicket":
                case "WealthTicket":
                case "LuckTicket":
                case "LegacyTicket":
                case "FortuneTicket":
                case "AffluenceTicket":
                case "JackpotTicket":
                    foreach (var line in tooltips)
                    {
                        if (line.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ThrowerTickets.Orig")))
                            line.Text = line.Text.Replace(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ThrowerTickets.Orig"), Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ThrowerTickets.Replace"));
                        if (line.Text.Contains("400%"))
                            line.Text = line.Text.Replace("400%", "50%");
                    }
                    break;

                case "BatNecklace":
                case "DivineNecklace":
                    foreach (var line in tooltips)
                    {
                        if (line.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.OrigTooltip1")))
                        {
                            line.Text = line.Text.Replace(
                                Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.OrigTooltip1"),
                                Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.Replace1")
                            );
                        }
                    }
                    InsertTooltip(tooltips, "StatisBlessing", "BatNecklace.StatisBlessing");
                    break;
            }
        }

        private void InsertTooltip(List<TooltipLine> tooltips, string lineName, string langKey)
        {
            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            int insertAt = tooltips.Count;
            int maxNumber = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        insertAt = i + 1;
                    }
                }
                else if (tooltips[i].Mod == "InfernalEclipseAPI")
                {
                    insertAt = i + 1;
                }
            }

            tooltips.Insert(insertAt, new TooltipLine(Mod, lineName, Language.GetTextValue($"Mods.HomewardRagnarok.ItemTooltips.{langKey}")) { OverrideColor = animatedColor });
        }
    }
}