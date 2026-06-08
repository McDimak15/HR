using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Items;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Buffs;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Accessories.MeleeExpansion;
using HomewardRagnarok.Config;
using HomewardRagnarok.Projectiles;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance
{
    public class OreRetier : ModSystem
    {
        public override void PostSetupContent()
        {
            var finalore = ModContent.GetInstance<TruePearlstone>();
            if (finalore != null)
            {
                finalore.MinPick = 275;
            }
        }
    }

    public class COJGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<Deconstructor>())
            {
                item.pick = 275;
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem == null) return;

            if (item.ModItem.Mod.Name == "ContinentOfJourney")
            {
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
            else if (item.ModItem.Mod.Name == "CalamityBardHealer")
            {
                switch (item.ModItem.Name)
                {
                    case "BloomingSaintessStatue":
                        player.GetModPlayer<TemplatePlayer>().SaviorsHeart = true;
                        break;
                }
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (item.ModItem != null && item.ModItem.Type == ModContent.ItemType<SunsHeart>() && player.GetModPlayer<HomeRagPlayer>().hasSunsHeartFlightUpgrade && ServerConfig.Instance.PermanentToAccessories == true)
            {
                return false;
            }
            if (item.ModItem != null && item.ModItem.Type == ModContent.ItemType<EurekaEffect>())
            {
                if (player.HasBuff(ModContent.BuffType<EurekaEffectBuff>()))
                    return false;

                if (player.whoAmI == Main.myPlayer)
                {
                    int portalType = player.altFunctionUse == 2 ? 1 : 0;

                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile p = Main.projectile[i];
                        if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<EurekaPortal>() && p.ai[0] == portalType)
                        {
                            p.Kill();
                        }
                    }
                    Projectile.NewProjectile(player.GetSource_ItemUse(item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<EurekaPortal>(), 0, 0, player.whoAmI, portalType);
                }
                player.itemTime = item.useTime;
                return false;
            }
            return base.CanUseItem(item, player);
        }
        public override bool? UseItem(Item item, Player player)
        {
            if (item.ModItem != null && item.ModItem.Type == ModContent.ItemType<SunsHeart>() && ServerConfig.Instance.PermanentToAccessories == true)
            {
                player.GetModPlayer<HomeRagPlayer>().hasSunsHeartFlightUpgrade = true;
                return true;
            }
            return base.UseItem(item, player);
        }
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.ModItem != null && item.ModItem.Mod.Name != "ContinentOfJourney")
            {
                if (item.TryGetGlobalItem<SpearBadge_GlobalItem>(out var cojGlobal))
                {
                    if (cojGlobal.isSpear == 2)
                    {
                        cojGlobal.isSpear = 1;
                        return false;
                    }
                }
            }
            if (item.ModItem != null && item.ModItem.Type == ModContent.ItemType<EurekaEffect>())
            {
                return true;
            }
            return base.AltFunctionUse(item, player);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem != null && item.ModItem.Mod.Name != "ContinentOfJourney")
            {
                if (item.TryGetGlobalItem<SpearBadge_GlobalItem>(out var cojGlobal))
                {
                    if (cojGlobal.isSpear == 2)
                    {
                        cojGlobal.isSpear = 1;
                    }
                }
                tooltips.RemoveAll(line => line.Mod == "ContinentOfJourney" && line.Name == "CoJSpearDash");
            }

            if (item.ModItem == null) return;

            if (item.ModItem.Mod.Name == "ContinentOfJourney")
            {
                switch (item.ModItem.Name)
                {
                    case "SunsHeart":
                        foreach (var line in tooltips)
                        {
                            string origText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.SunsHeart.OrigTooltip");
                            if (line.Text.Contains(origText))
                            {
                                line.Text = line.Text.Replace(origText, Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.SunsHeart.Replace"));
                            }
                        }
                        break;

                    case "MapleMushroom":
                        foreach (var line in tooltips)
                        {
                            if (line.Text.Contains("1"))
                                line.Text = line.Text.Replace("1", "2");
                        }
                        break;


                    case "EurekaEffect":
                        tooltips.RemoveAll(line => line.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.EurekaEffect.OrigTooltip1")));
                        foreach (var line in tooltips)
                        {
                            string origText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.EurekaEffect.OrigTooltip2");
                            if (line.Text.Contains(origText))
                            {
                                line.Text = line.Text.Replace(origText, Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.EurekaEffect.Replace2"));
                            }
                        }
                        break;

                    case "SteelFlask":
                    case "PlagueFlask":
                    case "ForceBreakFlask":
                    case "DivineFireFlask":
                        foreach (var line in tooltips)
                        {
                            string origText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.ItemOrigTooltip");
                            if (line.Text.Contains(origText))
                            {
                                line.Text = line.Text.Replace(origText, Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.FlasksTooltip.Replace"));
                            }
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
                            string origText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ThrowerTickets.Orig");
                            if (line.Text.Contains(origText))
                            {
                                line.Text = line.Text.Replace(origText, Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ThrowerTickets.Replace"));
                            }
                            if (line.Text.Contains("400%"))
                            {
                                line.Text = line.Text.Replace("400%", "50%");
                            }
                        }
                        break;

                    case "BatNecklace":
                    case "DivineNecklace":
                        foreach (var line in tooltips)
                        {
                            string origText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.OrigTooltip1");
                            if (line.Text.Contains(origText))
                            {
                                line.Text = line.Text.Replace(origText, Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.BatNecklace.Replace1"));
                            }
                        }
                        InsertTooltip(tooltips, "StatisBlessing", "BatNecklace.StatisBlessing");
                        break;

                    case "StarQuiver":
                    case "CrossbowScope":
                        InsertTooltip(tooltips, "StarQuiverEffect", "StarQuiverEffect");
                        break;
                }
            }
            else if (item.ModItem.Mod.Name == "CalamityBardHealer")
            {
                switch (item.ModItem.Name)
                {
                    case "BloomingSaintessStatue":
                        InsertTooltip(tooltips, "SaviorHeart", "SaviorsHeart");
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