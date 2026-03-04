using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using ContinentOfJourney.Items;

namespace HomewardRagnarok.Compat
{
    public class PotionBuffs : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<UltraHealingPotion>()
                || entity.type == ModContent.ItemType<UltraManaPotion>();
        }

        public override void SetDefaults(Item entity)
        {
            if (entity.type == ModContent.ItemType<UltraHealingPotion>())
                entity.healLife = 350;

            if (entity.type == ModContent.ItemType<UltraManaPotion>())
                entity.healMana = 450; 
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<UltraHealingPotion>())
            {
                foreach (var tooltip in tooltips)
                {
                    if (tooltip.Mod == "Terraria" && tooltip.Name == "HealLife" && tooltip.Text.Contains("300"))
                    {
                        tooltip.Text = tooltip.Text.Replace("300", "350");
                    }
                }
            }

            if (item.type == ModContent.ItemType<UltraManaPotion>())
            {
                foreach (var tooltip in tooltips)
                {
                    if (tooltip.Mod == "Terraria" && tooltip.Name == "HealMana" && tooltip.Text.Contains("450"))
                    {
                        tooltip.Text = tooltip.Text.Replace("450", "350");
                    }
                }
            }
        }
    }
}
