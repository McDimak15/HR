using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace HomewardRagnarok
{
    public class JellyMushroomRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;
            if (!ModLoader.TryGetMod("ThoriumMod", out Mod thorium)) return;

            if (!coj.TryFind("JellyMushroom", out ModItem jellyMushroom)) return;
            if (!coj.TryFind("MapleMushroom", out ModItem mapleMushroom)) return;
            if (!calamity.TryFind("PurifiedGel", out ModItem purifiedGel)) return;
            if (!calamity.TryFind("StaticRefiner", out ModTile staticRefiner)) return;
            if (!thorium.TryFind("BioMatter", out ModItem bioMatter)) return;

            Recipe recipe = Recipe.Create(jellyMushroom.Type);
            recipe.AddIngredient(mapleMushroom.Type, 1);
            recipe.AddIngredient(purifiedGel.Type, 10);
            recipe.AddIngredient(bioMatter.Type, 3);
            recipe.AddTile(staticRefiner.Type);
            recipe.Register();
        }
    }
}