using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace HomewardRagnarok
{
    public class AsgardsValorPatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj)) return;

            int asgardsValor = calamity.Find<ModItem>("AsgardsValor")?.Type ?? 0;
            int ankhShield = ItemID.AnkhShield;
            int circuitChip = coj.Find<ModItem>("CircuitChip")?.Type ?? 0;
            int vanguardBreastpiece = coj.Find<ModItem>("VanguardBreastpiece")?.Type ?? 0;

            if (asgardsValor == 0 || circuitChip == 0 || vanguardBreastpiece == 0)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == asgardsValor)
                {
                    recipe.requiredItem.RemoveAll(i => i != null && i.type == ankhShield);

                    if (!recipe.requiredItem.Exists(i => i != null && i.type == circuitChip))
                        recipe.AddIngredient(circuitChip);

                    if (!recipe.requiredItem.Exists(i => i != null && i.type == vanguardBreastpiece))
                        recipe.AddIngredient(vanguardBreastpiece);
                }
            }
        }
    }
}
