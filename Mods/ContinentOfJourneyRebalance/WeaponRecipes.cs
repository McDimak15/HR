using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class CoJCalamityRecipePatches : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.WeaponBalancing)
                return;

            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;

            coj.TryFind("ClottedStaff", out ModItem clottedStaff);
            coj.TryFind("OrganicStaff", out ModItem organicStaff);
            coj.TryFind("Vein", out ModItem vein);
            coj.TryFind("FT2Wildfire", out ModItem wildfire);
            coj.TryFind("FT1Sparkthrower", out ModItem sparkthrower);
            coj.TryFind("Denial", out ModItem denial);
            coj.TryFind("Acceptance", out ModItem acceptance);
            coj.TryFind("TrueDawnsBorder", out ModItem anger);
            coj.TryFind("Depression", out ModItem depression);

            calamity.TryFind("PlantationStaff", out ModItem plantationStaff);
            calamity.TryFind("ElementalAxe", out ModItem elementalAxe);
            calamity.TryFind("VeinBurster", out ModItem veinBurster);
            calamity.TryFind("PerfectDark", out ModItem perfectDark);
            calamity.TryFind("WildfireBloom", out ModItem wildfireBloom);
            calamity.TryFind("DarkPlasma", out ModItem darkPlasma);

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe == null || recipe.createItem == null || recipe.createItem.IsAir)
                    continue;

                int resultType = recipe.createItem.type;

                // Terraheart (ClottedStaff) swap OrganicStaff with PlantationStaff
                if (clottedStaff != null && resultType == clottedStaff.Type)
                {
                    if (organicStaff != null && recipe.TryGetIngredient(organicStaff.Type, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        if (plantationStaff != null) recipe.AddIngredient(plantationStaff.Type);
                    }
                }

                // Plantation Staff swap BladeStaff with OrganicStaff
                if (plantationStaff != null && resultType == plantationStaff.Type)
                {
                    if (recipe.TryGetIngredient(ItemID.Smolstar, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        if (organicStaff != null) recipe.AddIngredient(organicStaff.Type);
                    }
                }

                // Elemental Axe swap PlantationStaff with Terraheart (ClottedStaff)
                if (elementalAxe != null && resultType == elementalAxe.Type)
                {
                    if (plantationStaff != null && recipe.TryGetIngredient(plantationStaff.Type, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        if (clottedStaff != null) recipe.AddIngredient(clottedStaff.Type);
                    }
                }

                // Planterror Staff (Clamity) swap PlantationStaff with Terraheart
                if (ModLoader.TryGetMod("Clamity", out Mod clamity) && clamity.TryFind("PlanterrorStaff", out ModItem planterror))
                {
                    if (resultType == planterror.Type)
                    {
                        if (plantationStaff != null && recipe.TryGetIngredient(plantationStaff.Type, out Item ing))
                        {
                            recipe.RemoveIngredient(ing.type);
                            if (clottedStaff != null) recipe.AddIngredient(clottedStaff.Type);
                        }
                    }
                }


                // Add Vein to Vein Burster
                if (veinBurster != null && resultType == veinBurster.Type && vein != null)
                {
                    if (perfectDark != null && !recipe.HasIngredient(perfectDark.Type))
                        recipe.AddIngredient(vein.Type);
                }

                // Add Vein to Perfect Dark
                if (perfectDark != null && resultType == perfectDark.Type && vein != null)
                {
                    if (veinBurster != null && !recipe.HasIngredient(veinBurster.Type))
                        recipe.AddIngredient(vein.Type);
                }


                // Wildfire 
                if (wildfire != null && resultType == wildfire.Type)
                {
                    recipe.RemoveIngredient(ItemID.IronBar);
                    recipe.RemoveIngredient(ItemID.LeadBar);
                    recipe.RemoveIngredient(ItemID.RichMahogany);
                    if (sparkthrower != null) recipe.AddIngredient(sparkthrower.Type);
                }

                // Wildfire Bloom
                if (wildfireBloom != null && resultType == wildfireBloom.Type)
                {
                    if (recipe.TryGetIngredient(ItemID.Flamethrower, out Item ing))
                    {
                        recipe.RemoveIngredient(ing.type);
                        if (wildfire != null) recipe.AddIngredient(wildfire.Type);
                    }
                }

                // Add 3x Dark Plasma to Denial, Acceptance, Anger, Depression
                if (darkPlasma != null)
                {
                    if ((denial != null && resultType == denial.Type) ||
                        (acceptance != null && resultType == acceptance.Type) ||
                        (anger != null && resultType == anger.Type) ||
                        (depression != null && resultType == depression.Type))
                    {
                        if (!recipe.HasIngredient(darkPlasma.Type))
                        {
                            recipe.AddIngredient(darkPlasma.Type, 3);
                        }
                    }
                }
            }
        }
    }
}