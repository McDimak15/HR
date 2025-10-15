using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;

namespace HomewardRagnarok
{
    public class MapleMushroomEdits : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                coj.TryFind("MapleMushroom", out ModItem mapleMushroom) &&
                item.type == mapleMushroom.Type)
            {
                foreach (var line in tooltips)
                {
                    if (line.Text.Contains("Increase healing amount by 1"))
                    {
                        line.Text = "Increase healing amount by 2";
                    }
                }
            }
        }
    }

    public class MapleMushroomRecipe : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj)) return;
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) return;
            if (!ModLoader.TryGetMod("ThoriumMod", out Mod thorium)) return;

            if (!coj.TryFind("MapleMushroom", out ModItem mapleMushroom)) return;
            if (!coj.TryFind("HoneyMushroom", out ModItem honeyMushroom)) return;
            if (!calamity.TryFind("MurkyPaste", out ModItem murkyPaste)) return;
            if (!thorium.TryFind("LivingLeaf", out ModItem livingLeaf)) return;

            Recipe recipe = Recipe.Create(mapleMushroom.Type);
            recipe.AddIngredient(honeyMushroom.Type, 1);
            recipe.AddIngredient(murkyPaste.Type, 3);
            recipe.AddIngredient(livingLeaf.Type, 5);
            recipe.AddTile(TileID.Bottles); 
            recipe.Register();
        }
    }
}
