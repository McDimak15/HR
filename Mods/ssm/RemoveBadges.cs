using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class ClericBadgeSystem : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (ModLoader.TryGetMod("SSM", out Mod ssm))
            {
                ModItem healerBadge = ssm.Find<ModItem>("HealerBadge");

                ModItem BardBadge = ssm.Find<ModItem>("BardBadge");

                if (healerBadge != null)
                {
                    int healerBadgeType = healerBadge.Type;
                    int BardBadgeType = BardBadge.Type;

                    foreach (var recipe in Main.recipe)
                    {
                        if (recipe != null && recipe.createItem.type == healerBadgeType)
                        {
                            recipe.DisableRecipe();
                        }
                        if (recipe != null && recipe.createItem.type == BardBadgeType)
                        {
                            recipe.DisableRecipe();
                        }
                    }

                    foreach (var item in Main.item)
                    {
                        if (item != null && item.type == healerBadgeType)
                        {
                            item.TurnToAir();
                        }
                        if (item != null && item.type == BardBadgeType)
                        {
                            item.TurnToAir();
                        }
                    }
                }
            }
        }
    }
}
