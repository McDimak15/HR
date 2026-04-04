using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using CalamityMod.Items.Accessories;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Mods.Crossmod
{
    public class CrossModRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            bool hasThorium = ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
            bool hasSOTS = ModLoader.TryGetMod("SOTS", out Mod sots);

            int defender = (thorium != null && thorium.TryFind("TerrariumDefender", out ModItem def)) ? def.Type : 0;
            int bulwark = (sots != null && sots.TryFind("BulwarkOfTheAncients", out ModItem bul)) ? bul.Type : 0;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // Vanguard Breastpiece
                if (type == ModContent.ItemType<VanguardBreastpiece>())
                {
                    if (hasThorium)
                    {
                        recipe.AddIngredient(defender);
                        recipe.RemoveIngredient(ItemID.AnkhShield);
                    }
                    else if (hasSOTS && !hasThorium)
                    {
                        recipe.AddIngredient(bulwark);
                        recipe.RemoveIngredient(ItemID.AnkhShield);
                    }
                    recipe.requiredTile.Clear();
                    recipe.AddTile(TileID.LunarCraftingStation);
                }

                // Rampart of Deities 
                if (type == ModContent.ItemType<RampartofDeities>())
                {
                    recipe.AddIngredient(ModContent.ItemType<VanguardBreastpiece>());

                    if (hasThorium)
                    {
                        recipe.RemoveIngredient(defender);
                    }
                    else if (hasSOTS && !hasThorium)
                    {
                        recipe.RemoveIngredient(bulwark);
                    }
                }
            }
        }
    }
}