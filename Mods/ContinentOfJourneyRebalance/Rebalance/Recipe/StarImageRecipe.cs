using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class StarImagePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int starImage = coj.Find<ModItem>("StarImage")?.Type ?? 0;
            int essenceOfMatter = coj.Find<ModItem>("EssenceofMatter")?.Type ?? 0;

            if (starImage == 0 || essenceOfMatter == 0) return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == starImage)
                {
                    recipe.requiredItem.RemoveAll(i => i != null && i.type == essenceOfMatter);
                }
            }
        }
    }
}
