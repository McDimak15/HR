using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok.CrossMod
{
    public class NucleogenesisCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("Nucleogenesis", out ModItem nucleogenesis) &&
                item.type == nucleogenesis.Type)
            {
                return true;
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int conjuristsSoulType = fargo.Find<ModItem>("ConjuristsSoul")?.Type ?? -1;
                int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;

                if (item.type == conjuristsSoulType || item.type == universeSoulType)
                    return true;
            }

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();

            bool hasItem = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("Nucleogenesis", out ModItem nucleogenesis) &&
                item.type == nucleogenesis.Type)
            {
                hasItem = true;
            }

            if (!hasItem && ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int conjuristsSoulType = fargo.Find<ModItem>("ConjuristsSoul")?.Type ?? -1;
                int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;
                int eternitySoulType = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;

                if (item.type == conjuristsSoulType || item.type == universeSoulType)
                {
                    hasItem = true;
                }
            }

            if (hasItem)
            {
                modPlayer.armillarySphere = true;

                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod))
                {
                    if (cojMod.TryFind("ArmillarySphere", out ModProjectile armillaryProj))
                    {
                        int projType = armillaryProj.Type;
                        if (player.ownedProjectileCounts[projType] < 1)
                        {
                            Projectile.NewProjectile(
                                player.GetSource_Accessory(item), 
                                player.MountedCenter,
                                Vector2.Zero,
                                projType,
                                0,
                                0f,
                                player.whoAmI
                            );
                        }
                    }
                }
            }

        }


        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            bool showTooltip = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("Nucleogenesis", out ModItem nucleogenesis) &&
                item.type == nucleogenesis.Type)
            {
                showTooltip = true;
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int conjuristsSoulType = fargo.Find<ModItem>("ConjuristsSoul")?.Type ?? -1;
                if (item.type == conjuristsSoulType)
                    showTooltip = true;
            }

            if (!showTooltip)
                return;

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color purple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(purple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int armillarySphereType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.ArmillarySphere>();
            string iconTag = $"[i:{armillarySphereType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokBonus",
                iconTag + "Your sentries form a constellation that hurts for 50% of your held weapon's damage (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokBonus");
            tooltips.Add(customLine);
        }

    }
}
