using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class CalamityGlobalItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance || item.ModItem == null) return;

            string modName = item.ModItem.Mod.Name;
            string itemName = item.ModItem.Name;
            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();

            // Grand Spectral
            if ((modName == "CalamityMod" && itemName == "RampartofDeities") ||
                (modName == "Clamity" && itemName == "SupremeBarrier" && ServerConfig.Instance.ClamityBalance))
            {
                player.longInvince = true;
                modPlayer.Spectral = true;

                if (Main.myPlayer == player.whoAmI && modPlayer.SpectralSummon > 0)
                {
                    while (modPlayer.SpectralSummon > 0)
                    {
                        int proj = Projectile.NewProjectile(player.GetSource_Accessory(item), player.Center, new Vector2(12f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))), ModContent.ProjectileType<ContinentOfJourney.Projectiles.GrandSpectral>(), 0, 0, player.whoAmI);
                        Main.projectile[proj].netUpdate = true;
                        modPlayer.SpectralSummon -= 1;
                    }
                }

                // Vanguard Breastpiece
                player.buffImmune[39] = true;
                player.buffImmune[67] = true;
                player.buffImmune[69] = true;
                player.buffImmune[70] = true;
                player.buffImmune[ModContent.BuffType<ContinentOfJourney.Buffs.VulnerableBuff>()] = true;
                modPlayer.PinkPenny = true;
                modPlayer.TransactionCertificate = true;

                // Bottled Blue Ice 
                player.buffImmune[24] = true;
                player.buffImmune[323] = true;
            }

            if (modName != "CalamityMod") return;

            switch (itemName)
            {
                case "Nucleogenesis":
                    modPlayer.DivineNecklace = true;
                    break;

                case "StatisCurse":
                    modPlayer.DivineNecklace = true;
                    break;

                case "EtherealTalisman":
                    modPlayer.Negatama = true;
                    if (modPlayer.NegatamaCounter >= 32)
                    {
                        if (Main.myPlayer == player.whoAmI)
                        {
                            int proj = Projectile.NewProjectile(player.GetSource_Accessory(item), player.Center,
                                                                new Vector2(6f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                                                                ModContent.ProjectileType<ContinentOfJourney.Projectiles.Negatama>(), 0, 0, Main.myPlayer);
                            Main.projectile[proj].netUpdate = true;
                        }
                        modPlayer.NegatamaCounter = 0;
                    }
                    player.statManaMax2 += 80;
                    player.manaCost *= 0.98f;
                    player.manaFlower = true;
                    break;

                case "SigilofCalamitas":
                    modPlayer.Negatama = true;
                    if (modPlayer.NegatamaCounter >= 32)
                    {
                        if (Main.myPlayer == player.whoAmI)
                        {
                            int proj = Projectile.NewProjectile(player.GetSource_Accessory(item), player.Center,
                                                                new Vector2(6f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                                                                ModContent.ProjectileType<ContinentOfJourney.Projectiles.Negatama>(), 0, 0, Main.myPlayer);
                            Main.projectile[proj].netUpdate = true;
                        }
                        modPlayer.NegatamaCounter = 0;
                    }
                    break;

                case "AbyssalDivingSuit":
                    Lighting.AddLight((int)(Main.MouseWorld.X / 16f), (int)(Main.MouseWorld.Y / 16f), 2f, 2f, 2f);
                    break;

                case "PlanebreakersPouch":
                    modPlayer.GooglesOn = true;
                    modPlayer.StarQuiver = true;
                    player.magicQuiver = true;
                    player.aggro -= 400;
                    if (player.HeldItem.useAmmo == AmmoID.Bullet) player.scope = true;
                    player.GetDamage(DamageClass.Ranged) += 0.05f;
                    player.GetCritChance(DamageClass.Ranged) += 7;
                    player.arrowDamage *= 1.15f;
                    break;

                case "ElementalGauntlet":
                    modPlayer.DivineEmblem = true;
                    break;

                case "SeraphTracers":
                    player.GetCritChance(DamageClass.Generic) += 6;
                    modPlayer.DemolitionistsLuckyClover = true;
                    modPlayer.RocketJump = true;
                    modPlayer.SpecialRocket = 0;
                    modPlayer.RocketCooldown = 480;
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem == null) return;

            string modName = item.ModItem.Mod.Name;
            string itemName = item.ModItem.Name;

            if (modName == "Clamity" && ServerConfig.Instance.ClamityBalance)
            {
                if (itemName == "SupremeBarrier")
                {
                    if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl)) return;
                    InsertTooltip(tooltips, "GSS1", "GrandSpectral");
                    InsertTooltip(tooltips, "HR1", "CommemorativeCoin");
                    InsertTooltip(tooltips, "HR2", "TransactionCertificate");
                }
            }

            if (modName != "CalamityMod" || !ServerConfig.Instance.CalamityBalance) return;

            switch (itemName)
            {
                case "RampartofDeities":
                    if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl)) return;
                    InsertTooltip(tooltips, "GSS1", "GrandSpectral");
                    InsertTooltip(tooltips, "HR1", "CommemorativeCoin");
                    InsertTooltip(tooltips, "HR2", "TransactionCertificate");
                    break;
                case "Nucleogenesis":
                    InsertTooltip(tooltips, "DivineNecklaceBuff1", "DivineNecklace");
                    break;
                case "StatisCurse":
                    InsertTooltip(tooltips, "DivineNecklaceBuff1", "DivineNecklace");
                    break;
                case "SigilofCalamitas":
                    InsertTooltip(tooltips, "HRNegatama", "Negatama");
                    break;
                case "EtherealTalisman":
                    ReplaceTooltipLine(tooltips, "EtherealTalisman", 1);
                    InsertTooltip(tooltips, "HRNegatama", "Negatama");
                    break;
                case "AbyssalDivingSuit":
                    InsertTooltip(tooltips, "HR1", "AbyssCore");
                    break;
                case "PlanebreakersPouch":
                    InsertTooltip(tooltips, "HR_Bonus1", "CrossbowScope");
                    break;
                case "ElementalGauntlet":
                    InsertTooltip(tooltips, "DivineFireEffect", "DivineTouch");
                    break;
                case "SeraphTracers":
                    InsertTooltip(tooltips, "HR1", "Edgewalker.EdgeCrit");
                    InsertTooltip(tooltips, "HR2", "Edgewalker.EdgeRocket");
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

        private void ReplaceTooltipLine(List<TooltipLine> tooltips, string itemKey, int index)
        {
            string origKey = $"Mods.HomewardRagnarok.ItemTooltips.{itemKey}.OrigTooltip{index}";
            string replaceKey = $"Mods.HomewardRagnarok.ItemTooltips.{itemKey}.Replace{index}";

            string originalText = Language.GetTextValue(origKey);
            string replacementText = Language.GetTextValue(replaceKey);

            foreach (var line in tooltips)
            {
                if (line.Text.Contains(originalText))
                {
                    line.Text = line.Text.Replace(originalText, replacementText);
                }
            }
        }
    }
}