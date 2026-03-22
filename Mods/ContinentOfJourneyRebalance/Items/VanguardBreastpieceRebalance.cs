using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class VanguardBreastpieceFix : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<VanguardBreastpiece>();
        }

        public override void SetDefaults(Item item)
        {
            item.defense = 8;

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                thorium.Call("AddShieldAccessor", item.type, 2);
            }
        }
    }

    public class VanguardBreastpieceRecipeFix : ModSystem
    {
        public override void PostAddRecipes()
        {
            int vanguardType = ModContent.ItemType<VanguardBreastpiece>();
            int ankhAmuletType = ModContent.ItemType<AnkhAmulet>();

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type != vanguardType) continue;

                if (recipe.requiredItem.Any(i => i.type == ankhAmuletType))
                {
                    recipe.DisableRecipe();
                    continue;
                }
                recipe.RemoveIngredient(ModContent.ItemType<BottledBlueIce>());
            }
        }
    }
}