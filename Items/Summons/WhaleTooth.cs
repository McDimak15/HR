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
    public class WhaleTooth : ModItem
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

            int whaleType = coj.Find<ModNPC>("WorldsEndEverlastingFallingWhale")?.Type ?? -1;
            if (whaleType == -1)
                return false;

            return !NPC.AnyNPCs(whaleType);
        }

        public override bool? UseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int whaleType = coj.Find<ModNPC>("WorldsEndEverlastingFallingWhale")?.Type ?? -1;
            if (whaleType == -1)
                return false;

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, whaleType);
            }
            else
            {
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, whaleType);
            }

            return true;
        }
    }

    public class WhaleToothRecipeSystem : ModSystem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity) || !ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return;
            if (!calamity.TryFind<ModTile>("CosmicAnvil", out ModTile cosmicAnvil))
                return;

            int whaleToothType = ModContent.ItemType<WhaleTooth>();

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                if (thorium.TryFind("OceanEssence", out ModItem ocean) &&
                    thorium.TryFind("InfernoEssence", out ModItem inferno) &&
                    thorium.TryFind("DeathEssence", out ModItem death))
                {
                    Recipe recipe = Recipe.Create(whaleToothType);
                    recipe.AddIngredient(ocean.Type, 5);
                    recipe.AddIngredient(inferno.Type, 5);
                    recipe.AddIngredient(death.Type, 5);
                    recipe.AddTile(cosmicAnvil.Type);
                    recipe.Register();
                }
            }
            else if (coj.TryFind("TankOfThePastCorruption", out ModItem tank))
            {
                Recipe recipe = Recipe.Create(whaleToothType);
                recipe.AddIngredient(tank.Type, 10);
                recipe.AddTile(cosmicAnvil.Type); // Cosmic Anvil is required
                recipe.Register();
            }
        }
    }
}
