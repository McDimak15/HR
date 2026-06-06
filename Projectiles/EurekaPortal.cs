using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ContinentOfJourney.Buffs;
using ContinentOfJourney.Items;

namespace HomewardRagnarok.Projectiles
{
    public class EurekaPortal : ModProjectile
    {
        public override string Texture => "ContinentOfJourney/Projectiles/MiniLinka";

        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 7200;
            Projectile.netImportant = true;
        }

        public override void AI()
        {
            Projectile.localAI[0]++;

            Projectile.rotation += 0.05f;

            if (Main.rand.NextBool(4))
            {
                int dustType = Projectile.ai[0] == 0 ? DustID.GemRuby : DustID.GemDiamond;
                Dust.NewDust(Projectile.Center - new Vector2(10, 10), 20, 20, dustType, 0f, 0f, 0, default, 1f);
            }

            Player player = Main.player[Projectile.owner];

            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.MouseWorld.DistanceSQ(Projectile.Center) < 4000f && player.DistanceSQ(Projectile.Center) < 22500f)
                {
                    player.cursorItemIconEnabled = true;
                    player.cursorItemIconID = ModContent.ItemType<EurekaEffect>();

                    if (Projectile.localAI[0] > 30 && Main.mouseRight && Main.mouseRightRelease && !player.HasBuff(ModContent.BuffType<EurekaEffectBuff>()))
                    {
                        TeleportToOtherPortal(player);
                    }
                }
            }
        }

        private void TeleportToOtherPortal(Player player)
        {
            float targetAiType = Projectile.ai[0] == 0 ? 1 : 0;
            Projectile destinationPortal = null;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == Projectile.type && p.ai[0] == targetAiType)
                {
                    destinationPortal = p;
                    break;
                }
            }

            if (destinationPortal != null)
            {
                player.Teleport(destinationPortal.Center, 1);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, destinationPortal.Center.X, destinationPortal.Center.Y, 1);

                player.AddBuff(ModContent.BuffType<EurekaEffectBuff>(), 600, true, false);

                if (ModLoader.TryGetMod("ThoriumMod", out Mod thoriumMod))
                {
                    int healAmount = 40;
                    player.statLife += healAmount;
                    if (player.statLife > player.statLifeMax2)
                    {
                        player.statLife = player.statLifeMax2;
                    }
                    player.HealEffect(healAmount);
                    NetMessage.SendData(MessageID.PlayerHeal, -1, -1, null, player.whoAmI, healAmount);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Color drawColor = Projectile.ai[0] == 0 ? Color.Red : Color.Gray;

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, drawColor * 0.5f, -Projectile.rotation, texture.Size() / 2, Projectile.scale * 1.25f, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, drawColor, Projectile.rotation, texture.Size() / 2, Projectile.scale * 1.25f, SpriteEffects.None, 0);

            Player player = Main.player[Projectile.owner];
            if (!player.HasBuff(ModContent.BuffType<EurekaEffectBuff>()))
            {
                Texture2D readyTexture = ModContent.Request<Texture2D>("ContinentOfJourney/Projectiles/Materealizer_Master_14").Value;
                float pulseScale = Projectile.scale * 1.25f * (1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 4f) * 0.1f);
                Main.EntitySpriteDraw(readyTexture, Projectile.Center - Main.screenPosition, null, drawColor, 0f, readyTexture.Size() / 2, pulseScale, SpriteEffects.None, 0);
            }

            return false;
        }
    }
}