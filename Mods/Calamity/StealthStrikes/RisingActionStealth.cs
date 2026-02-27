using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;
using ContinentOfJourney.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class RisingActionRoguePatch : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<RisingAction>())
            {
                item.DamageType = ModContent.GetInstance<RogueDamageClass>();
            }
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (!ServerConfig.Instance.CustomStealthStrikes)
                return true;

            if (item.type == ModContent.ItemType<RisingAction>())
            {
                if (player.Calamity().StealthStrikeAvailable())
                {
                    int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                    if (proj.WithinBounds(Main.maxProjectiles))
                    {
                        Main.projectile[proj].Calamity().stealthStrike = true;
                    }
                    return false;
                }
            }
            return true;
        }
    }

    public class RisingActionStealthLogic : GlobalProjectile
    {
        private List<int> hitTargets = new List<int>();

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ServerConfig.Instance.CustomStealthStrikes)
                return;

            if (projectile.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.Meelee.RisingAction>())
            {
                if (projectile.Calamity().stealthStrike)
                {
                    if (!hitTargets.Contains(target.whoAmI))
                        hitTargets.Add(target.whoAmI);

                    PerformStealthRicochet(projectile, target);
                }
            }
        }

        private void PerformStealthRicochet(Projectile projectile, NPC currentTarget)
        {
            float bounceRange = 950f;
            float bounceSpeed = 28f;
            NPC nextTarget = null;
            float shortestDistance = bounceRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.whoAmI != currentTarget.whoAmI &&
                    !hitTargets.Contains(npc.whoAmI) && npc.CanBeChasedBy())
                {
                    float distance = Vector2.Distance(projectile.Center, npc.Center);
                    if (distance < shortestDistance && Collision.CanHit(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        shortestDistance = distance;
                        nextTarget = npc;
                    }
                }
            }

            if (nextTarget != null)
            {
                Vector2 newDirection = nextTarget.Center - projectile.Center;
                newDirection.Normalize();
                projectile.velocity = newDirection * bounceSpeed;

                projectile.ai[0] = 0f;
                projectile.ai[1] = 0f;

                projectile.netUpdate = true;
            }
            else
            {
                projectile.ai[1] = 1f;
            }
        }
    }
}