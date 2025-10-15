using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok.CrossMod
{
    public class NegatamaCrossMod : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("EtherealTalisman", out ModItem talisman) &&
                item.type == talisman.Type)
            {
                return true;
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int archWizardSoulType = fargo.Find<ModItem>("ArchWizardsSoul")?.Type ?? -1;
                int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;
                int eternitySoulType = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;

                if (item.type == archWizardSoulType ||
                    item.type == universeSoulType ||
                    item.type == eternitySoulType)
                    return true;
            }

            return false;
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj))
                return;

            var modPlayer = player.GetModPlayer<NegatamaPlayer>();
            modPlayer.Negatama = true;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            bool showTooltip = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("EtherealTalisman", out ModItem talisman) &&
                item.type == talisman.Type)
            {
                showTooltip = true;
            }

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int archWizardSoulType = fargo.Find<ModItem>("ArchWizardsSoul")?.Type ?? -1;
                int universeSoulType = fargo.Find<ModItem>("UniverseSoul")?.Type ?? -1;
                int eternitySoulType = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;

                if (item.type == archWizardSoulType)
                    showTooltip = true;

                if (item.type == universeSoulType || item.type == eternitySoulType)
                    showTooltip = false;
            }

            if (!showTooltip)
                return;

            float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
            Color darkPurple = new Color(128, 0, 128);
            Color white = Color.White;
            Color animatedColor = Color.Lerp(darkPurple, white, 0.5f * (1f + (float)Math.Sin(timer * MathHelper.TwoPi)));

            int negatamaType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.Negatama>();
            string iconTag = $"[i:{negatamaType}] ";

            TooltipLine customLine = new TooltipLine(Mod, "HomewardRagnarokNegatama",
                iconTag + "Releases dark energy, the lower mana you have, the more damage it deals (Homeward Ragnarok)")
            {
                OverrideColor = animatedColor
            };

            tooltips.RemoveAll(t => t.Name == "HomewardRagnarokNegatama");
            tooltips.Add(customLine);
        }
    }

    public class NegatamaPlayer : ModPlayer
    {
        public bool Negatama;
        public int NegatamaCounter;

        public override void ResetEffects()
        {
            Negatama = false;
        }

        public override void PostUpdate()
        {
            if (!Negatama)
                return;

            NegatamaCounter++;
            if (NegatamaCounter >= 32)
            {
                if (Main.myPlayer == Player.whoAmI &&
                    ModLoader.TryGetMod("ContinentOfJourney", out var coj) &&
                    coj.TryFind("Negatama", out ModItem negatama))
                {
                    int projType = coj.Find<ModProjectile>("Negatama")?.Type ?? -1;
                    if (projType != -1)
                    {
                        int proj = Projectile.NewProjectile(
                            Player.GetSource_Misc("Negatama"),
                            Player.Center,
                            new Vector2(6f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                            projType,
                            0,
                            0,
                            Player.whoAmI
                        );
                        Main.projectile[proj].netUpdate = true;
                    }
                }
                NegatamaCounter = 0;
            }
        }
    }
}
