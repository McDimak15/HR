using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace HomewardRagnarok
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class HomewardRagnarok : Mod
	{
        public override void HandlePacket(System.IO.BinaryReader reader, int whoAmI)
        {
            ModContent.GetInstance<ClamityNetHandler>().HandlePacket(reader, whoAmI);
        }
    }
}
