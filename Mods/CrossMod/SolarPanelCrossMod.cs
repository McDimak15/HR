using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class SolarPanelTooltipFix : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && ServerConfig.Instance.CalamityBalance)
            {
                string[] calamityItems = { "OrnateShield", "AsgardsValor", "AsgardianAegis" };
                foreach (string itemName in calamityItems)
                {
                    if (calamityMod.TryFind(itemName, out ModItem modItem) && entity.type == modItem.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("Clamity", out Mod clamityMod) && ServerConfig.Instance.ClamityBalance)
            {
                if (clamityMod.TryFind("SupremeBarrier", out ModItem barrier) && entity.type == barrier.Type)
                    return true;
            }

            return false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (ModLoader.TryGetMod("Clamity", out Mod clamity) &&
                        clamity.TryFind("SupremeBarrier", out ModItem barrier))
            {
                if (item.type == barrier.Type && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                {
                    return;
                }
            }

            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            int maxTooltipIndex = -1;
            int maxNumber = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        maxTooltipIndex = i;
                    }
                }
                else if (tooltips[i].Mod == "InfernalEclipseAPI")
                {
                    maxTooltipIndex = i;
                }
            }

            int insertAt = maxTooltipIndex != -1 ? maxTooltipIndex + 1 : tooltips.Count;
            tooltips.Insert(insertAt, new TooltipLine(Mod, "SolarPanelBonus", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.SolarPanel")) { OverrideColor = animatedColor });
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            bool calamityEquipped = false;
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod) && ServerConfig.Instance.CalamityBalance)
            {
                string[] calamityItems = { "OrnateShield", "AsgardsValor", "AsgardianAegis" };
                foreach (string itemName in calamityItems)
                {
                    if (calamityMod.TryFind(itemName, out ModItem modItem) && item.type == modItem.Type)
                    {
                        calamityEquipped = true;
                        break;
                    }
                }
            }

            bool clamityEquipped = false;
            if (ModLoader.TryGetMod("Clamity", out Mod clamityMod) && ServerConfig.Instance.ClamityBalance)
            {
                if (clamityMod.TryFind("SupremeBarrier", out ModItem barrier) && item.type == barrier.Type)
                {
                    clamityEquipped = true;
                }
            }

            if (calamityEquipped || clamityEquipped)
            {
                SolarPanelLogic(player);
            }
        }

        private void SolarPanelLogic(Player player)
        {
            player.GetModPlayer<SolarPanelPlayer>().RunEffect(player);
        }
    }

    public class SolarPanelPlayer : ModPlayer
    {
        private int solarPanelTimer = 0;

        public void RunEffect(Player player)
        {
            solarPanelTimer += 2;
            if (solarPanelTimer >= 15 && player.velocity.Length() > 2f)
            {
                solarPanelTimer = 0;
                if (Main.myPlayer == player.whoAmI)
                {
                    int proj = Projectile.NewProjectile(
                        player.GetSource_Accessory(player.armor[3]),
                        player.Center,
                        new Vector2(0.5f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                        ModContent.ProjectileType<ContinentOfJourney.Projectiles.SolarPanel>(),
                        80,
                        0,
                        Main.myPlayer
                    );
                    Main.projectile[proj].netUpdate = true;
                }
            }
        }
    }
}
