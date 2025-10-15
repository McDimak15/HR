using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using ContinentOfJourney;

namespace HomewardRagnarok
{
    public class DivineTouchBuffs : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (IsTargetItem(item))
            {
                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
                modPlayer.DivineEmblem = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (IsTooltipTargetItem(item))
            {
                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                    coj.TryFind("DivineTouch", out ModItem divineTouch))
                {
                    float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);

                    Color purple = new Color(128, 0, 128);
                    Color white = Color.White;
                    Color animatedColor = Color.Lerp(purple, white, (float)(0.5 * (1 + Math.Sin(timer * MathHelper.TwoPi))));

                    string iconTag = $"[i:{divineTouch.Type}] ";
                    tooltips.RemoveAll(t => t.Name == "DivineFireEffect");
                    tooltips.Add(new TooltipLine(Mod, "DivineFireEffect",
                        iconTag + "Causes melee attacks to inflict the Divine Fire debuff (Homeward Ragnarok)")
                    { OverrideColor = animatedColor });
                }
            }
        }

        private bool IsTargetItem(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                item.type == calamity.Find<ModItem>("ElementalGauntlet")?.Type)
                return true;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int berserkerSoul = fargo.Find<ModItem>("BerserkerSoul")?.Type ?? 0;
                int universeSoul = fargo.Find<ModItem>("UniverseSoul")?.Type ?? 0;
                int eternitySoul = fargo.Find<ModItem>("EternitySoul")?.Type ?? 0;

                if (item.type == berserkerSoul || item.type == universeSoul || item.type == eternitySoul)
                    return true;
            }

            return false;
        }

        private bool IsTooltipTargetItem(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                item.type == calamity.Find<ModItem>("ElementalGauntlet")?.Type)
                return true;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int berserkerSoul = fargo.Find<ModItem>("BerserkerSoul")?.Type ?? 0;
                int universeSoul = fargo.Find<ModItem>("UniverseSoul")?.Type ?? 0;

                if (item.type == berserkerSoul)
                    return true;
            }

            return false;
        }
    }
}
