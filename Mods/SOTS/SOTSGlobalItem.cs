using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class SOTSGlobalItem : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem == null || item.ModItem.Mod.Name != "SOTS") return;

            string name = item.ModItem.Name;

            if (name == "BulwarkOfTheAncients")
            {
                player.buffImmune[24] = true;
                player.buffImmune[323] = true;
            }

            if (name == "FortressGenerator")
            {
                player.maxTurrets += 1;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem == null) return;

            string modName = item.ModItem.Mod.Name;
            string itemName = item.ModItem.Name;

            if (modName != "SOTS" || !ServerConfig.Instance.SOTSBalance) return;

            switch (itemName)
            {
                case "FortressGenerator":
                    InsertTooltip(tooltips, "PDA1", "ConstructionPDA");
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