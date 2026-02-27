using Terraria.ModLoader;
using InfernumMode.Content.Tiles.Relics;
using InfernumMode.Content.Items.Relics;

namespace HomewardRagnarok.Items.Tiles.Relics
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]

    // Chariot
    public class ChariotRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Chariot Relic";
        public override int TileID => ModContent.TileType<ChariotRelicTile>();
    }
    public class ChariotRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<ChariotRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/ChariotRelicTile";
    }

    // Big Dipper
    public class BigDipperRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Big Dipper Relic";
        public override int TileID => ModContent.TileType<BigDipperRelicTile>();
    }
    public class BigDipperRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<BigDipperRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/BigDipperRelicTile";
    }

    // Puppet Opera
    public class PuppetRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Puppet Relic";
        public override int TileID => ModContent.TileType<PuppetRelicTile>();
    }
    public class PuppetRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<PuppetRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/PuppetRelicTile";
    }

    // Marquis Moonsquid 
    public class MarquisMoonsquidRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Marquis Moonsquid Relic";
        public override int TileID => ModContent.TileType<MarquisMoonsquidRelicTile>();
    }
    public class MarquisMoonsquidRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<MarquisMoonsquidRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/MarquisMoonsquidRelicTile";
    }

    // Priestess Rod
    public class PriestessRodRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Priestess Rod Relic";
        public override int TileID => ModContent.TileType<PriestessRodRelicTile>();
    }
    public class PriestessRodRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<PriestessRodRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/PriestessRodRelicTile";
    }

    // Diver
    public class DiverRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Diver Relic";
        public override int TileID => ModContent.TileType<DiverRelicTile>();
    }
    public class DiverRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<DiverRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/DiverRelicTile";
    }

    // The Motherbrain
    public class TheMotherbrainRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal The Motherbrain Relic";
        public override int TileID => ModContent.TileType<TheMotherbrainRelicTile>();
    }
    public class TheMotherbrainRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheMotherbrainRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheMotherbrainRelicTile";
    }

    // Wall of Shadow
    public class WallofShadowRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Wall of Shadow Relic";
        public override int TileID => ModContent.TileType<WallofShadowRelicTile>();
    }
    public class WallofShadowRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<WallofShadowRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/WallofShadowRelicTile";
    }

    // Slime God
    public class SolarRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Solar Relic";
        public override int TileID => ModContent.TileType<SolarRelicTile>();
    }
    public class SolarRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<SolarRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/SolarRelicTile";
    }

    // The Overwatcher
    public class TheOverwatcherRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal The Overwatcher Relic";
        public override int TileID => ModContent.TileType<TheOverwatcherRelicTile>();
    }
    public class TheOverwatcherRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheOverwatcherRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheOverwatcherRelicTile";
    }

    // The Materealizer
    public class TheMaterealizerRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal The Materealizer Relic";
        public override int TileID => ModContent.TileType<TheMaterealizerRelicTile>();
    }
    public class TheMaterealizerRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheMaterealizerRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheMaterealizerRelicTile";
    }

    // The Lifebringer
    public class TheLifebringerRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal The Lifebringer Relic";
        public override int TileID => ModContent.TileType<TheLifebringerRelicTile>();
    }
    public class TheLifebringerRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheLifebringerRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheLifebringerRelicTile";
    }

    // Scarab Belief
    public class ScarabBeliefRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal Scarab Belief Relic";
        public override int TileID => ModContent.TileType<ScarabBeliefRelicTile>();
    }
    public class ScarabBeliefRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<ScarabBeliefRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/ScarabBeliefRelicTile";
    }

    // Whale
    public class WhaleRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal World's End Everlasting Falling Whale Relic";
        public override int TileID => ModContent.TileType<WhaleRelicTile>();
    }
    public class WhaleRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<WhaleRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/WhaleRelicTile";
    }

    // The Son
    public class TheSonRelic : BaseRelicItem
    {
        public override string DisplayNameToUse => "Infernal The Son Relic";
        public override int TileID => ModContent.TileType<TheSonRelicTile>();
    }
    public class TheSonRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheSonRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheSonRelicTile";
    }
}