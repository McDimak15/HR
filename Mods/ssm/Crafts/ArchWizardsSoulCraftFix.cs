using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace HomewardRagnarok
{
    public class ArchWizardSoulPatch : ModSystem
    {
        public override void OnWorldLoad()
        {
            if (!ModLoader.TryGetMod("ssm", out _)) return;
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)) return;

            int archWizardSoulType = fargoMod.Find<ModItem>("ArchWizardsSoul")?.Type ?? 0;
            int starflowerType = cojMod.Find<ModItem>("Starflower")?.Type ?? 0;

            if (archWizardSoulType == 0 || starflowerType == 0) return;

            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == archWizardSoulType))
            {
                recipe.RemoveIngredient(starflowerType);
            }
        }
    }
}
