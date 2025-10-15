using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class NucleogenesisPatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int nucleogenesis = calamity.Find<ModItem>("Nucleogenesis")?.Type ?? 0;
            int armillarySphere = coj.Find<ModItem>("ArmillarySphere")?.Type ?? 0;

            if (nucleogenesis == 0 || armillarySphere == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == nucleogenesis)
                {
                    if (!recipe.requiredItem.Exists(i => i != null && i.type == armillarySphere))
                        recipe.AddIngredient(armillarySphere);
                }
            }
        }
    }
}
