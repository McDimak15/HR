using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class BootsProgressionFix : ModSystem
    {
        public override void OnWorldLoad()
        {
            ModLoader.TryGetMod("ssm", out Mod ssm);
            ModLoader.TryGetMod("CalamityMod", out Mod calamity);
            ModLoader.TryGetMod("ContinentOfJourney", out Mod coj);
            ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
            ModLoader.TryGetMod("SacredTools", out Mod st);

            int tracersElysianType = calamity?.Find<ModItem>("TracersElysian")?.Type ?? -1;
            int tracersCelestialType = calamity?.Find<ModItem>("TracersCelestial")?.Type ?? -1;
            int horizonType = coj?.Find<ModItem>("Horizon")?.Type ?? -1;
            int terrariumSprintersType = thorium?.Find<ModItem>("TerrariumParticleSprinters")?.Type ?? -1;
            int voidspursType = st?.Find<ModItem>("VoidSpurs")?.Type ?? -1;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null) continue;

                // Replace Horizon with Celestial Tracers in Elysian recipe
                if (tracersElysianType != -1 && tracersCelestialType != -1 && horizonType != -1 &&
                    recipe.HasResult(tracersElysianType))
                {
                    for (int i = 0; i < recipe.requiredItem.Count; i++)
                    {
                        if (recipe.requiredItem[i].type == horizonType)
                        {
                            recipe.requiredItem[i].SetDefaults(tracersCelestialType);
                        }
                    }
                }

                // Remove Terrarium Sprinters from Celestial Tracers recipe
                if (tracersCelestialType != -1 && terrariumSprintersType != -1 &&
                    recipe.HasResult(tracersCelestialType))
                {
                    for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                    {
                        if (recipe.requiredItem[i].type == terrariumSprintersType)
                        {
                            recipe.requiredItem.RemoveAt(i);
                        }
                    }
                }

                //remove Horizon from Voidspurs and add Celestial Tracers
                if (ssm != null && st != null && voidspursType != -1 && tracersCelestialType != -1 &&
                    horizonType != -1 && recipe.HasResult(voidspursType))
                {
                    for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                    {
                        if (recipe.requiredItem[i].type == horizonType)
                        {
                            recipe.requiredItem.RemoveAt(i);
                        }
                    }
                    recipe.requiredItem.Add(new Terraria.Item(tracersCelestialType));
                }
            }
        }
    }
}
