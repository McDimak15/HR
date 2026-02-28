using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using System.Linq;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class EtherealAndSoulsGlobal : GlobalItem
    {
        private static int etherealTalismanType;
        private static int starflowerType;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (etherealTalismanType == 0 && ModLoader.TryGetMod("CalamityMod", out var calamity))
                etherealTalismanType = calamity.Find<ModItem>("EtherealTalisman")?.Type ?? 0;

            if (starflowerType == 0 && ModLoader.TryGetMod("ContinentOfJourney", out var coj))
                starflowerType = coj.Find<ModItem>("Starflower")?.Type ?? 0;

            return entity.type == etherealTalismanType;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            player.statManaMax2 += 80;
            player.manaCost *= 0.88f;
            player.manaFlower = true;

            player.GetModPlayer<EtherealAndSoulsBuffs>().AlternativeYou = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            int maxTooltipIndex = -1;
            int maxNumber = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        maxTooltipIndex = i;
                    }
                }
                else if (tooltips[i].Mod == "InfernalEclipseAPI")
                {
                    maxTooltipIndex = i;
                }
            }
            int insertAt = maxTooltipIndex != -1 ? maxTooltipIndex + 1 : tooltips.Count;
            tooltips.Insert(insertAt, new TooltipLine(Mod, "StarflowerBuff1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.StarFlower")) { OverrideColor = animatedColor });
        }
    }

    public class EtherealAndSoulsBuffs : ModPlayer
    {
        public bool AlternativeYou;

        public override void ResetEffects()
        {
            AlternativeYou = false;
        }
    }

    public class RecipeEdits : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamityMod)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)) return;

            int etherealTalismanType = calamityMod.Find<ModItem>("EtherealTalisman")?.Type ?? 0;
            int starflowerType = cojMod.Find<ModItem>("Starflower")?.Type ?? 0;
            if (etherealTalismanType == 0 || starflowerType == 0) return;

            int hungeringBlossomType = 0;
            Mod thoriumMod = null;
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                thoriumMod = thorium;
                hungeringBlossomType = thoriumMod.Find<ModItem>("HungeringBlossom")?.Type ?? 0;
            }

            var manaFlowerTypes = new List<int> { ItemID.ManaFlower };

            if (calamityMod.TryFind<ModItem>("ManaFlower", out var calamityManaFlower))
                manaFlowerTypes.Add(calamityManaFlower.Type);

            if (thoriumMod != null && thoriumMod.TryFind<ModItem>("ManaFlower", out var thoriumManaFlower))
                manaFlowerTypes.Add(thoriumManaFlower.Type);

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null || recipe.createItem.type != etherealTalismanType) continue;

                bool hasAnyManaFlower = recipe.requiredItem.Any(i => i != null && manaFlowerTypes.Contains(i.type));
                bool hasHungeringBlossom = hungeringBlossomType != 0 &&
                                           recipe.requiredItem.Any(i => i != null && i.type == hungeringBlossomType);

                if (thoriumMod != null)
                {
                    if (hasAnyManaFlower && !hasHungeringBlossom)
                    {
                        recipe.DisableRecipe();
                    }
                    else if (hasHungeringBlossom)
                    {
                        recipe.requiredItem.RemoveAll(i => i != null && manaFlowerTypes.Contains(i.type));
                        if (!recipe.requiredItem.Any(i => i != null && i.type == starflowerType))
                            recipe.AddIngredient(starflowerType, 1);
                    }
                }
                else
                {
                    recipe.requiredItem.RemoveAll(i => i != null && manaFlowerTypes.Contains(i.type));
                    if (!recipe.requiredItem.Any(i => i != null && i.type == starflowerType))
                        recipe.AddIngredient(starflowerType, 1);
                }
            }
        }
    }
}
