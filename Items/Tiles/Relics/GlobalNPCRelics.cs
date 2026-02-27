using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using CalamityMod;
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
using InfernumMode.Content.Tiles.Relics;
using InfernumMode.Content.Items.Relics;
using InfernumMode.Core.GlobalInstances.Systems;
using InfernumMode;

namespace HomewardRagnarok.Items.Tiles.Relics
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]
    public class GlobalRelicsDrop : GlobalNPC
    {
        public override void ModifyNPCLoot(Terraria.NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            static bool isInfernum() => WorldSaveSystem.InfernumModeEnabled;
            if (npc.type == ModContent.NPCType<GoblinChariot>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<ChariotRelic>());
            }
            if (npc.type == ModContent.NPCType<BigDipper>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<BigDipperRelic>());
            }
            if (npc.type == ModContent.NPCType<PuppetOpera>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<PuppetRelic>());
            }
            if (npc.type == ModContent.NPCType<MarquisMoonsquid>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<MarquisMoonsquidRelic>());
            }
            if (npc.type == ModContent.NPCType<PriestessRod>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<PriestessRodRelic>());
            }
            if (npc.type == ModContent.NPCType<Diver>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<DiverRelic>());
            }
            if (npc.type == ModContent.NPCType<TheMotherbrain>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<TheMotherbrainRelic>());
            }
            if (npc.type == ModContent.NPCType<WallofShadow>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<WallofShadowRelic>());
            }
            if (npc.type == ModContent.NPCType<SlimeGod>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<SolarRelic>());
            }
            if (npc.type == ModContent.NPCType<TheOverwatcher>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<TheOverwatcherRelic>());
            }
            if (npc.type == ModContent.NPCType<TheMaterealizer>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<TheMaterealizerRelic>());
            }
            if (npc.type == ModContent.NPCType<TheLifebringerHead>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<TheLifebringerRelic>());
            }
            if (npc.type == ModContent.NPCType<ScarabBelief>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<ScarabBeliefRelic>());
            }
            if (npc.type == ModContent.NPCType<WorldsEndEverlastingFallingWhale>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<WhaleRelic>());
            }
            if (npc.type == ModContent.NPCType<TheSon>())
            {
                npcLoot.AddIf(isInfernum, ModContent.ItemType<TheSonRelic>());
            }
        }
    }
}