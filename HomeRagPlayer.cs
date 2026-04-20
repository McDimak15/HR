using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HomewardRagnarok.Items.Accessories;

namespace HomewardRagnarok
{
    public class HomeRagPlayer : ModPlayer
    {
        public bool hasRiftGenerator;
        public int devourDelay = 0;
        public bool herbalistAcidVenom;

        public override void ResetEffects()
        {
            hasRiftGenerator = false;
            herbalistAcidVenom = false;
        }

        public override void PostUpdateEquips()
        {
            if (devourDelay > 0) devourDelay--;

            if (hasRiftGenerator && Player.whoAmI == Main.myPlayer)
            {
                int projType = ModContent.ProjectileType<DevourScarf>();
                if (Player.ownedProjectileCounts[projType] <= 0)
                {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), Player.Center, Vector2.Zero, projType, 250, 2f, Player.whoAmI);
                }
            }
        }
    }
}