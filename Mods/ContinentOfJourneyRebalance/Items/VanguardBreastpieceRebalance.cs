using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System.Linq;
using ContinentOfJourney;

namespace HomewardRagnarok
{
    public class VanguardBreastpieceFix : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.ModItem?.Name == "VanguardBreastpiece")
            {
                item.defense = 8;
            }
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem?.Name != "VanguardBreastpiece") return;

            player.GetModPlayer<TemplatePlayer>().TransactionCertificate = false;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem?.Name != "VanguardBreastpiece") return;

            tooltips.RemoveAll(t => t.Text.Contains("Keep your hp at 1 upon receiving a fatal hit"));
        }
    }

    public class VanguardBreastpieceRecipeFix : ModSystem
    {
        public override void PostAddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod cojMod)) return;

            int vanguardType = cojMod.Find<ModItem>("VanguardBreastpiece")?.Type ?? -1;
            int tankJungleType = cojMod.Find<ModItem>("TankOfThePastJungle")?.Type ?? -1;
            int ankhAmuletType = cojMod.Find<ModItem>("AnkhAmulet")?.Type ?? -1;
            int transCertType = cojMod.Find<ModItem>("TransactionCertificate")?.Type ?? -1;

            if (vanguardType == -1) return;

            foreach (var recipe in Main.recipe)
            {
                if (recipe == null) continue;

                if (recipe.createItem.type != vanguardType) continue;

                if (ankhAmuletType != -1 &&
                    recipe.requiredItem.Any(i => i.type == ankhAmuletType))
                {
                    recipe.DisableRecipe();
                    continue;
                }

                if (tankJungleType != -1)
                {
                    for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                        if (recipe.requiredItem[i].type == tankJungleType)
                            recipe.requiredItem.RemoveAt(i);
                }

                if (transCertType != -1)
                {
                    for (int i = recipe.requiredItem.Count - 1; i >= 0; i--)
                        if (recipe.requiredItem[i].type == transCertType)
                            recipe.requiredItem.RemoveAt(i);
                }

                recipe.requiredTile.Clear();
                recipe.AddTile(TileID.MythrilAnvil);
            }
        }
    }
}
