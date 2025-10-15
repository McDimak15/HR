using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using ContinentOfJourney;

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
            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                if (item.type == fargo.Find<ModItem>("ConjuristsSoul")?.Type)
                    return true;
                if (item.type == fargo.Find<ModItem>("UniverseSoul")?.Type)
                    return true;
                if (item.type == fargo.Find<ModItem>("EternitySoul")?.Type)
                    return true;
            }
            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
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
                    bool fargoLoaded = ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo);

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

                    if (fargoLoaded)
                    {
                        int conjuristsSoulType = fargo.Find<ModItem>("ConjuristsSoul")?.Type ?? -1;
                        int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;
                        int eternitySoulType = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;

                        if (item.type == conjuristsSoulType || item.type == universeSoulType || item.type == eternitySoulType)
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
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) && coj.TryFind("LampreyScarf", out ModItem lampreyScarf))
            {
                bool isCalamityItem = false;
                bool isFargoItem = false;

                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                {
                    int statisCurseType = calamity.Find<ModItem>("StatisCurse")?.Type ?? -1;
                    int nucleogenesisType = calamity.Find<ModItem>("Nucleogenesis")?.Type ?? -1;
                    if (item.type == statisCurseType || item.type == nucleogenesisType)
                        isCalamityItem = true;
                }

                if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
                {
                    int conjuristsSoulType = fargo.Find<ModItem>("ConjuristsSoul")?.Type ?? -1;
                    int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;
                    int eternitySoulType = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;

                    if (item.type == conjuristsSoulType)
                        isFargoItem = true;
                }

                if (isCalamityItem || isFargoItem)
                {
                    AddLampreyTooltips(lampreyScarf, tooltips);
                }
            }
        }

        private void AddLampreyTooltips(ModItem lampreyScarf, List<TooltipLine> tooltips)
        {
            string iconTag = $"[i:{lampreyScarf.Type}] ";

            tooltips.RemoveAll(t => t.Name == "LampreyScarfBuff1");
            tooltips.RemoveAll(t => t.Name == "LampreyScarfBuff2");
            tooltips.RemoveAll(t => t.Name == "LampreyScarfBuff3");

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);

            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, (float)(0.5 * (1 + Math.Sin(timer * MathHelper.TwoPi))));

            tooltips.Add(new TooltipLine(Mod, "LampreyScarfBuff1",
                iconTag + "Summons Lamprey Scarf. (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            });

            tooltips.Add(new TooltipLine(Mod, "LampreyScarfBuff2",
                iconTag + "Increase whip range by 10%. (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            });

            tooltips.Add(new TooltipLine(Mod, "LampreyScarfBuff3",
                iconTag + "Increases your max number of sentries by 1. (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            });
        }
    }
}
