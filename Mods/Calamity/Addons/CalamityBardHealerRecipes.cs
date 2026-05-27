using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items.Accessories;
using CalamityBardHealer.Items;

namespace HomewardRagnarok.Mods.Calamity.Addons
{
    [JITWhenModsEnabled("CalamityBardHealer")]
    [ExtendsFromMod("CalamityBardHealer")]
    public class CalamityBardHealerRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.CalamityBalance && !ServerConfig.Instance.ThoriumBalance)
                return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // Blooming Saintess Statue
                if (type == ModContent.ItemType<BloomingSaintessStatue>())
                {
                    recipe.AddIngredient(ModContent.ItemType<SaviorsHeart>());
                }
            }
        }
    }
}