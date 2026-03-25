using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.Graphics.Shaders;
using System.Collections.Generic;
using SOTSBardHealer;
using ContinentOfJourney.Items.Accessories;
using HomewardRagnarok.Config;
using InfernalEclipseAPI.Core.Utils;

namespace HomewardRagnarok.Mods.SOTS
{
    [JITWhenModsEnabled("SOTSBardHealer", "SOTS")]
    [ExtendsFromMod("SOTSBardHealer", "SOTS")]
    public class SOTSSubspaceDash : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ModContent.ItemType<Horizon>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            int originalShoeDye = player.cShoe;

            if (player.cShoe == 0)
            {
                player.cShoe = GameShaders.Armor.GetShaderIdFromItemId(ItemID.BrightBlueDye);
            }

            ModContent.GetInstance<global::SOTS.Items.SubspaceBoosters>().UpdateAccessory(player, hideVisual);

            player.cShoe = originalShoeDye;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var keybind = InfernalEclipseAPI.InfernalEclipseAPI.SubpaceBoostHotkey;
            string hotkeyName = "Unbound";

            if (keybind != null)
            {
                var keys = keybind.GetAssignedKeys();
                if (keys.Count > 0)
                {
                    hotkeyName = keys[0];
                }
            }

            string dashTooltip = Language.GetTextValue(
                "Mods.InfernalEclipseAPI.ItemTooltip.MergedCraftingTreeTooltip.SubspaceBoostersDash",
                hotkeyName
            );

            int lastTooltipIndex = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" &&
                    tooltips[i].Name.StartsWith("Tooltip") &&
                    !tooltips[i].Text.StartsWith("'"))
                {
                    lastTooltipIndex = i;
                }
            }

            if (lastTooltipIndex != -1)
            {
                tooltips.Insert(lastTooltipIndex + 1, new TooltipLine(Mod, "HorizonDashTooltip", dashTooltip));
            }
        }
    }

    [JITWhenModsEnabled("SOTSBardHealer", "SOTS")]
    [ExtendsFromMod("SOTSBardHealer", "SOTS")]
    public class SubspaceDashColorFix : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private int _originalDye;

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<global::SOTS.Items.SubspaceBoosterProj>())
            {
                Player player = Main.player[projectile.owner];
                _originalDye = player.cShoe;

                if (player.cShoe == 0)
                {
                    player.cShoe = GameShaders.Armor.GetShaderIdFromItemId(ItemID.BrightBlueDye);
                }
            }
            return true;
        }

        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<global::SOTS.Items.SubspaceBoosterProj>())
            {
                Player player = Main.player[projectile.owner];
                player.cShoe = _originalDye;
            }
        }
    }
}