using Terraria;
using Terraria.ModLoader;
using ContinentOfJourney.Tiles;
using ContinentOfJourney.Tiles.Abyss;
using ContinentOfJourney;
using InfernalEclipseAPI.Core.Systems.Hooks.ILTileChanges;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Mods.SOTS
{
    [JITWhenModsEnabled("SOTS")]
    [ExtendsFromMod("SOTS")]
    public class MyMineralariumIntegration : ModSystem
    {
        public override void PostSetupContent()
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<DeepOre>(), 4540, 0.9, () => NPC.downedGolemBoss);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<EternalOre>(), 11160, 1.2, () => NPC.downedMoonlord);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<LivingOre>(), 11160, 1.1, () => NPC.downedMoonlord);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<CubistOre>(), 11160, 1, () => NPC.downedMoonlord);
            SOTSMineralariumHooks.ParseNewOre(ModContent.TileType<TruePearlstone>(), 11220, 1, () => DownedBossSystem.downedLifeGod);
        }
    }
}