using Terraria;
using Terraria.ModLoader;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items.Material;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Items.Materials;

namespace HomewardRagnarok.Mods.CrossMod
{
    public class FinalBarRecipe : ModSystem
    {
        public override void PostAddRecipes()
        {
            int finalBarType = ModContent.ItemType<FinalBar>();

            foreach (var r in Main.recipe)
            {
                if (r != null && r.HasResult(finalBarType))
                {
                    r.DisableRecipe();
                }
            }

            Recipe FinalBarRecipe = Recipe.Create(finalBarType);
            FinalBarRecipe.AddIngredient(ModContent.ItemType<FinalOre>(), 7);
            FinalBarRecipe.AddIngredient(ModContent.ItemType<EternalBar>(), 1);
            FinalBarRecipe.AddIngredient(ModContent.ItemType<LivingBar>(), 1);
            FinalBarRecipe.AddIngredient(ModContent.ItemType<CubistBar>(), 1);
            FinalBarRecipe.AddIngredient(ModContent.ItemType<UelibloomBar>(), 1);

            if (ModLoader.TryGetMod("SOTS", out Mod sots))
            {
                FinalBarRecipe.AddIngredient(sots.Find<ModItem>("PhaseBar").Type, 1);
            }

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) && ServerConfig.Instance.ThoriumBalance)
            {
                FinalBarRecipe.AddIngredient(thorium.Find<ModItem>("TerrariumCore").Type, 1);
                FinalBarRecipe.AddIngredient(thorium.Find<ModItem>("LodeStoneIngot").Type, 1);
            }
            else
            {
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyCopperBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyIronBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnySilverBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyGoldBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyCobaltBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyMythrilBarGroup", 1);
                FinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyAdamantiteBarGroup", 1);
            }

            FinalBarRecipe.AddTile(ModContent.TileType<CosmicAnvil>());
            FinalBarRecipe.Register();
        }
    }
}
