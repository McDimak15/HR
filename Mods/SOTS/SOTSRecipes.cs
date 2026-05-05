using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Accessories;
using SOTS.Items;
using SOTS.Items.Fragments;
using SOTS.Items.Inferno;
using SOTS.Items.Permafrost;
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

                if (recipe.createItem.ModItem?.Mod.Name == "ContinentOfJourney")
                {
                    int sightStack = 0;
                    int mightStack = 0;
                    int frightStack = 0;

                    foreach (Item item in recipe.requiredItem)
                    {
                        if (item.type == ItemID.SoulofSight) sightStack = item.stack;
                        else if (item.type == ItemID.SoulofMight) mightStack = item.stack;
                        else if (item.type == ItemID.SoulofFright) frightStack = item.stack;
                    }
                    if (sightStack > 0 && mightStack > 0 && frightStack > 0)
                    {
                        int neededStack = Math.Max(sightStack, Math.Max(mightStack, frightStack));
                        recipe.AddIngredient(ModContent.ItemType<SoulOfPlight>(), neededStack);
                    }

                    // Ancient Blessing
                    if (recipe.createItem.type == ModContent.ItemType<AncientBlessing>())
                    {
                        recipe.AddIngredient(ModContent.ItemType<AlchemistsCharm>());
                        recipe.RemoveIngredient(ItemID.CharmofMyths);
                    }
                }

                if (recipe.createItem.ModItem?.Mod.Name == "CalamityMod")
                {
                    // Chalice Of The Blood God
                    if (recipe.createItem.type == ModContent.ItemType<ChaliceOfTheBloodGod>())
                    {
                        recipe.RemoveIngredient(ModContent.ItemType<AlchemistsCharm>());
                    }
                }

                // Bulwark Of The Ancients
                if (recipe.createItem.type == ModContent.ItemType<BulwarkOfTheAncients>() && !recipe.requiredItem.Any(i => i.type == ModContent.ItemType<AnkhAmulet>()))
                {
                    recipe.AddIngredient(ModContent.ItemType<BottledBlueIce>());
                }

                // PlanebreakersPouch
                if (recipe.createItem.type == ModContent.ItemType<PlanebreakersPouch>() && recipe.requiredItem.Any(i => i.type == ModContent.ItemType<BlazingQuiver>()))
                {
                    recipe.RemoveIngredient(ModContent.ItemType<BlazingQuiver>());
                }

                // FortressGenerator
                if (recipe.createItem.type == ModContent.ItemType<FortressGenerator>())
                {
                    if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
                    {
                        if (thorium.TryFind("SteamkeeperWatch", out ModItem watchItem))
                        {
                            int watchType = watchItem.Type;

                            if (recipe.requiredItem.Any(i => i.type == watchType))
                            {
                                recipe.RemoveIngredient(watchType);
                                recipe.AddIngredient(ModContent.ItemType<ConstructionPDA>());
                            }
                        }
                    }
                }

                // Star Quiver
                if (recipe.createItem.type == ModContent.ItemType<StarQuiver>() && recipe.requiredItem.Any(i => i.type == ItemID.MoltenQuiver))
                {
                    recipe.AddIngredient(ModContent.ItemType<BlazingQuiver>());
                    recipe.RemoveIngredient(ItemID.MoltenQuiver);
                }
            }
        }
    }
}