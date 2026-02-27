using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using SOTSBardHealer.Items;
using ContinentOfJourney.Items.Material;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("SOTSBardHealer")]
    [ExtendsFromMod("SOTSBardHealer")]
    public class SOTSRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                if (recipe.createItem.type == ModContent.ItemType<PhaseKarambit>())
                {
                    recipe.AddIngredient(ModContent.ItemType<KnifeBag>());
                }
            }
        }
    }
}