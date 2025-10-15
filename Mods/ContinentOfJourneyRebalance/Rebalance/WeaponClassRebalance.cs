using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class CoJClassOverrides : GlobalItem
    {
        private static HashSet<int> rogueItems;
        private static int shorelineType;
        private static int fissionType;
        private static int daCapoType;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            if (rogueItems == null)
            {
                rogueItems = new HashSet<int>();

                if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                {
                    string[] rogueNames =
                    {
                        "AdamantiteKnife",
                        "BrokenHammer",
                        "Backstabber",
                        "Bloodthirst",
                        "CobaltKnife",
                        "CopperKnife",
                        "GoldKnife",
                        "IronKnife",
                        "MythrilKnife",
                        "OrichalcumKnife",
                        "PalladiumKnife",
                        "PlatinumKnife",
                        "SilverKnife",
                        "TinKnife",
                        "HardKnuckle",
                        "Hellfire",
                        "Longinus",
                        "InkyToss",
                        "RollingCutter",
                        "ItemDarkKnife",
                        "TitaniumKnife",
                        "TungstenKnife",
                        "ChlorophyteKnife",
                        "LeadKnife",
                        "ToothOfCthulhu",
                        "ItemCactusBall",
                        "ItemSolidTornado"
                    };

                    foreach (string name in rogueNames)
                    {
                        if (coj.TryFind(name, out ModItem modItem))
                            rogueItems.Add(modItem.Type);
                    }

                    shorelineType = coj.TryFind("Shoreline", out ModItem shoreline) ? shoreline.Type : -1;
                    fissionType = coj.TryFind("Fission", out ModItem fission) ? fission.Type : -1;
                    daCapoType = coj.TryFind("DaCapo", out ModItem daCapo) ? daCapo.Type : -1;
                }
            }

            return rogueItems.Contains(item.type) ||
                   item.type == shorelineType ||
                   item.type == fissionType ||
                   item.type == daCapoType;
        }

        public override void SetDefaults(Item item)
        {
            if (rogueItems.Contains(item.type))
            {
                if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                    calamity.TryFind("RogueDamageClass", out DamageClass rogueClass))
                {
                    item.DamageType = rogueClass;
                }
            }

            if (item.type == shorelineType)
            {
                item.DamageType = DamageClass.Melee;
            }

            if (item.type == fissionType)
            {
                item.DamageType = DamageClass.Melee;
            }

            if (item.type == daCapoType)
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
