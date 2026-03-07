using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using System.Collections.Generic;
using ContinentOfJourney;
using ContinentOfJourney.Buffs;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Items
{
    public class MoonwalkOverhaul : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            var cojPlayer = Player.GetModPlayer<TemplatePlayer>();

            if (cojPlayer.BackpackEquipped == 6)
            {
                cojPlayer.Gravity_Tile = 0;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == Player.whoAmI)
                    {
                        if (p.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.SpecialEffects.Gravity_proj>() ||
                            p.type == ModContent.ProjectileType<ContinentOfJourney.Projectiles.SpecialEffects.OneGiantStep_proj>())
                        {
                            p.Kill();
                        }
                    }
                }

                Player.slowFall = true;
                Player.jumpSpeedBoost += Player.numMinions * 0.05f;
            }

            if (Player.HasBuff(ModContent.BuffType<OneGiantStepBuff>()))
            {
                if (Player.whoAmI == Main.myPlayer)
                {
                    Player.GetDamage(DamageClass.Summon) *= 1.2f;
                    Player.maxRunSpeed *= 1.5f;
                    Player.runAcceleration *= 1.5f;

                    if (Player.controlLeft) { Player.controlLeft = false; Player.controlRight = true; }
                    else if (Player.controlRight) { Player.controlRight = false; Player.controlLeft = true; }

                    if (Player.velocity.X != 0)
                    {
                        Player.direction = Player.velocity.X > 0 ? 1 : -1;
                    }
                }
                else
                {
                    Player.maxRunSpeed *= 1.25f;
                    Player.runAcceleration *= 1.25f;
                }
            }
        }
    }

    public class MoonwalkTooltipRework : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<OneGiantLeap>())
            {
                tooltips.RemoveAll(line => line.Name.StartsWith("Tooltip"));
                tooltips.Add(new TooltipLine(Mod, "MoonwalkDesc", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.MoonwalkEffect")));
            }
        }
    }
}