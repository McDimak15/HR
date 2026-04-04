using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using CalamityMod;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance;

public class COJGlobalItem : GlobalItem
{
    public override bool InstancePerEntity => true;

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        if (item.ModItem == null || item.ModItem.Mod.Name != "ContinentOfJourney") return;

        bool useThoriumBalance = ServerConfig.Instance.ThoriumBalance && ModLoader.HasMod("ThoriumMod");

        string itemName = item.ModItem.Name;
        TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();

        switch (itemName)
        {
            case "ConstructionPDA":
                if (useThoriumBalance)
                {
                    ApplyThoriumEffect(player, "accSteamkeeperWatch", true);
                }
                break;

            case "BatNecklace":
                player.GetKnockback<SummonDamageClass>() += 2.5f;
                player.GetDamage<SummonDamageClass>() += 0.12f;
                player.maxTurrets += 2;
                if (useThoriumBalance)
                {
                    ApplyThoriumEffect(player, "accCrystalScorpion", true);
                    ApplyThoriumEffect(player, "accNecroticSkull", true);
                }
                break;

            case "DivineNecklace":
                player.GetKnockback<SummonDamageClass>() += 2.5f;
                player.GetDamage<SummonDamageClass>() += 0.12f;
                player.maxTurrets += 2;
                if (useThoriumBalance)
                {
                    ApplyThoriumEffect(player, "accCrystalScorpion", true);
                    ApplyThoriumEffect(player, "accNecroticSkull", true);
                }
                break;

            case "NaturalEssence":
                if (useThoriumBalance && hideVisual)
                {
                    ApplyThoriumEffect(player, "hungeringBlossom", true);
                }
                break;

            case "Starflower":
                if (useThoriumBalance && hideVisual)
                {
                    ApplyThoriumEffect(player, "hungeringBlossom", true);
                }
                break;
        }
    }

    private void ApplyThoriumEffect(Player player, string fieldName, bool value)
    {
        if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
        {
            if (thorium.TryFind<ModPlayer>("ThoriumPlayer", out ModPlayer thoriumPlayerInstance))
            {
                ModPlayer playerInstance = player.GetModPlayer(thoriumPlayerInstance);

                var field = playerInstance.GetType().GetField(fieldName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(playerInstance, value);
                }
            }
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        bool useThoriumBalance = ServerConfig.Instance.ThoriumBalance && ModLoader.HasMod("ThoriumMod");

        if (item.ModItem == null || item.ModItem.Mod.Name != "ContinentOfJourney") return;

        string itemName = item.ModItem.Name;

        switch (itemName)
        {
            case "ConstructionPDA":
                if (useThoriumBalance)
                {
                    InsertTooltip(tooltips, "SteamWatch", "SteamkeeperWatch");
                }
                break;

            case "NaturalEssence":
            case "Starflower":
                if (useThoriumBalance)
                {
                    InsertTooltip(tooltips, "HungerBlossom", "HungeringBlossom");
                }
                break;

            case "BatNecklace":
            case "DivineNecklace":
                foreach (var line in tooltips)
                {
                    string originalText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.OrigTooltip1");

                    if (line.Text.Contains(originalText))
                    {
                        line.Text = line.Text.Replace(
                            originalText,
                            Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.Replace1")
                        );
                    }
                }

                InsertTooltip(tooltips, "StatisBlessing", "BatNecklace.StatisBlessing");

                if (useThoriumBalance)
                {
                    InsertTooltip(tooltips, "ThoriumBatNecklace", "BatNecklace.ThoriumBuffs");
                }
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