using System.Linq;
using CalamityMod;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories.ThrowerLuckTickets;
using ContinentOfJourney.Projectiles;
using HomewardRagnarok.Config;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
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

    public class LuckTicketRebalance : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (!ServerConfig.Instance.EnableTickets) return;

            Player player = Main.player[projectile.owner];
            if (player == null || !player.active) return;
            if (!player.TryGetModPlayer(out TemplatePlayer modPlayer)) return;
            if (modPlayer.TicketEquipped != 2 || modPlayer.TicketTimerReset <= 0) return;

            if (!(source is EntitySource_ItemUse parent2) ||
                !CoJGlobalProjectile_Instance.isThrowingItem(parent2.Item, out bool isKnife, true))
                return;

            if (isKnife) return;

            float dmgRatio = 1.3f / 1.7f;
            float speedRatio = 1.2f / 1.4f;

            if (ClashingAccessories.ThrowerChargeProjs.Contains(projectile.type))
            {
                float oldBoost = 1f + (1.7f - 1f) * 0.33f;
                float newBoost = 1f + (1.3f - 1f) * 0.33f;
                dmgRatio = newBoost / oldBoost;
            }

            projectile.damage = (int)(projectile.damage * dmgRatio);
            projectile.velocity *= speedRatio;
        }
    }
}