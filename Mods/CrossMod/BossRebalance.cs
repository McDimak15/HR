using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class BossRebalance : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        private static bool calamityLoaded;
        private static bool infernumLoaded;
        private static bool fargoLoaded;
        private static bool thoriumLoaded;
        private static bool sotsLoaded;

        private static bool ssmLoaded;

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

        public override void OnSpawn(NPC npc, Terraria.DataStructures.IEntitySource source)
        {
            ssmLoaded = ssmLoaded || ModLoader.HasMod("ssm");
            if (ssmLoaded)
                return; 

            calamityLoaded = calamityLoaded || ModLoader.HasMod("CalamityMod");
            infernumLoaded = infernumLoaded || ModLoader.HasMod("InfernumMode");
            fargoLoaded = fargoLoaded || ModLoader.HasMod("FargowiltasSouls");
            thoriumLoaded = thoriumLoaded || ModLoader.HasMod("ThoriumMod");
            sotsLoaded = sotsLoaded || ModLoader.HasMod("SOTS");

            if (npc.ModNPC != null && npc.ModNPC.Mod?.Name == "ContinentOfJourney" && bossNames.Contains(npc.ModNPC.Name))
            {
                float hpMult = 1f;
                float dmgMult = 1f;
                float universalMult = 1f;

                if (calamityLoaded) hpMult += 0.20f;
                if (infernumLoaded) hpMult += 0.25f;
                if (fargoLoaded) hpMult += 0.20f;
                if (thoriumLoaded) hpMult += 0.10f;
                if (sotsLoaded) hpMult += 0.05f;

                if (calamityLoaded) dmgMult += 0.10f;
                if (infernumLoaded) dmgMult += 0.15f;
                if (fargoLoaded) dmgMult += 0.10f;
                if (thoriumLoaded) dmgMult += 0.10f;
                if (sotsLoaded) dmgMult += 0.05f;

                if (universalBosses.Contains(npc.ModNPC.Name))
                {
                    if (calamityLoaded) universalMult += 0.05f;
                    if (infernumLoaded) universalMult += 0.05f;
                    if (fargoLoaded) universalMult += 0.05f;
                }

                if (npc.ModNPC.Name == "TheSon" || npc.ModNPC.Name == "TheSon_Minion")
                {
                    universalMult *= 2f;
                }

                npc.lifeMax = (int)(npc.lifeMax * hpMult * universalMult);
                npc.damage = (int)(npc.damage * dmgMult);
                npc.life = npc.lifeMax;
            }
        }
    }
}
