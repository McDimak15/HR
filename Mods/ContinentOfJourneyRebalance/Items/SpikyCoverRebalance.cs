using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.Items.Accessories;
using CalamityMod.Buffs.DamageOverTime;
using ContinentOfJourney.Items.Accessories;
using HomewardRagnarok.Items.Accessories;

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
                if (player.armor[i].type == ModContent.ItemType<SpikyCover>() ||
                    player.armor[i].type == ModContent.ItemType<LampreyScarf>() ||
                    player.armor[i].type == ModContent.ItemType<Nucleogenesis>()
                    )
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

                    target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 600);

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
        public override bool AppliesToEntity(Item item, bool lateInstantiation) => item.type == ModContent.ItemType<SpikyCover>() ||
            item.type == ModContent.ItemType<LampreyScarf>() ||
            item.type == ModContent.ItemType<StatisCurse>() ||
            item.type == ModContent.ItemType<Nucleogenesis>()
            ;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.type == ModContent.ItemType<SpikyCover>())
            {
                player.whipRangeMultiplier -= 0.2f;
            }
            else if (item.type == ModContent.ItemType<LampreyScarf>())
            {
                player.whipRangeMultiplier -= 0.25f;
            }
            else
            {
                player.whipRangeMultiplier -= 0.10f;
            }
        }

       public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(l =>
                l.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Remove'range'")) ||
                l.Text.Contains(Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.Remove'Range'"))
            );
            Color animatedColor = Color.Lerp(Color.White, new Color(214, 145, 49), (float)(Math.Sin(Main.GlobalTimeWrappedHourly * 2.0) * 0.5 + 0.5));
            int maxTooltipIndex = -1;
            int maxNumber = -1;
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Terraria" && tooltips[i].Name.StartsWith("Tooltip"))
                {
                    if (int.TryParse(tooltips[i].Name.Substring(7), out int num) && num > maxNumber)
                    {
                        maxNumber = num;
                        maxTooltipIndex = i;
                    }
                }
                else if (tooltips[i].Mod == "InfernalEclipseAPI")
                {
                    maxTooltipIndex = i;
                }

            }
            int insertAt = maxTooltipIndex != -1 ? maxTooltipIndex + 1 : tooltips.Count;
            if (item.type == ModContent.ItemType<Nucleogenesis>())
            {
                tooltips.Insert(insertAt, new TooltipLine(Mod, "SpikyCoverRework", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.SpikyCoverRework")) { OverrideColor = animatedColor });
            }
            else if (item.type == ModContent.ItemType<SpikyCover>() || item.type == ModContent.ItemType<LampreyScarf>() || item.type == ModContent.ItemType<RiftGenerator>())
            {
                tooltips.Insert(insertAt, new TooltipLine(Mod, "SpikyCoverRework", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.SpikyCoverRework")));
            }
        }
    }
}