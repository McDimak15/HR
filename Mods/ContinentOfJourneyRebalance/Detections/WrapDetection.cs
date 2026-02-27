using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ThrowerUnification.Core.UnitedModdedThrowerClass;
using ContinentOfJourney.Projectiles;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance
{
    public class WrapDetection : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            if (!(projectile.DamageType is UnitedModdedThrower))
                return;

            Player player = Main.player[projectile.owner];
            if (player == null || !player.active || player.dead)
                return;

            foreach (var proj in Main.projectile)
            {
                if (proj.active && proj.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.SpecialEffects.Wrap_proj>() && proj.owner == projectile.owner)
                {
                    var wrap = (ContinentOfJourney.Projectiles.SpecialEffects.Wrap_proj)proj.ModProjectile;
                    if (wrap == null)
                        continue;

                    var bubbleListField = typeof(ContinentOfJourney.Projectiles.SpecialEffects.Wrap_proj)
                        .GetField("BubbleList");
                    if (bubbleListField == null)
                        continue;

                    var bubbleList = (Vector3[])bubbleListField.GetValue(wrap);

                    for (int i = 0; i < bubbleList.Length; i++)
                    {
                        if (bubbleList[i].Z != 0)
                        {
                            Rectangle bubbleHitbox = new Rectangle((int)bubbleList[i].X - 20, (int)bubbleList[i].Y - 20, 40, 40);

                            if (projectile.Hitbox.Intersects(bubbleHitbox))
                            {
                                var wrapProj = projectile.GetGlobalProjectile<CoJGlobalProjectile_Instance>();
                                if (wrapProj != null)
                                {
                                    int bubbleType = (int)bubbleList[i].Z;

                                    wrapProj.Wrap[bubbleType] = 1;

                                    int dustType = DustID.Water;
                                    switch (bubbleType)
                                    {
                                        case 1: dustType = DustID.Poisoned; break;
                                        case 2: dustType = 6; break;
                                        case 3: dustType = DustID.CursedTorch; break;
                                        case 4: dustType = DustID.Ichor; break;
                                        case 5: dustType = DustID.Venom; break;
                                        case 6: dustType = DustID.InfluxWaver; break;
                                        case 7: dustType = DustID.GemDiamond; break;
                                        case 8: dustType = DustID.Wraith; break;
                                    }

                                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, new Vector2(bubbleList[i].X, bubbleList[i].Y));

                                    for (int j = 0; j < 12; j++)
                                    {
                                        int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, dustType);
                                        Main.dust[dust].velocity += projectile.velocity * 0.5f;
                                        Main.dust[dust].noGravity = true;
                                        Main.dust[dust].scale = 2f + Main.rand.NextFloat();
                                    }

                                    for (int j = 0; j < 360; j += 12)
                                    {
                                        Vector2 offset = new Vector2(-10, 0).RotatedBy(MathHelper.ToRadians(j));
                                        int dust = Dust.NewDust(new Vector2(bubbleList[i].X, bubbleList[i].Y) - offset, 0, 0, dustType);
                                        Main.dust[dust].scale = 1.2f;
                                        Main.dust[dust].velocity = offset;
                                        Main.dust[dust].velocity.Y *= 0.927f;
                                        Main.dust[dust].noLight = true;
                                        Main.dust[dust].noGravity = true;
                                    }

                                    bubbleList[i].Z = 0;
                                    bubbleListField.SetValue(wrap, bubbleList);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
