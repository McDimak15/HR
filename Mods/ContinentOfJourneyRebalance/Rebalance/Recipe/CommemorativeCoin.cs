using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ContinentOfJourney.Items.Recipes
{
    public class CommemorativeCoinRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ModContent.TryFind<ModItem>("ContinentOfJourney", "CommemorativeCoin", out var coin))
                return;

            Recipe recipe = Recipe.Create(coin.Type);

            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.LuckyCoin, 1);  

            recipe.AddTile(TileID.MythrilAnvil); 
            recipe.Register();
        }
    }
}
