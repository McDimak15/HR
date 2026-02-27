using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.SummonItems;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.FurnitureStatigel;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class ThoriumRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            // Maple Mushroom including Calamity 
            Recipe.Create(ModContent.ItemType<MapleMushroom>())
                .AddIngredient(ModContent.ItemType<HoneyMushroom>(), 1)
                .AddIngredient(ModContent.ItemType<LivingLeaf>(), 5)
                .AddTile(TileID.Bottles)
                .Register();

            // Jelly Mushroom including Calamity 
            Recipe.Create(ModContent.ItemType<JellyMushroom>())
                .AddIngredient(ModContent.ItemType<MapleMushroom>(), 1)
                .AddIngredient(ModContent.ItemType<PurifiedGel>(), 10)
                .AddIngredient(ModContent.ItemType<BioMatter>(), 3)
                .AddTile(ModContent.TileType<StaticRefiner>())
                .Register();
        }

        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // Knifes
                if (type == ModContent.ItemType<TerrariumRippleKnife>() ||
                    type == ModContent.ItemType<gSandStoneThrowingKnife>())
                {
                    recipe.AddIngredient(ModContent.ItemType<KnifeBag>());
                }
            }
        }
    }
}