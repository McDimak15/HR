using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.Items.Accessories.Wings;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class AltitudeWingRebalance : GlobalItem
    {
        public const int FlightTime = 400;
        public const float FlightSpeed = 12.5f;
        public const float Acceleration = 3.5f;

        public const float BonusAscentWhileFalling = 1.2f;
        public const float BonusAscentWhileRising = 0.2f;
        public const float RisingSpeedThreshold = 1.3f;
        public const float MaxAscentSpeed = 3.5f;
        public const float BaseAscent = 0.17f;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<Altitude>();
        }

        public override void VerticalWingSpeeds(
            Item item, Player player,
            ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier,
            ref float constantAscend)
        {
            if (!AppliesToEntity(item, false)) return;

            ascentWhenFalling = BonusAscentWhileFalling;
            ascentWhenRising = BonusAscentWhileRising;
            maxCanAscendMultiplier = RisingSpeedThreshold;
            maxAscentMultiplier = MaxAscentSpeed;
            constantAscend = BaseAscent;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            string extra = ModLoader.TryGetMod("CalamityMod", out _)
                ? CalamityStyleTooltip.Build(FlightTime, FlightSpeed, Acceleration,
                    BaseAscent, MaxAscentSpeed, BonusAscentWhileFalling, BonusAscentWhileRising, RisingSpeedThreshold)
                : $"\n{FlightTime / 60}s flight time";

            var wingLine = tooltips.Find(t => t.Mod == "Terraria" && t.Text == Language.GetTextValue("CommonItemTooltip.FlightAndSlowfall"));
            if (wingLine != null)
                wingLine.Text += extra;
        }
    }
    public class AltitudeWingStatsRework : ModSystem
    {
        public override void PostSetupContent()
        {
            int slot = new Item(ModContent.ItemType<Altitude>()).wingSlot;

            if (slot > 0)
            {
                ArmorIDs.Wing.Sets.Stats[slot] = new WingStats(
                    AltitudeWingRebalance.FlightTime,
                    AltitudeWingRebalance.FlightSpeed,
                    AltitudeWingRebalance.Acceleration
                );
            }
        }
    }

    public static class CalamityStyleTooltip
    {
        public static string Build(int flightTimeFrames, float flightSpeed, float acceleration,
            float baseAscent, float maxAscentSpeed, float bonusAscentWhileFalling,
            float bonusAscentWhileRising, float risingSpeedThreshold)
        {
            int time = flightTimeFrames;
            float run = flightSpeed;
            float rAcc = acceleration * 0.08f;

            float baseJumpSpeed = (CalamityServerConfig.Instance.FasterJumpSpeed
                ? 5.71f
                : 5.01f) + 1f;

            var sb = new StringBuilder(512);
            sb.Append('\n');

            if (Main.keyState.PressingShift())
            {
                sb.Append(CalamityUtils.GetText("Common.WingStatsFull").Format(
                [
                    time.FramesToSeconds(),
                    BaseWings.HorizontalSpeedText(run),
                    run.ToMph(),
                    BaseWings.VerticalSpeedText(maxAscentSpeed),
                    (maxAscentSpeed * baseJumpSpeed).ToMph(),
                    BaseWings.HorizontalAccelerationText(acceleration),
                    rAcc.ToMphps(),
                    BaseWings.VerticalAccelerationText(baseAscent),
                    baseAscent.ToMphps(),
                    (baseAscent + bonusAscentWhileRising).ToMphps(),
                    (risingSpeedThreshold * baseJumpSpeed).ToMph(),
                    (baseAscent + bonusAscentWhileFalling).ToMphps()
                ]));
            }
            else
            {
                sb.Append(CalamityUtils.GetText("Common.WingStats").Format(
                [
                    time.FramesToSeconds(),
                    BaseWings.HorizontalSpeedText(run),
                    BaseWings.VerticalSpeedText(maxAscentSpeed),
                    BaseWings.HorizontalAccelerationText(rAcc),
                    BaseWings.VerticalAccelerationText(baseAscent)
                ]));
                sb.Append('\n');
                sb.Append($"[c/B8B8B8:{CalamityUtils.GetTextValue("UI.HoldShiftTooltipExtensionIndicator")}]");
            }

            return sb.ToString();
        }
    }
}