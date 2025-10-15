using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class RecipePatches : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int asgardianAegis = calamity.Find<ModItem>("AsgardianAegis")?.Type ?? 0;
            int transactionCertificate = coj.Find<ModItem>("TransactionCertificate")?.Type ?? 0;

            if (asgardianAegis == 0 || transactionCertificate == 0) return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == asgardianAegis)
                {
                    recipe.AddIngredient(transactionCertificate);
                }
            }
        }
    }
}
