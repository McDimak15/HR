using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ContinentOfJourney.Items.Accessories;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ContinentOfJourney.Projectiles;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class RejuvenatedCrossPlayer : ModPlayer
    {
        public int healerAttackCounter = 0;
        public int extraHeal = 0;
        public bool hasRejuvenatedCross = false;
        public Vector2 healerBallPos;

        public override void ResetEffects()
        {
            hasRejuvenatedCross = false;
        }

        public void OnHealerAttack()
        {
            if (hasRejuvenatedCross)
                healerAttackCounter++;
        }
    }

    public class RejuvenatedCrossHealer : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            return item.type == ModContent.ItemType<RejuvenatedCross>();
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            tooltips.RemoveAll(t => t.Text.Contains("20% decreased magic damage")
                                 || t.Text.Contains("Grants regeneration")
                                 || t.Text.Contains("upon every 5 magic attacks"));

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                tooltips.Add(new TooltipLine(Mod, "RejuvenatedCrossPatch1", "+5% increased radiant damage"));
                tooltips.Add(new TooltipLine(Mod, "RejuvenatedCrossPatch2", "Grants regeneration to the most damaged teammate"));
                tooltips.Add(new TooltipLine(Mod, "RejuvenatedCrossPatch3", "upon every 10 radiant attacks"));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            var modPlayer = player.GetModPlayer<RejuvenatedCrossPlayer>();
            modPlayer.hasRejuvenatedCross = true;

            player.GetDamage(DamageClass.Magic) += 0.20f;

            if (ModLoader.TryGetMod("ThoriumMod", out var thorium))
            {
                if (thorium.TryFind("HealerDamage", out DamageClass healerDamage))
                {
                    player.GetDamage(healerDamage) += 0.05f;

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
                                ply.statLife < lowestHealth)
                            {
                                lowestHealth = ply.statLife;
                                healTarget = ply.whoAmI;
                            }
                        }

                        Vector2 spawnPos = modPlayer.healerBallPos == default ? player.Center : modPlayer.healerBallPos;

                        int proj = Projectile.NewProjectile(
                            player.GetSource_Accessory(item),
                            spawnPos,
                            Vector2.Zero,
                            ModContent.ProjectileType<HealBubble>(),
                            0,
                            0,
                            player.whoAmI,
                            1 + modPlayer.extraHeal,
                            healTarget
                        );

                        Main.projectile[proj].netUpdate = true;
                    }
                }
            }
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (item.DamageType == healerDamage)
                player.GetModPlayer<RejuvenatedCrossPlayer>().OnHealerAttack();
        }
    }

    public class RejuvenatedCrossProjCounter : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers &&
                projectile.DamageType == healerDamage)
            {
                Player player = Main.player[projectile.owner];
                player.GetModPlayer<RejuvenatedCrossPlayer>().OnHealerAttack();
            }
        }
    }
}
