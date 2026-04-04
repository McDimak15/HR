using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System;
using System.Runtime.CompilerServices;
using HomewardRagnarok.Config;
using ThoriumMod.Items.Placeable;
using ThoriumMod.Tiles;
using ContinentOfJourney.Buffs;
using ContinentOfJourney.Items.Placables;
using ContinentOfJourney.Tiles;

namespace HomewardRagnarok.Items.Tiles
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class CampfireOfTheWildheart : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<CampfireOfTheWildheartTile>());
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ThoriumMod.Items.Placeable.TrueArenaMastersBrazier>())
                .AddIngredient(ModContent.ItemType<LifeLantern>())
                .AddIngredient(ModContent.ItemType<BushOfLife>())
                .AddTile(ModContent.TileType<ContinentOfJourney.Tiles.FinalAnvil>())
                .Register();
        }
    }

    [JITWhenModsEnabled("ThoriumMod")]
    public class CampfireOfTheWildheartTile : ModTile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 0;
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 32;

            AddMapEntry(new Color(80, 180, 120));
            DustType = DustID.CursedTorch;
        }

        public override void DrawEffects(int i, int j, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            if (drawData.tileFrameX == 16 && drawData.tileFrameY == 0)
            {
                if (Main.rand.NextBool(4))
                {
                    int dustIndex = Dust.NewDust(new Vector2(i * 16, j * 16 - 4), 16, 8, DustID.Smoke, 0f, -0.5f, 100, default, 1.5f);
                    Main.dust[dustIndex].velocity *= 0.3f;
                    Main.dust[dustIndex].velocity.Y -= 1f;
                }
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 3)
            {
                frameCounter = 0;
                frame++;
                if (frame >= 8)
                {
                    frame = 0;
                }
            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            if (!closer) return;

            Main.SceneMetrics.HasSunflower = true;
            Main.SceneMetrics.HasCampfire = true;
            Main.SceneMetrics.HasStarInBottle = true;
            Main.SceneMetrics.HasHeartLantern = true;
            Main.SceneMetrics.HasCatBast = true;
            if (ModLoader.HasMod("ThoriumMod"))
            {
                ApplyThoriumEffects(player);
            }
            player.AddBuff(ModContent.BuffType<LifeLanternBuff>(), 10);
            player.AddBuff(ModContent.BuffType<BushOfLifeBuff>(), 10);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ApplyThoriumEffects(Player player)
        {
            player.GetModPlayer<ArenaMastersBrazierPlayer>().honey = true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {

            r = 0.4f;
            g = 1.0f;
            b = 0.5f;
        }
    }
}