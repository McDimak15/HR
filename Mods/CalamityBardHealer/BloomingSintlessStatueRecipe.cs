using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class BloomingSaintessStatueRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityBardHealer", out Mod calamityBardHealer)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;

            int statueType = calamityBardHealer.Find<ModItem>("BloomingSaintessStatue")?.Type ?? 0;
            int saviorsHeartType = coj.Find<ModItem>("SaviorsHeart")?.Type ?? 0;

            if (statueType == 0 || saviorsHeartType == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == statueType)
                {
                    recipe.AddIngredient(saviorsHeartType, 1);
                }
            }
        }
    }
}
