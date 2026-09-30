using ContinentOfJourney.Items.Armor;
using HomewardRagnarok.Config;
using HomewardRagnarok.Config;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Rebalance
{
    public class ArmorRebalance : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!ServerConfig.Instance.ArmorBalancing)
                return;

            // Aurora Set
            if (item.type == ModContent.ItemType<AuroraHeadwear>())
                item.defense = 20; // Aurora Headwear 
            if (item.type == ModContent.ItemType<AuroraRobe>())
                item.defense = 24; // Aurora Breastplate
            if (item.type == ModContent.ItemType<AuroraBoots>())
                item.defense = 18; // Aurora Leggings

            // Sunlight Set
            if (item.type == ModContent.ItemType<SunlightHelmet>())
                item.defense = 20; // Sun God Helmet
            if (item.type == ModContent.ItemType<SunlightBreastplate>())
                item.defense = 26; // Sun God Breastplate
            if (item.type == ModContent.ItemType<SunlightLegging>())
                item.defense = 24; // Sun God Leggings

            // Heliology Set
            if (item.type == ModContent.ItemType<HeliologyMask>())
                item.defense = 22; // Six-star General Mask
            if (item.type == ModContent.ItemType<HeliologyHelmet>())
                item.defense = 22; // Five-star General Hat
            if (item.type == ModContent.ItemType<HeliologyPlate>())
                item.defense = 26; // Five-star General Coat
            if (item.type == ModContent.ItemType<HeliologyLeggings>())
                item.defense = 20; // Five-star General Trousers

            // Perpetual (Chrono)
            if (item.type == ModContent.ItemType<PerpetualHelmet>())
                item.defense = 16; // Chrono Helmet
            if (item.type == ModContent.ItemType<PerpetualPlate>())
                item.defense = 30; // Chrono Breastplate
            if (item.type == ModContent.ItemType<PerpetualLeggings>())
                item.defense = 18; // Chrono Leggings

            // Biological
            if (item.type == ModContent.ItemType<BiologicalHelmet>())
                item.defense = 30;
            if (item.type == ModContent.ItemType<BiologicalBreastplate>())
                item.defense = 38;
            if (item.type == ModContent.ItemType<BiologicalLeggings>())
                item.defense = 28;

            // Reflector
            if (item.type == ModContent.ItemType<ReflectorHelmet>())
                item.defense = 56;
            if (item.type == ModContent.ItemType<ReflectorBreastplate>())
                item.defense = 52; // Reflector Bodysuit

            // Watchman Set
            if (item.type == ModContent.ItemType<WatchmanHat>())
                item.defense = 18;
            if (item.type == ModContent.ItemType<WatchmanShirt>())
                item.defense = 32;
            if (item.type == ModContent.ItemType<WatchmanDress>())
                item.defense = 20;

            // Forest Set
            if (item.type == ModContent.ItemType<ForestHelmet>())
                item.defense = 24;
            if (item.type == ModContent.ItemType<ForestBreastplate>())
                item.defense = 32;
            if (item.type == ModContent.ItemType<ForestLeggings>())
                item.defense = 22;

            // Equilibrium Set
            if (item.type == ModContent.ItemType<EquilibriumBreastplate>())
                item.defense = 54; // Equilibrium Bodysuit
            if (item.type == ModContent.ItemType<EquilibriumLeggings>())
                item.defense = 48; // Equilibrium Stockings
        }
        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ModContent.ItemType<BiologicalHelmet>())
            {
                player.GetDamage(DamageClass.Ranged) = player.GetDamage(DamageClass.Ranged) / 1.2f * 1.14f;
            }
            if (item.type == ModContent.ItemType<BiologicalBreastplate>())
            {
                player.GetDamage(DamageClass.Ranged) = player.GetDamage(DamageClass.Ranged) / 1.24f * 1.15f;
                player.GetCritChance(DamageClass.Ranged) -= 14;
            }
            if (item.type == ModContent.ItemType<BiologicalLeggings>())
            {
                player.maxRunSpeed = player.maxRunSpeed / 1.25f * 1.15f;
                player.runAcceleration = player.runAcceleration / 1.25f * 1.15f;
            }
            if (item.type == ModContent.ItemType<HeliologyMask>())
            {
                player.maxTurrets += 1;
                player.maxMinions += 4;
            }

            if (item.type == ModContent.ItemType<ForestHelmet>())
            {
                player.GetDamage(DamageClass.Ranged) = player.GetDamage(DamageClass.Ranged) / 1.3f * 1.08f;
                player.GetCritChance(DamageClass.Ranged) -= 7;
            }
            if (item.type == ModContent.ItemType<ForestBreastplate>())
            {
                player.GetDamage(DamageClass.Ranged) = player.GetDamage(DamageClass.Ranged) / 1.27f * 1.1f;
                player.GetCritChance(DamageClass.Ranged) -= 19;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            foreach (var line in tooltips)
            {
                if (item.type == ModContent.ItemType<BiologicalHelmet>())
                {
                    if (line.Text.Contains("20%"))
                        line.Text = line.Text.Replace("20%", "14%");
                }
                else if (item.type == ModContent.ItemType<BiologicalLeggings>())
                {
                    if (line.Text.Contains("25%"))
                        line.Text = line.Text.Replace("25%", "15%");
                }
                else if (item.type == ModContent.ItemType<BiologicalBreastplate>())
                {
                    string origText = Language.GetTextValue("Mods.HomewardRagnarok.ArmorTooltips.BiologicalBreastplate.Orig");
                    string newText = Language.GetTextValue("Mods.HomewardRagnarok.ArmorTooltips.BiologicalBreastplate.Replace");

                    if (line.Text.Contains(origText))
                    {
                        line.Text = line.Text.Replace(origText, newText);
                        break;
                    }
                }
                if (item.type == ModContent.ItemType<ForestHelmet>())
                {
                    if (line.Text.Contains("30%"))
                        line.Text = line.Text.Replace("30%", "8%");
                    if (line.Text.Contains("12%"))
                        line.Text = line.Text.Replace("12%", "5%");
                }
                else if (item.type == ModContent.ItemType<ForestBreastplate>())
                {
                    string origText = Language.GetTextValue("Mods.HomewardRagnarok.ArmorTooltips.ForestBreastplate.Orig");
                    string newText = Language.GetTextValue("Mods.HomewardRagnarok.ArmorTooltips.ForestBreastplate.Replace");

                    if (line.Text.Contains(origText))
                    {
                        line.Text = line.Text.Replace(origText, newText);
                        break;
                    }
                }
            }
            if (item.type == ModContent.ItemType<HeliologyMask>())
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Text.Contains('1'))
                        line.Text = line.Text.Replace("1", "2");
                }
                string removeText = Language.GetTextValue("Mods.HomewardRagnarok.ArmorTooltips.HeliologyMaskRemove");
                if (!string.IsNullOrWhiteSpace(removeText))
                {
                    tooltips.RemoveAll(t => t.Text.Contains(removeText));
                }
            }
        }
    }

    public class ArmorRebalancePlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            if (Player.TryGetModPlayer<ContinentOfJourney.TemplatePlayer>(out var cojPlayer))
            {
                if (cojPlayer.HeliologyArmorSetEffect)
                {
                    Player.maxMinions = (int)System.Math.Round(Player.maxMinions / 1.77f * 1.20f);
                }
                if (cojPlayer.HeliologyArmorSentrySetEffect)
                {
                    Player.maxTurrets -= 3;
                    Player.maxTurrets = (int)System.Math.Round(Player.maxTurrets / 1.77f * 1.20f);
                }
            }
        }
    }

    public class ArmorRebalanceGlobalProjectile : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            Player player = Main.player[projectile.owner];
            if (player.TryGetModPlayer<ContinentOfJourney.TemplatePlayer>(out var cojPlayer))
            {
                if ((cojPlayer.HeliologyArmorSetEffect || cojPlayer.HeliologyArmorSentrySetEffect) && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
                {
                    float penaltyAmount = (projectile.damage - Utils.Clamp(projectile.ArmorPenetration, 0, target.defense) / 2) * 0.38f;
                    modifiers.FinalDamage.Flat += penaltyAmount;
                }
            }
        }
    }

    public class ArmorBonusRebalanceTooltip : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            if (Language.Exists("Mods.ContinentOfJourney.Armor_HeliologyBonus"))
            {
                LocalizedText localizedText = Language.GetText("Mods.ContinentOfJourney.Armor_HeliologyBonus");

                FieldInfo valueField = typeof(LocalizedText).GetField("value", BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?? typeof(LocalizedText).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);

                valueField?.SetValue(localizedText, "Increases max number of minions by 20%");
            }
            if (Language.Exists("Mods.ContinentOfJourney.Armor_HeliologySentryBonus"))
            {
                LocalizedText localizedText = Language.GetText("Mods.ContinentOfJourney.Armor_HeliologySentryBonus");

                FieldInfo valueField = typeof(LocalizedText).GetField("value", BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?? typeof(LocalizedText).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);

                valueField?.SetValue(localizedText, "\nIncreases whip range by 18% and speed by 18%\nIncrease sentry slots by 20%");
            }
        }
    }
}
