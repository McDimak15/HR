/* using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ContinentOfJourney.Items.Accessories.GrazeBadge;

namespace HomewardRagnarok.Items.Accessories
{
    public class AuricFrame : FrameItems
    {
        public override void init()
        {
            Item.rare = ItemRarityID.Red; 
            Item.value = Item.sellPrice(platinum: 1); 
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out var coj))
            {
                var clashType = coj.Code?.GetType("ContinentOfJourney.Items.Accessories.GrazeBadge.ClashingAccessories");
                var field = clashType?.GetField("SummonerBackpackIDs");

                if (field != null)
                {
                    var clashList = field.GetValue(null) as System.Collections.Generic.HashSet<int>;
                    if (clashList != null && clashList.Contains(incomingItem.type) && incomingItem.type != Type)
                        return false;
                }
            }

            return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
        }

        public override void UpdateEquip(Player player)
        {
            player.maxRunSpeed += 0.25f;
            player.runAcceleration += 0.25f;
            player.wingRunAccelerationMult += 0.25f;

            EquipEffect(player, layerMax: 6, efxLevel: 6, barStyle: 7, deceleration: 4f);

            player.GrazeBadge().SlotLayerMax = 6;
            player.GrazeBadge().EffectLevel = 6;
            player.GrazeBadge().barStyle = 7;
            player.GrazeBadge().deceleration = 4f;

            player.GrazeBadge().EquippingBadgeA ??= new Item();
            player.GrazeBadge().EquippingBadgeB ??= new Item();
        }


        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FrameWooden>())
                .AddIngredient(ModContent.ItemType<FrameIron>())
                .AddIngredient(ItemID.LunarBar, 20)
                .AddIngredient(ItemID.FragmentSolar, 10)
                .AddIngredient(ItemID.FragmentNebula, 10)
                .AddIngredient(ItemID.FragmentVortex, 10)
                .AddIngredient(ItemID.FragmentStardust, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
*/