using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using CalamityMod.Tiles.Furniture.CraftingStations;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Utilities;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Items.Accessories
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class DeluxeDewCollector : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 10);
            Item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.GetInstance<DewCollector>().UpdateAccessory(player, hideVisual);
            player.GetThoriumPlayer().healBonus += 6;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<DewCollector>(), 1) 
                .AddIngredient(ModContent.ItemType<FungusDeluxe>(), 1) 
                .AddIngredient(ModContent.ItemType<EssenceofLife>(), 1)
                .AddTile(ModContent.TileType<CosmicAnvil>())
                .Register();
        }
    }
}
