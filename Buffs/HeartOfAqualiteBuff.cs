using Terraria;
using Terraria.ModLoader;
using ThoriumMod;

namespace HomewardRagnarok.Buffs
{
    [JITWhenModsEnabled("ThoriumMod")]
    [ExtendsFromMod("ThoriumMod")]
    public class HeartOfAqualiteBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.05f;
        }
    }
}