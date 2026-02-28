using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.ThrowerWeapons;
using ContinentOfJourney.Items.Flamethrowers;
using ContinentOfJourney.Items.FielderSentries;
using ContinentOfJourney.Items.Rockets;
using ContinentOfJourney.Projectiles;
using ContinentOfJourney.Projectiles.Meelee;
using CalamityMod.Buffs.DamageOverTime;

namespace HomewardRagnarok
{
    public class BalanceTweaks : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            // Rapiers
            if (item.ModItem is CopperRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is GoldRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is IronRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is LeadRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is PlatinumRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is SilverRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is TinRapier) item.damage = (int)(item.damage * 1.3f);
            if (item.ModItem is TungstenRapier) item.damage = (int)(item.damage * 1.3f);

            // Laser sniper rifle
            if (item.ModItem is LaserSniperRifle)
            {
                item.damage = 777;
            }

            // Backstabber
            if (item.ModItem is ContinentOfJourney.Items.Backstabber)
                item.useTime = item.useAnimation = 12;

            // Bloodthirst
            if (item.ModItem is ContinentOfJourney.Items.Bloodthirst)
                item.useTime = item.useAnimation = 20;

            // Kunai
            if (item.ModItem is ContinentOfJourney.Items.ConniversKunai)
                item.damage = 24;

            // Wildfire
            if (item.ModItem is FT2Wildfire)
                item.damage = 18;

            // Sparkthrower
            if (item.ModItem is FT1Sparkthrower)
                item.damage = 9;

            // Cursed Flower Staff
            if (item.ModItem is StaffCursedFlower)
                item.damage = 140;

            // Cactus Ball
            if (item.ModItem is ItemCactusBall)
            {
                item.useTime = 30;
                item.useAnimation = 30;
            }

            // Tooth of Cthulhu
            if (item.ModItem is ContinentOfJourney.Items.ToothOfCthulhu)
                item.damage = 15;

            // Vein
            if (item.ModItem is Vein)
            {
                item.damage = 28;
                item.useTime = item.useAnimation = 20;
            }

            //  Celestial Slime Staff
            if (item.ModItem is CelestialSlimeStaff)
                item.damage = 153;

            // White Dwarf Staff
            if (item.ModItem is WhiteDwarfStaff)
                item.damage = 162;

            // Star Eater Staff
            if (item.ModItem is StarEaterStaff)
                item.damage = 70;

            // Understanding
            if (item.ModItem is ContinentOfJourney.Items.BetShooter)
                item.damage = 90;

            // Sentry Gun Wand
            if (item.ModItem is StaffLevelThreeSentry)
                item.damage = 85;

            // Flyback Hand
            if (item.ModItem is ContinentOfJourney.Items.FlybackHand)
                item.damage = 210;

            // Galvanized Hand
            if (item.ModItem is ContinentOfJourney.Items.GalvanizedHand)
                item.damage = 682;

            // Handgun
            if (item.ModItem is ContinentOfJourney.Items.Handgun)
                item.damage = 147;

            // Metal Gear
            if (item.ModItem is ContinentOfJourney.Items.MetalGear)
                item.damage = 241;

            // Sweep Hand
            if (item.ModItem is ContinentOfJourney.Items.SecondHand)
            {
                item.damage = 340;
                item.scale = 1.5f;
            }

            // What a Beautiful World
            if (item.ModItem is FlowerFieldsForever)
            {
                item.damage = 110;
                item.mana = 16;
            }

            // Dialectics
            if (item.ModItem is ContinentOfJourney.Items.Dialectics)
                item.damage = 420;

            // Fission
            if (item.ModItem is Fission)
                item.damage = 127;

            // Materialism
            if (item.ModItem is ContinentOfJourney.Items.Materialism)
            {
                item.useAnimation = 13;
                item.useTime = 13;
                item.crit = 10;
                item.scale = 1.3f;
            }

            // Book Full of Projection
            if (item.ModItem is TomeOfHolographic)
            {
                item.useAnimation = 15;
                item.useTime = 15;
                item.damage = 235;
            }

            // Duality
            if (item.ModItem is Duality)
            {
                item.useAnimation = 26;
                item.useTime = 26;
                item.damage = 480;
            }

            // The Scottish Resistance
            if (item.ModItem is TheScottishResistance)
                item.useAnimation = item.useTime = 24;

            // Vector Addition
            if (item.ModItem is ContinentOfJourney.Items.VectorAddition)
            {
                item.damage = 284;
                item.mana = 8;
            }

            // Doctor Expeller
            if (item.ModItem is DoctorExpeller)
                item.damage = 170;

            // Rose Garden
            if (item.ModItem is EdenGift)
            {
                item.damage = 190;
                item.useAnimation = item.useTime = 24;
                item.mana = 20;
            }

            // Entropy Reduction
            if (item.ModItem is EntropyReduction)
            {
                item.damage = 460;
                item.mana = 26;
            }

            // Force-A-Nature
            if (item.ModItem is ForceANature)
            {
                item.useAnimation = item.useTime = 32;
                item.damage = 192;
            }

            // It Lived
            if (item.ModItem is ContinentOfJourney.Items.ItLived)
            {
                item.damage = 620;
                item.useAnimation = item.useTime = 14;
                item.scale = 1.3f;
            }

            // Passione
            if (item.ModItem is Lifesaber)
                item.damage = 309;

            // Pillar Staff
            if (item.ModItem is PillarStaff)
                item.damage = 109;

            // The Black Box
            if (item.ModItem is TheBlackBox)
                item.useAnimation = item.useTime = 16;

            // Virtue
            if (item.ModItem is Virtue)
                item.damage = 102;

            // Denial
            if (item.ModItem is ContinentOfJourney.Items.Denial)
                item.damage = 130;

            // Acceptance
            if (item.ModItem is ContinentOfJourney.Items.Acceptance)
                item.damage = 102;

            // Anger
            if (item.ModItem is ContinentOfJourney.Items.TrueDawnsBorder)
                item.damage = 350;

            // Accident
            if (item.ModItem is Accident)
                item.damage = 9;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            if (item.ModItem is ItemCactusBall)
            {
                tooltips.Add(new TooltipLine(Mod, "PoisonEffect", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.PoisonEffect")));
            }
            if (item.ModItem is StarEaterStaff)
            {
                tooltips.Add(new TooltipLine(Mod, "HolyFlamesEffect", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.HolyFlamesEffect")));
            }
            if (item.ModItem is Fission)
            {
                tooltips.Add(new TooltipLine(Mod, "Vaporfied", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VaporfiedEffect")));
            }
        }

    }

    public class BalanceTweaksProj : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return true;

            // FlybackHand
            if (projectile.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.FlybackHand>())
            {
                Player player = Main.player[projectile.owner];
                int duration = player.itemAnimationMax;
                projectile.velocity = Vector2.Normalize(projectile.velocity);
                float halfDuration = duration * 0.5f;

                if (projectile.timeLeft < halfDuration)
                {
                    if (projectile.ai[1] < 5 && projectile.owner == Main.myPlayer)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 vel = projectile.velocity.RotatedBy((i - 1) * 0.12f);
                            vel *= 24f * player.GetAttackSpeed(DamageClass.Melee);

                            Projectile.NewProjectile(
                                projectile.GetSource_FromThis(),
                                projectile.Center,
                                vel,
                                ModContent.ProjectileType<ContinentOfJourney.Projectiles.Meelee.SecondHand>(),
                                projectile.damage,
                                projectile.knockBack,
                                projectile.owner
                            );
                        }
                        projectile.ai[1] = 10;
                    }
                }
                return true;
            }

            // Dialectics
            if (projectile.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.Dialectics>())
            {
                Player player = Main.player[projectile.owner];
                int duration = player.itemAnimationMax;
                float halfDuration = duration * 0.5f;

                if (projectile.timeLeft < halfDuration)
                {
                    if (projectile.ai[1] < 5 && projectile.owner == Main.myPlayer)
                    {
                        Vector2 shootVel = Vector2.Normalize(projectile.velocity).RotatedBy(MathHelper.ToRadians(10));
                        shootVel *= 18f * player.GetAttackSpeed(DamageClass.Melee);

                        Projectile.NewProjectile(
                            projectile.GetSource_FromThis(),
                            projectile.Center,
                            shootVel,
                            ModContent.ProjectileType<ContinentOfJourney.Projectiles.Meelee.Materialism>(),
                            projectile.damage,
                            projectile.knockBack,
                            projectile.owner
                        );
                    }
                }
                return true;
            }

            // Evolution
            if (projectile.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.Evolution>())
            {
                Player player = Main.player[projectile.owner];
                int duration = player.itemAnimationMax;
                float halfDuration = duration * 0.5f;

                if (projectile.timeLeft < halfDuration)
                {
                    if (projectile.ai[1] < 5 && projectile.owner == Main.myPlayer)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 vel = projectile.velocity.RotatedBy((i - 1) * 0.12f);
                            vel *= 24f * player.GetAttackSpeed(DamageClass.Melee);

                            Projectile.NewProjectile(
                                projectile.GetSource_FromThis(),
                                projectile.Center,
                                vel,
                                ModContent.ProjectileType<ContinentOfJourney.Projectiles.Meelee.ItLived>(),
                                projectile.damage,
                                projectile.knockBack,
                                projectile.owner
                            );
                        }
                        projectile.ai[1] = 10;
                    }
                }
                return true;
            }

            return base.PreAI(projectile);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            // Cactus Ball
            if (projectile.ModProjectile is ProjCactusBall_1 ||
                projectile.ModProjectile is ProjCactusBall_2)
            {
                target.AddBuff(BuffID.Poisoned, 180);
            }

            // Star Eater Staff
            if (projectile.ModProjectile is StarEater)
            {
                target.AddBuff(ModContent.BuffType<HolyFlames>(), 60);
            }

            // Fission
            if (projectile.ModProjectile is Fission_1)
            {
                target.AddBuff(ModContent.BuffType<Vaporfied>(), 60);
            }
        }
    }
}

