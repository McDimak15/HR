using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ContinentOfJourney.Items.Accessories.Bookmarks;
using ContinentOfJourney;
using System.Linq;

namespace HomewardRagnarok.Items.Accessories
{
    public class EvilFlask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(gold: 20);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<EvilFlaskPlayer>().evilFlaskActive = true;

            player.GetModPlayer<TemplatePlayer>().FTAmmoSave = true;
            player.Book().bookMarkTier = 1;
            player.Book().isUsingTestTube = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (ClashingAccessories.BookmarkIDs.Contains(incomingItem.type) && incomingItem.type != Type) return false;
            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
        }

        public override void AddRecipes()
        {
            int rottenMatterType = 0;
            if (ModLoader.TryGetMod("CalamityMod", out var calamity))
            {
                rottenMatterType = calamity.Find<ModItem>("RottenMatter").Type;
            }

            CreateRecipe()
                .AddIngredient(ItemID.Lens, 5)
                .AddIngredient(ItemID.ShadowScale, 5)
                .AddIngredient(rottenMatterType, 5)
                .AddCondition(Condition.NearWater)
                .Register();
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "EvilFlask1", "20% chance to not consume ammo when using a flamethrower"));
            tooltips.Add(new TooltipLine(Mod, "EvilFlask2", "Allows you to customize the projectile of your flamethrower"));
            tooltips.Add(new TooltipLine(Mod, "EvilFlask3", "Your flamethrower inflicts Brain Rot debuff"));
            tooltips.Add(new TooltipLine(Mod, "EvilFlask4", "When equipped, show items that can be inserted"));
            tooltips.Add(new TooltipLine(Mod, "EvilFlask5", "You can insert 2 ingredients at most"));
        }
    }

    public class EvilFlaskPlayer : ModPlayer
    {
        public bool evilFlaskActive;

        public override void ResetEffects()
        {
            evilFlaskActive = false;
        }
    }

    public class EvilFlaskGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            EvilFlaskPlayer modPlayer = player.GetModPlayer<EvilFlaskPlayer>();

            if (!modPlayer.evilFlaskActive) return;

            bool isFlamethrower = false;

            if (projectile.aiStyle == 27)
                isFlamethrower = true;

            if (projectile.type == ProjectileID.Flames ||
                projectile.type == ProjectileID.GreenLaser ||
                projectile.type == ProjectileID.ShadowFlame)
                isFlamethrower = true;

            if (projectile.ModProjectile is IFlamethrowerProjectile)
                isFlamethrower = true;

            Item heldItem = player.HeldItem;
            if (!heldItem.IsAir && heldItem.useAmmo == AmmoID.Gel)
                isFlamethrower = true;

            if (isFlamethrower)
            {
                int brainRot = ModContent.Find<ModBuff>("CalamityMod", "BrainRot")?.Type ?? 0;
                if (brainRot > 0)
                    target.AddBuff(brainRot, 300); 
            }
        }
    }

    public interface IFlamethrowerProjectile { }
}
