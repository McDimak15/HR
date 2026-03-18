using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ContinentOfJourney.Projectiles;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class WoodenCrossPlayer : ModPlayer
    {
        public int healerAttackCounter = 0;
        public bool hasWoodenCross = false;

        public override void ResetEffects()
        {
            hasWoodenCross = false;
        }

        public void OnHealerAttack()
        {
            if (hasWoodenCross)
                healerAttackCounter++;
        }
    }

    public class WoodenCrossHealer : GlobalItem
    {
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            if (!ServerConfig.Instance.ThoriumBalance)
                return false;

            return ModContent.TryFind<ModItem>("ContinentOfJourney", "WoodenCross", out var woodenCross)
                   && entity.type == woodenCross.Type;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, false)) return;

            if (ModLoader.TryGetMod("ThoriumMod", out _))
            {
                foreach (var tooltip in tooltips)
                {
                    if (tooltip.Mod == "Terraria" && tooltip.Name.Contains("Tooltip"))
                    {
                        tooltip.Hide();
                    }
                }

                tooltips.Add(new TooltipLine(Mod, "WoodenCrossPatch1", Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.WoodenCross")));
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!AppliesToEntity(item, false)) return;

            var modPlayer = player.GetModPlayer<WoodenCrossPlayer>();
            modPlayer.hasWoodenCross = true;

            player.GetDamage(DamageClass.Magic) += 0.05f;

            if (ModLoader.TryGetMod("ThoriumMod", out var thorium) &&
                thorium.TryFind("HealerDamage", out DamageClass healerDamage))
            {
                player.GetDamage(healerDamage) += 0.02f;

                var templatePlayer = player.GetModPlayer<ContinentOfJourney.TemplatePlayer>();
                templatePlayer.Healertimer = -9999;
                templatePlayer.CrossTimer = int.MaxValue;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    var proj = Main.projectile[i];
                    if (proj.active &&
                        proj.type == ModContent.ProjectileType<HealBubble>() &&
                        proj.ai[0] == 4) 
                    {
                        proj.Kill();
                    }
                }

                if (modPlayer.healerAttackCounter >= 8)
                {
                    modPlayer.healerAttackCounter = 0;

                    int healTarget = player.whoAmI;
                    int lowestHealth = int.MaxValue;

                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player ply = Main.player[i];
                        if (ply.active && !ply.dead && ply.team == player.team &&
                            ply.statLife < ply.statLifeMax2 &&
                            ply.statLife < lowestHealth &&
                            Vector2.Distance(ply.Center, player.Center) < 1500)
                        {
                            lowestHealth = ply.statLife;
                            healTarget = ply.whoAmI;
                        }
                    }

                    int proj = Projectile.NewProjectile(
                        player.GetSource_Accessory(item),
                        player.Center,
                        Vector2.Zero,
                        ModContent.ProjectileType<HealBubble>(),
                        0,
                        0,
                        player.whoAmI,
                        2, // heal amount
                        healTarget
                    );

                    Main.projectile[proj].netUpdate = true;
                }
            }
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (item.DamageType == healerDamage)
                player.GetModPlayer<WoodenCrossPlayer>().OnHealerAttack();
        }
    }

    public class WoodenCrossProjCounter : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!ModLoader.TryGetMod("ThoriumMod", out var thorium)) return;
            if (!thorium.TryFind("HealerDamage", out DamageClass healerDamage)) return;

            if (projectile.owner >= 0 && projectile.owner < Main.maxPlayers &&
                projectile.DamageType == healerDamage)
            {
                Player player = Main.player[projectile.owner];
                var modPlayer = player.GetModPlayer<WoodenCrossPlayer>();

                if (modPlayer.hasWoodenCross)
                    modPlayer.OnHealerAttack();
            }
        }
    }
}
