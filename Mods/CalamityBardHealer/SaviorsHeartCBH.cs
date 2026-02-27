using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;
using ThoriumMod;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]

    public class CBHSaviorsHeartCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            if (ModLoader.TryGetMod("CalamityBardHealer", out Mod cbhMod))
            {
                string[] cbhItems = { "BloomingSaintessStatue", "ElementalBloom" };
                foreach (string name in cbhItems)
                {
                    if (cbhMod.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
            {
                string[] fargoItems = { "UniverseSoul", "EternitySoul" };
                foreach (string name in fargoItems)
                {
                    if (fargoMod.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.GetModPlayer<TemplatePlayer>().SaviorsHeart = true;
            player.GetModPlayer<ThoriumPlayer>().healBonus += 4;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod))
            {
                string[] hiddenTooltipItems = { "UniverseSoul", "EternitySoul" };
                foreach (string name in hiddenTooltipItems)
                {
                    if (fargoMod.TryFind(name, out ModItem hiddenItem) && item.type == hiddenItem.Type)
                    {
                        tooltips.RemoveAll(t => t.Name == "SaviorsHeart1" || t.Name == "SaviorsHeart2");
                        return; 
                    }
                }
            }

            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));

            TooltipLine line1 = new TooltipLine(Mod, "SaviorsHeart1","Heals you by 20 health for every 100 mana consumed")
            {
                OverrideColor = animatedColor
            };

            TooltipLine line2 = new TooltipLine(Mod, "SaviorsHeart2","Healing spells will heal an additional 6 life")
            {
                OverrideColor = animatedColor
            };

            tooltips.Add(line1);
            tooltips.Add(line2);
        }

    }
}
