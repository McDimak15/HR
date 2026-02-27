using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class FinalBarRecipeEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) ||
                !coj.TryFind("FinalBar", out ModItem finalBar))
            {
                return;
            }

            int finalBarType = finalBar.Type;

            foreach (var r in Main.recipe)
            {
                if (r != null && r.HasResult(finalBarType))
                {
                    r.DisableRecipe();
                }
            }

            Recipe newFinalBarRecipe = Recipe.Create(finalBarType);

            newFinalBarRecipe.AddIngredient(coj.Find<ModItem>("EternalBar").Type, 1);
            newFinalBarRecipe.AddIngredient(coj.Find<ModItem>("LivingBar").Type, 1);
            newFinalBarRecipe.AddIngredient(coj.Find<ModItem>("CubistBar").Type, 1);
            newFinalBarRecipe.AddIngredient(coj.Find<ModItem>("FinalOre").Type, 7);


            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && ServerConfig.Instance.CalamityBalance)
            {
                newFinalBarRecipe.AddIngredient(calamity.Find<ModItem>("LifeAlloy").Type, 1);
                newFinalBarRecipe.AddTile(calamity.Find<ModTile>("CosmicAnvil").Type);
            }
            else
            {
                newFinalBarRecipe.AddTile(TileID.LunarCraftingStation);
            }

            if (ModLoader.TryGetMod("SOTS", out Mod sots))
            {
                newFinalBarRecipe.AddIngredient(sots.Find<ModItem>("FrigidBar").Type, 1);
            }

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) && ServerConfig.Instance.ThoriumBalance)
            {
                newFinalBarRecipe.AddIngredient(thorium.Find<ModItem>("TerrariumCore").Type, 1);
            }
            else
            {
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyCopperBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyIronBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnySilverBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyGoldBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyCobaltBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyMythrilBarGroup", 1);
                newFinalBarRecipe.AddRecipeGroup("CoJMod:CoJAnyAdamantiteBarGroup", 1);
            }

            newFinalBarRecipe.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && calamityMod.TryFind("ShadowspecBar", out ModItem shadowspecBar) && ServerConfig.Instance.CalamityBalance)
            {
                int shadowspecBarType = shadowspecBar.Type;

                foreach (var rec in Main.recipe)
                {
                    if (rec != null && rec.HasResult(shadowspecBarType))
                    {
                        bool hasFinalBar = false;
                        foreach (var ing in rec.requiredItem)
                        {
                            if (ing.type == finalBarType)
                            {
                                hasFinalBar = true;
                                break;
                            }
                        }

                        if (!hasFinalBar)
                        {
                            rec.AddIngredient(finalBarType);
                        }
                    }
                }
            }
        }
    }
}
