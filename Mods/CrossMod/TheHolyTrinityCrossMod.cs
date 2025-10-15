using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney;


namespace HomewardRagnarok
{
    public class VampiricTalismanTrinityPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return false;
            int vampType = calamity.Find<ModItem>("VampiricTalisman")?.Type ?? 0;
            return vampType > 0 && entity.type == vampType;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.HolyTrinity = true;
            modPlayer.AlphaTrinity = true;
            modPlayer.OmegaTrinity = true;
            modPlayer.EpsilonTrinity = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            AddTrinityTooltip(tooltips);
        }

        private void AddTrinityTooltip(List<TooltipLine> tooltips)
        {
            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color darkPurple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(darkPurple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int holyTrinityType = ModContent.ItemType<TheHolyTrinity>();
            string iconTag = $"[i:{holyTrinityType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokTrinity",
                iconTag + "A ring will rotate around you\n" +
                iconTag + "A ring will stay at a distance from you\n" +
                iconTag + "A ring will stay between you and the enemy with the lowest health\n" +
                iconTag + "Thrower weapons passing through the ring gain 30% more damage")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokTrinity");
            tooltips.Add(customLine);
        }
    }

    public class DraculasCharmTrinityPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("Clamity", out var clamity)) return false;
            int draculaCharmType = clamity.Find<ModItem>("DraculasCharm")?.Type ?? 0;
            return draculaCharmType > 0 && entity.type == draculaCharmType;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.HolyTrinity = true;
            modPlayer.AlphaTrinity = true;
            modPlayer.OmegaTrinity = true;
            modPlayer.EpsilonTrinity = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color darkPurple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(darkPurple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int holyTrinityType = ModContent.ItemType<TheHolyTrinity>();
            string iconTag = $"[i:{holyTrinityType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokTrinity",
                iconTag + "A ring will rotate around you\n" +
                iconTag + "A ring will stay at a distance from you\n" +
                iconTag + "A ring will stay between you and the enemy with the lowest health\n" +
                iconTag + "Thrower weapons passing through the ring gain 30% more damage")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokTrinity");
            tooltips.Add(customLine);
        }
    }

    public class VagabondSoulTrinityPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("FargowiltasCrossmod", out var fargoCross)) return false;
            int vagabondType = fargoCross.Find<ModItem>("VagabondsSoul")?.Type ?? 0;
            return vagabondType > 0 && entity.type == vagabondType;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.HolyTrinity = true;
            modPlayer.AlphaTrinity = true;
            modPlayer.OmegaTrinity = true;
            modPlayer.EpsilonTrinity = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color darkPurple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(darkPurple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int holyTrinityType = ModContent.ItemType<TheHolyTrinity>();
            string iconTag = $"[i:{holyTrinityType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokTrinity",
                iconTag + "A ring will rotate around you\n" +
                iconTag + "A ring will stay at a distance from you\n" +
                iconTag + "A ring will stay between you and the enemy with the lowest health\n" +
                iconTag + "Thrower weapons passing through the ring gain 30% more damage")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokTrinity");
            tooltips.Add(customLine);
        }
    }

    public class FargoSoulsTrinityPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ModLoader.TryGetMod("FargowiltasSouls", out var fargoSouls)) return false;
            int universeType = fargoSouls.Find<ModItem>("UniverseSoul")?.Type ?? 0;
            int eternityType = fargoSouls.Find<ModItem>("EternitySoul")?.Type ?? 0;

            return (universeType > 0 && entity.type == universeType) ||
                   (eternityType > 0 && entity.type == eternityType);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.HolyTrinity = true;
            modPlayer.AlphaTrinity = true;
            modPlayer.OmegaTrinity = true;
            modPlayer.EpsilonTrinity = true;
        }
    }

    public class VampiricTalismanRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out var calamity)) return;

            int vampType = calamity.Find<ModItem>("VampiricTalisman")?.Type ?? 0;
            if (vampType == 0) return;

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type == vampType)
                {
                    if (!recipe.requiredItem.Exists(i => i.type == ModContent.ItemType<TheHolyTrinity>()))
                    {
                        recipe.AddIngredient(ModContent.ItemType<TheHolyTrinity>());
                    }
                }
            }
        }
    }
}
