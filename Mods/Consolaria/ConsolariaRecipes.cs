using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Items.Accessories;
using Consolaria.Content.Items.Materials;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Mods.Consolaria
{
    [JITWhenModsEnabled("Consolaria")]
    [ExtendsFromMod("Consolaria")]
    public class ConsolariaRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            if (!ServerConfig.Instance.ConsolariaBalance) return;

            Recipe.Create(ModContent.Find<ModItem>("Consolaria", "SoulofBlight").Type, 3)
                .AddIngredient(ModContent.Find<ModItem>("Consolaria", "SoulofBlight").Type)
                .AddIngredient(ModContent.ItemType<EssenceofDarkness>(), 3)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}