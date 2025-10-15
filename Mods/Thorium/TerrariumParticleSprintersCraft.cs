using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace HomewardRagnarok
{
    public class RecipeFixesSystem : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                return;

            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
                return;

            if (!thoriumMod.TryFind("TerrariumParticleSprinters", out ModItem sprinters))
                return;

            if (!fargoMod.TryFind("AeolusBoots", out ModItem aeolusBoots))
                return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == sprinters.Type)
                {
                    var ingredients = recipe.requiredItem
                        .Where(i => !i.IsAir)
                        .ToList();

                    recipe.requiredItem.Clear();

                    bool addedBoots = false;
                    foreach (var ingredient in ingredients)
                    {
                        if (ingredient.type == aeolusBoots.Type)
                        {
                            if (!addedBoots)
                            {
                                recipe.AddIngredient(ingredient.type, ingredient.stack); 
                                addedBoots = true;
                            }
                        }
                        else
                        {
                            recipe.AddIngredient(ingredient.type, ingredient.stack);
                        }
                    }
                }
            }
        }
    }
}
