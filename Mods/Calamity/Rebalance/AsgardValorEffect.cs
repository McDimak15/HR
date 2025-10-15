using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
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

            int vanguardType = ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)
                ? cojMod.Find<ModItem>("VanguardBreastpiece")?.Type ?? 0
                : 0;
            string iconTag = vanguardType != 0 ? $"[i:{vanguardType}] " : "";

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, (float)(0.5f * (1 + System.Math.Sin(timer * 6.2831))));

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarok1" || t.Name == "HomewardRagnarok2");

            tooltips.Add(new TooltipLine(Mod, "HomewardRagnarok1", iconTag + "Grants immunity to Vulnerable (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            });
            tooltips.Add(new TooltipLine(Mod, "HomewardRagnarok2", iconTag + "Damage taken by debuffs decreased by 35% (Homeward Ragnarok)")
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
