using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Buffs;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class BattalionsBackupRebalance : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<BattalionsBackup>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
            player.statDefense -= (int)modPlayer.MinionCount;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            foreach (var line in tooltips)
            {
                if (line.Text.Contains("defense by 2"))
                    line.Text = line.Text.Replace("defense by 2", "defense by 1");

                if (line.Text.Contains("reduced by 70%"))
                    line.Text = line.Text.Replace("70%", "20%");

                if (line.Text.Contains("defense increased by 30"))
                    line.Text = line.Text.Replace("30", "15");
            }
        }
    }

    public class BattalionsBuffPatch : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            if (type == ModContent.BuffType<BattaliansBackupBuff>())
            {
                player.statDefense -= 15;
                player.endurance -= 0.50f;
            }
        }

        public override void ModifyBuffText(int type, ref string displayName, ref string tooltip, ref int drawOffset)
        {
            if (type == ModContent.BuffType<BattaliansBackupBuff>())
            {
                tooltip = tooltip.Replace("70%", "20%").Replace("30", "15");
            }
        }
    }
}