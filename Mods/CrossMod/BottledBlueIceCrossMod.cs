using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ContinentOfJourney;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class BottledBlueIceCrossmod : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem == null) return;

            string name = item.ModItem.Name;

            if (name == "BulwarkOfTheAncients" || name == "TerrariumDefender" || name == "RampartofDeities")
            {
                player.buffImmune[24] = true;
                player.buffImmune[323] = true;
            }
        }
    }
}
