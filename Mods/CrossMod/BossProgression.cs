using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace HomewardRagnarok
{
    public class CompatRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Mod coj = null, calamity = null, thorium = null, sots = null, fargo = null;
            ModLoader.TryGetMod("ContinentOfJourney", out coj);
            ModLoader.TryGetMod("CalamityMod", out calamity);
            ModLoader.TryGetMod("ThoriumMod", out thorium);
            ModLoader.TryGetMod("SOTS", out sots);
            ModLoader.TryGetMod("FargowiltasSouls", out fargo);

            int swollenStar = -1, squidfood = -1, cannedSoulofFlight = -1, unstableGlobe = -1;
            int metalSpine = -1, sunlightCrown = -1, solarFlareScoria = -1, deepBar = -1;
            int essenceOfTime = -1, essenceOfMatter = -1, essenceOfLife = -1, essenceOfDeath = -1;
            int soulOfBlight = -1, anglerCoin = -1, anglerGoldCoin = -1;

            if (coj != null)
            {
                swollenStar = coj.Find<ModItem>("SwollenStar")?.Type ?? -1;
                squidfood = coj.Find<ModItem>("Squidfood")?.Type ?? -1;
                cannedSoulofFlight = coj.Find<ModItem>("CannedSoulofFlight")?.Type ?? -1;
                unstableGlobe = coj.Find<ModItem>("UnstableGlobe")?.Type ?? -1;
                metalSpine = coj.Find<ModItem>("MetalSpine")?.Type ?? -1;
                sunlightCrown = coj.Find<ModItem>("SunlightCrown")?.Type ?? -1;
                solarFlareScoria = coj.Find<ModItem>("SolarFlareScoria")?.Type ?? -1;
                deepBar = coj.Find<ModItem>("DeepBar")?.Type ?? -1;
                essenceOfTime = coj.Find<ModItem>("EssenceofTime")?.Type ?? -1;
                essenceOfMatter = coj.Find<ModItem>("EssenceofMatter")?.Type ?? -1;
                essenceOfLife = coj.Find<ModItem>("EssenceofLife")?.Type ?? -1;
                essenceOfDeath = coj.Find<ModItem>("EssenceofDeath")?.Type ?? -1;
                soulOfBlight = coj.Find<ModItem>("SoulofBlight")?.Type ?? -1;
                anglerCoin = coj.Find<ModItem>("AnglerCoin")?.Type ?? -1;
                anglerGoldCoin = coj.Find<ModItem>("AnglerGoldCoin")?.Type ?? -1;
            }

            // Calamity
            int pearlShard = -1, starblightSoot = -1, scoriaBar = -1;
            int abombination = -1, unholyEssence = -1, cosmicWorm = -1;

            if (calamity != null)
            {
                pearlShard = calamity.Find<ModItem>("PearlShard")?.Type ?? -1;
                starblightSoot = calamity.Find<ModItem>("StarblightSoot")?.Type ?? -1;
                scoriaBar = calamity.Find<ModItem>("ScoriaBar")?.Type ?? -1;
                abombination = calamity.Find<ModItem>("Abombination")?.Type ?? -1;
                unholyEssence = calamity.Find<ModItem>("UnholyEssence")?.Type ?? -1;
                cosmicWorm = calamity.Find<ModItem>("CosmicWorm")?.Type ?? -1;
            }

            // SOTS
            int soulOfPlight = -1;
            if (sots != null)
                soulOfPlight = sots.Find<ModItem>("SoulOfPlight")?.Type ?? -1;

            // Thorium
            int starCaller = -1;
            if (thorium != null)
                starCaller = thorium.Find<ModItem>("StarCaller")?.Type ?? -1;

            // Fargo
            int sigilOfChampions = -1, abomsCurse = -1, abomVoodooDoll = -1;
            if (fargo != null)
            {
                sigilOfChampions = fargo.Find<ModItem>("SigilOfChampions")?.Type ?? -1;
                abomsCurse = fargo.Find<ModItem>("AbomsCurse")?.Type ?? -1;
                abomVoodooDoll = fargo.Find<ModItem>("AbominationnVoodooDoll")?.Type ?? -1;
            }

            for (int i = 0; i < Main.recipe.Length; i++)
            {
                var recipe = Main.recipe[i];
                if (recipe == null) continue;

                int createType = recipe.createItem.type;

                // StarCaller
                if (createType == starCaller && swollenStar != -1)
                    recipe.AddIngredient(swollenStar, 5);

                // Squidfood
                if (createType == squidfood)
                {
                    bool requiresAngler = false;
                    foreach (var ing in recipe.requiredItem)
                        if (ing.type == anglerCoin) { requiresAngler = true; break; }

                    if (requiresAngler)
                        recipe.DisableRecipe();
                    else if (pearlShard != -1)
                        recipe.AddIngredient(pearlShard, 3);
                }

                // CannedSoulofFlight
                if (createType == cannedSoulofFlight)
                {
                    bool requiresAnglerGold = false;
                    foreach (var ing in recipe.requiredItem)
                        if (ing.type == anglerGoldCoin) { requiresAnglerGold = true; break; }

                    if (requiresAnglerGold)
                        recipe.DisableRecipe();
                    else
                    {
                        if (swollenStar != -1) recipe.AddIngredient(swollenStar, 5);
                        if (soulOfPlight != -1) recipe.AddIngredient(soulOfPlight, 10);
                    }
                }

                // UnstableGlobe
                if (createType == unstableGlobe)
                {
                    bool requiresAnglerGold = false;
                    foreach (var ing in recipe.requiredItem)
                        if (ing.type == anglerGoldCoin) { requiresAnglerGold = true; break; }

                    if (requiresAnglerGold)
                        recipe.DisableRecipe();
                    else if (starblightSoot != -1)
                        recipe.AddIngredient(starblightSoot, 5);
                }

                // MetalSpine
                if (createType == metalSpine)
                {
                    bool removeRecipe = false;
                    bool hasScoriaBar = false;

                    foreach (var ing in recipe.requiredItem)
                    {
                        if (ing.type == anglerGoldCoin || ing.type == deepBar) removeRecipe = true;
                        if (scoriaBar != -1 && ing.type == scoriaBar) hasScoriaBar = true;
                    }

                    if (removeRecipe)
                        recipe.DisableRecipe();
                    else if (hasScoriaBar && deepBar != -1)
                        recipe.AddIngredient(deepBar, 6);
                }

                // Abombination
                if (createType == abombination && soulOfBlight != -1)
                    recipe.AddIngredient(soulOfBlight, 5);

                // SunlightCrown
                if (createType == sunlightCrown && unholyEssence != -1)
                    recipe.AddIngredient(unholyEssence, 10);

                // CosmicWorm
                if (createType == cosmicWorm && solarFlareScoria != -1)
                    recipe.AddIngredient(solarFlareScoria, 5);

                // SigilOfChampions
                if (createType == sigilOfChampions && solarFlareScoria != -1 && essenceOfTime != -1)
                {
                    recipe.AddIngredient(solarFlareScoria, 5);
                }

                // AbomsCurse
                if (createType == abomsCurse && essenceOfTime != -1 && essenceOfMatter != -1 && essenceOfLife != -1)
                {
                    recipe.AddIngredient(essenceOfTime, 5);
                    recipe.AddIngredient(essenceOfMatter, 5);
                    recipe.AddIngredient(essenceOfLife, 5);
                }

                // AbominationnVoodooDoll
                if (createType == abomVoodooDoll && essenceOfDeath != -1)
                    recipe.AddIngredient(essenceOfDeath, 5);
            }
        }
    }
}
