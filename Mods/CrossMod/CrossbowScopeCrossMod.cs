using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok.CrossMod
{
    public class ElementalQuiverAndSnipersSoulCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("ElementalQuiver", out ModItem eq) &&
                entity.type == eq.Type)
                return true;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo) &&
                fargo.TryFind("SnipersSoul", out ModItem ss) &&
                entity.type == ss.Type)
                return true;

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            var modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.GooglesOn = true;
            modPlayer.StarQuiver = true;

            player.magicQuiver = true;
            player.aggro -= 400;

            if (player.HeldItem.useAmmo == AmmoID.Bullet)
                player.scope = true;

            player.GetDamage(DamageClass.Ranged) += 0.05f;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.arrowDamage *= 1.15f;
        }

        private static float colorTimer = 0f;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            colorTimer += 0.0025f;
            if (colorTimer > 1f) colorTimer = 0f;

            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, 0.5f * (1f + (float)Math.Sin(colorTimer * MathHelper.TwoPi)));

            int airborneType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.AirborneGoogles>();
            int starQuiverType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.StarQuiver>();
            int crossbowScopeType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.CrossbowScope>();

            tooltips.RemoveAll(t => t.Name.StartsWith("HR_Bonus"));

            tooltips.Add(new TooltipLine(Mod, "HR_Bonus1", $"[i:{airborneType}] Show a bullet trace of bullet-firing guns (Homeward Ragnarok)") { OverrideColor = animatedColor });
            tooltips.Add(new TooltipLine(Mod, "HR_Bonus2", $"[i:{starQuiverType}] Turn wooden arrows into holy arrows (Homeward Ragnarok)") { OverrideColor = animatedColor });
            tooltips.Add(new TooltipLine(Mod, "HR_Bonus3", $"[i:{starQuiverType}] Increase the damage of arrows (Homeward Ragnarok)") { OverrideColor = animatedColor });
            tooltips.Add(new TooltipLine(Mod, "HR_Bonus4", $"[i:{starQuiverType}] Enemies are less likely to target you (Homeward Ragnarok)") { OverrideColor = animatedColor });
            tooltips.Add(new TooltipLine(Mod, "HR_Bonus5", $"[i:{crossbowScopeType}] +7% increased ranged critical strike chance (Homeward Ragnarok)") { OverrideColor = animatedColor });
            tooltips.Add(new TooltipLine(Mod, "HR_Bonus6", $"[i:{crossbowScopeType}] +5% increased ranged damage (Homeward Ragnarok)") { OverrideColor = animatedColor });
        }
    }
}
