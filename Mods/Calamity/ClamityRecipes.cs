using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using Clamity.Content.Items.Accessories;
using ContinentOfJourney.Items.Material;

namespace HomewardRagnarok.Mods.Calamity
{
    [JITWhenModsEnabled("Clamity")]
    [ExtendsFromMod("Clamity")]
    public class ClamityRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.ClamityBalance)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // Supreme Barrier
                if (type == ModContent.ItemType<SupremeBarrier>())
                {
                    recipe.AddIngredient(ModContent.ItemType<EssenceofBright>(), 5);
                }
            }
        }
    }
}