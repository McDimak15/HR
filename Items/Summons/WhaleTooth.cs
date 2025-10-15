using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;

namespace HomewardRagnarok.Items.Summons
{
    public class WhaleTooth : ModItem
    {
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
            tooltips.Add(new TooltipLine(Mod, "WhaleToothTooltip", "Summons Worlds End Everlasting Falling Whale\nCan be used in any biome"));
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
                // Singleplayer 
                NPC.SpawnOnPlayer(player.whoAmI, whaleType);
            }
            else
            {
                // Multiplayer
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, whaleType);
            }

            return true;
        }
        public override void AddRecipes()
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) ||
                !ModLoader.TryGetMod("CalamityMod", out Mod calamity))
                return;

            int whaleType = ModContent.ItemType<WhaleTooth>();
            int tankCorruption = coj.Find<ModItem>("TankOfThePastCorruption")?.Type ?? -1;
            int cosmicAnvilTile = calamity.Find<ModTile>("CosmicAnvil")?.Type ?? -1;

            if (ModLoader.TryGetMod("ThoriumMod", out Mod thorium))
            {
                int oceanEssence = thorium.Find<ModItem>("OceanEssence")?.Type ?? -1;
                int infernoEssence = thorium.Find<ModItem>("InfernoEssence")?.Type ?? -1;
                int deathEssence = thorium.Find<ModItem>("DeathEssence")?.Type ?? -1;

                if (oceanEssence > 0 && infernoEssence > 0 && deathEssence > 0 && cosmicAnvilTile > 0)
                {
                    Recipe recipe = Recipe.Create(whaleType);
                    recipe.AddIngredient(oceanEssence, 5);
                    recipe.AddIngredient(infernoEssence, 5);
                    recipe.AddIngredient(deathEssence, 5);
                    recipe.AddTile(cosmicAnvilTile);
                    recipe.Register();
                }
            }
            else
            {
                if (tankCorruption > 0 && cosmicAnvilTile > 0)
                {
                    Recipe recipe = Recipe.Create(whaleType);
                    recipe.AddIngredient(tankCorruption, 10);
                    recipe.AddTile(cosmicAnvilTile);
                    recipe.Register();
                }
            }
        }
    }
}
