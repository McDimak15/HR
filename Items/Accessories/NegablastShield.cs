using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HomewardRagnarok.Items.Accessories
{
    public class NegablastShield : ModItem
    {
        public override string Texture => "HomewardRagnarok/Items/Accessories/NegablastShield";

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "NegablastShield",
                "50 base damage\n" +
                "Slightly increases length of invincibility after taking damage\n" +
                "Increases the knockback you receive\n" +
                "Release an explosions when damaged\n" +
                "Immune to enemy knockback & enemies are more likely to target you\n" +
                "Damage taken reduced by 10%\n" +
                "Every 3 seconds, taking damage will unleash a volatile explosion all around you\n" +
                "Explosion damage scales with your defense"));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.accessory = true;
            Item.defense = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");
                if (thorium != null)
                {
                    ModItem blastShield = thorium.Find<ModItem>("BlastShield");
                    blastShield?.UpdateAccessory(player, hideVisual);
                }

                Mod coj = ModLoader.GetMod("ContinentOfJourney");
                if (coj != null)
                {
                    ModItem negashield = coj.Find<ModItem>("Negashield");
                    negashield?.UpdateAccessory(player, hideVisual);
                }
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
