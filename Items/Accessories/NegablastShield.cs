using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HomewardRagnarok.Config;
using ThoriumMod.Items;

namespace HomewardRagnarok.Items.Accessories
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class NegablastShield : ThoriumItem
    {
        public override bool IsLoadingEnabled(Mod mod) => ServerConfig.Instance.CustomContent;

        public override string Texture => "HomewardRagnarok/Items/Accessories/NegablastShield";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.accessory = true;
            Item.defense = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            this.accessoryType = AccessoryType.OmniShield;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            var shieldTag = new TooltipLine(Mod, "OmniShieldTag", Terraria.Localization.Language.GetTextValue("Mods.HomewardRagnarok.ItemTooltips.OmniShield"))
            {
                OverrideColor = new Color(102, 255, 255)
            };
            int index = tooltips.FindIndex(tt => tt.Name == "ItemName");
            if (index != -1) tooltips.Insert(index + 1, shieldTag);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                thorium.Find<ModItem>("BlastShield")?.UpdateAccessory(player, hideVisual);
            }

            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                coj.Find<ModItem>("Negashield")?.UpdateAccessory(player, hideVisual);
            }
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj) &&
                ModLoader.TryGetMod("ThoriumMod", out var thorium))
            {
                int negashield = coj.Find<ModItem>("Negashield")?.Type ?? -1;
                int deepBar = coj.Find<ModItem>("DeepBar")?.Type ?? -1;
                int blastShield = thorium.Find<ModItem>("BlastShield")?.Type ?? -1;

                if (negashield != -1 && deepBar != -1 && blastShield != -1)
                {
                    CreateRecipe()
                        .AddIngredient(negashield)
                        .AddIngredient(blastShield)
                        .AddIngredient(deepBar, 5)
                        .AddTile(TileID.MythrilAnvil)
                        .Register();
                }
            }
        }
    }
}