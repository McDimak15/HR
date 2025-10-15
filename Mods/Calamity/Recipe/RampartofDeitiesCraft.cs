using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class RampartOfDeitiesPatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int rampartOfDeities = calamity.Find<ModItem>("RampartofDeities")?.Type ?? 0;
            int grandSpectral = coj.Find<ModItem>("GrandSpectral")?.Type ?? 0;

            if (rampartOfDeities == 0 || grandSpectral == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == rampartOfDeities)
                {
                    if (!recipe.requiredItem.Exists(i => i != null && i.type == grandSpectral))
                        recipe.AddIngredient(grandSpectral);
                }
            }
        }
    }
}