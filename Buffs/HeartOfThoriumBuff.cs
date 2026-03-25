using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace HomewardRagnarok.Buffs
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class HeartOfThoriumBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.bardResourceMax2 += 5;
        }
    }
}