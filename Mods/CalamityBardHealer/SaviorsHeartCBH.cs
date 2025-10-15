using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok
{
    public class CBHSaviorsHeartCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityBardHealer", out Mod cbhMod))
            {
                string[] cbhItems = { "BloomingSaintessStatue", "ElementalBloom" };
                foreach (string name in cbhItems)
                {
                    if (cbhMod.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("SSM", out Mod ssmMod))
            {
                if (ssmMod.TryFind("GuardianAngelsSoul", out ModItem item) && entity.type == item.Type)
                    return true;
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
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod))
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

            int saviorsHeartType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.SaviorsHeart>();
            string iconTag = $"[i:{saviorsHeartType}] ";

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            TooltipLine line1 = new TooltipLine(Mod, "SaviorsHeart1",
                iconTag + "Heals you by 20 health for every 100 mana consumed (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            TooltipLine line2 = new TooltipLine(Mod, "SaviorsHeart2",
                iconTag + "Increase healing amount by 6 (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "SaviorsHeart1" || t.Name == "SaviorsHeart2");
            tooltips.Add(line1);
            tooltips.Add(line2);
        }

    }
}
