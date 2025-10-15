using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace HomewardRagnarok
{
    public class BerserkersSoulWorldPatch : ModSystem
    {
        public override void OnWorldLoad()
        {
            if (!ModLoader.TryGetMod("ssm", out _)) return;
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod)) return;

            int berserkersSoulType = fargoMod.Find<ModItem>("BerserkerSoul")?.Type ?? 0;
            if (berserkersSoulType == 0) return;

            int blizzardPouchType = ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod)
                ? thoriumMod.Find<ModItem>("BlizzardPouch")?.Type ?? 0
                : 0;

            int philosophersStoneType = ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)
                ? cojMod.Find<ModItem>("PhilosophersStone")?.Type ?? 0
                : 0;

            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == berserkersSoulType))
            {
                if (blizzardPouchType != 0)
                    recipe.RemoveIngredient(blizzardPouchType);
                if (philosophersStoneType != 0)
                    recipe.RemoveIngredient(philosophersStoneType);
            }
        }
    }
}
