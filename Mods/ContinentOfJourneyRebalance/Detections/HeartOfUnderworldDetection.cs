using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MonoMod.RuntimeDetour;
using ContinentOfJourney.Tiles.Cores;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Tiles.Abyss;
using ContinentOfJourney.Buffs;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Systems
{
    public class COJUnderworldTileFixSystem : ModSystem
    {
        private Hook nearbyEffectsHook;
        private static HashSet<int> ValidModdedBars;

        private readonly int[,] _structure = {
            {0,0,0,2,0,0,0 }, {0,0,0,2,0,0,0 }, {0,0,0,2,0,0,0 }, {0,0,0,2,0,0,0 },
            {0,0,0,2,0,0,0 }, {0,0,0,2,0,0,0 }, {0,0,0,5,0,0,0 }, {0,0,0,1,0,0,0 },
            {0,0,3,3,3,0,0 }, {0,3,3,3,3,3,0 }, {3,3,3,3,3,3,3 }
        };

        private readonly int[,] _structure2 = {
            {0,0,0,0,2,0,0,0,0 }, {0,0,0,0,2,0,0,0,0 }, {0,0,0,0,2,0,0,0,0 },
            {0,0,0,0,2,0,0,0,0 }, {0,0,0,0,2,0,0,0,0 }, {0,0,0,0,2,0,0,0,0 },
            {0,0,0,0,5,0,0,0,0 }, {0,0,0,0,1,0,0,0,0 }, {0,0,0,3,3,3,0,0,0 },
            {0,0,4,4,4,4,4,0,0 }, {0,3,3,3,3,3,3,3,0 }, {4,4,4,4,4,4,4,4,4 }
        };

        public override void Load()
        {
            MethodInfo nearbyEffectsMethod = typeof(Underworld).GetMethod("NearbyEffects", BindingFlags.Public | BindingFlags.Instance);

            if (nearbyEffectsMethod != null)
            {
                nearbyEffectsHook = new Hook(nearbyEffectsMethod, OnNearbyEffects);
                nearbyEffectsHook.Apply();
            }
        }

        public override void Unload()
        {
            nearbyEffectsHook?.Undo();
            nearbyEffectsHook = null;
            ValidModdedBars = null;
        }

        private delegate void orig_NearbyEffects(Underworld self, int i, int j, bool closer);

        private void OnNearbyEffects(orig_NearbyEffects orig, Underworld self, int i, int j, bool closer)
        {
            Tile thistile = Framing.GetTileSafely(i, j);
            thistile.IsActuated = false;
            thistile.IsHalfBlock = false;
            thistile.Slope = SlopeType.Solid;

            bool activate = true;
            bool secondstructure = true;

            for (int y = 0; y < _structure2.GetLength(0); y++)
            {
                for (int x = 0; x < _structure2.GetLength(1); x++)
                {
                    int k = i - 4 + x;
                    int l = j - 7 + y;
                    Tile tile = Framing.GetTileSafely(k, l);

                    if (_structure2[y, x] == 2 && tile.HasTile) { secondstructure = false; break; }
                    else if (_structure2[y, x] == 3)
                    {
                        if (!IsValidBar(tile)) { secondstructure = false; break; }
                    }
                    else if (_structure2[y, x] == 4)
                    {
                        if (!tile.HasTile || tile.TileType != ModContent.TileType<DeathBlock>()) { secondstructure = false; break; }
                    }
                    else if (_structure2[y, x] == 5)
                    {
                        if (tile.HasTile && tile.TileType == TileID.Glass) { }
                        else if (!tile.HasTile) { }
                        else { secondstructure = false; break; }
                    }
                }
                if (!secondstructure) break;
            }

            if (!secondstructure)
            {
                for (int y = 0; y < _structure.GetLength(0); y++)
                {
                    for (int x = 0; x < _structure.GetLength(1); x++)
                    {
                        int k = i - 3 + x;
                        int l = j - 7 + y;
                        Tile tile = Framing.GetTileSafely(k, l);

                        if (_structure[y, x] == 2 && tile.HasTile) { activate = false; break; }
                        else if (_structure[y, x] == 3)
                        {
                            if (!IsValidBar(tile)) { activate = false; break; }
                        }
                        else if (_structure[y, x] == 5)
                        {
                            if (tile.HasTile && tile.TileType == TileID.Glass) { }
                            else if (!tile.HasTile) { }
                            else { activate = false; break; }
                        }
                    }
                    if (!activate) break;
                }
            }

            if (activate) thistile.IsActuated = true;

            for (int n = 0; n < Main.maxNPCs; n++)
            {
                NPC npc = Main.npc[n];
                if (npc.boss && npc.active) { activate = false; break; }
            }

            if (Main.invasionType != InvasionID.None) activate = false;

            Player player = Main.LocalPlayer;
            if (!player.dead && activate)
            {
                if (secondstructure) player.AddBuff(ModContent.BuffType<HeartUnderworldBuff_2>(), 30);
                else player.AddBuff(ModContent.BuffType<HeartUnderworldBuff>(), 30);
            }
        }

        private bool IsValidBar(Tile tile)
        {
            if (!tile.HasTile) return false;

            if (tile.TileType == TileID.MetalBars ||
                tile.TileType == ModContent.TileType<PostMoonlordBars>() ||
                tile.TileType == ModContent.TileType<DeepBar>() ||
                tile.TileType == ModContent.TileType<OtherworldBar>())
            {
                return true;
            }

            if (ValidModdedBars == null)
            {
                ValidModdedBars = new HashSet<int>();
                // Calamity
                if (ModLoader.TryGetMod("CalamityMod", out Mod cal))
                {
                    string[] calBars = { "AerialiteBarTile", "AstralBarTile", "AuricBarTile", "CosmiliteBarTile", "CryonicBarTile", "PerennialBarTile", "ScoriaBarTile", "ShadowspecBarTile", "UelibloomBarTile" };
                    foreach (string bar in calBars) if (cal.TryFind<ModTile>(bar, out ModTile cT)) ValidModdedBars.Add(cT.Type);
                }

                // Thorium
                if (ModLoader.TryGetMod("ThoriumMod", out Mod thor))
                {
                    string[] thorBars = { "Bars" };
                    foreach (string bar in thorBars) if (thor.TryFind<ModTile>(bar, out ModTile tT)) ValidModdedBars.Add(tT.Type);
                }

                // SOTS
                if (ModLoader.TryGetMod("SOTS", out Mod sots))
                {
                    string[] sotsBars = { "TheBars" };
                    foreach (string bar in sotsBars) if (sots.TryFind<ModTile>(bar, out ModTile sT)) ValidModdedBars.Add(sT.Type);
                }
            }

            return ValidModdedBars.Contains(tile.TileType);
        }
    }
}