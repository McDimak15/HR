using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items.Material;
using CalamityHunt.Content.Items.Misc;

namespace HomewardRagnarok.Mods.Calamity.Addons
{
    [JITWhenModsEnabled("CalamityHunt")]
    [ExtendsFromMod("CalamityHunt")]
    public class CalamityHuntRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // Pluripotent Spawn Egg
                if (type == ModContent.ItemType<PluripotentSpawnEgg>())
                {
                    recipe.AddIngredient(ModContent.ItemType<SunlightGel>());
                }
            }
        }
    }
}