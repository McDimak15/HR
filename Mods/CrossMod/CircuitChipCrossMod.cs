using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney;
using ContinentOfJourney.Buffs;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class MiniLinkaCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) && ServerConfig.Instance.CalamityBalance)
            {
                string[] calamityItems = { "AsgardsValor", "AsgardianAegis" };
                foreach (string name in calamityItems)
                {
                    if (calamity.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }

            if (ModLoader.TryGetMod("Clamity", out Mod clamity) && ServerConfig.Instance.ClamityBalance)
            {
                string[] clamityItems = { "SupremeBarrier" };
                foreach (string name in clamityItems)
                {
                    if (clamity.TryFind(name, out ModItem item) && entity.type == item.Type)
                        return true;
                }
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.MiniLinka = true;
            player.AddBuff(ModContent.BuffType<MiniLinkaBuff>(), 2);
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
            tooltips.Insert(insertAt, new TooltipLine(Mod, "MiniLinkaEffect", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.CircuitChip")) { OverrideColor = animatedColor });
        }
    }
}
