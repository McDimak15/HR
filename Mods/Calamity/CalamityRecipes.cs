using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Items.Potions;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Items.Accessories.MeleeExpansion;
using ContinentOfJourney.Items.Material;

namespace HomewardRagnarok
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

                // Asgardian Aegis
                if (type == ModContent.ItemType<AsgardianAegis>())
                {
                    recipe.AddIngredient(ModContent.ItemType<TransactionCertificate>());
                }

                // Asgard's Valor
                if (type == ModContent.ItemType<AsgardsValor>())
                {
                    recipe.requiredItem.RemoveAll(i => i != null && i.type == ItemID.AnkhShield);
                    recipe.AddIngredient(ModContent.ItemType<CircuitChip>());
                    recipe.AddIngredient(ModContent.ItemType<VanguardBreastpiece>());
                }

                // PlanebreakersPouch
                if (type == ModContent.ItemType<PlanebreakersPouch>())
                {
                    recipe.AddIngredient(ModContent.ItemType<CrossbowScope>());
                }

                // Nucleogenesis
                if (type == ModContent.ItemType<Nucleogenesis>())
                {
                    recipe.AddIngredient(ModContent.ItemType<ArmillarySphere>());
                }

                // Ornate Shield
                if (type == ModContent.ItemType<OrnateShield>())
                {
                    recipe.AddIngredient(ModContent.ItemType<SolarPanel>());
                }

                // Rampart of Deities
                if (type == ModContent.ItemType<RampartofDeities>())
                {
                    recipe.AddIngredient(ModContent.ItemType<GrandSpectral>());
                }

                // Seraph Tracers
                if (type == ModContent.ItemType<SeraphTracers>())
                {
                    recipe.RemoveIngredient(ModContent.ItemType<WingsofRebirth>());
                    recipe.AddIngredient(ModContent.ItemType<Altitude>());
                }

                // Statis Curse
                if (type == ModContent.ItemType<StatisCurse>())
                {
                    recipe.AddIngredient(ModContent.ItemType<ConstructionPDA>());
                    recipe.AddIngredient(ModContent.ItemType<LampreyScarf>());
                }

                // Ethereal Talisman
                if (type == ModContent.ItemType<EtherealTalisman>())
                {
                    recipe.AddIngredient(ModContent.ItemType<Negatama>());
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
            }
        }
    }
}