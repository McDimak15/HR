using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace HomewardRagnarok
{
    public class ElementalGauntletRecipeTweaks : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                return;

            bool ssmLoaded = ModLoader.TryGetMod("SSM", out _);

            int fireGauntlet = ItemID.FireGauntlet;
            int divineTouchType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.DivineTouch>();

            if (!ssmLoaded)
            {
                int elementalGauntletType = calamity.Find<ModItem>("ElementalGauntlet")?.Type ?? 0;
                if (elementalGauntletType == 0)
                    return;

                foreach (Recipe recipe in Main.recipe)
                {
                    if (recipe.createItem.type == elementalGauntletType)
                    {
                        recipe.RemoveIngredient(fireGauntlet);
                        recipe.AddIngredient(divineTouchType);
                    }
                }
            }
            else
            {
                foreach (Recipe recipe in Main.recipe)
                {
                    if (recipe.createItem.type == divineTouchType)
                    {
                        recipe.AddIngredient(fireGauntlet);
                    }
                }
            }
        }
    }
}
