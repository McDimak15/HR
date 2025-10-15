using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney.Projectiles.Flame;

namespace HomewardRagnarok
{
    public class ModdedFlamethrowerFlame : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];

            var modPlayer = player.GetModPlayer<ContinentOfJourney.Items.Accessories.Bookmarks.BookmarkPlayer>();
            if (!modPlayer.isUsingTestTube)
                return;

            bool isModdedFlamethrower = false;

            if (projectile.DamageType == DamageClass.Ranged) 
            {
                    Item projItem = null;
                    for (int i = 0; i < player.inventory.Length; i++)
                    {
                        if (player.inventory[i].shoot == projectile.type)
                        {
                            projItem = player.inventory[i];
                            break;
                        }
                    }

                    if (projItem != null && projItem.useAmmo == Terraria.ID.AmmoID.Gel)
                        isModdedFlamethrower = true;
            }

            if (projectile.aiStyle == 27)
                isModdedFlamethrower = true;

            if (isModdedFlamethrower && Main.myPlayer == player.whoAmI)
            {
                Projectile flame = Projectile.NewProjectileDirect(
                    projectile.GetSource_FromThis(),
                    target.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<Flame>(),
                    damageDone,
                    hit.Knockback,
                    player.whoAmI
                );

                flame.timeLeft = 10; 
            }
        }
    }
}
