using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class FungusDeluxeRecipeEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;

            if (!coj.TryFind("FungusDeluxe", out ModItem fungusDeluxe)) return;
            if (!coj.TryFind("JellyMushroom", out ModItem jellyMushroom)) return;
            if (!calamity.TryFind("CosmicAnvil", out ModTile cosmicAnvil)) return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == fungusDeluxe.Type)
                {
                    recipe.requiredTile.Clear();
                    recipe.AddIngredient(jellyMushroom.Type, 1);
                    recipe.AddTile(cosmicAnvil.Type);
                }
            }
        }
    }
}
