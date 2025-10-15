using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace HomewardRagnarok
{
    public class BlightRecipeCloner : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("Consolaria", out Mod consolaria))
                return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return;

            int consolariaBlight = consolaria.Find<ModItem>("SoulofBlight")?.Type ?? -1;
            int cojBlight = coj.Find<ModItem>("SoulofBlight")?.Type ?? -1;

            if (consolariaBlight == -1 || cojBlight == -1)
                return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe == null || recipe.requiredItem == null)
                    continue;

                bool needsClone = false;
                foreach (Item ingredient in recipe.requiredItem)
                {
                    if (ingredient != null && ingredient.type == consolariaBlight)
                    {
                        needsClone = true;
                        break;
                    }
                }

                if (needsClone)
                {
                    Recipe newRecipe = recipe.Clone();

                    for (int i = 0; i < newRecipe.requiredItem.Count; i++)
                    {
                        if (newRecipe.requiredItem[i].type == consolariaBlight)
                        {
                            newRecipe.requiredItem[i].SetDefaults(cojBlight);
                            newRecipe.requiredItem[i].stack = recipe.requiredItem[i].stack;
                        }
                    }

                    newRecipe.Register();
                }
            }
        }
    }
}
