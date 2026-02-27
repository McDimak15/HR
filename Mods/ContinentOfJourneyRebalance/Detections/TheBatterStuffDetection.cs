using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ThrowerUnification.Core.UnitedModdedThrowerClass;
using ContinentOfJourney;
using ContinentOfJourney.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;

namespace HomewardRagnarok
{
    public class CatchersGloveThrowerPatch : GlobalItem
    {
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            if (item.consumable && item.DamageType is UnitedModdedThrower)
            {
                var modPlayer = player.GetModPlayer<TemplatePlayer>();

                if (modPlayer.CatchersGlove && modPlayer.TheBatter)
                    return 1.17f;

                if (modPlayer.TheBatter)
                    return 1.07f;

                if (modPlayer.CatchersGlove)
                    return 1.10f;
            }

            return base.UseSpeedMultiplier(item, player);
        }
    }

    public class BattersCapThrowerPatch : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
                return;

            Player player = Main.player[projectile.owner];
            if (player == null)
                return;

            TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();

            if (source is EntitySource_ItemUse itemSource &&
                itemSource.Item.consumable &&
                itemSource.Item.DamageType is UnitedModdedThrower)
            {
                if (modPlayer.BattersCap)
                {
                    projectile.velocity *= 1.5f;

                    if (itemSource.Item.type != ItemID.Beenade)
                    {
                        if (!projectile.usesIDStaticNPCImmunity)
                        {
                            projectile.usesLocalNPCImmunity = true;
                            if (projectile.localNPCHitCooldown == 0)
                                projectile.localNPCHitCooldown = 10;
                        }
                    }
                }
                else if (modPlayer.SteadyCap)
                {
                    projectile.velocity *= 1.4f;

                    if (itemSource.Item.type != ItemID.Beenade)
                    {
                        if (!projectile.usesIDStaticNPCImmunity)
                        {
                            projectile.usesLocalNPCImmunity = true;
                            if (projectile.localNPCHitCooldown == 0)
                                projectile.localNPCHitCooldown = 10;
                        }
                    }
                }
            }
        }
    }

    public class BatterTooltipPatch : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ModContent.ItemType<TheBatter>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("Increase penetration of consumable ranged weapons by 2"))
                    {
                        tooltips.RemoveAt(i);
                        i--;
                    }
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                    if (line.Text.Contains("70%"))
                    {
                        line.Text = line.Text.Replace("70%", "50%");
                    }
                }
            }
            if (item.type == ModContent.ItemType<BatterCap>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("Increase penetration of consumable ranged weapons by 2"))
                    {
                        tooltips.RemoveAt(i);
                        i--;
                    }
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                    if (line.Text.Contains("70%"))
                    {
                        line.Text = line.Text.Replace("70%", "50%");
                    }
                }
            }
            if (item.type == ModContent.ItemType<RedCube>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("Increase penetration of consumable ranged weapons by 2"))
                    {
                        tooltips.RemoveAt(i);
                        i--;
                    }
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                    if (line.Text.Contains("70%"))
                    {
                        line.Text = line.Text.Replace("70%", "50%");
                    }
                }
            }
            if (item.type == ModContent.ItemType<SteadyCap>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("Increase penetration of consumable ranged weapons by 1"))
                    {
                        tooltips.RemoveAt(i);
                        i--;
                    }
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                }
            }
            if (item.type == ModContent.ItemType<CatchersGlove>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                }
            }
            if (item.type == ModContent.ItemType<RunnersLegging>())
            {
                for (int i = 0; i < tooltips.Count; i++)
                {
                    TooltipLine line = tooltips[i];
                    if (line.Text.Contains("ranged"))
                    {
                        line.Text = line.Text.Replace("ranged", "thrower");
                    }
                }
            }
        }
    }

    public class TheBatterRecipePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            foreach (var recipe in Main.recipe)
            {
                if (recipe.createItem.type == ModContent.ItemType<TheBatter>())
                {
                    int YharonSoulFragmentType = ModContent.ItemType<YharonSoulFragment>();
                    recipe.AddIngredient(YharonSoulFragmentType, 10);
                    recipe.requiredTile.Clear();
                    recipe.AddTile(ModContent.TileType<CosmicAnvil>());
                }
            }
        }
    }
}