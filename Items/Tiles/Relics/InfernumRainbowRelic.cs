using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using InfernumMode.Content.Tiles.Relics;

namespace HomewardRagnarok.Items.Tiles.Relics
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]
    public abstract class InfernumRainbowRelic : BaseInfernumBossRelic
    {
        public abstract string CoJRelicName { get; }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Vector2 offScreen = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Point p = new Point(i, j);
            Tile tile = Main.tile[p.X, p.Y];
            if (tile == null || !tile.HasTile) return;

            Texture2D texture = RelicTexture.Value;
            Texture2D glowTexture = RelicGlowTexture.Value;

            int frameY = tile.TileFrameX / FrameWidth;
            Rectangle frame = texture.Frame(HorizontalFrames, VerticalFrames, 0, frameY);
            Vector2 origin = frame.Size() / 2f;
            Vector2 worldPos = p.ToWorldCoordinates(24f, 64f);
            Color color = Lighting.GetColor(p.X, p.Y);
            bool direction = tile.TileFrameY / FrameHeight != 0;
            SpriteEffects effects = direction ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            const float TwoPi = (float)Math.PI * 2f;
            float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);
            Vector2 drawPos = worldPos + offScreen - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offset * 4f);

            spriteBatch.Draw(texture, drawPos, frame, color, 0f, origin, 1f, effects, 0f);
            spriteBatch.Draw(glowTexture, drawPos, frame, Color.White, 0f, origin, 1f, effects, 0f);

            float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
            float sheenTime = (float)Main.timeForVisualEffects;
            sheenTime %= 360;
            sheenTime = Utils.Clamp(sheenTime, 0, 180);
            sheenTime = (float)Math.Sin(sheenTime * Math.PI / 180f) * 0.3f;
            float sheenTimeSeat = sheenTime * 0.25f;

            Color effectColor = Color.Lerp(color, Main.hslToRgb((float)(Main.timeForVisualEffects + 20) / 60 % 1, 0.9f, 0.5f) with { A = 0 }, sheenTime);
            effectColor.A = 0;
            effectColor *= 0.1f * scale;

            string cojPathBase = "ContinentOfJourney/Tiles/" + CoJRelicName;

            if (ModContent.RequestIfExists<Texture2D>(cojPathBase + "_Outline", out var outlineAsset) &&
                ModContent.RequestIfExists<Texture2D>(cojPathBase + "_White", out var whiteAsset))
            {
                Texture2D sheen = outlineAsset.Value;
                Texture2D sheenInner = whiteAsset.Value;
                int step = 2;
                for (int k = 0; k < texture.Height; k += step)
                {
                    Color dColor = Main.hslToRgb((float)(Main.timeForVisualEffects + k * 0.8f) / 60 % 1, 0.9f, 0.5f);
                    spriteBatch.Draw(sheenInner, new Vector2(drawPos.X, drawPos.Y + k), new Rectangle(0, k, texture.Width, 2), dColor with { A = 0 } * sheenTime, 0f, origin, 1f, effects, 0f);
                    spriteBatch.Draw(sheen, new Vector2(drawPos.X, drawPos.Y + k), new Rectangle(0, k, texture.Width, 2), Color.Lerp(dColor, Color.Black, 0.5f) * sheenTime, 0f, origin, 1f, effects, 0f);
                }
            }
            for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
            {
                spriteBatch.Draw(texture, drawPos + (TwoPi * num5).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, 0f, origin, 1f, effects, 0f);
            }
            if (ModContent.RequestIfExists<Texture2D>("ContinentOfJourney/Tiles/RelicPedestal_White", out var seatAsset))
            {
                Texture2D sheenSeat = seatAsset.Value;
                Vector2 dPos = new Vector2(i * 16 + 24, j * 16 + 50 + 12) + offScreen - Main.screenPosition;
                Color dColorSeat = Main.hslToRgb((float)(Main.timeForVisualEffects + 40) / 60 % 1, 0.9f, 0.5f);
                for (int k = 0; k < 4; k++)
                {
                    spriteBatch.Draw(sheenSeat, dPos + new Vector2(4, 0).RotatedBy(MathHelper.TwoPi / 4 * k), new Rectangle(0, 0, 48, 16), dColorSeat with { A = 0 } * sheenTimeSeat, 0f, new Vector2(24, 12), 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}