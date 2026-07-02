using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Flamethrowers;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Materials;
using ContinentOfJourney.Items.ThrowerWeapons;
using ContinentOfJourney;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Rebalance
{
    public class WeaponRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<ItemMetalBlade>())
            .AddRecipeGroup(RecipeGroupID.IronBar, 10)
            .AddRecipeGroup("CoJMod:CoJAnySilverBarGroup", 10)
            .AddRecipeGroup(RecipeGroupID.Wood, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            string[] throwerItems = {
                "ItemCactusBall","ItemSolidTornado","SpikyBomb", "ItemCobaltThrowhammer", "ItemCopperThrowhammer",
                "ItemPalladiumThrowhammer", "ItemTinThrowhammer", "ItemLeadBowlingBall", "ItemSilverTomahawk",
                "ItemTungstenTomahawk", "ItemGoldenRang", "ItemPlatinumRang", "ItemBloodyShuriken",
                "ItemEvilShuriken", "ItemMetalBlade", "ConniversKunai"
            };

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                Item craftedItem = recipe.createItem;
                int resultType = craftedItem.type;

                if (recipe == null || recipe.createItem == null || recipe.createItem.IsAir)
                    continue;

                if (craftedItem.ModItem != null && throwerItems.Contains(craftedItem.ModItem.Name))
                {
                    recipe.createItem.stack = 1;
                }

                // Terraheart (ClottedStaff) swap OrganicStaff with PlantationStaff
                if (resultType == ModContent.ItemType<ClottedStaff>())
                {
                    if (recipe.TryGetIngredient(ModContent.ItemType<OrganicStaff>(), out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        recipe.AddIngredient(ModContent.ItemType<PlantationStaff>());
                    }
                }

                // Plantation Staff swap BladeStaff with OrganicStaff
                if (resultType == ModContent.ItemType<PlantationStaff>())
                {
                    if (recipe.TryGetIngredient(ItemID.Smolstar, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        recipe.AddIngredient(ModContent.ItemType<OrganicStaff>());
                    }
                }


                // Planterror Staff (Clamity) swap PlantationStaff with Terraheart 
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

                // Remove ImpStaff from Sinking West
                if (resultType == ModContent.ItemType<SinkingWest>())
                {
                    recipe.RemoveIngredient(ItemID.ImpStaff);
                }

                // Add Vein to Vein Burster
                if (resultType == ModContent.ItemType<VeinBurster>())
                {
                    if (!recipe.HasIngredient(ModContent.ItemType<PerfectDark>()))
                        recipe.AddIngredient(ModContent.ItemType<Vein>());
                }

                // Add Vein to Perfect Dark
                if (resultType == ModContent.ItemType<PerfectDark>())
                {
                    if (!recipe.HasIngredient(ModContent.ItemType<VeinBurster>()))
                        recipe.AddIngredient(ModContent.ItemType<Vein>());
                }

                if (resultType == ModContent.ItemType<ItemCactusBall>())
                {
                    recipe.RemoveIngredient(ItemID.Cactus);
                    recipe.AddIngredient(ItemID.Cactus, 25);
                }

                if (resultType == ModContent.ItemType<ItemCobaltThrowhammer>())
                {
                    recipe.RemoveIngredient(ItemID.CobaltBar);
                    recipe.AddIngredient(ItemID.CobaltBar, 15);
                }

                if (resultType == ModContent.ItemType<ItemPalladiumThrowhammer>())
                {
                    recipe.RemoveIngredient(ItemID.PalladiumBar);
                    recipe.AddIngredient(ItemID.PalladiumBar, 15);
                }

                if (resultType == ModContent.ItemType<ItemCopperThrowhammer>())
                {
                    recipe.RemoveIngredient(ItemID.CopperBar);
                    recipe.AddIngredient(ItemID.CopperBar, 15);
                }

                if (resultType == ModContent.ItemType<ItemTinThrowhammer>())
                {
                    recipe.RemoveIngredient(ItemID.TinBar);
                    recipe.AddIngredient(ItemID.TinBar, 15);
                }

                if (resultType == ModContent.ItemType<ItemSilverTomahawk>())
                {
                    recipe.RemoveIngredient(ItemID.SilverBar);
                    recipe.AddIngredient(ItemID.SilverBar, 12);
                }

                if (resultType == ModContent.ItemType<ItemTungstenTomahawk>())
                {
                    recipe.RemoveIngredient(ItemID.TungstenBar);
                    recipe.AddIngredient(ItemID.TungstenBar, 12);
                }

                if (resultType == ModContent.ItemType<ItemLeadBowlingBall>())
                {
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.AddIngredient(ItemID.LeadBar, 20);
                }

                if (resultType == ModContent.ItemType<ItemGoldenRang>())
                {
                    recipe.RemoveIngredient(ItemID.GoldBar);
                    recipe.AddIngredient(ItemID.GoldBar, 12);
                }

                if (resultType == ModContent.ItemType<ItemPlatinumRang>())
                {
                    recipe.RemoveIngredient(ItemID.PlatinumBar);
                    recipe.AddIngredient(ItemID.PlatinumBar, 12);
                }

                if (resultType == ModContent.ItemType<ItemEvilShuriken>())
                {
                    recipe.RemoveIngredient(ItemID.DemoniteBar);
                    recipe.AddIngredient(ItemID.DemoniteBar, 13);
                }

                if (resultType == ModContent.ItemType<ItemBloodyShuriken>())
                {
                    recipe.RemoveIngredient(ItemID.CrimtaneBar);
                    recipe.AddIngredient(ItemID.CrimtaneBar, 12);
                }

                if (resultType == ModContent.ItemType<ItemSolidTornado>())
                {
                    recipe.RemoveIngredient(ItemID.Cloud);
                    recipe.RemoveIngredient(ItemID.RainCloud);
                    recipe.AddIngredient(ItemID.Cloud, 20);
                    recipe.AddIngredient(ItemID.RainCloud, 10);
                }


                if (resultType == ModContent.ItemType<ItemMetalBlade>() && recipe.Mod.Name == "ContinentOfJourney")
                {
                    recipe.DisableRecipe();
                }

                // Wildfire 
                if (resultType == ModContent.ItemType<FT2Wildfire>())
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.RemoveIngredient(ItemID.RichMahogany);
                    recipe.AddIngredient(ModContent.ItemType<FT1Sparkthrower>());
                }

                // Wildfire Bloom 
                if (resultType == ModContent.ItemType<WildfireBloom>())
                {
                    if (recipe.TryGetIngredient(ItemID.Flamethrower, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        recipe.AddIngredient(ModContent.ItemType<FT2Wildfire>());
                    }
                }

                // Add 3x Dark Plasma to Denial, Acceptance, Anger, Depression
                if (resultType == ModContent.ItemType<Denial>() ||
                    resultType == ModContent.ItemType<Acceptance>() ||
                    resultType == ModContent.ItemType<TrueDawnsBorder>() ||
                    resultType == ModContent.ItemType<Depression>())
                {
                    if (!recipe.HasIngredient(ModContent.ItemType<DarkPlasma>()))
                    {
                        recipe.AddIngredient(ModContent.ItemType<DarkPlasma>(), 3);
                    }
                }
            }
        }
    }
}