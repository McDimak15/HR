using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ThoriumMod.Items;
using ContinentOfJourney.Items.Accessories;
using CalamityMod.Items.Accessories;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [ExtendsFromMod("ThoriumMod")]
    public class AddOmniShield : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            if (item.type == ModContent.ItemType<VanguardBreastpiece>())
            {
                if (item.ModItem is ThoriumItem tItem)
                {
                    tItem.accessoryType = AccessoryType.OmniShield;
                }
            }
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            if (IsBarrierOrVanguard(equippedItem) && IsBarrierOrVanguard(incomingItem)) return false;

            if (equippedItem.ModItem is ThoriumItem modItem1 && modItem1.accessoryType != AccessoryType.Normal &&
                incomingItem.ModItem is ThoriumItem modItem2 && modItem2.accessoryType != AccessoryType.Normal)
            {
                return modItem1.accessoryType != modItem2.accessoryType;
            }

            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
        }

        private bool IsBarrierOrVanguard(Item item)
        {
            if (item.type == ModContent.ItemType<VanguardBreastpiece>()) return true;

            if (item.ModItem is ThoriumItem t && t.accessoryType == AccessoryType.OmniShield) return true;

            if (ModLoader.TryGetMod("CalamityMod", out Mod clam) && clam.TryFind("SupremeBarrier", out ModItem supreme) && item.type == supreme.Type) return true;
            if (ModLoader.TryGetMod("SOTS", out Mod sots) && sots.TryFind("BulwarkOfTheAncients", out ModItem bulwark) && item.type == bulwark.Type) return true;

            if (item.type == ModContent.ItemType<RampartofDeities>()) return true;

            return false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            if (item.type == ModContent.ItemType<VanguardBreastpiece>() && ModLoader.HasMod("ThoriumMod"))
            {
                var shieldTag = new TooltipLine(Mod, "OmniShieldTag", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.OmniShield"))
                {
                    OverrideColor = new Color(102, 255, 255)
                };

                int index = tooltips.FindIndex(tt => tt.Name == "ItemName" && tt.Mod == "Terraria");
                if (index != -1)
                {
                    tooltips.Insert(index + 1, shieldTag);
                }
            }
        }
    }
}