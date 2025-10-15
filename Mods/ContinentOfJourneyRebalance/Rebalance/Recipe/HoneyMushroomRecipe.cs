using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class HoneyMushroomRecipeEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!coj.TryFind("HoneyMushroom", out ModItem honeyMushroom)) return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == honeyMushroom.Type)
                {
                    recipe.RemoveIngredient(ItemID.BeeWax);
                }
            }
        }
    }
}
