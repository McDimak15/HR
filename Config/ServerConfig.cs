using Terraria.ModLoader.Config;
using System.ComponentModel;
using ContinentOfJourney.NPCs.Boss_SlimeGod;
using ContinentOfJourney.Items;

namespace HomewardRagnarok.Config
{
    public class ServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("MainCategory")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableBossScaling { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool HWJRecipes { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableBookmarks { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool CustomContent { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool BloodOrbPotions { get; set; }

        [Header("WeaponChanges")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool WeaponBalancing { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool CustomStealthStrikes { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableRogueClassChanges { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableMeleeClassChanges { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool EnableHealerClassChanges { get; set; }

        [Header("ArmorChanges")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ArmorBalancing { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ArmorCraft { get; set; }

        [Header("ModChanges")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool CalamityBalance { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ClamityBalance { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ThoriumBalance { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool SOTSBalance { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ConsolariaBalance { get; set; }

        public static ServerConfig Instance;

        public override void OnLoaded()
        {
            Instance = this;
        }
    }
}
