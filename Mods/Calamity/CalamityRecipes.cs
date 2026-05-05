using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Items.Potions;
using CalamityMod.Systems;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Accessories.MeleeExpansion;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Flamethrowers;

namespace HomewardRagnarok.Mods.Calamity
{
    public class CalamityRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            // Alpha
            Recipe.Create(ModContent.ItemType<Alpha>())
                .AddIngredient(ItemID.JungleSpores, 6)
                .AddIngredient(ItemID.Vine, 2)
                .AddTile(TileID.Anvils)
                .Register();

            // Omega
            Recipe.Create(ModContent.ItemType<Omega>())
                .AddIngredient(ModContent.ItemType<EssenceofEleum>(), 5)
                .AddIngredient(ModContent.ItemType<CryonicBar>(), 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();

            // Epsilon including Thorium 
            int epsilonTile = ModContent.TileType<CosmicAnvil>();
            Recipe epsilonRecipe = Recipe.Create(ModContent.ItemType<Epsilon>())
                .AddIngredient(ModContent.ItemType<LifeAlloy>(), 3);
            if (ModLoader.HasMod("ThoriumMod"))
            {
                epsilonTile = ModLoader.GetMod("ThoriumMod").Find<ModTile>("SoulForge").Type;
                epsilonRecipe.AddIngredient(ModLoader.GetMod("ThoriumMod").Find<ModItem>("DreadSoul").Type, 5);
            }
            epsilonRecipe.AddTile(epsilonTile);
            epsilonRecipe.Register();
        }

        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            // Blood Orb
            Recipe bloodRecipe = Recipe.Create(ModContent.ItemType<BloodOrb>());
            bloodRecipe.AddIngredient(ModContent.ItemType<Blood>(), 1);
            bloodRecipe.Register();

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem == null) continue;

                int type = recipe.createItem.type;

                // PlanebreakersPouch
                if (type == ModContent.ItemType<PlanebreakersPouch>())
                {
                    recipe.RemoveRecipeGroup(RecipeSystem.AnyQuiver);
                    recipe.RemoveIngredient(ItemID.MagicQuiver);
                    recipe.AddIngredient(ModContent.ItemType<CrossbowScope>());
                }

                // Elemental Gauntlet
                if (type == ModContent.ItemType<ElementalGauntlet>())
                {
                    recipe.RemoveIngredient(ItemID.FireGauntlet);
                    recipe.AddIngredient(ModContent.ItemType<DivineTouch>());
                }

                // Rampart of Deities
                if (type == ModContent.ItemType<RampartofDeities>())
                {
                    recipe.AddIngredient(ModContent.ItemType<GrandSpectral>());
                }

                // Seraph Tracers
                if (type == ModContent.ItemType<SeraphTracers>())
                {
                    recipe.AddIngredient(ModContent.ItemType<Horizon>());
                    recipe.AddIngredient(ModContent.ItemType<Edgewalker>());
                    recipe.RemoveIngredient(ModContent.ItemType<VoidStriders>());
                }

                // Abyssal Diving Suit
                if (type == ModContent.ItemType<AbyssalDivingSuit>())
                {
                    recipe.AddIngredient(ModContent.ItemType<AbyssCore>());
                }

                // Star-Tainted Generator
                if (type == ModContent.ItemType<StarTaintedGenerator>())
                {
                    recipe.AddIngredient(ModContent.ItemType<SoulofBlight>(), 10);
                }

                // Ancient Blessing
                if (type == ModContent.ItemType<AncientBlessing>())
                {
                    recipe.AddIngredient(ModContent.ItemType<TrinketofChi>());
                    recipe.RemoveIngredient(ItemID.CelestialShell);
                }

                // Chalice Of The Blood God
                if (recipe.createItem.type == ModContent.ItemType<ChaliceOfTheBloodGod>())
                {
                    recipe.AddIngredient(ModContent.ItemType<AncientBlessing>());
                }

                // Statis Curse
                if (type == ModContent.ItemType<StatisCurse>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<StatisBlessing>());
                    recipe.RemoveIngredient(ModContent.ItemType<Necroplasm>());
                    recipe.AddIngredient(ModContent.ItemType<DivineNecklace>());
                    recipe.AddIngredient(ModContent.ItemType<EssenceofNothingness>(), 6);
                }

                // Sigil of Calamitas
                if (type == ModContent.ItemType<SigilofCalamitas>())
                {
                    recipe.AddIngredient(ModContent.ItemType<Negatama>());
                }

                // Ethereal Talisman
                if (type == ModContent.ItemType<EtherealTalisman>())
                {
                    recipe.RemoveIngredient(ItemID.ManaFlower);
                    recipe.AddIngredient(ModContent.ItemType<Starflower>());
                }

                // Knifes
                if (type == ModContent.ItemType<FeatherKnife>() ||
                    type == ModContent.ItemType<WulfrumKnife>() ||
                    type == ModContent.ItemType<CalamityMod.Items.Weapons.Rogue.MythrilKnife>() ||
                    type == ModContent.ItemType<TimeBolt>())
                {
                    recipe.AddIngredient(ModContent.ItemType<KnifeBag>());
                }

                // Fungus Deluxe
                if (type == ModContent.ItemType<FungusDeluxe>())
                {
                    recipe.requiredTile.Clear();
                    recipe.AddIngredient(ModContent.ItemType<JellyMushroom>());
                    recipe.AddTile(ModContent.TileType<CosmicAnvil>());
                }

                // Philosophers Stone 
                if (type == ModContent.ItemType<PhilosophersStone>())
                {
                    recipe.requiredTile.Clear();
                    recipe.AddTile(ModContent.TileType<CosmicAnvil>());
                }

                // The Holy Trinity 
                if (type == ModContent.ItemType<TheHolyTrinity>())
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ItemID.Ectoplasm);
                    recipe.AddIngredient(ModContent.ItemType<Necroplasm>(), 7);
                }

                // Red Cube
                if (type == ModContent.ItemType<RedCube>())
                {
                    recipe.AddIngredient(ModContent.ItemType<Lumenyl>(), 5);
                }

                // Shadowspec Bar
                if (type == ModContent.ItemType<ShadowspecBar>())
                {
                    recipe.AddIngredient(ModContent.ItemType<EssenceofDeath>());
                }

                // Cold Blood
                if (type == ModContent.ItemType<ColdBlood>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<TankOfThePastSnowland>());
                    recipe.AddIngredient(ModContent.ItemType<UnholyEssence>(), 6);
                }

                // Miracle Matter
                if (type == ModContent.ItemType<MiracleMatter>())
                {
                    if (ModLoader.HasMod("ThoriumMod"))
                    {
                        recipe.requiredItem.RemoveAll(i => i != null && i.type == ModLoader.GetMod("ThoriumMod").Find<ModItem>("TerrariumCore").Type);
                    }
                    recipe.AddIngredient(ModContent.ItemType<FinalBar>(), 2);
                }

                // Ultra Healing Potion
                if (type == ModContent.ItemType<UltraHealingPotion>())
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ItemID.BottledWater);
                    recipe.AddIngredient(ModContent.ItemType<OmegaHealingPotion>());
                }

                // Ultra Mana Potion
                if (type == ModContent.ItemType<UltraManaPotion>())
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ItemID.BottledWater);
                    recipe.AddIngredient(ItemID.SuperManaPotion);
                }

                // Supreme Mana Potion
                if (type == ModContent.ItemType<SupremeManaPotion>())
                {
                    recipe.requiredItem.RemoveAll(i => i.type == ItemID.SuperManaPotion);
                    recipe.AddIngredient(ModContent.ItemType<UltraManaPotion>());
                }

                // Wildfire
                if (type == ModContent.ItemType<WildfireBloom>())
                {
                    if (recipe.TryGetIngredient(ItemID.Flamethrower, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        recipe.AddIngredient(ModContent.ItemType<FT2Wildfire>());
                    }
                }

                // Legion of Celestia
                if (type == ModContent.ItemType<LegionofCelestia>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<PlantationStaff>());
                    recipe.RemoveIngredient(ItemID.Smolstar);
                    recipe.AddIngredient(ModContent.ItemType<ClottedStaff>());
                }
            }
        }
    }
}