using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney;
using ContinentOfJourney.Projectiles;
using System;

namespace HomewardRagnarok
{
    public class HorizonRebalance : GlobalItem
    {
        private float flightCounter = 0f;

        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<Horizon>())
            {
                var cojPlayer = player.GetModPlayer<TemplatePlayer>();


                if (player.controlJump && cojPlayer.Horizon_Time > 0 && cojPlayer.Horizon_Time < 165)
                {
                    if (Main.GameUpdateCount % 2 == 0 && player.velocity.Y != 0)
                    {
                        cojPlayer.Horizon_Time += 1;
                    }
                }

                player.moveSpeed -= 0.03f;
                player.accRunSpeed -= 3.25f;

                if (player.equippedWings == null)
                {
                    if (player.controlJump && cojPlayer.Horizon_Time > 0 && player.velocity.Y != 0)
                    {
                        flightCounter += 1f;

                        float progress = MathHelper.Clamp(flightCounter / 150f, 0f, 1f);

                        if (player.velocity.Y > -20.5f)
                        {
                            float smoothThrust = MathHelper.Lerp(0.25f, 0.65f, progress);

                            player.velocity.Y -= smoothThrust;
                        }

                        if (player.velocity.Y > 0f) player.velocity.Y -= 0.15f;

                        if (Main.rand.NextBool(5) && Main.myPlayer == player.whoAmI)
                        {
                            int proj = Projectile.NewProjectile(player.GetSource_Accessory(item), player.MountedCenter,
                                new Vector2(Main.rand.NextFloat(-50f, 50f), 0),
                                ModContent.ProjectileType<Horizon_Ascension>(), 0, 0f, player.whoAmI);
                            Main.projectile[proj].netUpdate = true;
                        }
                    }
                    else
                    {
                        flightCounter = 0f;
                    }
                }
                else if (!hideVisual && player.velocity.Y < player.oldVelocity.Y && player.controlJump && Main.rand.NextBool(3) && Main.myPlayer == player.whoAmI)
                {
                    int proj = Projectile.NewProjectile(player.GetSource_Accessory(item), player.MountedCenter,
                        new Vector2(Main.rand.NextFloat(-50f, 50f), 0),
                        ModContent.ProjectileType<Horizon_Ascension>(), 0, 0f, player.whoAmI);
                    Main.projectile[proj].netUpdate = true;
                }
            }
        }
    }
}