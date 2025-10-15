using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using HomewardRagnarok.Items.Accessories;

namespace HomewardRagnarok
{
    [ExtendsFromMod("ThoriumMod")]
    public class VagabondSoulBiblePatch : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (ModLoader.TryGetMod("FargowiltasCrossmod", out Mod fargo))
            {
                int vagabondSoulType = fargo.Find<ModItem>("VagabondsSoul")?.Type ?? -1;
                if (vagabondSoulType == -1) return;

                int bibleType = ModContent.ItemType<TheBibleOfTheThrowerVol4>();
                int vol3Type = ModLoader.TryGetMod("ThoriumMod", out Mod thorium)
                    ? thorium.Find<ModItem>("ThrowingGuideVolume3")?.Type ?? -1
                    : -1;

                foreach (var recipe in Main.recipe)
                {
                    if (recipe == null || recipe.createItem.type != vagabondSoulType)
                        continue;

                    if (!recipe.requiredItem.Exists(i => i.type == bibleType))
                    {
                        recipe.requiredItem.Add(new Item(bibleType));
                    }

                    if (ModLoader.HasMod("ssm") && vol3Type != -1)
                    {
                        for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                        {
                            if (recipe.requiredItem[i].type == vol3Type)
                                recipe.requiredItem.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
