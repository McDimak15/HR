using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class StatisCursePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int statisCurse = calamity.Find<ModItem>("StatisCurse")?.Type ?? 0;
            int constructionPDA = coj.Find<ModItem>("ConstructionPDA")?.Type ?? 0;
            int lampreyScarf = coj.Find<ModItem>("LampreyScarf")?.Type ?? 0;

            if (statisCurse == 0 || constructionPDA == 0 || lampreyScarf == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == statisCurse)
                {
                    if (!recipe.requiredItem.Exists(i => i != null && i.type == constructionPDA))
                        recipe.AddIngredient(constructionPDA);

                    if (!recipe.requiredItem.Exists(i => i != null && i.type == lampreyScarf))
                        recipe.AddIngredient(lampreyScarf);
                }
            }
        }
    }
}