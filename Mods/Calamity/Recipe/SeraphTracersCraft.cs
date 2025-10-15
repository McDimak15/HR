using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Accessories.Wings;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok
{
    public class SeraphTracersRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == ModContent.ItemType<TracersSeraph>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<WingsofRebirth>());

                    if (!recipe.requiredItem.Exists(i => i.type == ModContent.ItemType<Altitude>()))
                    {
                        recipe.AddIngredient(ModContent.ItemType<Altitude>());
                    }
                }
            }
        }
    }
}
