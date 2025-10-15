using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System.Collections.Generic;

namespace HomewardRagnarok.Items.Summons
{
    public class EternalClock : ModItem
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
            tooltips.Add(new TooltipLine(Mod, "EternalClockTooltip", "Summons The Overwatcher\nCan be used in any biome"));
        }

        public override bool CanUseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int overwatcherType = coj.Find<ModNPC>("TheOverwatcher")?.Type ?? -1;
            if (overwatcherType == -1)
                return false;

            return !NPC.AnyNPCs(overwatcherType);
        }

        public override bool? UseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int overwatcherType = coj.Find<ModNPC>("TheOverwatcher")?.Type ?? -1;
            if (overwatcherType == -1)
                return false;

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // singleplayer
                NPC.SpawnOnPlayer(player.whoAmI, overwatcherType);
            }
            else
            {
                // multiplayer
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, overwatcherType);
            }

            return true;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                int solarFlareScoria = coj.Find<ModItem>("SolarFlareScoria")?.Type ?? -1;
                if (solarFlareScoria != -1)
                {
                    CreateRecipe()
                        .AddIngredient(solarFlareScoria, 10)
                        .AddTile(TileID.LunarCraftingStation)
                        .Register();
                }
            }
        }
    }
}
