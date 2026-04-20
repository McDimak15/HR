using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using MonoMod.RuntimeDetour;
using SOTS;
using SOTS.Items.Planetarium.FromChests;
using SOTS.Projectiles.Minions;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using ContinentOfJourney.Buffs;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Rarities;
using CalamityMod.Items.Materials;
using HomewardRagnarok.Buffs;

namespace HomewardRagnarok.Items.Accessories
{
    [ExtendsFromMod("SOTS")]
    [JITWhenModsEnabled("SOTS")]
    public class RiftGenerator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.width = 32;
            Item.height = 58;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ModContent.RarityType<CosmicPurple>();
            Item.accessory = true;
            Item.defense = 8;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 1;
            player.maxTurrets += 1;
            player.dd2Accessory = true;
            PlatformPlayer modPlayer = player.GetModPlayer<PlatformPlayer>();
            modPlayer.platformPairs += 2;
            modPlayer.fortress = true;
            if (hideVisual) modPlayer.hideChains = true;

            player.GetModPlayer<HomeRagPlayer>().hasRiftGenerator = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1)
                .AddIngredient(ModContent.ItemType<SOTS.Items.FortressGenerator>())
                .AddIngredient(ModContent.ItemType<LampreyScarf>())
                .AddIngredient(ModContent.ItemType<AscendantSpiritEssence>(), 4)
                .AddTile(ModContent.TileType<CosmicAnvil>())
                .Register();
        }
    }

    [ExtendsFromMod("SOTS")]
    [JITWhenModsEnabled("SOTS")]
    public class RiftGeneratorSystem : ModSystem
    {
        private Hook _holoPlatformDrawHook;

        public override void Load()
        {
            if (ModLoader.HasMod("SOTS"))
                ApplyHook();
        }

        [JITWhenModsEnabled("SOTS")]
        private void ApplyHook()
        {
            MethodInfo originalDraw = typeof(HoloPlatform).GetMethod("Draw", BindingFlags.Public | BindingFlags.Instance);
            if (originalDraw != null)
            {
                _holoPlatformDrawHook = new Hook(originalDraw, new hook_Draw(CustomHoloPlatformDraw));
                _holoPlatformDrawHook.Apply();
            }
        }

        public override void Unload()
        {
            _holoPlatformDrawHook?.Undo();
            _holoPlatformDrawHook = null;
        }

        private delegate void orig_Draw(HoloPlatform self, SpriteBatch spriteBatch);
        private delegate void hook_Draw(orig_Draw orig, HoloPlatform self, SpriteBatch spriteBatch);

        [JITWhenModsEnabled("SOTS")]
        private void CustomHoloPlatformDraw(orig_Draw orig, HoloPlatform self, SpriteBatch spriteBatch)
        {
            Player player = Main.player[self.Projectile.owner];

            if (!player.GetModPlayer<HomeRagPlayer>().hasRiftGenerator)
            {
                orig(self, spriteBatch);
                return;
            }

            bool isPink = (self.Projectile.ai[0] % 2 == 0 && self.Projectile.ai[1] == 1) || (self.Projectile.ai[0] % 2 == 1 && self.Projectile.ai[1] == -1);
            string color = isPink ? "Pink" : "Cyan";
            string path = "HomewardRagnarok/Assets/Images/";

            Texture2D textureChainOutline = ModContent.Request<Texture2D>(path + color + "HoloPlatformChainOutline").Value;
            Texture2D textureChainFill = ModContent.Request<Texture2D>(path + color + "HoloPlatformChainFill").Value;
            Texture2D texturePlatformFill = ModContent.Request<Texture2D>(path + color + "HoloPlatformRookFill").Value;
            Texture2D texturePlatformOutline = ModContent.Request<Texture2D>(path + color + "HoloPlatformRookOutline").Value;

            int shader = SOTSPlayer.ModPlayer(player).platformShader;
            if (shader != 0)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetSecondaryShader(shader, player).Apply(null);
            }

            PlatformPlayer modPlayer = player.GetModPlayer<PlatformPlayer>();
            float verticalOffset = -2;
            Rectangle frameLeft = new Rectangle(0, 0, 10, 16);
            Rectangle frameMiddle = new Rectangle(13, 0, 1, 16);
            Rectangle frameRight = new Rectangle(34, 0, 10, 16);
            Vector2 chainOrigin = new Vector2(textureChainOutline.Width * 0.5f, textureChainOutline.Height * 0.5f);
            Color drawColor = new Color(100, 100, 100, 0);
            int counter = (int)(Main.GameUpdateCount % 3600);

            if (player.active && !player.dead && !modPlayer.hideChains)
            {
                Vector2 distanceToOwner = new Vector2(self.Projectile.Center.X, self.Projectile.position.Y + self.Projectile.height + 6) - player.Center;
                float radius = distanceToOwner.Length() / 2;
                if (distanceToOwner.X < 0) radius = -radius;

                Vector2 centerOfCircle = player.Center + distanceToOwner / 2;
                float startingRadians = distanceToOwner.ToRotation();
                for (int i = 19; i > 0; i--)
                {
                    Vector2 rotationPos = new Vector2(radius, 0).RotatedBy(MathHelper.ToRadians(9 * i));
                    rotationPos.Y /= 3.5f;
                    rotationPos = rotationPos.RotatedBy(startingRadians);
                    Vector2 pos = rotationPos += centerOfCircle;
                    Vector2 dynamicAddition = new Vector2(2.5f, 0).RotatedBy(MathHelper.ToRadians(i * 36 + counter * 2));
                    Vector2 drawPos = pos - Main.screenPosition;
                    for (int k = 0; k < 4; k++)
                    {
                        if (k == 0)
                            spriteBatch.Draw(textureChainFill, drawPos + dynamicAddition, null, drawColor * 0.5f, MathHelper.ToRadians(18 * i - 45) + startingRadians, chainOrigin, self.Projectile.scale * 0.75f, SpriteEffects.None, 0f);
                        spriteBatch.Draw(textureChainOutline, drawPos + dynamicAddition, null, drawColor * (1f - (self.Projectile.alpha / 255f)), MathHelper.ToRadians(18 * i - 45) + startingRadians, chainOrigin, self.Projectile.scale * 0.75f, SpriteEffects.None, 0f);
                    }
                }
            }

            Vector2 drawPos2 = self.Projectile.position - Main.screenPosition;

            for (int k = 0; k < 4; k++)
            {
                if (k == 0) spriteBatch.Draw(texturePlatformFill, drawPos2 + new Vector2(0, verticalOffset), frameLeft, drawColor * 0.5f, 0, Vector2.Zero, self.Projectile.scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texturePlatformOutline, drawPos2 + new Vector2(0, verticalOffset), frameLeft, drawColor * (1f - (self.Projectile.alpha / 255f)), 0f, Vector2.Zero, self.Projectile.scale, SpriteEffects.None, 0f);
            }

            float middleWidth = self.Projectile.width - frameLeft.Width - frameRight.Width;
            middleWidth = (middleWidth - 10) / 2;
            Vector2 horiScale = new Vector2(middleWidth, 1f);
            if (middleWidth > 0)
            {
                for (int k = 0; k < 4; k++)
                {
                    if (k == 0) spriteBatch.Draw(texturePlatformFill, drawPos2 + new Vector2(frameLeft.Width, verticalOffset), frameMiddle, drawColor * 0.5f, 0, Vector2.Zero, horiScale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(texturePlatformOutline, drawPos2 + new Vector2(frameLeft.Width, verticalOffset), frameMiddle, drawColor * (1f - (self.Projectile.alpha / 255f)), 0f, Vector2.Zero, horiScale, SpriteEffects.None, 0f);
                }
                for (int k = 0; k < 4; k++)
                {
                    if (k == 0) spriteBatch.Draw(texturePlatformFill, drawPos2 + new Vector2(frameLeft.Width + middleWidth, verticalOffset), new Rectangle(22, 0, 10, 16), drawColor * 0.5f, 0, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    spriteBatch.Draw(texturePlatformOutline, drawPos2 + new Vector2(frameLeft.Width + middleWidth, verticalOffset), new Rectangle(22, 0, 10, 16), drawColor * (1f - (self.Projectile.alpha / 255f)), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
                for (int k = 0; k < 4; k++)
                {
                    if (k == 0) spriteBatch.Draw(texturePlatformFill, drawPos2 + new Vector2(frameLeft.Width + middleWidth + 10, verticalOffset), frameMiddle, drawColor * 0.5f, 0, Vector2.Zero, horiScale, SpriteEffects.None, 0f);
                    spriteBatch.Draw(texturePlatformOutline, drawPos2 + new Vector2(frameLeft.Width + middleWidth + 10, verticalOffset), frameMiddle, drawColor * (1f - (self.Projectile.alpha / 255f)), 0f, Vector2.Zero, horiScale, SpriteEffects.None, 0f);
                }
            }

            for (int k = 0; k < 4; k++)
            {
                if (k == 0) spriteBatch.Draw(texturePlatformFill, drawPos2 + new Vector2(self.Projectile.width - frameRight.Width, verticalOffset), frameRight, drawColor * 0.5f, 0, Vector2.Zero, self.Projectile.scale, SpriteEffects.None, 0f);
                spriteBatch.Draw(texturePlatformOutline, drawPos2 + new Vector2(self.Projectile.width - frameRight.Width, verticalOffset), frameRight, drawColor * (1f - (self.Projectile.alpha / 255f)), 0f, Vector2.Zero, self.Projectile.scale, SpriteEffects.None, 0f);
            }

            if (shader != 0)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            }
        }
    }

    public class DevourScarf : ModProjectile
    {
        public override string Texture => "HomewardRagnarok/Assets/Images/DevourScarf";

        public override void SetStaticDefaults() => Main.projFrames[Projectile.type] = 3;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;

            Rectangle f1 = new Rectangle(0, 0, 32, 28); 
            Rectangle f2 = new Rectangle(0, 28, 32, 28);
            Rectangle f3 = new Rectangle(0, 56, 32, 30);

            Player player = Main.player[Projectile.owner];
            Vector2 start = player.Center;
            Vector2 end = Projectile.Center;

            int seg = getSeg(start, end);
            float segmentLength = 24f;

            float dF = getSeg(start, end, true) / segmentLength;
            dF = (float)Math.PI / seg * dF;

            float factor1 = MathHelper.Clamp(Projectile.localAI[1] / 30f, 0f, 1f);
            float startAngleDrift = player.direction * ((float)Math.PI / 2f * (1f - factor1));
            float segDrift = -player.direction * (float)(Math.PI - dF) * (1f - factor1) / seg;

            float startAngle = (start - end).ToRotation();
            float rot = startAngle + startAngleDrift + (float)Math.PI;

            SpriteEffects sprEfx = player.direction > 0 ? SpriteEffects.FlipVertically : SpriteEffects.None;

            Main.EntitySpriteDraw(texture, end - Main.screenPosition, f1, lightColor, rot, new Vector2(16, 14), 1f, sprEfx, 0);

            Vector2 startVec = new Vector2(segmentLength, 0).RotatedBy(startAngle).RotatedBy(startAngleDrift);

            for (int k = 1; k < seg; k++)
            {
                startVec = startVec.RotatedBy(segDrift);
                end += startVec;

                Vector2 currentOrigin = (k == seg - 1) ? new Vector2(16, 15) : new Vector2(16, 14);
                Rectangle currentFrame = (k == seg - 1) ? f3 : f2;

                Main.EntitySpriteDraw(texture, end - Main.screenPosition, currentFrame, lightColor, rot + k * segDrift, currentOrigin, 1f, sprEfx, 0);
            }

            return false;
        }

        public int getSeg(Vector2 start, Vector2 end, bool getDivideFactor = false)
        {
            float dist = (start - end).Length();
            float factor1 = MathHelper.Clamp(Projectile.localAI[1] / 30f, 0f, 1f);
            float factor2 = MathHelper.Lerp(1.5707963f, 1f, factor1);

            float segmentLength = 24f;

            if (getDivideFactor) return (int)((dist * factor2) % segmentLength);

            int seg = (int)((dist * factor2) / segmentLength);
            return seg + 1;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<LampreySummonTag>(), 120);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead || !player.GetModPlayer<HomeRagPlayer>().hasRiftGenerator) { Projectile.Kill(); return; }
            Projectile.timeLeft = 2;

            if (Projectile.ai[2] < 1)
            {
                Projectile.velocity = (player.MountedCenter + new Vector2(0, -96) - Projectile.Center) / 4f;
                if (Projectile.localAI[1] > 0) Projectile.localAI[1]--;

                int target = -1;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy() && Vector2.Distance(npc.Center, Projectile.Center) < 700)
                    {
                        if (Collision.CanHitLine(Projectile.Center, 8, 8, npc.position, npc.width, npc.height))
                        {
                            target = i;
                            break;
                        }
                    }
                }
                if (target >= 0) Projectile.ai[2] = target + 1;
            }
            else
            {
                if (Projectile.localAI[1] < 30) Projectile.localAI[1]++;
                NPC npc = Main.npc[(int)Projectile.ai[2] - 1];
                if (Projectile.ai[1] < 1)
                {
                    float speed = Math.Min(20f, Projectile.Distance(npc.Center));
                    Vector2 move = Projectile.DirectionTo(npc.Center);
                    if (!move.HasNaNs()) Projectile.velocity = Vector2.Lerp(Projectile.velocity, move * speed, 0.125f);
                    if (Projectile.Distance(npc.Center) <= 30f) Projectile.ai[1] = 1;
                }
                else { Projectile.Center = npc.Center; }

                if (Vector2.Distance(npc.Center, player.Center) > 800 || !npc.active || npc.dontTakeDamage)
                {
                    Projectile.ai[1] = 0;
                    Projectile.ai[2] = 0;
                }
            }
        }
    }
}