using System;
using ContinentOfJourney;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Tiles.Abyss;
using HomewardRagnarok.Config;
using InfernalEclipseAPI.Core.Systems;
using SOTS.Items.Furniture.Functional;
using static SOTS.Items.Furniture.Functional.MineralariumTE;
using Terraria;
using Terraria.ModLoader;

namespace HomewardRagnarok.Mods.SOTS
{
    [JITWhenModsEnabled("SOTS")]
    [ExtendsFromMod("SOTS")]
    public class MineralariumOres : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<DeepOre>(), 4540, 0.9, (OreType.SpawnCondition)(() => NPC.downedGolemBoss));
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<EternalOre>(), 11160, 1.2, (OreType.SpawnCondition)(() => NPC.downedMoonlord));
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<LivingOre>(), 11160, 1.1, (OreType.SpawnCondition)(() => NPC.downedMoonlord));
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<CubistOre>(), 11160, 1d, (OreType.SpawnCondition)(() => NPC.downedMoonlord));
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<TruePearlstone>(), 11220, 1d, (OreType.SpawnCondition)(() => DownedBossSystem.downedLifeGod));
        }
    }
}