using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class ElementalQuiverPatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int elementalQuiver = calamity.Find<ModItem>("ElementalQuiver")?.Type ?? 0;
            int crossbowScope = coj.Find<ModItem>("CrossbowScope")?.Type ?? 0;

            if (elementalQuiver == 0 || crossbowScope == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == elementalQuiver)
                {
                    if (!recipe.requiredItem.Exists(i => i != null && i.type == crossbowScope))
                        recipe.AddIngredient(crossbowScope);
                }
            }
        }
    }
}
