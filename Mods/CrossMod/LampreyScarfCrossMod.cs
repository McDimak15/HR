using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class LampreyScarfCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) && coj.TryFind("LampreyScarf", out ModItem lampreyScarf))
            {
                if (item.type == lampreyScarf.Type)
                    return true;
            }
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (item.type == calamity.Find<ModItem>("StatisCurse")?.Type)
                    return true;
                if (item.type == calamity.Find<ModItem>("Nucleogenesis")?.Type)
                    return true;
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) && coj.TryFind("LampreyScarf", out ModItem lampreyScarf))
            {
                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();

                if (item.type == lampreyScarf.Type)
                {
                    player.dd2Accessory = true;
                    player.whipRangeMultiplier += 0.10f;
                    modPlayer.Lamprey = true;
                    player.AddBuff(ModContent.BuffType<ContinentOfJourney.Buffs.LampreyBuff>(), 2);
                }
                else
                {
                    bool calamityLoaded = ModLoader.TryGetMod("CalamityMod", out Mod calamity);

                    if (calamityLoaded)
                    {
                        int statisCurseType = calamity.Find<ModItem>("StatisCurse")?.Type ?? -1;
                        int nucleogenesisType = calamity.Find<ModItem>("Nucleogenesis")?.Type ?? -1;

                        if (item.type == statisCurseType || item.type == nucleogenesisType)
                        {
                            player.dd2Accessory = true;
                            player.whipRangeMultiplier += 0.10f;
                            modPlayer.Lamprey = true;
                            player.AddBuff(ModContent.BuffType<ContinentOfJourney.Buffs.LampreyBuff>(), 2);
                        }
                    }
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ServerConfig.Instance.CalamityBalance)
                return;

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) && coj.TryFind("LampreyScarf", out ModItem lampreyScarf))
            {
                bool isCalamityItem = false;

                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    int statisCurseType = calamity.Find<ModItem>("StatisCurse")?.Type ?? -1;
                    int nucleogenesisType = calamity.Find<ModItem>("Nucleogenesis")?.Type ?? -1;
                    if (item.type == statisCurseType || item.type == nucleogenesisType)
                        isCalamityItem = true;
                }

                if (isCalamityItem)
                {
                    AddLampreyTooltips(lampreyScarf, tooltips);
                }
            }
        }

        private void AddLampreyTooltips(ModItem lampreyScarf, List<TooltipLine> tooltips)
        {
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
            tooltips.Insert(insertAt, new TooltipLine(Mod, "LampreyScarfBuff1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.LampreyScarf")) { OverrideColor = animatedColor });
        }
    }
}
