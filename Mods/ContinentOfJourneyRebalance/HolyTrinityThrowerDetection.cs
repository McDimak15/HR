using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok.Compat
{
    public class TrinityCompatPlayer : ModPlayer
    {
        public Rectangle AlphaRect, OmegaRect, EpsilonRect;
        public bool AlphaActive, OmegaActive, EpsilonActive;

        public override void PostUpdate()
        {
            AlphaActive = OmegaActive = EpsilonActive = false;

            if (!ModLoader.TryGetMod("ContinentOfJourney", out _))
                return;

            var tp = Player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
            if (!tp.HolyTrinity) return;

            int pCx = (int)Player.Center.X;
            int pCy = (int)Player.Center.Y;
            float timer = tp.TemplateTimer;

            if (tp.AlphaTrinity)
            {
                var center = new Point(
                    pCx + (int)(System.Math.Sin(timer * 2f / 30f) * 64f),
                    pCy - (int)(System.Math.Cos(timer * 2f / 30f) * 64f)
                );
                AlphaRect = new Rectangle(center.X - 19, center.Y - 19, 38, 38);
                AlphaActive = true;
            }

            if (tp.OmegaTrinity)
            {
                Vector2 dir = Player.Center.DirectionTo(Main.MouseWorld);
                Vector2 center = Player.Center + 320f * (dir == Vector2.Zero ? Vector2.UnitX : Vector2.Normalize(dir));
                OmegaRect = new Rectangle((int)center.X - 19, (int)center.Y - 19, 38, 38);
                OmegaActive = true;
            }

            if (tp.EpsilonTrinity)
            {
                int targetIndex = -1;
                float lowestLife = float.MaxValue;

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (!npc.CanBeChasedBy()) continue;
                    if (Vector2.Distance(npc.Center, Player.Center) > 1000f) continue;
                    if (!Collision.CanHitLine(Player.Center, 1, 1, npc.position, npc.width, npc.height)) continue;

                    if (npc.life < lowestLife)
                    {
                        lowestLife = npc.life;
                        targetIndex = i;
                    }
                }

                if (targetIndex != -1)
                {
                    NPC npc = Main.npc[targetIndex];
                    float radius = System.Math.Max(npc.width, npc.height) + 32f;
                    Vector2 center = npc.Center + radius * Vector2.Normalize(Player.Center - npc.Center);
                    EpsilonRect = new Rectangle((int)center.X - 19, (int)center.Y - 19, 38, 38);
                    EpsilonActive = true;
                }
            }
        }
    }

    public class TrinityCompatGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private int alphaCd, omegaCd, epsilonCd;
        private bool alphaBuffed, omegaBuffed, epsilonBuffed;

        public override void AI(Projectile projectile)
        {
            if (projectile.owner != Main.myPlayer) return;
            Player player = Main.player[projectile.owner];
            if (!player.active || player.dead) return;
            if (!projectile.friendly || projectile.damage <= 0) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out _)) return;

            var tp = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
            if (!tp.HolyTrinity) return;

            if (!IsRogueOrThrower(projectile)) return;

            var compat = player.GetModPlayer<TrinityCompatPlayer>();
            float dummy = 0f;

            if (compat.AlphaActive && !alphaBuffed && alphaCd <= 0 &&
                Collision.CheckAABBvLineCollision(compat.AlphaRect.TopLeft(), compat.AlphaRect.Size(),
                    projectile.Center, projectile.Center + projectile.velocity * 2f, 24f, ref dummy))
            {
                projectile.damage = (int)(projectile.damage * 1.2f);
                SpawnRingVisual(projectile, compat.AlphaRect.Center.ToVector2());
                alphaCd = 40;
                alphaBuffed = true;
            }

            if (compat.OmegaActive && !omegaBuffed && omegaCd <= 0 &&
                Collision.CheckAABBvLineCollision(compat.OmegaRect.TopLeft(), compat.OmegaRect.Size(),
                    projectile.Center, projectile.Center + projectile.velocity * 2f, 24f, ref dummy))
            {
                projectile.damage = (int)(projectile.damage * 1.2f);
                SpawnRingVisual(projectile, compat.OmegaRect.Center.ToVector2());
                omegaCd = 40;
                omegaBuffed = true;
            }

            if (compat.EpsilonActive && !epsilonBuffed && epsilonCd <= 0 &&
                Collision.CheckAABBvLineCollision(compat.EpsilonRect.TopLeft(), compat.EpsilonRect.Size(),
                    projectile.Center, projectile.Center + projectile.velocity * 2f, 24f, ref dummy))
            {
                projectile.damage = (int)(projectile.damage * 1.3f);
                SpawnRingVisual(projectile, compat.EpsilonRect.Center.ToVector2());
                epsilonCd = 40;
                epsilonBuffed = true;
            }

            if (alphaCd > 0) alphaCd--;
            if (omegaCd > 0) omegaCd--;
            if (epsilonCd > 0) epsilonCd--;
        }

        private void SpawnRingVisual(Projectile proj, Vector2 spawnPos)
        {
            for (int i = 0; i < 12; i++)
            {
                int dust = Dust.NewDust(proj.position, proj.width, proj.height, DustID.GemDiamond);
                Main.dust[dust].velocity += proj.velocity;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale = 1.8f + Main.rand.NextFloat();
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, proj.position);

            if (Main.myPlayer == proj.owner)
            {
                int efx = Projectile.NewProjectile(
                    proj.GetSource_FromThis(),
                    spawnPos,
                    Vector2.Zero,
                    ModContent.ProjectileType<ContinentOfJourney.Projectiles.RingEffect>(),
                    0, 0, proj.owner
                );
                Main.projectile[efx].netUpdate = true;
            }
        }

        private bool IsRogueOrThrower(Projectile proj)
            {
                if (ModLoader.TryGetMod("CalamityMod", out var calamity))
                {
                    if (calamity.TryFind("ThrowingDamageClass", out DamageClass calamityThrower))
                    {
                        if (proj.DamageType == calamityThrower) return true;
                    }
                }
                if (ModLoader.TryGetMod("ThrowerUnification", out var tu))
                {
                    if (tu.TryFind("UnitedModdedThrower", out DamageClass unifiedThrower))
                    {
                        if (proj.DamageType == unifiedThrower) return true;
                    }
                }
                return false;
            }
        }
    


    public class TrinityTooltipFix : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj))
                return;

            if (coj.TryFind("Epsilon", out ModItem epsilon) && item.type == epsilon.Type)
            {
                foreach (var line in tooltips)
                    if (line.Text.Contains("70%"))
                        line.Text = line.Text.Replace("70%", "30%");

                foreach (var line in tooltips)
                    if (line.Text.Contains("Consumable ranged"))
                        line.Text = line.Text.Replace("Consumable ranged", "Thrower");
            }

            if ((coj.TryFind("Alpha", out ModItem alpha) && item.type == alpha.Type) ||
                (coj.TryFind("Omega", out ModItem omega) && item.type == omega.Type))
            {
                foreach (var line in tooltips)
                    if (line.Text.Contains("70%"))
                        line.Text = line.Text.Replace("70%", "20%");

                foreach (var line in tooltips)
                    if (line.Text.Contains("Consumable ranged"))
                        line.Text = line.Text.Replace("Consumable ranged", "Thrower");
            }

            if ((coj.TryFind("TheHolyTrinity", out ModItem holy) && item.type == holy.Type) ||
                (coj.TryFind("FatherAndSon", out ModItem father) && item.type == father.Type))
            {
                foreach (var line in tooltips)
                    if (line.Text.Contains("70%"))
                        line.Text = line.Text.Replace("70%", "30%");

                foreach (var line in tooltips)
                    if (line.Text.Contains("Consumable ranged"))
                        line.Text = line.Text.Replace("Consumable ranged", "Thrower");
            }
        }
    }
}
