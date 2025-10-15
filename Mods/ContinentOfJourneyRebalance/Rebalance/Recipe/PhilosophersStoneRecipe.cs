using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class PhilosophersStoneRecipeEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) ||
                !coj.TryFind("PhilosophersStone", out ModItem philosophersStone))
                return;

            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity) ||
                !calamity.TryFind("CosmicAnvil", out ModTile cosmicAnvil))
                return;

            int philosophersStoneType = philosophersStone.Type;
            int cosmicAnvilType = cosmicAnvil.Type;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.HasResult(philosophersStoneType))
                {
                    recipe.requiredTile.Clear();
                    recipe.AddTile(cosmicAnvilType);
                }
            }
        }
    }
}
