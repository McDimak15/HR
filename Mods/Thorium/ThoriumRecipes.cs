using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ThoriumMod.Items.Terrarium;
using ThoriumMod.Items.Sandstone;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.BossThePrimordials.Omni;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.FurnitureStatigel;
using CalamityMod.Items.Accessories;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;
using HomewardRagnarok.Items.Accessories;
using InfernalEclipseAPI.Common.GlobalItems.CraftingTrees.EtherealTalismanCraftingTree;

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
                if (type == ModContent.ItemType<TerrariumRippleKnife>())
                {
                    recipe.AddIngredient(ModContent.ItemType<KnifeBag>());
                }

                // Construction PDA
                if (type == ModContent.ItemType<ConstructionPDA>())
                {
                    recipe.AddIngredient(ModContent.ItemType<SteamkeeperWatch>());
                }

                // Rift Generator
                if (type == ModContent.ItemType<RiftGenerator>())
                {
                    recipe.AddIngredient(ModContent.ItemType<TerrariumCore>(), 5);
                }

                // Natural Essence
                if (type == ModContent.ItemType<NaturalEssence>())
                {
                    recipe.RemoveIngredient(ItemID.NaturesGift);
                    recipe.AddIngredient(ModContent.ItemType<HungeringBlossom>());
                }

                // Ethereal Talisman
                if (type == ModContent.ItemType<EtherealTalisman>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<HungeringBlossom>());
                    recipe.RemoveRecipeGroup(RecipeGroup.recipeGroupIDs["AnyManaFlowerAccessory"]);
                }

                // Star-Tainted Generator
                if (type == ModContent.ItemType<StarTaintedGenerator>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<SteamkeeperWatch>());
                }

                // Ancient Blessing
                if (type == ModContent.ItemType<AncientBlessing>())
                {
                    recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 3);
                }
            }
        }
    }
}