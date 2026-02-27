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
    public class ScarabIdol : ModItem
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
            tooltips.Add(new TooltipLine(Mod, "ScarabIdolTooltip", "Summons Scarab Belief\nCan be used in any biome"));
        }

        public override bool CanUseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int scarabType = coj.Find<ModNPC>("ScarabBelief")?.Type ?? -1;
            if (scarabType == -1)
                return false;

            return !NPC.AnyNPCs(scarabType);
        }

        public override bool? UseItem(Player player)
        {
            if (!ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
                return false;

            int scarabType = coj.Find<ModNPC>("ScarabBelief")?.Type ?? -1;
            if (scarabType == -1)
                return false;

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, scarabType);
            }
            else
            {
                NetMessage.SendData(61, -1, -1, null, player.whoAmI, scarabType, 0f, 0f, 0, 0, 0);
            }

            return true;
        }

        public override void AddRecipes()
        {
            if (ModLoader.TryGetMod("ContinentOfJourney", out Mod coj))
            {
                int tankHallowType = coj.Find<ModItem>("TankOfThePastHallow")?.Type ?? -1;
                if (tankHallowType != -1)
                {
                    CreateRecipe()
                        .AddIngredient(tankHallowType, 10)
                        .AddTile(TileID.LunarCraftingStation)
                        .Register();
                }
            }
        }
    }
}
