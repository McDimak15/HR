using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ContinentOfJourney;

namespace HomewardRagnarok
{
    public class AsgardsValorTooltip : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem?.Name != "AsgardsValor") return;

            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            tooltips.Add(new TooltipLine(Mod, "HomewardRagnarok1","Grants immunity to Vulnerable")
            {
                OverrideColor = animatedColor
            });
            tooltips.Add(new TooltipLine(Mod, "HomewardRagnarok2","Damage taken by debuffs decreased by 35%")
            {
                OverrideColor = animatedColor
            });
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem?.Name != "AsgardsValor") return;

            player.noKnockback = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Ichor] = true;

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod))
            {
                int vulnerableBuff = ModContent.BuffType<ContinentOfJourney.Buffs.VulnerableBuff>();
                player.buffImmune[vulnerableBuff] = true;
            }

            player.GetModPlayer<TemplatePlayer>().PinkPenny = true;
            player.GetModPlayer<TemplatePlayer>().TransactionCertificate = false; 
        }
    }
}
