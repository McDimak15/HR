using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class CrossbowScopeRecipeEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) ||
                !coj.TryFind("CrossbowScope", out ModItem crossbowScope))
            {
                return;
            }

            int crossbowScopeType = crossbowScope.Type;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.HasResult(crossbowScopeType))
                {
                    recipe.DisableRecipe();
                }
            }

            if (!coj.TryFind("StarQuiver", out ModItem starQuiver) ||
                !coj.TryFind("MachinaScope", out ModItem machinaScope) ||
                !coj.TryFind("EternalBar", out ModItem eternalBar) ||
                !coj.TryFind("SolarFlareScoria", out ModItem solarFlareScoria))
            {
                return;
            }

            Recipe newRecipe = Recipe.Create(crossbowScopeType);
            newRecipe.AddIngredient(starQuiver.Type, 1);
            newRecipe.AddIngredient(machinaScope.Type, 1);
            newRecipe.AddIngredient(eternalBar.Type, 6);
            newRecipe.AddIngredient(solarFlareScoria.Type, 12);
            newRecipe.AddTile(TileID.LunarCraftingStation);
            newRecipe.Register();
        }
    }
}
