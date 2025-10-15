using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace HomewardRagnarok
{
    public class BootsProgressionFixFargo : ModSystem
    {
        public override void OnWorldLoad()
        {
            Mod calamity = null;
            Mod thorium = null;
            Mod fargo = null;

            ModLoader.TryGetMod("CalamityMod", out calamity);
            ModLoader.TryGetMod("ThoriumMod", out thorium);
            ModLoader.TryGetMod("FargowiltasSouls", out fargo);

            if (fargo == null && calamity != null && thorium != null)
            {
                int celestialTracersType = calamity.TryFind("TracersCelestial", out ModItem celestialItem) ? celestialItem.Type : -1;
                int terrariumSprintersType = thorium.TryFind("TerrariumParticleSprinters", out ModItem sprintersItem) ? sprintersItem.Type : -1;

                if (celestialTracersType == -1 || terrariumSprintersType == -1)
                    return;

                foreach (Recipe recipe in Main.recipe)
                {
                    if (recipe == null || recipe.requiredItem == null)
                        continue;

                    if (recipe.createItem.type == celestialTracersType)
                    {
                        for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                        {
                            if (recipe.requiredItem[i].type == terrariumSprintersType)
                                recipe.requiredItem.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
