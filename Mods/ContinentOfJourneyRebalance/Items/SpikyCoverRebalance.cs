using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ContinentOfJourney.Items.Accessories;

namespace HomewardRagnarok.Compat
{
    public class SpikyCoverProjectilePatch : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ProjectileID.Sets.IsAWhip[projectile.type]) return;

            Player player = Main.player[projectile.owner];
            bool equipped = false;
            for (int i = 3; i < 10; i++) 
            {
                if (player.armor[i].type == ModContent.ItemType<SpikyCover>())
                {
                    equipped = true;
                    break;
                }
            }

            if (equipped)
            {
                float dist = Vector2.Distance(player.Center, target.Center);

                if (dist > 110f * player.whipRangeMultiplier)
                {
                    modifiers.FinalDamage *= 1.5f;

                    if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                    {
                        target.AddBuff(calamity.Find<ModBuff>("HeavyBleeding").Type, 600);
                    }

                    for (int i = 0; i < 12; i++)
                    {
                        Dust d = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Blood);
                        d.velocity *= 1.5f;
                        d.scale = 1.2f;
                    }
                }
            }
        }
    }

    public class SpikyCoverItemPatch : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation) => item.type == ModContent.ItemType<SpikyCover>();

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            player.whipRangeMultiplier -= 0.2f;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(l => l.Text.Contains("range") || l.Text.Contains("Range"));

            var line = new TooltipLine(Mod, "SpikyCoverRework", "Whip tips deal 1.5x damage and cause bleeding");
            tooltips.Add(line);
        }
    }
}