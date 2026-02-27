using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ContinentOfJourney.Projectiles;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class MythrilCrossPlayer : ModPlayer
    {
        public int healerAttackCounter = 0;
        public bool hasMythrilCross = false;

        public override void ResetEffects()
        {
            hasMythrilCross = false;
        }

        public void OnHealerAttack()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out _)) return;
            if (hasMythrilCross)
                healerAttackCounter++;
        }
    }

    public class MythrilCrossHealer : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            if (!ModLoader.TryGetMod("ThoriumMod", out _)) return false;
            return ModContent.TryFind<ModItem>("ContinentOfJourney", "MythrilCross", out var mythrilCross)
                   && entity.type == mythrilCross.Type;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            tooltips.RemoveAll(t => t.Text.Contains("Heals the most damaged teammate")
                                 || t.Text.Contains("upon every 7 magic attacks")
                                 || t.Text.Contains("15% decreased magic damage"));

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                tooltips.Add(new TooltipLine(Mod, "MythrilCrossPatch1", "+4% increased radiant damage"));
                tooltips.Add(new TooltipLine(Mod, "MythrilCrossPatch2", "Heals the most damaged teammate by 4"));
                tooltips.Add(new TooltipLine(Mod, "MythrilCrossPatch3", "upon every 9 radiant attacks"));
            }
            else
            {
                tooltips.Add(new TooltipLine(Mod, "MythrilCrossNoThorium", "(Radiant effects require Thorium)"));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            var modPlayer = player.GetModPlayer<MythrilCrossPlayer>();
            modPlayer.hasMythrilCross = true;

            player.GetDamage(DamageClass.Magic) += 0.15f;

            if (ModLoader.TryGetMod("ThoriumMod", out var thorium))
            {
                if (thorium.TryFind("HealerDamage", out DamageClass healerDamage))
                    player.GetDamage(healerDamage) += 0.04f;
            }

            var templatePlayer = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
            templatePlayer.Healertimer = -9999;
            templatePlayer.CrossTimer = int.MaxValue;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.active &&
                    proj.type == ModContent.ProjectileType<HealBubble>() &&
                    proj.ai[0] == 8)
                {
                    proj.Kill();
                }
            }

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                if (modPlayer.healerAttackCounter >= 9)
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
                        4, // heal = 4
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
                player.GetModPlayer<MythrilCrossPlayer>().OnHealerAttack();
        }
    }

    public class MythrilCrossProjCounter : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers &&
                projectile.DamageType == healerDamage)
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.GetModPlayer<MythrilCrossPlayer>();

                if (modPlayer.hasMythrilCross)
                    modPlayer.OnHealerAttack();
            }
        }
    }
}
