using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using HomewardRagnarok.Config;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Tiles;

namespace HomewardRagnarok.Items.Summons
{
    public class YggdrasilTree : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.consumable = false;
            Item.noMelee = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int bossType = coj.Find<ModNPC>("TheLifebringerHead")?.Type ?? -1;
            if (bossType == -1) return false;

            return !NPC.AnyNPCs(bossType);
        }

        public override bool? UseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int bossType = coj.Find<ModNPC>("TheLifebringerHead")?.Type ?? -1;
            if (bossType == -1) return false;

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, bossType);
            }
            else
            {
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, bossType);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LivingBar>(), 3)
                .AddIngredient(ModContent.ItemType<EssenceofLife>(), 2)
                .AddTile(ModContent.TileType<FountainofLife>())
                .Register();
        }
    }
}
