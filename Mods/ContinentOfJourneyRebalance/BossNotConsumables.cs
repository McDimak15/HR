using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items;

namespace HomewardRagnarok.CrossMod
{
    public class CoJSummonPatch : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ModContent.ItemType<PurpleFlareGun>() ||
                item.type == ModContent.ItemType<MaliciousPacket>() ||
                item.type == ModContent.ItemType<CannedSoulofFlight>() ||
                item.type == ModContent.ItemType<UnstableGlobe>() ||
                item.type == ModContent.ItemType<MetalSpine>() ||
                item.type == ModContent.ItemType<UltimateTorch>() ||
                item.type == ModContent.ItemType<SunlightCrown>())
            {
                item.consumable = false;
                item.maxStack = 1;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<PurpleFlareGun>() ||
                item.type == ModContent.ItemType<MaliciousPacket>() ||
                item.type == ModContent.ItemType<CannedSoulofFlight>() ||
                item.type == ModContent.ItemType<UnstableGlobe>() ||
                item.type == ModContent.ItemType<MetalSpine>() ||
                item.type == ModContent.ItemType<UltimateTorch>() ||
                item.type == ModContent.ItemType<SunlightCrown>())
            {
                tooltips.RemoveAll(t => t.Mod == "Terraria" && t.Name == "Consumable");

                tooltips.Add(new TooltipLine(Mod, "NonConsumableInfo", "Not consumable"));
            }
        }
    }
}