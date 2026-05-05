using Terraria;
using Terraria.ModLoader;
using CalamityMod;
using HomewardRagnarok.Config;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories.ThrowerLuckTickets;
using ThrowerUnification.Core.UnitedModdedThrowerClass;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Detections
{
    public class TicketReworkPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (!ServerConfig.Instance.EnableTickets) return;

            if (Player.TryGetModPlayer(out TemplatePlayer cojPlayer))
            {
                Item heldItem = Player.HeldItem;

                bool isUnitedThrower = heldItem != null &&
                                      !heldItem.IsAir &&
                                      heldItem.DamageType is UnitedModdedThrower;

                bool isConsumableRanger = heldItem != null &&
                                          !heldItem.IsAir &&
                                          heldItem.DamageType == DamageClass.Ranged &&
                                          heldItem.consumable;

                if (isConsumableRanger)
                {
                    cojPlayer.TicketEquipped = -1;
                    cojPlayer.TicketTimer = 0;
                    cojPlayer.TicketTimerReset = 0;
                }
                if (isUnitedThrower)
                {
                    if (!Player.Calamity().StealthStrikeAvailable())
                    {
                        cojPlayer.TicketTimer = 0;
                    }
                }
                if (cojPlayer.TicketEquipped == 7)
                {
                    Player.GetModPlayer<Ticket_ModPlayer>().JackpotDmgBoost *= 0.125f;
                }
            }
        }
    }
}