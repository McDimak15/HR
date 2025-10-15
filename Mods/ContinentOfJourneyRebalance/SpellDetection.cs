/*
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ContinentOfJourney.Items.Accessories.Bookmarks; // BookmarkPlayer
using ContinentOfJourney.Projectiles.SpecialEffects;  // BookmarkGlobalProjectile (if exists)


namespace HomewardRagnarok.Patches
{
    public class UniversalSpellPatch : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (item.DamageType == DamageClass.Magic)
            {
                if (player.TryGetModPlayer(out BookmarkPlayer bookmark))
                {
                    if (bookmark.SpellReady && bookmark.SpellActive)
                    {
                        int proj = Projectile.NewProjectile(
                            source,
                            position,
                            velocity,
                            type,
                            damage,
                            knockback,
                            player.whoAmI
                        );

                        if (Main.projectile.IndexInRange(proj))
                        {
                            Projectile p = Main.projectile[proj];

                            var gProj = p.GetGlobalProjectile<BookmarkGlobalProjectile>();
                            gProj.SpellName = bookmark.CurrentSpellName; // adjust to real property
                            gProj.notHittingEnemyId = bookmark.notHittingNpc;

                            p.usesLocalNPCImmunity = true;
                            p.localNPCHitCooldown = 10;
                            p.netUpdate = true;
                        }

                        bookmark.SpellActive = false;
                        bookmark.SpellCastTimer = 0;

                        return false; // block vanilla shooting
                    }
                }
            }
            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }
    }
}
*/