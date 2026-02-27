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
    public class OrichalcumCrossPlayer : ModPlayer
    {
        public int healerAttackCounter = 0;
        public bool hasOrichalcumCross = false;

        public override void ResetEffects()
        {
            hasOrichalcumCross = false;
        }

        public void OnHealerAttack()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out _)) return;
            if (hasOrichalcumCross)
                healerAttackCounter++;
        }
    }

    public class OrichalcumCrossHealer : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            return ModContent.TryFind<ModItem>("ContinentOfJourney", "OrichalcumCross", out var cross)
                   && entity.type == cross.Type;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                tooltips.RemoveAll(t => t.Text.Contains("Heals the most damaged teammate")
                                 || t.Text.Contains("upon every 7 magic attacks")
                                 || t.Text.Contains("14% decreased magic damage"));

                tooltips.Add(new TooltipLine(Mod, "OrichalcumCrossPatch1", "+4% increased radiant damage"));
                tooltips.Add(new TooltipLine(Mod, "OrichalcumCrossPatch2", "Heals the most damaged teammate by 4"));
                tooltips.Add(new TooltipLine(Mod, "OrichalcumCrossPatch3", "upon every 9 radiant attacks"));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            var modPlayer = player.GetModPlayer<OrichalcumCrossPlayer>();
            modPlayer.hasOrichalcumCross = true;

            player.GetDamage(DamageClass.Magic) += 0.14f;

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
                        4, // fixed heal = 4
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
                player.GetModPlayer<OrichalcumCrossPlayer>().OnHealerAttack();
        }
    }

    public class OrichalcumCrossProjCounter : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers &&
                projectile.DamageType == healerDamage)
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.GetModPlayer<OrichalcumCrossPlayer>();

                if (modPlayer.hasOrichalcumCross)
                    modPlayer.OnHealerAttack();
            }
        }
    }
}
