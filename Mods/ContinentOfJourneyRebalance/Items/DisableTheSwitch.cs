using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok
{
    public class DisableTheSwitchGlobal : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<TheSwitch>();
        }

        public override void SetDefaults(Item item)
        {
            item.useStyle = 0;           
            item.consumable = false;    
            item.value = 0; 
            item.rare = -1;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (tooltips.Count > 1)
            {
                tooltips.RemoveRange(1, tooltips.Count - 1);
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            return false; 
        }

        public override void AddRecipes()
        {
            int targetID = ModContent.ItemType<TheSwitch>();

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe.createItem.type == targetID)
                {
                    recipe.DisableRecipe();
                }
            }
        }
    }
}
