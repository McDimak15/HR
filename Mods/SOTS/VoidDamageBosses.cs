using Terraria;
using Terraria.ModLoader;
using SOTS.Void;
using System.Collections.Generic;
using ContinentOfJourney.NPCs.Boss_PuppetOpera;
using ContinentOfJourney.NPCs.Boss_BigDipper;
using ContinentOfJourney.NPCs.Boss_PriestessRod;
using ContinentOfJourney.NPCs.Boss_MarquisMoonsquid;
using ContinentOfJourney.NPCs.Boss_Diver;
using ContinentOfJourney.NPCs.Boss_TheMotherbrain;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.NPCs.Boss_WallofShadow;
using ContinentOfJourney.NPCs.Boss_TheOverwatcher;
using ContinentOfJourney.NPCs.Boss_TheMaterealizer;
using ContinentOfJourney.NPCs.Boss_TheLifebringer;
using ContinentOfJourney.NPCs.Boss_ScarabBelief;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;
using ContinentOfJourney.NPCs.Boss_TheSon;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Common.Globals.GlobalNPCs
{
    [JITWhenModsEnabled("SOTS")]
    [ExtendsFromMod("SOTS")]
    public class HomewardVoidTouchNPC : GlobalNPC
    {
        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo info)
        {
            if (!ServerConfig.Instance.SOTSBalance) return;

            if (IsHomewardVoidBoss(npc.type))
            {
                int voidDrain = 1 + (npc.damage / 6);
                VoidPlayer.VoidDamage(Mod, target, voidDrain);
            }
        }

        private bool IsHomewardVoidBoss(int npcType)
        {
            return npcType == ModContent.NPCType<PuppetOpera>() ||
                   npcType == ModContent.NPCType<BigDipper>() ||
                   npcType == ModContent.NPCType<PriestessRod>() ||
                   npcType == ModContent.NPCType<Diver>() ||
                   npcType == ModContent.NPCType<MarquisMoonsquid>() ||
                   npcType == ModContent.NPCType<TheMotherbrain>() ||
                   npcType == ModContent.NPCType<WallofShadow>() ||
                   npcType == ModContent.NPCType<SlimeGod>() ||
                   npcType == ModContent.NPCType<TheOverwatcher>() ||
                   npcType == ModContent.NPCType<TheLifebringerHead>() ||
                   npcType == ModContent.NPCType<TheMaterealizer>() ||
                   npcType == ModContent.NPCType<ScarabBelief>() ||
                   npcType == ModContent.NPCType<WorldsEndEverlastingFallingWhale>() ||
                   npcType == ModContent.NPCType<TheSon>();
        }
    }
}