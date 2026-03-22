using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Accessories;
using SOTS.Items;
using SOTS.Items.Fragments;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Mods.SOTS
{
    [JITWhenModsEnabled("SOTS")]
    [ExtendsFromMod("SOTS")]
    public class SOTSRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            // Bulwark Of The Ancients second recipe
            Recipe.Create(ModContent.ItemType<BulwarkOfTheAncients>())
                .AddIngredient(ModContent.ItemType<OlympianAegis>())
                .AddIngredient(ModContent.ItemType<ChiseledBarrier>())
                .AddIngredient(ItemID.ObsidianShield)
                .AddIngredient(ModContent.ItemType<AnkhAmulet>())
                .AddIngredient(ModContent.ItemType<TerminalCluster>())
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }

        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                // Bulwark Of The Ancients
                if (recipe.createItem.type == ModContent.ItemType<BulwarkOfTheAncients>() && !recipe.requiredItem.Any(i => i.type == ModContent.ItemType<AnkhAmulet>()))
                {
                    recipe.AddIngredient(ModContent.ItemType<BottledBlueIce>());
                }
            }
        }
    }
}