using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Materials; 

namespace HomewardRagnarok.Recipes
{
    [ExtendsFromMod("ContinentOfJourney", "CalamityMod")]
    public class CoJRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Mod coj = ModLoader.GetMod("ContinentOfJourney");

            if (coj == null) return;

            if (coj.TryFind("Alpha", out ModItem alpha))
            {
                Recipe r = Recipe.Create(alpha.Type)
                    .AddIngredient(ItemID.JungleSpores, 6)
                    .AddIngredient(ModContent.ItemType<MurkyPaste>(), 4)
                    .AddIngredient(ItemID.Vine, 2)
                    .AddTile(TileID.Anvils);
                r.Register();
            }

            if (coj.TryFind("Omega", out ModItem omega))
            {
                Recipe r = Recipe.Create(omega.Type)
                    .AddIngredient(ModContent.ItemType<EssenceofEleum>(), 5)
                    .AddIngredient(ModContent.ItemType<CryonicBar>(), 7)
                    .AddTile(TileID.MythrilAnvil);
                r.Register();
            }

            if (coj.TryFind("Epsilon", out ModItem epsilon))
            {
                int craftingTile = TileID.MythrilAnvil;
                if (ModLoader.HasMod("ThoriumMod"))
                {
                    craftingTile = ModLoader.GetMod("ThoriumMod").Find<ModTile>("SoulForge").Type;
                }

                Recipe r = Recipe.Create(epsilon.Type)
                    .AddIngredient(ModContent.ItemType<LifeAlloy>(), 3);

                if (ModLoader.HasMod("ThoriumMod"))
                {
                    r.AddIngredient(ModLoader.GetMod("ThoriumMod").Find<ModItem>("DreadSoul").Type, 5);
                }

                r.AddTile(craftingTile);
                r.Register();
            }
        }

        public override void PostAddRecipes()
        {
            Mod coj = ModLoader.GetMod("ContinentOfJourney");
            if (coj == null) return;

            if (coj.TryFind("TheHolyTrinity", out ModItem trinity))
            {
                foreach (Recipe recipe in Main.recipe)
                {
                    if (recipe.HasResult(trinity.Type))
                    {
                        for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                        {
                            if (recipe.requiredItem[i].type == ItemID.Ectoplasm)
                                recipe.requiredItem.RemoveAt(i);
                        }

                        recipe.AddIngredient(ModContent.ItemType<Necroplasm>(), 7);
                    }
                }
            }
        }
    }
}
