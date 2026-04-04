using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class BossRebalance : GlobalNPC
    {
        private static bool? _calamity;
        private static bool? _infernum;
        private static bool? _fargo;
        private static bool? _thorium;
        private static bool? _sots;

        private static bool Calamity => _calamity ??= ModLoader.HasMod("CalamityMod");
        private static bool Infernum => _infernum ??= ModLoader.HasMod("InfernumMode");
        private static bool Fargo => _fargo ??= ModLoader.HasMod("FargowiltasSouls");
        private static bool Thorium => _thorium ??= ModLoader.HasMod("ThoriumMod");
        private static bool Sots => _sots ??= ModLoader.HasMod("SOTS");

        private static readonly HashSet<string> bossNames = new()
        {
            "BigDipper","Diver","GoblinChariot","TheLifebringerHead","MarquisMoonsquid","PriestessRod","PuppetOpera",
            "ScarabBelief","ScarabBelief_Minion","SlimeGod","FlyingSpikeSlime","TheMaterealizer","TheMaterealizer_Minion",
            "TheMotherbrain","TheMotherbrain_Minion","TheOverwatcher","TheOverwatcher_Minion",
            "TheSon","TheSon_Minion","WallofShadow","WallofShadow_Minion","WorldsEndEverlastingFallingWhale"
        };

        private static readonly HashSet<string> universalBosses = new()
        {
            "TheLifebringerHead","ScarabBelief","ScarabBelief_Minion","SlimeGod","FlyingSpikeSlime",
            "TheMaterealizer","TheMaterealizer_Minion","TheOverwatcher","TheOverwatcher_Minion",
            "WorldsEndEverlastingFallingWhale"
        };

        public override void SetDefaults(NPC npc)
        {
            if (!ServerConfig.Instance.EnableBossScaling) return;

            if (npc.ModNPC != null && npc.ModNPC.Mod.Name == "ContinentOfJourney" && bossNames.Contains(npc.ModNPC.Name))
            {
                float hpMult = 1f;
                float dmgMult = 1f;
                float universalMult = 1f;

                if (Calamity) hpMult += 0.20f;
                if (Infernum) hpMult += 0.25f;
                if (Fargo) hpMult += 0.20f;
                if (Thorium) hpMult += 0.10f;
                if (Sots) hpMult += 0.05f;

                if (Calamity) dmgMult += 0.10f;
                if (Infernum) dmgMult += 0.15f;
                if (Fargo) dmgMult += 0.10f;
                if (Thorium) dmgMult += 0.10f;
                if (Sots) dmgMult += 0.05f;

                if (universalBosses.Contains(npc.ModNPC.Name))
                {
                    if (Calamity) universalMult += 0.05f;
                    if (Infernum) universalMult += 0.05f;
                    if (Fargo) universalMult += 0.05f;
                }

                if (npc.ModNPC.Name == "TheSon" || npc.ModNPC.Name == "TheSon_Minion")
                {
                    universalMult *= 2f;
                }

                npc.lifeMax = (int)(npc.lifeMax * hpMult * universalMult);
                npc.damage = (int)(npc.damage * dmgMult);
            }
        }
    }
}