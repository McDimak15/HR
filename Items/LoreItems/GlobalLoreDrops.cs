using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using CalamityMod;
using ContinentOfJourney; 
using HomewardRagnarok.Items.LoreItems; 
using ContinentOfJourney.NPCs.Boss_PriestessRod;
using ContinentOfJourney.NPCs.Boss_GoblinChariot;
using ContinentOfJourney.NPCs.Boss_BigDipper;
using ContinentOfJourney.NPCs.Boss_MarquisMoonsquid;
using ContinentOfJourney.NPCs.Boss_PuppetOpera;
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

namespace HomewardRagnarok.Items.LoreItems
{
    public class GlobalLoreDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(Terraria.NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<GoblinChariot>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedGoblinChariot,
                    ModContent.ItemType<LoreGoblinChariot>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<BigDipper>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedBigDipper,
                    ModContent.ItemType<LoreBigDipper>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<PuppetOpera>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedPuppetOpera,
                    ModContent.ItemType<LorePuppetOpera>(), true, DropHelper.FirstKillText);
            }
            if (npc.type == ModContent.NPCType<MarquisMoonsquid>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedSquidBoss,
                    ModContent.ItemType<LoreMarquisMoonsquid>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<PriestessRod>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedRodBoss,
                    ModContent.ItemType<LorePriestessRod>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<Diver>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedDiverBoss,
                    ModContent.ItemType<LoreDiver>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<TheMotherbrain>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedMechBrainBoss,
                    ModContent.ItemType<LoreTheMotherbrain>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<WallofShadow>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedBarrier,
                    ModContent.ItemType<LoreWallofShadow>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<SlimeGod>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedSunGod,
                    ModContent.ItemType<LoreSlimeGod>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<TheOverwatcher>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedTimeGod,
                    ModContent.ItemType<LoreOverwatcher>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<TheMaterealizer>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedMatterGod,
                    ModContent.ItemType<LoreTheMaterealizer>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<TheLifebringerHead>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedLifeGod,
                    ModContent.ItemType<LoreTheLifebringer>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<ScarabBelief>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedMyths,
                    ModContent.ItemType<LoreScarabBelief>(), true, DropHelper.FirstKillText);
            }
    
            if (npc.type == ModContent.NPCType<WorldsEndEverlastingFallingWhale>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedDeath,
                    ModContent.ItemType<LoreWhale>(), true, DropHelper.FirstKillText);
            }

            if (npc.type == ModContent.NPCType<TheSon>())
            {
                npcLoot.AddConditionalPerPlayer(() => !ContinentOfJourney.DownedBossSystem.downedSon,
                    ModContent.ItemType<LoreTheSon>(), true, DropHelper.FirstKillText);
            }
        }
    }
}