using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace HomewardRagnarok
{
    public class ColossusSoulRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (ModLoader.TryGetMod("FargowiltasSouls", out var fargo) &&
                ModLoader.TryGetMod("ContinentOfJourney", out var coj) &&
                ModLoader.TryGetMod("ThoriumMod", out var thor))
            {
                int colossusSoul = fargo.Find<ModItem>("ColossusSoul")?.Type ?? -1;
                int ancientBlessing = coj.Find<ModItem>("AncientBlessing")?.Type ?? -1;
                int blastshield = thor.Find<ModItem>("BlastShield")?.Type ?? -1;
                int negablastshield = ModContent.ItemType<Items.Accessories.NegablastShield>();

                if (colossusSoul != -1 && ancientBlessing != -1)
                {
                    foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == colossusSoul))
                    {
                        if (recipe.createItem.type == colossusSoul)
                        {
                            recipe.AddIngredient(ancientBlessing);
                        }
                        if (recipe.requiredItem.Any(i => i.type == blastshield))
                        {
                            recipe.requiredItem.RemoveAll(i => i.type == blastshield);
                            recipe.AddIngredient(negablastshield);
                        }
                    }
                }
            }
        }
    }
}
