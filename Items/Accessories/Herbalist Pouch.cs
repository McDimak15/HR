using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Items.Accessories
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class HerbalistPouch : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.buyPrice(gold: 5);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "HerbalistPouch1", "While equipped, your yoyo damage inflicts frostburn and venom and has a chance to freeze enemies\nDamaging an enemy with a yoyo will grant you 10% damage reduction until you take damage\nThis effect must recharge for 15 seconds after protecting you"));
            tooltips.Add(new TooltipLine(Mod, "HerbalistPouch2", "Hitting enemies with yoyos charge a leaf shield\nThe shield is fully charged in 10 hits\nThe shield decreases up to 21% of the next damage you take\nThe shield is launched when retrieving the yoyo"));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
            {
                ModItem blizzardPouch = thorium.Find<ModItem>("BlizzardPouch");
                blizzardPouch?.UpdateAccessory(player, hideVisual);
            }

            Mod coj = ModLoader.GetMod("ContinentOfJourney");
            if (coj != null)
            {
                ModItem leafOil = coj.Find<ModItem>("LeafOil");
                leafOil?.UpdateAccessory(player, hideVisual);
            }

            player.GetModPlayer<HerbalistPouchPlayer>().herbalistAcidVenom = true;
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            Mod thorium = ModLoader.GetMod("ThoriumMod");
            if (thorium != null)
                recipe.AddIngredient(thorium.Find<ModItem>("BlizzardPouch").Type);

            Mod coj = ModLoader.GetMod("ContinentOfJourney");
            if (coj != null)
                recipe.AddIngredient(coj.Find<ModItem>("LeafOil").Type);

            Mod calamity = ModLoader.GetMod("CalamityMod");
            if (calamity != null)
                recipe.AddIngredient(calamity.Find<ModItem>("LivingShard").Type, 5);

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
