using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.StrangePotions;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class PotionBloodOrb : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.BloodOrbPotions)
                return;

            int bloodOrb = ModContent.ItemType<BloodOrb>();

            int[] potionsToDuplicate = new int[]
            {
                ModContent.ItemType<FlightPotion>(),
                ModContent.ItemType<HastePotion>(),
                ModContent.ItemType<ReanimationPotion>(),
                ModContent.ItemType<GreedPotion>(),
                ModContent.ItemType<YangPotion>(),
                ModContent.ItemType<YinPotion>()
            };

            foreach (int potionType in potionsToDuplicate)
            {
                Recipe recipe = Recipe.Create(potionType, 2);
                recipe.AddIngredient(potionType, 1);
                recipe.AddIngredient(bloodOrb, 10);
                recipe.AddTile(TileID.AlchemyTable);
                recipe.AddCondition(Condition.DownedSkeletron);

                recipe.Register();
            }
        }
    }
}