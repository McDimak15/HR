using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.Items.Summons
{
    public class BrokenChest : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.Instance.CustomContent;
        }

        private const int FrameCount = 7;
        private int frameHeight;

        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(10, FrameCount)); 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 74; 
            Item.height = 78; 
            frameHeight = 546 / FrameCount;

            Item.maxStack = 20;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.consumable = false;
            Item.noMelee = true;

            Item.scale = 0.5f;
        }

        public override void HoldItem(Player player)
        {
            Item.scale = 0.5f; 
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "BrokenChestTooltip", "Summons The Materealizer\nCan be used in any biome"));
        }

        public override bool CanUseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int bossType = coj.Find<ModNPC>("TheMaterealizer")?.Type ?? -1;
            if (bossType == -1)
                return false;

            return !NPC.AnyNPCs(bossType);
        }

        public override bool? UseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int bossType = coj.Find<ModNPC>("TheMaterealizer")?.Type ?? -1;
            if (bossType == -1)
                return false;

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, bossType);
            }
            else 
            {
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, (float)bossType, 0f, 0f, 0, 0, 0);
            }

            return true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
            Rectangle frame = new Rectangle(0, frameHeight * Main.itemFrame[Item.type], 74, frameHeight);
            Vector2 position = Item.Center - Main.screenPosition;
            spriteBatch.Draw(texture, position, frame, lightColor, rotation, frame.Size() / 2, 0.5f, SpriteEffects.None, 0f);
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj) &&
                ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                int essenceDeathType = coj.Find<ModItem>("EssenceofDeath")?.Type ?? -1;
                int yharonSoulType = calamity.Find<ModItem>("YharonSoulFragment")?.Type ?? -1;

                if (essenceDeathType != -1 && yharonSoulType != -1)
                {
                    Recipe recipe = CreateRecipe();
                    recipe.AddIngredient(essenceDeathType, 5);
                    recipe.AddIngredient(yharonSoulType, 5);
                    recipe.AddTile(TileID.LunarCraftingStation);
                    recipe.Register();
                }
            }
        }
    }
}
