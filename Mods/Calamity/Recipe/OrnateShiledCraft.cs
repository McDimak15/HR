using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class OrnateShieldPatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int ornateShield = calamity.Find<ModItem>("OrnateShield")?.Type ?? 0;
            int solarPanel = coj.Find<ModItem>("SolarPanel")?.Type ?? 0;

            if (ornateShield == 0 || solarPanel == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == ornateShield)
                {
                    if (!recipe.requiredItem.Exists(i => i != null && i.type == solarPanel))
                        recipe.AddIngredient(solarPanel);
                }
            }
        }
    }
}