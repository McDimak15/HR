using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace HWJBardHealer.Content
{
    public class TicketBarHook : ModSystem
    {
        private static Texture2D ticketBarTex;
        private static Texture2D symbolBarTex;
        private static bool interceptingTicket;
        private Hook detour;

        private delegate void OrigEntitySpriteDraw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle,
            Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float worthless);

        public override void Load()
        {
            ticketBarTex = ModContent.Request<Texture2D>("ContinentOfJourney/Images/TicketBar", AssetRequestMode.ImmediateLoad).Value;
            symbolBarTex = ModContent.Request<Texture2D>("ContinentOfJourney/Images/SymbolBar", AssetRequestMode.ImmediateLoad).Value;

            MethodInfo target = typeof(Main).GetMethod(nameof(Main.EntitySpriteDraw), new[] {
                typeof(Texture2D), typeof(Vector2), typeof(Rectangle?), typeof(Color),
                typeof(float), typeof(Vector2), typeof(Vector2), typeof(SpriteEffects), typeof(float)
            });

            detour = new Hook(target, DetourEntitySpriteDraw);
        }

        public override void Unload()
        {
            detour?.Dispose();
            detour = null;
            ticketBarTex = null;
            symbolBarTex = null;
        }

        private static void DetourEntitySpriteDraw(OrigEntitySpriteDraw orig, Texture2D texture, Vector2 position,
            Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float worthless)
        {
            if (texture == ticketBarTex)
                interceptingTicket = true;
            else if (texture == symbolBarTex)
                interceptingTicket = false;

            if (interceptingTicket && (texture == ticketBarTex || sourceRectangle == new Rectangle(0, 0, 1, 6)))
                position.Y += -30f;

            orig(texture, position, sourceRectangle, color, rotation, origin, scale, effects, worthless);
        }
    }
}