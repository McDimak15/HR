using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using HomewardRagnarok.Config;

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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "YggdrasilTreeTooltip", "Summons The Lifebringer\nCan be used in any biome"));
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
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) ||
                !ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                return;

            int essenceNothingnessType = coj.Find<ModItem>("EssenceofNothingness")?.Type ?? -1;
            int cosmiliteBarType = calamity.Find<ModItem>("CosmiliteBar")?.Type ?? -1;

            if (essenceNothingnessType != -1 && cosmiliteBarType != -1)
            {
                Recipe recipe = Recipe.Create(ModContent.ItemType<YggdrasilTree>());
                recipe.AddIngredient(essenceNothingnessType, 5);
                recipe.AddIngredient(cosmiliteBarType, 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
        }
    }
}
