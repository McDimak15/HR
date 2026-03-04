using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ContinentOfJourney.Items.Accessories; 
using HomewardRagnarok.Config;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class MushroomAccessoryEdits : GlobalItem
    {
        public override void UpdateEquip(Item item, Player player)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            var thoriumPlayer = player.GetModPlayer<ThoriumMod.ThoriumPlayer>();
            int type = item.type;

            if (type == ModContent.ItemType<HoneyMushroom>()) thoriumPlayer.healBonus += 1;
            else if (type == ModContent.ItemType<MapleMushroom>()) thoriumPlayer.healBonus += 2;
            else if (type == ModContent.ItemType<JellyMushroom>()) thoriumPlayer.healBonus += 4;
            else if (type == ModContent.ItemType<FungusDeluxe>()) thoriumPlayer.healBonus += 6;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            int bonus = 0;
            if (item.type == ModContent.ItemType<HoneyMushroom>()) bonus = 1;
            else if (item.type == ModContent.ItemType<MapleMushroom>()) bonus = 2;
            else if (item.type == ModContent.ItemType<JellyMushroom>()) bonus = 4;
            else if (item.type == ModContent.ItemType<FungusDeluxe>()) bonus = 6;

            if (bonus > 0)
            {
                tooltips.RemoveAll(line => line.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.MushroomAccessory.OrigTooltip")));
                var line = new TooltipLine(Mod, "ThoriumHealerBonus", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.MushroomAccessory.Replace",bonus));
                tooltips.Add(line);
            }
        }
    }
}