using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace HomewardRagnarok.Items.Accessories
{
    public class HerbalistPouchPlayer : ModPlayer
    {
        public bool herbalistAcidVenom;

        public override void ResetEffects()
        {
            herbalistAcidVenom = false;
        }
    }

    public class HerbalistPouchGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
           
            Player player = Main.player[projectile.owner];

            
            if (player.active && player.GetModPlayer<HerbalistPouchPlayer>().herbalistAcidVenom)
            {
                
                if (projectile.aiStyle == 99)
                {
                    target.AddBuff(BuffID.Venom, 300);
                }
            }
        }
    }
}
