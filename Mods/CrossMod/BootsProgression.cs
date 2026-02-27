using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace HomewardRagnarok
{
    public class BootsProgression : ModSystem
    {
        public override void PostAddRecipes()
        {
            bool cojLoaded = ModLoader.TryGetMod("ContinentOfJourney", out Mod coj);
            bool sotsLoaded = ModLoader.TryGetMod("SOTS", out Mod sots);
            bool thoriumLoaded = ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
            bool calamityLoaded = ModLoader.TryGetMod("CalamityMod", out Mod calamity);
            bool ssmLoaded = ModLoader.TryGetMod("ssm", out Mod ssm);
            bool fargoLoaded = ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo);

            int horizonBootsType = coj.Find<ModItem>("Horizon")?.Type ?? -1;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == horizonBootsType)
                    recipe.DisableRecipe();
            }

            if (sotsLoaded)
            {
                int mantreadsType = coj.Find<ModItem>("GunBoot")?.Type ?? -1;
                int otherworldBarType = coj.Find<ModItem>("AbyssalChunk")?.Type ?? -1;
                int subspaceBoostersType = sots.Find<ModItem>("SubspaceBoosters")?.Type ?? -1;

                if (mantreadsType != -1 && otherworldBarType != -1 && subspaceBoostersType != -1)
                {
                    Recipe newRecipe = Recipe.Create(horizonBootsType);
                    newRecipe.AddIngredient(mantreadsType);
                    newRecipe.AddIngredient(otherworldBarType, 6);
                    newRecipe.AddIngredient(subspaceBoostersType);
                    newRecipe.AddTile(TileID.TinkerersWorkbench);
                    newRecipe.Register();
                }
            }
            else if (thoriumLoaded && !sotsLoaded)
            {
                int mantreadsType = coj.Find<ModItem>("GunBoot")?.Type ?? -1;
                int abyssalChunkType = coj.Find<ModItem>("AbyssalChunk")?.Type ?? -1;
                int terrariumSprintersType = thorium.Find<ModItem>("TerrariumParticleSprinters")?.Type ?? -1;

                if (mantreadsType != -1 && abyssalChunkType != -1 && terrariumSprintersType != -1)
                {
                    Recipe newRecipe = Recipe.Create(horizonBootsType);
                    newRecipe.AddIngredient(terrariumSprintersType);
                    newRecipe.AddIngredient(mantreadsType);
                    newRecipe.AddIngredient(abyssalChunkType, 6);
                    newRecipe.AddTile(TileID.TinkerersWorkbench);
                    newRecipe.Register();
                }
            }

            if (sotsLoaded && calamityLoaded)
            {
                int celestialTracersType = calamity.Find<ModItem>("TracersCelestial")?.Type ?? -1;
                int subspaceBoostersType = sots.Find<ModItem>("SubspaceBoosters")?.Type ?? -1;

                if (celestialTracersType != -1 && subspaceBoostersType != -1)
                {
                    foreach (Recipe recipe in Main.recipe)
                    {
                        if (recipe != null && recipe.createItem.type == celestialTracersType)
                        {
                            for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                            {
                                if (recipe.requiredItem[i].type == subspaceBoostersType)
                                    recipe.requiredItem.RemoveAt(i);
                            }
                            if (!recipe.requiredItem.Any(item => item.type == horizonBootsType))
                                recipe.AddIngredient(horizonBootsType);
                        }
                    }
                }
            }

            if (sotsLoaded && thoriumLoaded && calamityLoaded)
            {
                int celestialTracersType = calamity.Find<ModItem>("TracersCelestial")?.Type ?? -1;
                int terrariumSprintersType = thorium.Find<ModItem>("TerrariumParticleSprinters")?.Type ?? -1;

                if (celestialTracersType != -1 && terrariumSprintersType != -1)
                {
                    foreach (Recipe recipe in Main.recipe)
                    {
                        if (recipe != null && recipe.createItem.type == celestialTracersType)
                        {
                            for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                            {
                                if (recipe.requiredItem[i].type == terrariumSprintersType)
                                    recipe.requiredItem.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            if (!sotsLoaded && calamityLoaded && thoriumLoaded)
            {
                int celestialTracersType = calamity.Find<ModItem>("TracersCelestial")?.Type ?? -1;
                int terrariumSprintersType = thorium.Find<ModItem>("TerrariumParticleSprinters")?.Type ?? -1;

                if (celestialTracersType != -1 && terrariumSprintersType != -1)
                {
                    foreach (Recipe recipe in Main.recipe)
                    {
                        if (recipe != null && recipe.createItem.type == celestialTracersType)
                        {
                            for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                            {
                                if (recipe.requiredItem[i].type == terrariumSprintersType)
                                    recipe.requiredItem.RemoveAt(i);
                            }

                            if (!recipe.requiredItem.Any(item => item.type == horizonBootsType))
                                recipe.AddIngredient(horizonBootsType);
                        }
                    }
                }
            }
        }       
    }
}
