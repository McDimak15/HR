using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
    public class BalanceTweaks : GlobalItem
    {
        private static int laserSniperRifleType;
        private static int toothOfCthulhuType;
        private static int cactusBallType;
        private static int veinType;

        private static readonly Dictionary<string, float> rapierDamageBoosts = new()
        {
            { "CopperRapier", 1.3f },
            { "GoldRapier", 1.3f },
            { "IronRapier", 1.3f },
            { "LeadRapier", 1.3f },
            { "PlatinumRapier", 1.3f },
            { "SilverRapier", 1.3f },
            { "TinRapier", 1.3f },
            { "TungstenRapier", 1.3f }
        };

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj))
            {
                laserSniperRifleType = coj.Find<ModItem>("LaserSniperRifle")?.Type ?? 0;
                toothOfCthulhuType = coj.Find<ModItem>("ToothOfCthulhu")?.Type ?? 0;
                cactusBallType = coj.Find<ModItem>("ItemCactusBall")?.Type ?? 0;
                veinType = coj.Find<ModItem>("Vein")?.Type ?? 0;

                if (entity.type == laserSniperRifleType ||
                    entity.type == toothOfCthulhuType ||
                    entity.type == cactusBallType ||
                    entity.type == veinType)
                    return true;

                foreach (var rapier in rapierDamageBoosts.Keys)
                {
                    int rapierType = coj.Find<ModItem>(rapier)?.Type ?? 0;
                    if (entity.type == rapierType)
                        return true;
                }

                if (entity.type == coj.Find<ModItem>("Backstabber")?.Type) return true;
                if (entity.type == coj.Find<ModItem>("Bloodthirst")?.Type) return true;
                if (entity.type == coj.Find<ModItem>("ConniversKunai")?.Type) return true;
                if (entity.type == coj.Find<ModItem>("FT2Wildfire")?.Type) return true;
                if (entity.type == coj.Find<ModItem>("FT1Sparkthrower")?.Type) return true;
                if (entity.type == coj.Find<ModItem>("StaffCursedFlower")?.Type) return true;
            }

            return false;
        }

        public override void SetDefaults(Item item)
        {
            // Laser sniper rifle
            if (item.type == laserSniperRifleType)
            {
                item.damage -= 400;
                if (item.damage < 1) item.damage = 1;
            }

            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj))
            {
                // Rapier buffs
                foreach (var kvp in rapierDamageBoosts)
                {
                    int rapierType = coj.Find<ModItem>(kvp.Key)?.Type ?? 0;
                    if (item.type == rapierType)
                    {
                        item.damage = (int)(item.damage * kvp.Value);
                    }
                }

                // Backstabber
                if (item.type == coj.Find<ModItem>("Backstabber")?.Type)
                    item.useTime = item.useAnimation = 12;

                // Bloodthirst
                if (item.type == coj.Find<ModItem>("Bloodthirst")?.Type)
                    item.useTime = item.useAnimation = 20;

                // Connivers Kunai
                if (item.type == coj.Find<ModItem>("ConniversKunai")?.Type)
                    item.damage = 24;

                // Wildfire
                if (item.type == coj.Find<ModItem>("FT2Wildfire")?.Type)
                    item.damage = 18;

                // Sparkthrower
                if (item.type == coj.Find<ModItem>("FT1Sparkthrower")?.Type)
                    item.damage = 9;

                // FountainStaff
                if (item.type == coj.Find<ModItem>("StaffCursedFlower")?.Type)
                    item.damage = 140;

                // Cactus Ball
                if (item.type == cactusBallType)
                {
                    item.useTime = 30;
                    item.useAnimation = 30;
                }

                // Tooth of Cthulhu
                if (item.type == toothOfCthulhuType)
                {
                    item.damage = 15;
                }

                // Vein
                if (item.type == veinType)
                {
                    item.damage = 28; 
                    item.useTime = item.useAnimation = 20;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == cactusBallType)
            {
                tooltips.Add(new TooltipLine(Mod, "PoisonEffect", "Inflicts Poison"));
            }
        }
    }

    public class BalanceTweaksProj : GlobalProjectile
    {
        private static int cactusBall1ProjType;
        private static int cactusBall2ProjType;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            if (cactusBall1ProjType == 0 || cactusBall2ProjType == 0)
            {
                if (ModLoader.TryGetMod("ContinentOfJourney", out var coj))
                {
                    cactusBall1ProjType = coj.Find<ModProjectile>("ProjCactusBall_1")?.Type ?? 0;
                    cactusBall2ProjType = coj.Find<ModProjectile>("ProjCactusBall_2")?.Type ?? 0;
                }
            }

            return entity.type == cactusBall1ProjType || entity.type == cactusBall2ProjType;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == cactusBall1ProjType || projectile.type == cactusBall2ProjType)
            {
                target.AddBuff(BuffID.Poisoned, 180);
            }
        }
    }
}
