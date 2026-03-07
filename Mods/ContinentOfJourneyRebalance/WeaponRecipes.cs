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

namespace HomewardRagnarok
{
    public class CoJCalamityRecipePatches : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            // Direct check for required mods
            if (!ModLoader.HasMod("ContinentOfJourney") || !ModLoader.HasMod("CalamityMod"))
                return;

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe == null || recipe.createItem == null || recipe.createItem.IsAir)
                    continue;

                int resultType = recipe.createItem.type;

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

                // Add Terraheart and remove BladeStaff and PlantationStaff from Legion of Celestia 
                if (resultType == ModContent.ItemType<LegionofCelestia>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<PlantationStaff>());
                    recipe.RemoveIngredient(ItemID.Smolstar);
                    recipe.AddIngredient(ModContent.ItemType<ClottedStaff>());
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