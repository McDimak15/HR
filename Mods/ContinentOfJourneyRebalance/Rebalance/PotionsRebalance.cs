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
                entity.healLife = 350; // from 300 to 350

            if (entity.type == ModContent.ItemType<UltraManaPotion>())
                entity.healMana = 450; // from 320 to 450
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<UltraHealingPotion>())
            {
                tooltips.RemoveAll(t => t.Text.Contains("Restores 300 life"));
                tooltips.Add(new TooltipLine(Mod, "UltraHealingPotionBuff", "Restores 350 life"));
            }

            if (item.type == ModContent.ItemType<UltraManaPotion>())
            {
                tooltips.RemoveAll(t => t.Text.Contains("Restores 320 mana"));
                tooltips.RemoveAll(t => t.Text.Contains("Restores 450"));
                tooltips.Add(new TooltipLine(Mod, "UltraManaPotionBuff", "Restores 450 mana"));
            }
        }
    }
}
