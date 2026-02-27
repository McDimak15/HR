using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Compat
{
    public class AltitudePatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<Altitude>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            base.UpdateAccessory(item, player, hideVisual);

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (calamity.TryFind("WingsofRebirth", out ModItem rebirthWings))
                {
                    rebirthWings.UpdateAccessory(player, hideVisual);
                }
            }
        }

        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                return;

            if (!calamity.TryFind("WingsofRebirth", out ModItem rebirthWings))
                return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.TryGetResult(ModContent.ItemType<Altitude>(), out _))
                {
                    recipe.AddIngredient(rebirthWings.Type);
                }
            }
        }
    }
}
