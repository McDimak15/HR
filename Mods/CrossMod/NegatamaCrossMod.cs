using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class NegatamaCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("EtherealTalisman", out ModItem talisman) &&
                item.type == talisman.Type)
            {
                return true;
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            var modPlayer = player.GetModPlayer<NegatamaPlayer>();
            modPlayer.Negatama = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            bool showTooltip = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("EtherealTalisman", out ModItem talisman) &&
                item.type == talisman.Type)
            {
                showTooltip = true;
            }

            if (!showTooltip)
                return;

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
            tooltips.Insert(insertAt, new TooltipLine(Mod, "HomewardRagnarokNegatama", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Negatama")) { OverrideColor = animatedColor });
        }
    }

    public class NegatamaPlayer : ModPlayer
    {
        public bool Negatama;
        public int NegatamaCounter;

        public override void ResetEffects()
        {
            Negatama = false;
        }

        public override void PostUpdate()
        {
            if (!Negatama)
                return;

            NegatamaCounter++;
            if (NegatamaCounter >= 32)
            {
                if (Main.myPlayer == Player.whoAmI &&
                    ModLoader.TryGetMod("ContinentOfJourney", out var coj) &&
                    coj.TryFind("Negatama", out ModItem negatama))
                {
                    int projType = coj.Find<ModProjectile>("Negatama")?.Type ?? -1;
                    if (projType != -1)
                    {
                        int proj = Projectile.NewProjectile(
                            Player.GetSource_Misc("Negatama"),
                            Player.Center,
                            new Vector2(6f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                            projType,
                            0,
                            0,
                            Player.whoAmI
                        );
                        Main.projectile[proj].netUpdate = true;
                    }
                }
                NegatamaCounter = 0;
            }
        }
    }
}
