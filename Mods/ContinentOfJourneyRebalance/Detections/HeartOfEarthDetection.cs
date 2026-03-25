using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Tiles.Cores;
//using ThoriumMod.Tiles;

namespace HomewardRagnarok
{
    public class HeartOfEarthDetection : GlobalTile
    {
        private readonly int[,] _structure = {
            {0,0,0,0,3,5,0,0,0 },
            {0,4,4,0,3,0,4,4,5 },
            {0,4,4,3,3,3,4,4,5 },
            {0,0,3,2,2,2,3,5,0 },
            {3,3,3,2,1,2,3,3,3 },
            {0,0,3,2,2,2,3,5,0 },
            {0,4,4,3,3,3,4,4,5 },
            {0,4,4,0,3,0,4,4,5 },
            {0,0,0,0,3,5,0,0,0 },
        };

        private readonly int[,] _structure2 = {
            {0,0,0,0,0,0,4,9,0,0,0,0,0 },
            {0,0,0,0,0,4,4,4,9,0,0,0,0 },
            {0,0,3,3,0,4,3,4,0,3,3,9,0 },
            {0,0,3,4,4,0,3,0,4,4,3,9,0 },
            {0,0,0,4,4,5,5,5,4,4,9,0,0 },
            {0,4,4,0,5,2,2,2,5,0,4,4,9 },
            {4,4,3,3,5,2,1,2,5,3,3,4,4 },
            {0,4,4,0,5,2,2,2,5,0,4,4,9 },
            {0,0,0,4,4,5,5,5,4,4,9,0,0 },
            {0,0,3,4,4,0,3,0,4,4,3,9,0 },
            {0,0,3,3,0,4,3,4,0,3,3,9,0 },
            {0,0,0,0,0,4,4,4,9,0,0,0,0 },
            {0,0,0,0,0,0,4,9,0,0,0,0,0 },
        };

        public override void NearbyEffects(int i, int j, int type, bool closer)
        {
            if (type == ModContent.TileType<Earth>())
            {
                int thoriumOre = TryGetThoriumTile("ThoriumOre");
                int lifeQuartz = TryGetThoriumTile("LifeQuartz");
                int aqualite = TryGetThoriumTile("AquaiteBare");
                int smoothCoal = TryGetThoriumTile("SmoothCoal");
                int lodestone = TryGetThoriumTile("LodeStone");
                int valadium = TryGetThoriumTile("ValadiumChunk");
                int illumite = TryGetThoriumTile("IllumiteChunk");

                int buffToApply = -1;

                if (CheckStructure(i, j, _structure2, 6, thoriumOre) || CheckStructure(i, j, _structure, 4, thoriumOre))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfThoriumBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, lifeQuartz) || CheckStructure(i, j, _structure, 4, lifeQuartz))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfLifeQuartzBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, aqualite) || CheckStructure(i, j, _structure, 4, aqualite))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfAqualiteBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, smoothCoal) || CheckStructure(i, j, _structure, 4, smoothCoal))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfSmoothCoalBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, lodestone) || CheckStructure(i, j, _structure, 4, lodestone))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfLodestoneBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, valadium) || CheckStructure(i, j, _structure, 4, valadium))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfValadiumBuff>();
                }
                else if (CheckStructure(i, j, _structure2, 6, illumite) || CheckStructure(i, j, _structure, 4, illumite))
                {
                    buffToApply = ModContent.BuffType<Buffs.HeartOfIllumiteBuff>();
                }

                if (buffToApply != -1)
                {
                    Player player = Main.LocalPlayer;
                    Tile thistile = Framing.GetTileSafely(i, j);

                    // Overwrite CoJ turning it off
                    thistile.IsActuated = true;

                    if (!player.dead)
                    {
                        player.AddBuff(buffToApply, 90);
                    }
                }
            }
        }

        private int TryGetThoriumTile(string tileName)
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) && thorium.TryFind<ModTile>(tileName, out ModTile tile))
                return tile.Type;
            return -2;
        }

        private bool CheckStructure(int i, int j, int[,] structure, int offset, int targetOreType)
        {
            for (int y = 0; y < structure.GetLength(0); y++)
            {
                for (int x = 0; x < structure.GetLength(1); x++)
                {
                    // Match CoJ optimization skips
                    if (x == 0)
                    {
                        if (offset == 6) // structure2
                        {
                            if (y == 0) x = 6; if (y == 1) x = 5; if (y == 2) x = 2; if (y == 3) x = 2;
                            if (y == 4) x = 3; if (y == 5) x = 1; if (y == 7) x = 1; if (y == 8) x = 3;
                            if (y == 9) x = 2; if (y == 10) x = 2; if (y == 11) x = 5; if (y == 12) x = 6;
                        }
                        else // structure1
                        {
                            if (y == 0) x = 4; if (y == 1) x = 1; if (y == 2) x = 1; if (y == 3) x = 2;
                            if (y == 5) x = 2; if (y == 6) x = 1; if (y == 7) x = 1; if (y == 8) x = 4;
                        }
                    }

                    int k = i - offset + x;
                    int l = j - offset + y;
                    Tile tile = Framing.GetTileSafely(k, l);
                    int req = structure[y, x];

                    if (req == 9 && offset == 6) break;
                    if (req == 5 && offset == 4) break;

                    if (req == 2 && tile.HasTile) return false;
                    else if (req == 3)
                    {
                        if (!(tile.HasTile && (tile.TileType == TileID.Stone || tile.TileType == TileID.StoneSlab || tile.TileType == TileID.GrayBrick)))
                            return false;
                    }
                    else if (req == 4)
                    {
                        if (!(tile.HasTile && tile.TileType == targetOreType))
                            return false;
                    }
                    else if (req == 5 && offset == 6)
                    {
                        if (!(tile.HasTile && tile.TileType == ModContent.TileType<DeathBlock>()))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}