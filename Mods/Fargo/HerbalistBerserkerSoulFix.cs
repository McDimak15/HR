using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;

namespace HomewardRagnarok
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class HerbalistSoulGlobal : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod)) return;

            int berserkersSoulType = fargoMod.Find<ModItem>("BerserkerSoul")?.Type ?? 0;
            int universeSoulType = fargoMod.Find<ModItem>("UniverseSoul")?.Type ?? 0;
            int eternitySoulType = fargoMod.Find<ModItem>("EternitySoul")?.Type ?? 0;

            if (berserkersSoulType == 0 && universeSoulType == 0 && eternitySoulType == 0) return;

            int herbalistPouchType = ModContent.ItemType<Items.Accessories.HerbalistPouch>();

            if (item.type == berserkersSoulType || item.type == universeSoulType || item.type == eternitySoulType)
            {
                Item herbalistPouch = new Item();
                herbalistPouch.SetDefaults(herbalistPouchType);
                herbalistPouch.ModItem?.UpdateAccessory(player, hideVisual);
            }
        }
    }

    public class HerbalistBerserkersSoulWorldPatch : ModSystem
    {
        public override void OnWorldLoad()
        {
            if (!ModLoader.TryGetMod("FargowiltasSouls", out Mod fargoMod)) return;

            int berserkersSoulType = fargoMod.Find<ModItem>("BerserkerSoul")?.Type ?? 0;
            if (berserkersSoulType == 0) return;

            int herbalistPouchType = ModContent.ItemType<Items.Accessories.HerbalistPouch>();

            foreach (var recipe in Main.recipe.Where(r => r != null && r.createItem.type == berserkersSoulType))
            {
                if (!recipe.requiredItem.Any(i => i != null && i.type == herbalistPouchType))
                    recipe.AddIngredient(herbalistPouchType, 1);
            }
        }
    }
}