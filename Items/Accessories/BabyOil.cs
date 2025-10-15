using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.GameContent;

namespace HomewardRagnarok.Items.Accessories
{
    public class BabyOil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "BabyOil1", "When damaged cause 3-5 droplets of oil to drop out from you"));
            tooltips.Add(new TooltipLine(Mod, "BabyOil2", "Droplets do damage to enemies"));

            tooltips.Add(new TooltipLine(Mod, "BabyOilFancy", "- Contributor Item -"));
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Name == "BabyOilFancy" && line.Mod == Mod.Name)
            {
                float timer = (float)(Main.GlobalTimeWrappedHourly * 0.5);
                float wave = (float)Math.Sin(timer * MathHelper.TwoPi) * 0.5f + 0.5f;

                Color left = new Color(186, 85, 211);  
                Color right = new Color(138, 43, 226);
                Color gradientColor = Color.Lerp(left, right, wave);

                for (int i = 0; i < 4; i++)
                {
                    Vector2 offset = new Vector2(
                        (float)Math.Cos(i * MathHelper.PiOver2) * 1.5f,
                        (float)Math.Sin(i * MathHelper.PiOver2) * 1.5f
                    );

                    ChatManager.DrawColorCodedStringWithShadow(
                        Main.spriteBatch,
                        FontAssets.MouseText.Value,
                        line.Text,
                        new Vector2(line.X, line.Y) + offset,
                        Color.Multiply(gradientColor, 0.5f),
                        0f,
                        Vector2.Zero,
                        Vector2.One
                    );
                }

                ChatManager.DrawColorCodedStringWithShadow(
                    Main.spriteBatch,
                    FontAssets.MouseText.Value,
                    line.Text,
                    new Vector2(line.X, line.Y),
                    gradientColor,
                    0f,
                    Vector2.Zero,
                    Vector2.One
                );

                return false; 
            }

            return true;
        }



        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BabyOilPlayer>().babyOil = true;
        }
    }

    public class BabyOilPlayer : ModPlayer
    {
        public bool babyOil;

        public override void ResetEffects()
        {
            babyOil = false;
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            if (babyOil)
            {
                int dropletCount = Main.rand.Next(3, 6); 
                for (int i = 0; i < dropletCount; i++)
                {
                    Vector2 velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-5f, -2f));
                    Projectile.NewProjectile(
                        Player.GetSource_Accessory(Player.HeldItem),
                        Player.Center,
                        velocity,
                        ModContent.ProjectileType<BabyOilDroplet>(),
                        20, // damage
                        2f, // knockback
                        Player.whoAmI
                    );
                }
            }
        }
    }

    public class BabyOilDroplet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;  
            Projectile.height = 64; 
            Projectile.aiStyle = 1; 
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6) 
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Oiled, 300); 
        }
    }

    public class BabyOilGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Nurse && Main.rand.NextFloat() < 0.10f) 
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BabyOil>());
            }
        }
    }
}
 