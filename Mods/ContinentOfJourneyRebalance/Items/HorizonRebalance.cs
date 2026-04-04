using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.GameContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Collections.Generic;
using CalamityMod;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Projectiles;

namespace HomewardRagnarok.Items.Accessories
{
    public class HorizonRebalance : GlobalItem
    {
        public static int HorizonWingSlot = -1;
        public override bool InstancePerEntity => true;
        public bool wingsDisabled = false;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<Horizon>();
        }

        public override void Load()
        {
            EquipLoader.AddEquipTexture(Mod, "ContinentOfJourney/Items/Accessories/Altitude_Wings_Unused", EquipType.Wings, null, "Horizon_Wings");
        }

        public override void SetStaticDefaults()
        {
            HorizonWingSlot = EquipLoader.GetEquipSlot(Mod, "Horizon_Wings", EquipType.Wings);

            if (HorizonWingSlot != -1)
            {
                ArmorIDs.Wing.Sets.Stats[HorizonWingSlot] = new WingStats(225, 10.5f, 2.75f, false, -1f, 1f);
            }
        }

        public override void SetDefaults(Item item)
        {
            if (HorizonWingSlot != -1)
            {
                item.wingSlot = wingsDisabled ? -1 : HorizonWingSlot;
            }
        }

        public override bool CanRightClick(Item item)
        {
            return Utils.PressingShift(Main.keyState);
        }

        public override void RightClick(Item item, Player player)
        {
            wingsDisabled = !wingsDisabled;
            item.wingSlot = wingsDisabled ? -1 : HorizonWingSlot;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            return false;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag.Add("HorizonWingsDisabled", wingsDisabled);
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            wingsDisabled = tag.GetBool("HorizonWingsDisabled");
            item.wingSlot = wingsDisabled ? -1 : HorizonWingSlot;
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(wingsDisabled);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            wingsDisabled = reader.ReadBoolean();
            item.wingSlot = wingsDisabled ? -1 : HorizonWingSlot;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (wingsDisabled)
            {
                string originalText = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Horizon.Remove");
                tooltips.RemoveAll(line => line.Text == originalText);
            }

            int insertAt = tooltips.Count;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name == "Tooltip0")
                {
                    insertAt = i + 1;
                    break;
                }
            }

            bool isShiftHeld = Main.keyState.IsKeyDown(Keys.LeftShift) || Main.keyState.IsKeyDown(Keys.RightShift);

            if (!wingsDisabled)
            {
                tooltips.Insert(insertAt++, new TooltipLine(Mod, "DefaultStats", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Horizon.DefaultStats")));

                if (isShiftHeld)
                {
                    tooltips.Insert(insertAt++, new TooltipLine(Mod, "HidedStats", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Horizon.HidedStats")));
                }
                else
                {
                    TooltipLine shiftLine = new TooltipLine(Mod, "HoldShift", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Horizon.HoldShift"))
                    {
                        OverrideColor = Color.Gray
                    };
                    tooltips.Insert(insertAt++, shiftLine);
                }
            }

            tooltips.Insert(insertAt++, new TooltipLine(Mod, "RunningAccel", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Horizon.RunningAccel")));

            TooltipLine toggleLine = new TooltipLine(Mod, "HorizonToggle", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.ToggleWings"))
            {
                OverrideColor = Color.Gray
            };
            tooltips.Add(toggleLine);
        }

        public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, !wingsDisabled);
        }

        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.9f;
            ascentWhenRising = 0.135f;
            maxCanAscendMultiplier = 1.15f;
            maxAscentMultiplier = 2.2f;
            constantAscend = 0.14f;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var cojPlayer = player.GetModPlayer<TemplatePlayer>();
            cojPlayer.Horizon = null;

            player.accRunSpeed -= 3.45f;
            player.moveSpeed += 0.04f;

            if (wingsDisabled)
            {
                player.rocketBoots = (player.vanityRocketBoots = 2);
            }

            if (!hideVisual && player.controlJump && player.velocity.Y < player.oldVelocity.Y && player.wingTime > 0)
            {
                if (Main.rand.NextBool(3) && Main.myPlayer == player.whoAmI)
                {
                    int proj = Projectile.NewProjectile(
                        player.GetSource_Accessory(item),
                        player.MountedCenter,
                        new Vector2(Main.rand.NextFloat(-50f, 50f), 0),
                        ModContent.ProjectileType<Horizon_Ascension>(),
                        0, 0f, player.whoAmI
                    );
                    Main.projectile[proj].netUpdate = true;
                }
            }
        }
    }
}