using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using COJItem = ContinentOfJourney.Items.Accessories.ArmillarySphere;
using COJProj = ContinentOfJourney.Projectiles.SpecialEffects.ArmillarySphere;
using ContinentOfJourney;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.Rebalance.Items
{
    public class ArmillarySphereRebalance : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.type == ModContent.ItemType<COJItem>();
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            Item held = player.HeldItem;

            bool usingSummon =
                held.damage > 0 &&
                (held.DamageType == DamageClass.Summon ||
                 held.DamageType == DamageClass.SummonMeleeSpeed ||
                 held.CountsAsClass(DamageClass.Summon));

            int projType = ModContent.ProjectileType<COJProj>();

            if (!usingSummon)
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == projType)
                        p.Kill();
                }
                return;
            }

            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
            modPlayer.armillarySphere = true;

            if (player.ownedProjectileCounts[projType] < 1)
            {
                Projectile.NewProjectile(
                    player.GetSource_Accessory(item),
                    player.MountedCenter,
                    Vector2.Zero,
                    projType,
                    0, 0f,
                    player.whoAmI
                );
            }
        }
    }

    public class ArmillarySphereTooltipFix : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out var coj))
                return;

            if (coj.TryFind("ArmillarySphere", out ModItem arms) && item.type == arms.Type)
            {
                foreach (var line in tooltips)
                    if (line.Text.Contains("holding"))
                        line.Text = line.Text.Replace("holding", "summoner");
            }
        }
    }
}
