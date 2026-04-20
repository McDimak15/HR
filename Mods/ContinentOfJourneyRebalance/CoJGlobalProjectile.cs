using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney.Buffs;
using ThrowerUnification.Core.UnitedModdedThrowerClass;

namespace HomewardRagnarok.Common.GlobalProjectiles
{
    public class ThrowerFlaskApplication : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[projectile.owner];

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
        }
    }
}