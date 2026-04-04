using Terraria;
using Terraria.ModLoader;
using HomewardRagnarok.Config;

namespace HomewardRagnarok.CrossMod
{
    public class SOTSGlobalItem : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.ModItem == null || item.ModItem.Mod.Name != "SOTS") return;

            string name = item.ModItem.Name;

            if (name == "BulwarkOfTheAncients")
            {
                player.buffImmune[24] = true;
                player.buffImmune[323] = true;
            }
        }
    }
}