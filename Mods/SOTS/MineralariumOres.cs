using ContinentOfJourney;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Tiles.Abyss;
using HomewardRagnarok.Config;
using InfernalEclipseAPI.Core.Systems;
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

            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<DeepOre>(), 4540, 0.9, () => NPC.downedGolemBoss);
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<EternalOre>(), 11160, 1.2, () => NPC.downedMoonlord);
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<LivingOre>(), 11160, 1.1, () => NPC.downedMoonlord);
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<CubistOre>(), 11160, 1, () => NPC.downedMoonlord);
            InfernalCrossmod.SOTS.Mod.Call("AddMineralariumOre", ModContent.TileType<TruePearlstone>(), 11220, 1, () => DownedBossSystem.downedLifeGod);
        }
    }
}