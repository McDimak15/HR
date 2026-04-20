using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ThoriumMod;

namespace HomewardRagnarok.CrossMod
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class ThoriumGlobalItem : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.ThoriumBalance || item.ModItem == null) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();

            if (item.ModItem.Mod.Name == "ThoriumMod")
            {
                if (item.ModItem.Name == "TerrariumDefender")
                {
                    player.buffImmune[24] = true;
                    player.buffImmune[323] = true;
                }
            }
            else if (item.ModItem.Mod.Name == "ContinentOfJourney")
            {
                switch (item.ModItem.Name)
                {
                    case "ConstructionPDA":
                        thoriumPlayer.accSteamkeeperWatch = true;
                        break;

                    case "BatNecklace":
                    case "DivineNecklace":
                        thoriumPlayer.accCrystalScorpion = true;
                        thoriumPlayer.accNecroticSkull = true;
                        break;

                    case "NaturalEssence":
                    case "Starflower":
                        if (hideVisual)
                        {
                            thoriumPlayer.hungeringBlossom = true;
                        }
                        break;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.ThoriumBalance || item.ModItem == null) return;

            if (item.ModItem.Mod.Name == "ContinentOfJourney")
            {
                switch (item.ModItem.Name)
                {
                    case "ConstructionPDA":
                        InsertTooltip(tooltips, "SteamWatch", "SteamkeeperWatch");
                        break;

                    case "NaturalEssence":
                    case "Starflower":
                        InsertTooltip(tooltips, "HungerBlossom", "HungeringBlossom");
                        break;

                    case "BatNecklace":
                    case "DivineNecklace":
                        InsertTooltip(tooltips, "ThoriumBatNecklace", "BatNecklace.ThoriumBuffs");
                        break;
                }
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