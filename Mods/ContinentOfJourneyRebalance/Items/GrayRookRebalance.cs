using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Buffs;
using ContinentOfJourney;
using CalamityMod;
using CalamityMod.Items.Weapons.Rogue;
using System.Runtime.CompilerServices;

namespace HomewardRagnarok.ContinentOfJourneyRebalance.Items
{
    public class GrayRookPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<GrayRook>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type != ModContent.ItemType<GrayRook>())
                return;

            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            float speedToRemove = modPlayer.MinionCount * 0.02f;

            player.GetAttackSpeed(DamageClass.Melee) -= speedToRemove;

            if (player.HasBuff(ModContent.BuffType<GrayRookBuff>()))
            {
                player.whipRangeMultiplier -= 0.10f;
                player.GetAttackSpeed(ModContent.GetInstance<RogueDamageClass>()) += 0.10f;

                if (ModLoader.HasMod("ThoriumMod"))
                {
                    ApplyThoriumEffects(player);
                }
            }
        }


        [JITWhenModsEnabled("ThoriumMod")]
        private void ApplyThoriumEffects(Player player)
        {
            var thoriumPlayer = player.GetModPlayer<ThoriumMod.ThoriumPlayer>();
            thoriumPlayer.bardResourceMax2 += 5;
            thoriumPlayer.healBonus++;
        }

        public override void ModifyItemScale(Item item, Player player, ref float scale)
        {
            if (player.HasBuff(ModContent.BuffType<GrayRookBuff>()))
            {
                if (item.DamageType == DamageClass.Melee)
                {
                    scale *= 1.25f;
                }
            }
        }

        public override float UseSpeedMultiplier(Item item, Player player)
        {
            if (player.HasBuff(ModContent.BuffType<GrayRookBuff>()))
            {
                return 1f / 1.35f;
            }
            return base.UseSpeedMultiplier(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type != ModContent.ItemType<GrayRook>())
                return;

            tooltips.RemoveAll(l => l.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.Remove'UseTime'")));

            int maxTooltipIndex = -1;
            int maxNumber = -1;

            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.OrigTooltip1")))
                {
                    tooltips[i].Text = tooltips[i].Text.Replace(
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.OrigTooltip1"),
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.Replace1"));
                }

                if (tooltips[i].Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.OrigTooltip2")))
                {
                    tooltips[i].Text = tooltips[i].Text.Replace(
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.OrigTooltip2"), 
                        Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.Replace2"));
                }

                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        maxTooltipIndex = i;
                    }
                }
            }

            int insertAt = maxTooltipIndex != -1 ? maxTooltipIndex : tooltips.Count;

            tooltips.Insert(insertAt++, new TooltipLine(Mod, "Rebalance1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.MeleeRebalance")));
            tooltips.Insert(insertAt++, new TooltipLine(Mod, "Rogue", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.RogueRebalance")));

            if (ModLoader.HasMod("ThoriumMod"))
            {
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Healer", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.HealerRebalance")));
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Bard", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRook.BardRebalance")));
            }
        }
    }

    public class GrayRookBuffPatch : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string displayName, ref string tooltip, ref int drawOffset)
        {
            if (type == ModContent.BuffType<GrayRookBuff>())
            {
                tooltip = tooltip.Replace(
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.OrigTooltip1"),
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.Replace1"));
                tooltip = tooltip.Replace(
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.OrigTooltip2"), 
                    Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.Replace2"));
                tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.MeleeRebalance");

                if (ModLoader.HasMod("ThoriumMod"))
                {
                    tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.HealerRebalance");
                    tooltip += Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.GrayRookBuff.BardRebalance");
                }
            }
        }
    }
}