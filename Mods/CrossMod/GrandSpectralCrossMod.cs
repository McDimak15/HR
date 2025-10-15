using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using ContinentOfJourney;

namespace HomewardRagnarok
{
    public class GrandSpectralGlobal : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (IsGrandSpectralTarget(item) &&
                ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                coj.TryFind("GrandSpectral", out ModItem _))
            {
                player.longInvince = true;

                TemplatePlayer modPlayer = player.GetModPlayer<TemplatePlayer>();
                modPlayer.Spectral = true;

                if (Main.myPlayer == player.whoAmI && modPlayer.SpectralSummon > 0)
                {
                    do
                    {
                        int proj = Projectile.NewProjectile(
                            player.GetSource_Accessory(item),
                            player.Center,
                            new Vector2(12f, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(0, 360))),
                            ModContent.ProjectileType<ContinentOfJourney.Projectiles.GrandSpectral>(),
                            0,
                            0,
                            player.whoAmI
                        );
                        Main.projectile[proj].netUpdate = true;
                        modPlayer.SpectralSummon -= 1;
                    }
                    while (modPlayer.SpectralSummon > 0);
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ShouldShowTooltip(item) &&
                ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                coj.TryFind("GrandSpectral", out ModItem grandSpectral))
            {
                float timer = (float)(Main.GlobalTimeWrappedHourly * 0.3);
                Color purple = new Color(128, 0, 128);
                Color white = Color.White;
                Color animatedColor = Color.Lerp(purple, white, (float)(0.5 * (1 + Math.Sin(timer * MathHelper.TwoPi))));

                string iconTag = $"[i:{grandSpectral.Type}] ";

                tooltips.RemoveAll(t => t.Name.StartsWith("GSS"));

                tooltips.Add(new TooltipLine(Mod, "GSS1", iconTag + "Increases length of invincibility (Homeward Ragnarok)")
                { OverrideColor = animatedColor });

                tooltips.Add(new TooltipLine(Mod, "GSS2", iconTag + "Summon spectrals when damaged (Homeward Ragnarok)")
                { OverrideColor = animatedColor });

                tooltips.Add(new TooltipLine(Mod, "GSS3", iconTag + "Summon spectrals when hitting enemy (Homeward Ragnarok)")
                { OverrideColor = animatedColor });
            }
        }

        private bool IsGrandSpectralTarget(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("RampartofDeities", out ModItem rampart) &&
                item.type == rampart.Type)
                return true;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int colossus = fargo.Find<ModItem>("ColossusSoul")?.Type ?? -1;
                int eternity = fargo.Find<ModItem>("EternitySoul")?.Type ?? -1;
                int dimension = fargo.Find<ModItem>("DimensionSoul")?.Type ?? -1;

                if (item.type == colossus || item.type == eternity || item.type == dimension)
                    return true;
            }

            if (ModLoader.TryGetMod("Clamity", out Mod clamity) &&
                clamity.TryFind("SupremeBarrier", out ModItem barrier) &&
                item.type == barrier.Type)
                return true;

            return false;
        }

        private bool ShouldShowTooltip(Item item)
        {
            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity) &&
                calamity.TryFind("RampartofDeities", out ModItem rampart) &&
                item.type == rampart.Type)
                return true;

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod fargo))
            {
                int colossus = fargo.Find<ModItem>("ColossusSoul")?.Type ?? -1;
                return item.type == colossus;
            }

            return false;
        }
    }
}
