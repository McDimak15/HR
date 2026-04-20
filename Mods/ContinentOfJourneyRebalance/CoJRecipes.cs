using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.ThrowerWeapons;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Placables;
using ContinentOfJourney.Items.Accessories.MechArms;
using ContinentOfJourney.Items.Flamethrowers;
using ContinentOfJourney.Tiles;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;

namespace HomewardRagnarok
{
    public class CoJRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.HWJRecipes)
                return;

            Recipe.Create(ModContent.ItemType<CommemorativeCoin>())
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.LuckyCoin, 1)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            Recipe.Create(ModContent.ItemType<WoodenClaw>())
                .AddRecipeGroup(RecipeGroupID.Wood, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            Recipe.Create(ModContent.ItemType<UmbroShield>())
                .AddIngredient(ModContent.ItemType<SimpleShield>())
                .AddIngredient(ModContent.ItemType<SwollenStar>(), 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            Recipe.Create(ModContent.ItemType<TransactionCertificate>())
                .AddIngredient(ItemID.Silk, 10)
                .AddIngredient(ItemID.Bone, 5)
                .AddIngredient(ModContent.ItemType<SwollenStar>(), 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void PostAddRecipes()
        {
            bool doHWJ = ServerConfig.Instance.HWJRecipes;

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe == null || recipe.createItem == null || recipe.createItem.IsAir)
                    continue;

                int resultType = recipe.createItem.type;

                if (doHWJ)
                {
                    if (resultType == ModContent.ItemType<HoneyMushroom>())
                        recipe.RemoveIngredient(ItemID.BeeWax);

                    if (resultType == ModContent.ItemType<StarImage>())
                        recipe.requiredItem.RemoveAll(ing => ing.type == ModContent.ItemType<EssenceofMatter>());

                    if (resultType == ModContent.ItemType<ContinentOfJourney.Items.Placables.SpongeBlock>())
                    {
                        recipe.requiredItem.RemoveAll(ing => ing.type == ModContent.ItemType<EssenceofNothingness>());
                        recipe.AddIngredient(ModContent.ItemType<EssenceofDeath>());
                    }

                    if (resultType == ModContent.ItemType<BumpAttack>())
                    {
                        recipe.requiredItem.RemoveAll(ing => ing.type == ModContent.ItemType<EssenceofDeath>());
                        recipe.AddIngredient(ModContent.ItemType<EssenceofBright>(), 5);
                    }

                    if (resultType == ModContent.ItemType<SurvivalCrisis>())
                        recipe.AddRecipeGroup("Boss2Material", 10);

                    if (resultType == ModContent.ItemType<BatNecklace>())
                    {
                        recipe.RemoveIngredient(ItemID.PygmyNecklace);
                        recipe.AddIngredient(ModContent.ItemType<StatisBlessing>());
                        recipe.AddIngredient(ModContent.ItemType<Necroplasm>(), 4);
                    }

                    if (resultType == ModContent.ItemType<DivineNecklace>())
                    {
                        recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 5);
                        recipe.AddIngredient(ModContent.ItemType<UnholyEssence>(), 3);
                    }

                    if (resultType == ModContent.ItemType<Horizon>() || resultType == ModContent.ItemType<CrossbowScope>())
                        recipe.DisableRecipe();

                    if (resultType == ModContent.ItemType<ClottedStaff>())
                    {
                        if (recipe.TryGetIngredient(ModContent.ItemType<OrganicStaff>(), out Item ing))
                        {
                            recipe.RemoveIngredient(ing.type);
                            recipe.AddIngredient(ModContent.ItemType<PlantationStaff>());
                        }
                    }

                    if (resultType == ModContent.ItemType<PlantationStaff>())
                    {
                        if (recipe.TryGetIngredient(ItemID.Smolstar, out Item ing))
                        {
                            recipe.RemoveIngredient(ing.type);
                        }
                        recipe.AddIngredient(ModContent.ItemType<OrganicStaff>());
                    }

                    if (ModLoader.TryGetMod("Clamity", out Mod clamity))
                    {
                        if (clamity.TryFind("PlanterrorStaff", out ModItem planterror) && resultType == planterror.Type)
                        {
                            if (recipe.TryGetIngredient(ModContent.ItemType<PlantationStaff>(), out Item ing))
                            {
                                recipe.RemoveIngredient(ing.type);
                                recipe.AddIngredient(ModContent.ItemType<ClottedStaff>());
                            }
                        }
                    }

                    if (resultType == ModContent.ItemType<SinkingWest>())
                        recipe.RemoveIngredient(ItemID.ImpStaff);

                    if (resultType == ModContent.ItemType<VeinBurster>() || resultType == ModContent.ItemType<PerfectDark>())
                        recipe.AddIngredient(ModContent.ItemType<Vein>());

                    if (resultType == ModContent.ItemType<FT2Wildfire>())
                    {
                        recipe.RemoveIngredient(ItemID.IronBar);
                        recipe.RemoveIngredient(ItemID.LeadBar);
                        recipe.RemoveIngredient(ItemID.RichMahogany);
                        recipe.AddIngredient(ModContent.ItemType<FT1Sparkthrower>());
                    }

                    if (resultType == ModContent.ItemType<Denial>() || resultType == ModContent.ItemType<Acceptance>() ||
                        resultType == ModContent.ItemType<TrueDawnsBorder>() || resultType == ModContent.ItemType<Depression>())
                    {
                        if (!recipe.HasIngredient(ModContent.ItemType<DarkPlasma>()))
                            recipe.AddIngredient(ModContent.ItemType<DarkPlasma>(), 3);
                    }
                }
            }

            if (doHWJ)
            {
                Recipe.Create(ModContent.ItemType<CrossbowScope>())
                    .AddIngredient(ModContent.ItemType<StarQuiver>())
                    .AddIngredient(ModContent.ItemType<MachinaScope>())
                    .AddIngredient(ModContent.ItemType<TankOfThePastCorruption>(), 6)
                    .AddTile<ContinentOfJourney.Tiles.FinalAnvil>()
                    .Register();

                Recipe.Create(ModContent.ItemType<CrossbowScope>())
                    .AddIngredient(ModContent.ItemType<StarQuiver>())
                    .AddIngredient(ModContent.ItemType<MachinaScope>())
                    .AddIngredient(ModContent.ItemType<TankOfThePastCrimson>(), 6)
                    .AddTile<ContinentOfJourney.Tiles.FinalAnvil>()
                    .Register();

                Recipe.Create(ModContent.ItemType<Horizon>())
                    .AddIngredient(ModContent.ItemType<VoidStriders>())
                    .AddIngredient(ModContent.ItemType<TankOfThePastJungle>(), 6)
                    .AddIngredient(ModContent.ItemType<FinalBar>())
                    .AddTile<ContinentOfJourney.Tiles.FinalAnvil>()
                    .Register();
            }
        }
    }
}