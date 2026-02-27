using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.ThrownItems;
using ContinentOfJourney.Items.Accessories;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Items.Materials;
using ContinentOfJourney;
using Microsoft.Xna.Framework;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Items.Accessories
{
    [ExtendsFromMod("ThoriumMod", "CalamityMod")]
    public class TheBibleOfTheThrowerVol4 : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void SetStaticDefaults()
        {
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            TooltipLine exhaustion = new TooltipLine(Mod, "BibleExhaustion","Removes all Exhaustion when equipped");
            exhaustion.OverrideColor = new Color(95, 193, 4); 
            tooltips.Add(exhaustion);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                player.GetModPlayer<ThoriumMod.ThoriumPlayer>().throwGuide3 = true;
            }

            player.GetDamage<ThrowingDamageClass>() += 0.2f;
 
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                TemplatePlayer cojPlayer = player.GetModPlayer<TemplatePlayer>();
                cojPlayer.BattersCap = true;
                cojPlayer.CatchersGlove = true;
                cojPlayer.TheBatter = true;
                cojPlayer.RunnersLegging = true;
                cojPlayer.GrandSlam = true;
                cojPlayer.HolyTrinity = true;
                cojPlayer.AlphaTrinity = true;
                cojPlayer.OmegaTrinity = true;
                cojPlayer.EpsilonTrinity = true;
            }
        }

        public override void AddRecipes()
        {
            if (ModLoader.HasMod("ThoriumMod") && ModLoader.HasMod("ContinentOfJourney") && ModLoader.HasMod("CalamityMod"))
            {
                CreateRecipe()
                    .AddIngredient(ModContent.ItemType<ThrowingGuideVolume3>())
                    .AddIngredient(ModContent.ItemType<TheBatter>())
                    .AddIngredient(ModContent.ItemType<ShadowspecBar>(), 5)
                    .AddIngredient(ModContent.ItemType<ContinentOfJourney.Items.Material.EssenceofBright>(), 5)
                    .AddTile(ModContent.TileType<DraedonsForge>())
                    .Register();
            }

        }
    }
}