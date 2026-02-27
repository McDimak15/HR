using Terraria;
using Terraria.ModLoader;
using HomewardRagnarok.Config;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class CoJClassOverrides : GlobalItem
    {
        public override bool InstancePerEntity => false;
        private static HashSet<int> rogueItems;
        private static int shorelineType;
        private static int daCapoType;

        private void SetupTypes()
        {
            if (rogueItems != null) return;

            rogueItems = new();
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                string[] rogueNames = {
                    "AdamantiteKnife","BrokenHammer","Backstabber","Bloodthirst","CobaltKnife",
                    "CopperKnife","GoldKnife","IronKnife","MythrilKnife","OrichalcumKnife",
                    "PalladiumKnife","PlatinumKnife","SilverKnife","TinKnife","HardKnuckle",
                    "Hellfire","Longinus","InkyToss","RollingCutter","ItemDarkKnife",
                    "TitaniumKnife","TungstenKnife","ChlorophyteKnife","LeadKnife",
                    "ToothOfCthulhu","ItemCactusBall","ItemSolidTornado","SpikyBomb","Fission",
                    "SamsaraOfDawnlight", "RisingAction"
                };

                foreach (string name in rogueNames)
                    if (coj.TryFind(name, out ModItem modItem))
                        rogueItems.Add(modItem.Type);

                shorelineType = coj.TryFind("Shoreline", out ModItem shore) ? shore.Type : -1;
                daCapoType = coj.TryFind("DaCapo", out ModItem capo) ? capo.Type : -1;
            }
        }

        public override void SetDefaults(Item item)
        {
            SetupTypes();

            var cfg = ServerConfig.Instance;
            if (cfg == null) return;

            if (rogueItems.Contains(item.type))
            {
                if (cfg.EnableRogueClassChanges &&
                    ModLoader.TryGetMod("ThrowerUnification", out Mod TU) &&
                    TU.TryFind("UnitedModdedThrower", out DamageClass throwerClass))
                {
                    item.DamageType = throwerClass;
                }
            }

            if (item.type == shorelineType && cfg.EnableMeleeClassChanges)
            {
                item.DamageType = DamageClass.Melee;
            }

            if (item.type == daCapoType && cfg.EnableHealerClassChanges)
            {
                if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium) &&
                    thorium.TryFind("HealerDamage", out DamageClass healerClass))
                {
                    item.DamageType = healerClass;
                }
            }
        }
    }
}