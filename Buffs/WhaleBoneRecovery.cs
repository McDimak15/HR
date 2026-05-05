using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace HomewardRagnarok.Buffs
{
    public class WhaleBoneRecovery : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void ModifyBuffText(ref string displayName, ref string tooltip, ref int drawOffset)
        {
            Player localPlayer = Main.LocalPlayer;

            if (localPlayer.TryGetModPlayer(out HomeRagPlayer hrPlayer))
            {
                int displayAmount = (int)Math.Ceiling(hrPlayer.whaleBoneHealPool);
                tooltip = Language.GetTextValue("Mods.HomewardRagnarok.Buffs.WhaleBoneRecovery.Description", displayAmount);
            }
        }
    }
}