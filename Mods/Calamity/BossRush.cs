/*
using Terraria.ModLoader;
using Terraria.ID; 
using static CalamityMod.Events.BossRushEvent;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.DevourerofGods;
using ContinentOfJourney.NPCs.Boss_GoblinChariot;
using ContinentOfJourney.NPCs.Boss_BigDipper;
using ContinentOfJourney.NPCs.Boss_MarquisMoonsquid;
using ContinentOfJourney.NPCs.Boss_PriestessRod;
using ContinentOfJourney.NPCs.Boss_Diver;
using ContinentOfJourney.NPCs.Boss_TheMotherbrain;
using ContinentOfJourney.NPCs.Boss_TheLifebringer;
using ContinentOfJourney.NPCs.Boss_TheMaterealizer;
using ContinentOfJourney.NPCs.Boss_WorldsEndEverlastingFallingWhale;

namespace HomewardRagnarok
{
    public class CoJBossRushPatch : ModSystem
    {
        public override void PostSetupContent()
        {
            for (int i = Bosses.Count - 1; i >= 0; i--)
            {
                int npcID = Bosses[i].EntityID;

                // Goblin Chariot after Brain of Cthulhu
                if (npcID == NPCID.BrainofCthulhu)
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<GoblinChariot>(), TimeChangeContext.Night));
                }

                // Big Dipper after Queen Bee 
                if (npcID == NPCID.QueenBee)
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<BigDipper>(), TimeChangeContext.Night));
                }

                // Marquis Moonsquid after Calamity Slime God
                if (npcID == ModContent.NPCType<SlimeGodCore>())
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<MarquisMoonsquid>(), TimeChangeContext.Night));
                }

                // Priestess Rod after Skeletron Prime
                if (npcID == NPCID.SkeletronPrime)
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<PriestessRod>(), TimeChangeContext.Night));
                }

                // Diver after Golem
                if (npcID == NPCID.Golem)
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<Diver>(), TimeChangeContext.Night));
                }

                // The Motherbrain after Diver
                if (npcID == ModContent.NPCType<Diver>())
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<TheMotherbrain>(), TimeChangeContext.Night));
                }

                // Whale after Materealizer
                if (npcID == ModContent.NPCType<TheMaterealizer>())
                {
                    Bosses.Insert(i + 1, new Boss(ModContent.NPCType<WorldsEndEverlastingFallingWhale>(), TimeChangeContext.Night));
                }
            }
        }
    }
}
*/