using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using CalamityMod;
using ContinentOfJourney.Items.Accessories;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class TheSwitchRebalance : GlobalItem
    {
        public override bool InstancePerEntity => true;

        // true = Cold Blood, false = Whale Bone Charm
        public bool modeColdBlood = true;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ServerConfig.Instance.PermanentToAccessories)
            {
                return entity.type == ModContent.ItemType<TheSwitch>();
            }

            return false;
        }

        public override void SetDefaults(Item item)
        {
            item.accessory = true;
            item.consumable = false;
            item.useStyle = ItemUseStyleID.None;
            item.useAnimation = 0;
            item.useTime = 0;
            item.UseSound = null;
        }

        public override bool CanRightClick(Item item)
        {
            return Utils.PressingShift(Main.keyState);
        }

        public override void RightClick(Item item, Player player)
        {
            modeColdBlood = !modeColdBlood;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            return false;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("TheSwitchModeColdBlood", modeColdBlood);
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            modeColdBlood = tag.GetBool("TheSwitchModeColdBlood");
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(modeColdBlood);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            modeColdBlood = reader.ReadBoolean();
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(line => line.Mod == "ContinentOfJourney" && line.Name == "CoJTS");

            int insertIndex = tooltips.FindLastIndex(line => !line.Name.Contains("Prefix") && !line.Name.Contains("Price"));
            if (insertIndex == -1) insertIndex = tooltips.Count - 1;

            string stateText = modeColdBlood ?
                Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.TheSwitch.ColdBlood") :
                Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.TheSwitch.WhaleBoneCharm");

            tooltips.Insert(insertIndex + 1, new TooltipLine(Mod, "TheSwitchState", stateText));

            TooltipLine toggleLine = new TooltipLine(Mod, "TheSwitchToggle", "Hold SHIFT and Right Click to switch modes")
            {
                OverrideColor = Color.Gray
            };
            tooltips.Add(toggleLine);
        }

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, modeColdBlood);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var hrPlayer = player.GetModPlayer<HomeRagPlayer>();

            if (modeColdBlood)
                hrPlayer.equippedImprovedColdBlood = true;
            else
                hrPlayer.equippedImprovedWhaleBone = true;
        }
    }
}