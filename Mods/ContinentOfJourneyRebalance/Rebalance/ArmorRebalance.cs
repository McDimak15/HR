using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using HomewardRagnarok.Config;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class ArmorDefensePatch : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!ServerConfig.Instance.ArmorBalancing)
                return;

            // Aurora Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.AuroraHeadwear>())
                item.defense = 20; // Aurora Headwear 
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.AuroraRobe>())
                item.defense = 24; // Aurora Breastplate
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.AuroraBoots>())
                item.defense = 18; // Aurora Leggings

            // Sunlight Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.SunlightHelmet>())
                item.defense = 20; // Sun God Helmet
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.SunlightBreastplate>())
                item.defense = 26; // Sun God Breastplate
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.SunlightLegging>())
                item.defense = 24; // Sun God Leggings

            // Heliology Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.HeliologyHelmet>())
                item.defense = 22; // Five-star General Hat
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.HeliologyPlate>())
                item.defense = 26; // Five-star General Coat
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.HeliologyLeggings>())
                item.defense = 20; // Five-star General Trousers

            // Perpetual (Chrono)
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.PerpetualHelmet>())
                item.defense = 16; // Chrono Helmet
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.PerpetualPlate>())
                item.defense = 30; // Chrono Breastplate
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.PerpetualLeggings>())
                item.defense = 18; // Chrono Leggings

            // Biological
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.BiologicalHelmet>())
                item.defense = 40;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.BiologicalBreastplate>())
                item.defense = 48;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.BiologicalLeggings>())
                item.defense = 40;

            // Reflector
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.ReflectorHelmet>())
                item.defense = 56;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.ReflectorBreastplate>())
                item.defense = 52; // Reflector Bodysuit

            // Watchman Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.WatchmanHat>())
                item.defense = 18;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.WatchmanShirt>())
                item.defense = 32;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.WatchmanDress>())
                item.defense = 20;

            // Forest Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.ForestHelmet>())
                item.defense = 44;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.ForestBreastplate>())
                item.defense = 50;
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.ForestLeggings>())
                item.defense = 42;

            // Equilibrium Set
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.EquilibriumBreastplate>())
                item.defense = 54; // Equilibrium Bodysuit
            if (item.type == ModContent.ItemType<ContinentOfJourney.Items.Armor.EquilibriumLeggings>())
                item.defense = 48; // Equilibrium Stockings
        }
    }

    public class ArmorRebalancePlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            if (Player.TryGetModPlayer<ContinentOfJourney.TemplatePlayer>(out var cojPlayer))
            {
                if (cojPlayer.HeliologyArmorSetEffect)
                {
                    float baseMinions = Player.maxMinions / 1.77f;
                    Player.maxMinions = (int)System.Math.Round(baseMinions * 1.20f);
                }
            }
        }
    }

    public class ArmorGlobalProjectile : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            Player player = Main.player[projectile.owner];
            if (player.TryGetModPlayer<ContinentOfJourney.TemplatePlayer>(out var cojPlayer))
            {
                if (cojPlayer.HeliologyArmorSetEffect && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
                {
                    float penaltyAmount = (projectile.damage - Terraria.Utils.Clamp(projectile.ArmorPenetration, 0, target.defense) / 2) * 0.38f;
                    modifiers.FinalDamage.Flat += penaltyAmount;
                }
            }
        }
    }

    public class ArmorTooltipSystem : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ServerConfig.Instance.ArmorBalancing) return;

            string key = "Mods.ContinentOfJourney.Armor_HeliologyBonus";
            string newText = "Increases max number of minions by 20%";

            if (Language.Exists(key))
            {
                LocalizedText localizedText = Language.GetText(key);

                FieldInfo valueField = typeof(LocalizedText).GetField("value", BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?? typeof(LocalizedText).GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance);

                if (valueField != null)
                {
                    valueField.SetValue(localizedText, newText);
                }
            }
        }
    }

    public class ArmorRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.ArmorCraft)
                return;

            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj) &&
                ModLoader.TryGetMod("CalamityMod", out var calamity))
            {
                int bioHelm = coj.Find<ModItem>("BiologicalHelmet")?.Type ?? -1;
                int bioChest = coj.Find<ModItem>("BiologicalBreastplate")?.Type ?? -1;
                int bioLegs = coj.Find<ModItem>("BiologicalLeggings")?.Type ?? -1;

                int godSlayerHelm = calamity.Find<ModItem>("GodSlayerHeadMelee")?.Type ?? -1;
                int godSlayerChest = calamity.Find<ModItem>("GodSlayerChestplate")?.Type ?? -1;
                int godSlayerLegs = calamity.Find<ModItem>("GodSlayerLeggings")?.Type ?? -1;

                int vortexHelm = Terraria.ID.ItemID.VortexHelmet;
                int vortexChest = Terraria.ID.ItemID.VortexBreastplate;
                int vortexLegs = Terraria.ID.ItemID.VortexLeggings;

                foreach (Recipe recipe in Main.recipe)
                {
                    if (recipe.createItem.type == bioHelm)
                        ReplaceIngredient(recipe, vortexHelm, godSlayerHelm);

                    if (recipe.createItem.type == bioChest)
                        ReplaceIngredient(recipe, vortexChest, godSlayerChest);

                    if (recipe.createItem.type == bioLegs)
                        ReplaceIngredient(recipe, vortexLegs, godSlayerLegs);
                }
            }
        }

        private void ReplaceIngredient(Recipe recipe, int oldItem, int newItem)
        {
            for (int i = 0; i < recipe.requiredItem.Count; i++)
            {
                if (recipe.requiredItem[i].type == oldItem)
                {
                    int stack = recipe.requiredItem[i].stack;
                    recipe.requiredItem[i].SetDefaults(newItem);
                    recipe.requiredItem[i].stack = stack;
                }
            }
        }
    }
}
