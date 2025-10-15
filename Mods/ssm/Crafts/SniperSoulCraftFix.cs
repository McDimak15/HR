using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace HomewardRagnarok
{
    public class SniperSoulPatch : ModSystem
    {
        public override void OnWorldLoad()
        {
            if (!ModLoader.TryGetMod("ssm", out _)) return;
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)) return;

            int sniperSoulType = fargoMod.Find<ModItem>("SnipersSoul")?.Type ?? 0;
            int crossbowScopeType = cojMod.Find<ModItem>("CrossbowScope")?.Type ?? 0;
            int theBattlerType = cojMod.Find<ModItem>("TheBatter")?.Type ?? 0;

            if (sniperSoulType == 0) return;

            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == sniperSoulType))
            {
                if (crossbowScopeType != 0)
                    recipe.RemoveIngredient(crossbowScopeType);

                if (theBattlerType != 0)
                    recipe.RemoveIngredient(theBattlerType);
            }
        }
    }
}
