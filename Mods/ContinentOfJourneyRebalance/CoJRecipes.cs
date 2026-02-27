using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Accessories.MechArms;
using CalamityMod.Systems;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class CoJRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.HWJRecipes)
                return;

            // Commemorative Coin
            Recipe.Create(ModContent.ItemType<CommemorativeCoin>())
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.LuckyCoin, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            // Wooden Claw
            Recipe.Create(ModContent.ItemType<WoodenClaw>())
                .AddRecipeGroup(RecipeGroupID.Wood, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            // Umbro Shield
            Recipe.Create(ModContent.ItemType<UmbroShield>())
                .AddIngredient(ModContent.ItemType<SimpleShield>())
                .AddIngredient(ModContent.ItemType<SwollenStar>(), 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            // Transaction Certificate
            Recipe.Create(ModContent.ItemType<TransactionCertificate>())
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ModContent.ItemType<SwollenStar>(), 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.HWJRecipes)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                // Honey Mushroom
                if (recipe.createItem.type == ModContent.ItemType<HoneyMushroom>())
                {
                    recipe.RemoveIngredient(ItemID.BeeWax);
                }

                //  Star Image
                if (recipe.createItem.type == ModContent.ItemType<StarImage>())
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ModContent.ItemType<EssenceofMatter>());
                }

                if (recipe.createItem.type == ModContent.ItemType<SurvivalCrisis>())
                {
                    recipe.AddRecipeGroup("Boss2Material", 10);
                }

                // Disable Crossbow Scope 
                if (recipe.createItem.type == ModContent.ItemType<CrossbowScope>())
                {
                    recipe.DisableRecipe();
                }

            }

            // Crossbow Scope 
            Recipe.Create(ModContent.ItemType<CrossbowScope>())
                .AddIngredient(ModContent.ItemType<StarQuiver>(), 1)
                .AddIngredient(ModContent.ItemType<MachinaScope>(), 1)
                .AddIngredient(ModContent.ItemType<EternalBar>(), 6)
                .AddIngredient(ModContent.ItemType<SolarFlareScoria>(), 12)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public class CoJTooltips : GlobalItem
        {
            public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
            {
                if (item.type == ModContent.ItemType<MapleMushroom>())
                {
                    foreach (var line in tooltips)
                    {
                        if (line.Text.Contains("Increase healing amount by 1"))
                        {
                            line.Text = "Increase healing amount by 2";
                        }
                    }
                }
            }
        }
    }
}