using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok
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
            

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                newFinalBarRecipe.AddIngredient(calamity.Find<ModItem>("AerialiteBar").Type, 1);
                newFinalBarRecipe.AddIngredient(calamity.Find<ModItem>("AstralBar").Type, 1);
                newFinalBarRecipe.AddIngredient(calamity.Find<ModItem>("LifeAlloy").Type, 1);
                newFinalBarRecipe.AddIngredient(calamity.Find<ModItem>("UelibloomBar").Type, 1);
                newFinalBarRecipe.AddTile(calamity.Find<ModTile>("CosmicAnvil").Type);
            }

            if (ModLoader.TryGetMod("SOTS", out Mod sots))
            {
                newFinalBarRecipe.AddIngredient(sots.Find<ModItem>("FrigidBar").Type, 1);
                newFinalBarRecipe.AddIngredient(sots.Find<ModItem>("PhaseBar").Type, 1);
                newFinalBarRecipe.AddIngredient(sots.Find<ModItem>("VibrantBar").Type, 1);
                newFinalBarRecipe.AddIngredient(sots.Find<ModItem>("AbsoluteBar").Type, 1);
            }

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                newFinalBarRecipe.AddIngredient(thorium.Find<ModItem>("TerrariumCore").Type, 1);
            }

            newFinalBarRecipe.Register();

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) &&
                calamityMod.TryFind("ShadowspecBar", out ModItem shadowspecBar))
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
