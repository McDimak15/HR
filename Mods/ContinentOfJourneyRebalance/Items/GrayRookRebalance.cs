using Terraria;
using Terraria.ModLoader;
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

            tooltips.RemoveAll(l => l.Text.Contains("-item use time decreased by 35%"));

            int maxTooltipIndex = -1;
            int maxNumber = -1;

            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Text.Contains("melee speed"))
                {
                    tooltips[i].Text = tooltips[i].Text.Replace("Increase your melee speed by 2% and", "Increase your");
                }

                if (tooltips[i].Text.Contains("range increased"))
                {
                    tooltips[i].Text = tooltips[i].Text.Replace("-whip range increased by 35%", "-whip range increased by 25%");
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

            tooltips.Insert(insertAt++, new TooltipLine(Mod, "Rebalance1", "-melee size increased by 25%"));
            tooltips.Insert(insertAt++, new TooltipLine(Mod, "Rogue", "-rogue attack speed is increased by 10%"));

            if (ModLoader.HasMod("ThoriumMod"))
            {
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Healer", "-healing bonus is increased by 1"));
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "Bard", "-max inspiration is increased by 2"));
            }
        }
    }

    public class GrayRookBuffPatch : GlobalBuff
    {
        public override void ModifyBuffText(int type, ref string displayName, ref string tooltip, ref int drawOffset)
        {
            if (type == ModContent.BuffType<GrayRookBuff>())
            {
                tooltip = tooltip.Replace("30%", "25%");
                tooltip = tooltip.Replace("item use time decreased by 35%", "thrower attack speed is increased by 10%");
                tooltip += "\nmelee size increased by 25%";

                if (ModLoader.HasMod("ThoriumMod"))
                {
                    tooltip += "\nhealing bonus is increased by 1";
                    tooltip += "\nmax inspiration is increased by 2";
                }
            }
        }
    }
}