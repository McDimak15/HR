using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;

namespace HomewardRagnarok.Items.Tiles
{
    public class TimelessFountain: ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 80;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Yellow;
            Item.createTile = ModContent.TileType<TimelessFountainTile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "TimelessFountainInfo",
                "Counts as a Fountain of Life, Fountain of Matter, Fountain of Time"));
        }

        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;

            int fountainLife = coj.Find<ModItem>("FountainofLife")?.Type ?? 0;
            int fountainMatter = coj.Find<ModItem>("FountainofMatter")?.Type ?? 0;
            int fountainTime = coj.Find<ModItem>("FountainofTime")?.Type ?? 0;
            int cosmiliteBar = calamity.Find<ModItem>("CosmiliteBar")?.Type ?? 0;

            if (fountainLife == 0 || fountainMatter == 0 || fountainTime == 0 || cosmiliteBar == 0)
                return; 

            int profanedCrucibleTile = calamity.Find<ModTile>("ProfanedCrucible")?.Type
                                       ?? calamity.Find<ModTile>("Profaned_Crucible")?.Type ?? 0;

            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(fountainLife, 1);
            recipe.AddIngredient(fountainMatter, 1);
            recipe.AddIngredient(fountainTime, 1);
            recipe.AddIngredient(cosmiliteBar, 5);

            if (profanedCrucibleTile != 0)
                recipe.AddTile(profanedCrucibleTile);
            else
                recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }

    public class TimelessFountainTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileLavaDeath[Type] = false;

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                var adj = new System.Collections.Generic.List<int>();
                int fLife = coj.Find<ModTile>("FountainofLife")?.Type ?? 0;
                int fMatter = coj.Find<ModTile>("FountainofMatter")?.Type ?? 0;
                int fTime = coj.Find<ModTile>("FountainofTime")?.Type ?? 0;
                if (fLife > 0) adj.Add(fLife);
                if (fMatter > 0) adj.Add(fMatter);
                if (fTime > 0) adj.Add(fTime);
                if (adj.Count > 0)
                    AdjTiles = adj.ToArray();
            }
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 6; 
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 0;
            TileObjectData.newTile.Origin = new Point16(2, 5);
            TileObjectData.addTile(Type);


            AddMapEntry(new Color(150, 150, 250), CreateMapEntryName());

            AnimationFrameHeight = 96; 
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 8)
            {
                frameCounter = 0;
                frame++;
                if (frame >= 18) frame = 0;
            }
        }
    }
}
