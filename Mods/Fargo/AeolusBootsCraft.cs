using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class RecipeChangesSystem : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
                return;

            if (!fargoMod.TryFind("AeolusBoots", out ModItem aeolusBoots))
                return;

            bool hasSOTS = ModLoader.TryGetMod("SOTS", out Mod sotsMod);
            bool hasCalamity = ModLoader.TryGetMod("CalamityMod", out Mod calamityMod);

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == aeolusBoots.Type)
                {
                    if (hasSOTS && sotsMod.TryFind("SubspaceBoosters", out ModItem subspaceBoosters))
                    {
                        recipe.RemoveIngredient(subspaceBoosters.Type);
                    }
                }
            }
        }
    }
}
