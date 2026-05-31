using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using InfernumMode.Content.Tiles.Relics;
using InfernumMode.Content.Items.Relics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HomewardRagnarok.Items.Tiles.Relics
{
    [JITWhenModsEnabled("InfernumMode")]
    [ExtendsFromMod("InfernumMode")]
    // Lunar (Blood Moon)
    public class LunarRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<LunarRelicTile>();
    }
    public class LunarRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<LunarRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/LunarRelicTile";
    }

    // Chariot
    public class ChariotRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
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
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<SolarRelicTile>();
    }
    public class SolarRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<SolarRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/SolarRelicTile";
    }

    // Oozyssey
    public class OozysseyRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<OozysseyRelicTile>();
    }
    public class OozysseyRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<OozysseyRelic>();
        public override string CoJRelicName => "OozysseyRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/OozysseyRelicTile";
    }

    // The Overwatcher
    public class TheOverwatcherRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<TheOverwatcherRelicTile>();
    }
    public class TheOverwatcherRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheOverwatcherRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheOverwatcherRelicTile";
    }

    // Khrono Trigger
    public class KhronoTriggerRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<KhronoTriggerRelicTile>();
    }
    public class KhronoTriggerRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<KhronoTriggerRelic>();
        public override string CoJRelicName => "KhronoTriggerRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/KhronoTriggerRelicTile";
    }

    // The Materealizer
    public class TheMaterealizerRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<TheMaterealizerRelicTile>();
    }
    public class TheMaterealizerRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheMaterealizerRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheMaterealizerRelicTile";
    }

    // Spiral Stairs
    public class SpiralStairsRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<SpiralStairsRelicTile>();
    }
    public class SpiralStairsRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<SpiralStairsRelic>();
        public override string CoJRelicName => "SpiralStairsRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/SpiralStairsRelicTile";
    }

    // The Lifebringer
    public class TheLifebringerRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<TheLifebringerRelicTile>();
    }
    public class TheLifebringerRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheLifebringerRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheLifebringerRelicTile";
    }

    // EtzHaChayim
    public class EtzHaChayimRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<EtzHaChayimRelicTile>();
    }
    public class EtzHaChayimRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<EtzHaChayimRelic>();
        public override string CoJRelicName => "EtzHaChayimRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/EtzHaChayimRelicTile";
    }

    // Scarab Belief
    public class ScarabBeliefRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<ScarabBeliefRelicTile>();
    }
    public class ScarabBeliefRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<ScarabBeliefRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/ScarabBeliefRelicTile";
    }

    // Three Thousand Worlds
    public class ThreeThousandWorldsRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<ThreeThousandWorldsRelicTile>();
    }
    public class ThreeThousandWorldsRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<ThreeThousandWorldsRelic>();
        public override string CoJRelicName => "ThreeThousandWorldsRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/ThreeThousandWorldsRelicTile";
    }

    // Whale
    public class WhaleRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<WhaleRelicTile>();
    }
    public class WhaleRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<WhaleRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/WhaleRelicTile";
    }

    // Rising World
    public class RisingWorldRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<RisingWorldRelicTile>();
    }
    public class RisingWorldRelicTile : InfernumRainbowRelic
    {
        public override int DropItemID => ModContent.ItemType<RisingWorldRelic>();
        public override string CoJRelicName => "RisingWorldRelic";
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/RisingWorldRelicTile";
    }

    // The Son
    public class TheSonRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<TheSonRelicTile>();
    }
    public class TheSonRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheSonRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheSonRelicTile";
    }

    // The Holy
    public class TheHolyRelic : BaseRelicItem
    {
        public override LocalizedText Tooltip => Language.GetText($"Mods.HomewardRagnarok.Items.{this.Name}.Tooltip");
        public override int TileID => ModContent.TileType<TheHolyRelicTile>();
    }
    public class TheHolyRelicTile : BaseInfernumBossRelic
    {
        public override int DropItemID => ModContent.ItemType<TheHolyRelic>();
        public override string RelicTextureName => "HomewardRagnarok/Items/Tiles/Relics/TheHolyRelicTile";
    }
}