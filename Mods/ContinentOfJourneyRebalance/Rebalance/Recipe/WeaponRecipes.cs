using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace HomewardRagnarok
{
    public class CoJCalamityRecipePatches : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;
            if (!ModLoader.TryGetMod("Clamity", out Mod clamity)) return;

            // CoJ
            int terraheart = coj.Find<ModItem>("ClottedStaff")?.Type ?? 0;
            int organicStaffCoJ = coj.Find<ModItem>("OrganicStaff")?.Type ?? 0;
            int VeinCoJ = coj.Find<ModItem>("Vein")?.Type ?? 0;
            int wildfireCoJ = coj.Find<ModItem>("FT2Wildfire")?.Type ?? 0;
            int sparkthrowerCoJ = coj.Find<ModItem>("FT1Sparkthrower")?.Type ?? 0;

            // Calamity
            int plantationStaffCalamity = calamity.Find<ModItem>("PlantationStaff")?.Type ?? 0;
            int elementalAxe = calamity.Find<ModItem>("ElementalAxe")?.Type ?? 0;
            int veinBurster = calamity.Find<ModItem>("VeinBurster")?.Type ?? 0;
            int perfectDark = calamity.Find<ModItem>("PerfectDark")?.Type ?? 0;
            int wildfireBloomCalamity = calamity.Find<ModItem>("WildfireBloom")?.Type ?? 0;

            // Clamity
            int planterrorStaff = clamity.Find<ModItem>("PlanterrorStaff")?.Type ?? 0;

            // Terraria
            int bladeStaffVanilla = ItemID.Smolstar;
            int flamethrowerVanilla = ItemID.Flamethrower;
            int ironBarVanilla = ItemID.IronBar;
            int richMahoganyVanilla = ItemID.RichMahogany;

            // Terraheart swap OrganicStaff with PlantationStaff
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == terraheart))
            {
                if (organicStaffCoJ != 0 && recipe.requiredItem.Any(i => i.type == organicStaffCoJ))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == organicStaffCoJ);
                    if (plantationStaffCalamity != 0)
                        recipe.AddIngredient(plantationStaffCalamity);
                }
            }

            // Plantation Staff swap BladeStaff with OrganicStaff
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == plantationStaffCalamity))
            {
                if (recipe.requiredItem.Any(i => i.type == bladeStaffVanilla))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == bladeStaffVanilla);
                    if (organicStaffCoJ != 0)
                        recipe.AddIngredient(organicStaffCoJ);
                }
            }

            // Elemental Axe swap PlantationStaff with Terraheart
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == elementalAxe))
            {
                if (recipe.requiredItem.Any(i => i.type == plantationStaffCalamity))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == plantationStaffCalamity);
                    if (terraheart != 0)
                        recipe.AddIngredient(terraheart);
                }
            }

            // Planterror Staff swap PlantationStaff with Terraheart
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == planterrorStaff))
            {
                if (recipe.requiredItem.Any(i => i.type == plantationStaffCalamity))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == plantationStaffCalamity);
                    if (terraheart != 0)
                        recipe.AddIngredient(terraheart);
                }
            }

            // Add Vein to Vein Burster
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == veinBurster))
            {
                if (!recipe.requiredItem.Any(i => i.type == perfectDark))
                {
                    recipe.AddIngredient(VeinCoJ);
                }
            }

            // Add Vein to Perfect Dark
            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == perfectDark))
            {
                if (!recipe.requiredItem.Any(i => i.type == veinBurster))
                {
                    recipe.AddIngredient(VeinCoJ);
                }
            }

            // Wildfire  - remove Iron Bar and Rich Mahogany, add Sparkthrower
            if (wildfireCoJ != 0)
            {
                foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == wildfireCoJ))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ironBarVanilla || i.type == richMahoganyVanilla);
                    if (sparkthrowerCoJ != 0 && !recipe.requiredItem.Any(i => i.type == sparkthrowerCoJ))
                        recipe.AddIngredient(sparkthrowerCoJ);
                }
            }

            // Wildfire Bloom - remove Flamethrower, add Wildfire
            if (wildfireBloomCalamity != 0 && wildfireCoJ != 0)
            {
                foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == wildfireBloomCalamity))
                {
                    recipe.requiredItem.RemoveAll(i => i.type == flamethrowerVanilla);
                    if (!recipe.requiredItem.Any(i => i.type == wildfireCoJ))
                        recipe.AddIngredient(wildfireCoJ);
                }
            }
        }
    }
}
