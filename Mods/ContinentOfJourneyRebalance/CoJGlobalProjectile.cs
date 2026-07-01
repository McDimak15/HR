using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney.Buffs;
using ContinentOfJourney;
using CalamityMod.Buffs.DamageOverTime;
using ThrowerUnification.Core.UnitedModdedThrowerClass;
using HomewardRagnarok.Config;
using System.Collections.Generic;

namespace HomewardRagnarok.Common.GlobalProjectiles
{
    public class CoJGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => false;
        private static HashSet<int> rogueProjectiles;

        private void SetupTypes()
        {
            if (rogueProjectiles != null) return;

            rogueProjectiles = new();
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                string[] rogueProjNames = {
                    "AdamantiteKnife","BrokenHammer","Backstabber","Bloodthirst","CobaltKnife",
                    "CopperKnife","GoldKnife","IronKnife","MythrilKnife","OrichalcumKnife",
                    "PalladiumKnife","PlatinumKnife","SilverKnife","TinKnife","HardKnuckle",
                    "Hellfire","Longinus","InkyToss","RollingCutter","ProjDarkKnife_1",
                    "TitaniumKnife","TungstenKnife","ChlorophyteKnife","LeadKnife",
                    "ToothOfCthulhu","ProjCactusBall_1","ProjSolidTornado_1","SpikyBomb","Fission_1",
                    "SamsaraOfDawnlight_Star", "RisingAction", "ProjCobaltThrowhammer_1", "ProjCopperThrowhammer_1",
                    "ProjPalladiumThrowhammer_1", "ProjTinThrowhammer_1", "ProjLeadBowlingBall_1", "ProjSilverTomahawk_1",
                    "ProjTungstenTomahawk_1", "ProjGoldenRang_1", "ProjPlatinumRang_1", "ProjBloodyShuriken_1",
                    "ProjEvilShuriken_1", "ProjMetalBlade_1", "ConniversKunai"
                };

                foreach (string name in rogueProjNames)
                    if (coj.TryFind(name, out ModProjectile modProj))
                        rogueProjectiles.Add(modProj.Type);
            }
        }

        public override void SetDefaults(Projectile projectile)
        {
            SetupTypes();

            var cfg = ServerConfig.Instance;
            if (cfg == null) return;

            if (rogueProjectiles.Contains(projectile.type) && cfg.EnableRogueClassChanges)
            {
                if (ModLoader.TryGetMod("ThrowerUnification", out Mod TU) &&
                    TU.TryFind("UnitedModdedThrower", out DamageClass throwerClass))
                {
                    projectile.DamageType = throwerClass;
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];
            TemplatePlayer cojPlayer = player.GetModPlayer<TemplatePlayer>();

            if (projectile.DamageType.CountsAsClass<UnitedModdedThrower>())
            {
                if (player.HasBuff(ModContent.BuffType<Flask_PlagueBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<PlagueBuff>(), 300);
                }
                if (player.HasBuff(ModContent.BuffType<Flask_DivineFireBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<PlagueBuff>(), 300);
                }
                if (player.HasBuff(ModContent.BuffType<Flask_SteelBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<VulnerableBuff>(), 300);
                }
                if (player.HasBuff(ModContent.BuffType<Flask_ForceBreakBuff>()))
                {
                    target.AddBuff(ModContent.BuffType<ForceBreakBuff>(), 600);
                }
            }

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers)
            {
                if (cojPlayer.StarQuiver)
                {
                    if (projectile.DamageType.CountsAsClass(DamageClass.Ranged))
                    {
                        target.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
                    }
                }
            }
        }
    }
}