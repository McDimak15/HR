using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class SolarPanelTooltipFix : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
            {
                string[] calamityItems = { "OrnateShield", "AsgardsValor", "AsgardianAegis" };
                foreach (string itemName in calamityItems)
                {
                    if (calamityMod.TryFind(itemName, out ModItem modItem) && entity.type == modItem.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
            {
                int colossusSoulType = fargoMod.Find<ModItem>("ColossusSoul")?.Type ?? 0;
                if (entity.type == colossusSoulType)
                    return true;
            }

            if (ModLoader.TryGetMod("Clamity", out Mod clamityMod))
            {
                if (clamityMod.TryFind("SupremeBarrier", out ModItem barrier) && entity.type == barrier.Type)
                    return true;
            }

            return false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out _))
                return;

            if (ModLoader.TryGetMod("Clamity", out Mod clamityMod))
            {
                if (clamityMod.TryFind("SupremeBarrier", out ModItem barrier) && item.type == barrier.Type)
                    return;
            }

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, (float)(0.5 * (1 + Math.Sin(timer * MathHelper.TwoPi))));

            int solarPanelType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.SolarPanel>();
            string iconTag = $"[i:{solarPanelType}] ";

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokSolarPanel");
            tooltips.Add(new TooltipLine(Mod, "HomewardRagnarokSolarPanel", iconTag + "Generates energy balls while moving (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            });
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out _))
                return;

            int colossusSoulType = -1;
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
            {
                colossusSoulType = fargoMod.Find<ModItem>("ColossusSoul")?.Type ?? -1;
            }

            bool calamityEquipped = false;
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamityMod))
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
            if (ModLoader.TryGetMod("Clamity", out Mod clamityMod))
            {
                if (clamityMod.TryFind("SupremeBarrier", out ModItem barrier) && item.type == barrier.Type)
                {
                    clamityEquipped = true;
                }
            }

            if (item.type == colossusSoulType || calamityEquipped || clamityEquipped)
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
