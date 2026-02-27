using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Collections.Generic;
using ContinentOfJourney.Items.Accessories.Bookmarks;
using HomewardRagnarok.Config;
using System;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance
{
    public class SpellPersistencePlayer : ModPlayer
    {
        private static FieldInfo bookMarkField;
        public static bool IsValidMagicItem(Item item)
        {
            bool isMagic = item.DamageType == DamageClass.Magic || item.mana > 0;
            if (!isMagic) return false;

            if (item.ModItem != null)
            {
                Type type = item.ModItem.GetType();
                while (type != null)
                {
                    if (type.Name == "VoidItem")
                    {
                        return false;
                    }
                    type = type.BaseType;
                }
            }

            return true;
        }

        public override void PreUpdate()
        {
            if (!ServerConfig.Instance.EnableBookmarks) return;

            if (Player.whoAmI == Main.myPlayer && Player.TryGetModPlayer<BookmarkPlayer>(out BookmarkPlayer bp))
            {
                Item item = Player.HeldItem;

                if (IsValidMagicItem(item) && !bp.isUsingTestTube)
                {
                    if (bp.bookMarkTier <= 0) bp.bookMarkTier = 1;

                    if (bookMarkField == null)
                        bookMarkField = typeof(BookmarkPlayer).GetField("<BookMark>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (bookMarkField != null)
                        bookMarkField.SetValue(bp, true);

                    if (bp.CurrentSpellItem != null)
                    {
                        Player.itemAnimation = 2;
                        Player.itemTime = 2;
                    }
                }
            }
        }
    }

    public class SpellDetection : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            if (!ServerConfig.Instance.EnableBookmarks) return;

            if (player.whoAmI == Main.myPlayer && player.TryGetModPlayer<BookmarkPlayer>(out BookmarkPlayer bp))
            {
                bool isValid = SpellPersistencePlayer.IsValidMagicItem(item);
                bool hasBookmarksEquipped = bp.bookMarkTier > 0;
                bool hasDefaultRightClick = item.ModItem != null && item.ModItem.AltFunctionUse(player);

                if (isValid && !bp.isUsingTestTube && !hasDefaultRightClick && hasBookmarksEquipped)
                {
                    if (Main.mouseRight && Main.mouseRightRelease && bp.CurrentSpellItem == null && bp.canStart)
                    {
                        bp.CurrentSpellItem = item;

                        if (bp.CurrentSpell != null)
                        {
                            bp.CurrentSpell.Clear();
                        }

                        bp.mouseRighttimer = 0;
                        bp.mouseLefttimer = 0;

                        Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, player.position);
                    }

                    if (bp.CurrentSpellItem != null && bp.castSpellID != "" && bp.CastTimer == 1)
                    {
                        if (item.type >= ItemID.Count && item.ModItem?.Mod.Name != "ContinentOfJourney")
                        {
                            FireUniversalSpell(bp, item, player);
                        }
                    }
                }
            }
        }

        private void FireUniversalSpell(BookmarkPlayer bp, Item item, Player player)
        {
            Vector2 position = player.MountedCenter;
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - position) * (item.shootSpeed > 0 ? item.shootSpeed : 10f);
            int damage = player.GetWeaponDamage(item);
            float knockback = player.GetWeaponKnockback(item, item.knockBack);
            int type = item.shoot > 0 ? item.shoot : ProjectileID.MagicMissile;

            int projIndex = Projectile.NewProjectile(player.GetSource_ItemUse(item), position, velocity, type, damage, knockback, player.whoAmI);

            foreach (var globalProj in Main.projectile[projIndex].Globals)
            {
                if (globalProj.GetType().Name == "BookmarkGlobalProjectile")
                {
                    FieldInfo field = globalProj.GetType().GetField("SpellName", BindingFlags.Public | BindingFlags.Instance);
                    if (field != null) field.SetValue(globalProj, bp.castSpellID);
                    break;
                }
            }
        }

        public override bool CanShoot(Item item, Player player)
        {
            if (player.TryGetModPlayer<BookmarkPlayer>(out BookmarkPlayer bp))
            {
                if (bp.CurrentSpellItem != null) return false;
            }
            return base.CanShoot(item, player);
        }
    }
}