using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ContinentOfJourney.Projectiles;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class BiomatterCrossPlayer : ModPlayer
    {
        public int healerAttackCounter = 0;
        public bool hasBiomatterCross = false;

        public override void ResetEffects()
        {
            hasBiomatterCross = false;
        }

        public void OnHealerAttack()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out _)) return;
            if (hasBiomatterCross)
                healerAttackCounter++;
        }
    }

    public class BiomatterCrossHealer : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out _)) return false;
            return ModContent.TryFind<ModItem>("ContinentOfJourney", "BiomatterCross", out var biomatterCross)
                   && entity.type == biomatterCross.Type;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            tooltips.RemoveAll(t => t.Text.Contains("Heals the most damaged teammate")
                                 || t.Text.Contains("upon every 4 magic attacks")
                                 || t.Text.Contains("20% decreased magic damage"));

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                tooltips.Add(new TooltipLine(Mod, "BiomatterCrossPatch1", "+8% increased radiant damage"));
                tooltips.Add(new TooltipLine(Mod, "BiomatterCrossPatch2", "Heals the most damaged teammate by 5"));
                tooltips.Add(new TooltipLine(Mod, "BiomatterCrossPatch3", "upon every 10 radiant attacks"));
            }
            else
            {
                tooltips.Add(new TooltipLine(Mod, "BiomatterCrossPatchNoThorium", "(Radiant effects require Thorium)"));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            var modPlayer = player.GetModPlayer<BiomatterCrossPlayer>();
            modPlayer.hasBiomatterCross = true;

            player.GetDamage(DamageClass.Magic) += 0.20f;

            if (ModLoader.TryGetMod("ThoriumMod", out var thorium))
            {
                if (thorium.TryFind("HealerDamage", out DamageClass healerDamage))
                    player.GetDamage(healerDamage) += 0.08f;
            }

            var templatePlayer = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
            templatePlayer.Healertimer = -9999;
            templatePlayer.CrossTimer = int.MaxValue;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.active &&
                    proj.type == ModContent.ProjectileType<HealBubble>() &&
                    proj.ai[0] == 5)
                {
                    proj.Kill();
                }
            }

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                if (modPlayer.healerAttackCounter >= 10)
                {
                    modPlayer.healerAttackCounter = 0;

                    int healTarget = player.whoAmI;
                    int lowestHealth = int.MaxValue;

                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player ply = Main.player[i];
                        if (ply.active && !ply.dead && ply.team == player.team &&
                            ply.statLife < ply.statLifeMax2 &&
                            ply.statLife < lowestHealth &&
                            Vector2.Distance(ply.Center, player.Center) < 1500)
                        {
                            lowestHealth = ply.statLife;
                            healTarget = ply.whoAmI;
                        }
                    }

                    int proj = Projectile.NewProjectile(
                        player.GetSource_Accessory(item),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<HealBubble>(),
                        0,
                        0,
                        player.whoAmI,
                        5, // heal 
                        healTarget
                    );

                    Main.projectile[proj].netUpdate = true;
                }
            }
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (item.DamageType == healerDamage)
                player.GetModPlayer<BiomatterCrossPlayer>().OnHealerAttack();
        }
    }

    public class BiomatterCrossProjCounter : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers &&
                projectile.DamageType == healerDamage)
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.GetModPlayer<BiomatterCrossPlayer>();

                if (modPlayer.hasBiomatterCross)
                    modPlayer.OnHealerAttack();
            }
        }
    }
}
