using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class GrandSpectralGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (IsGrandSpectralTarget(item) &&
                ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                coj.TryFind("GrandSpectral", out ModItem _))
            {
                player.longInvince = true;

                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
                modPlayer.Spectral = true;

                if (Main.myPlayer == player.whoAmI && modPlayer.SpectralSummon > 0)
                {
                    do
                    {
                        int proj = Projectile.NewProjectile(
                            player.GetSource_Accessory(item),
                            player.Center,
                            new Vector2(12f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                            ModContent.ProjectileType<ContinentOfJourney.Projectiles.GrandSpectral>(),
                            0,
                            0,
                            player.whoAmI
                        );
                        Main.projectile[proj].netUpdate = true;
                        modPlayer.SpectralSummon -= 1;
                    }
                    while (modPlayer.SpectralSummon > 0);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (IsTarget(item) && ModLoader.HasMod("ContinentOfJourney"))
            {
                if (item.ModItem?.Mod.Name == "Clamity" && item.ModItem.Name == "SupremeBarrier" && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                    return;

                if (item.ModItem?.Mod.Name == "CalamityMod" && item.ModItem.Name == "RampartofDeities" && Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
                    return;

                Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

                string gss1 = "Increases length of invincibility";
                string gss2 = "Summon spectrals when damaged";
                string gss3 = "Summon spectrals when hitting enemy";

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
                tooltips.Insert(insertAt, new TooltipLine(Mod, "GSS1", gss1) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt + 1, new TooltipLine(Mod, "GSS2", gss2) { OverrideColor = animatedColor });
                tooltips.Insert(insertAt + 2, new TooltipLine(Mod, "GSS3", gss3) { OverrideColor = animatedColor });
            }
        }

        private bool IsGrandSpectralTarget(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("RampartofDeities", out ModItem rampart) &&
                item.type == rampart.Type && ServerConfig.Instance.CalamityBalance)
                return true;
            if (ModLoader.TryGetMod("Clamity", out Mod clamity) &&
                clamity.TryFind("SupremeBarrier", out ModItem barrier) &&
                item.type == barrier.Type && ServerConfig.Instance.ClamityBalance)
                return true;

            return false;
        }

        private bool IsTarget(Item item)
        {
            if (item.ModItem == null) return false;
            string mod = item.ModItem.Mod.Name;
            string name = item.ModItem.Name;

            bool isCalamity = mod == "CalamityMod" && name == "RampartofDeities" && ServerConfig.Instance.CalamityBalance;
            bool isClamity = mod == "Clamity" && name == "SupremeBarrier" && ServerConfig.Instance.ClamityBalance;

            return isCalamity || isClamity;
        }
    }
}
