using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using ContinentOfJourney.Projectiles;
using ContinentOfJourney.Items.Accessories;
using ThrowerUnification.Core.UnitedModdedThrowerClass;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Detections
{
    public class TrinityCompatGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private int lastIValA = 0;
        private bool initialized = false;

        public override void PostAI(Projectile projectile)
        {
            if (projectile.DamageType != ModContent.GetInstance<UnitedModdedThrower>())
                return;

            if (!projectile.TryGetGlobalProjectile(out CoJGlobalProjectile_Instance cojProj))
                return;

            if (!initialized)
            {
                if (cojProj.iValA == 0) cojProj.iValA = 1;

                cojProj.iValD = 0;
                lastIValA = cojProj.iValA;
                initialized = true;
            }

            if (cojProj.iValA != lastIValA)
            {
                int factor = cojProj.iValA / lastIValA;
                if (factor == 2 || factor == 3 || factor == 5)
                {
                    float originalDamage = (float)Math.Round(projectile.damage / 1.7f);
                    projectile.damage = (int)(originalDamage * 1.2f);
                }
                lastIValA = cojProj.iValA;
            }
        }
    }

    public class TrinityTooltipFix : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            bool isTrinityItem = item.type == ModContent.ItemType<Alpha>() ||
                                 item.type == ModContent.ItemType<Omega>() ||
                                 item.type == ModContent.ItemType<Epsilon>() ||
                                 item.type == ModContent.ItemType<TheHolyTrinity>() ||
                                 item.type == ModContent.ItemType<FatherAndSon>();

            if (!isTrinityItem) return;

            string repClass = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.HolyTrinity.RepTrinityClass");
            string searchClass = Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.HolyTrinity.OrigTrinityClass");

            foreach (var line in tooltips)
            {
                if (line.Text.Contains("70%"))
                {
                    line.Text = line.Text.Replace("70%", "20%");
                }

                if (line.Text.Contains(searchClass))
                {
                    line.Text = line.Text.Replace(searchClass, repClass);
                }
            }
        }
    }
}