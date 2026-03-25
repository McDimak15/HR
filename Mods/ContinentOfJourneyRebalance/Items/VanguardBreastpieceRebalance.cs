using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class VanguardBreastpieceFix : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<VanguardBreastpiece>();
        }

        public override void SetDefaults(Item item)
        {
            item.defense = 8;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                thorium.Find<ModItem>("TerrariumDefender").UpdateAccessory(player, hideVisual);

                player.longInvince = false;
                player.statLifeMax2 -= 20;
            }

            if (ModLoader.TryGetMod("SOTS", out Mod sots))
            {
                sots.Find<ModItem>("OlympianAegis").UpdateAccessory(player, hideVisual);
                sots.Find<ModItem>("ChiseledBarrier").UpdateAccessory(player, hideVisual);
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            tooltips.RemoveAll(l => l.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Remove1")));

            int maxTooltipIndex = -1;
            int maxNumber = -1;

            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.OrigTooltip1")))
                {
                    tooltips[i].Text = tooltips[i].Text.Replace(
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.OrigTooltip1"),
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Replace1"));
                }
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

            if (ModLoader.HasMod("SOTS"))
            {
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Sots1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Sots1")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Sots2", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Sots2")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Sots3", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Sots3")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Sots4", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Sots4")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Sots5", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Sots5")) { OverrideColor = animatedColor });
            }

            if (ModLoader.HasMod("ThoriumMod"))
            {
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Thorium1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Thorium1")) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Thorium2", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.VanguardBreastpiece.Thorium2")) { OverrideColor = animatedColor });
            }
        }
    }

    public class VanguardBreastpieceRecipeFix : ModSystem
    {
        public override void PostAddRecipes()
        {
            int vanguardType = ModContent.ItemType<VanguardBreastpiece>();
            int ankhAmuletType = ModContent.ItemType<AnkhAmulet>();

            foreach (Recipe recipe in Main.recipe)
            {
                if (recipe.createItem.type != vanguardType) continue;

                if (recipe.requiredItem.Any(i => i.type == ankhAmuletType))
                {
                    recipe.DisableRecipe();
                    continue;
                }
                recipe.RemoveIngredient(ModContent.ItemType<BottledBlueIce>());
            }
        }
    }
}