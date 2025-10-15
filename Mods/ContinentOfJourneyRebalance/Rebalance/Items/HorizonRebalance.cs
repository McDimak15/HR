using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace HomewardRagnarok
{
    public class HorizonBootsPlayer : ModPlayer
    {
        private bool hasHorizonBoots;
        private bool fargoLoaded;

        public override void OnEnterWorld()
        {
            fargoLoaded = ModLoader.HasMod("FargowiltasSouls");
        }

        public override void ResetEffects()
        {
            hasHorizonBoots = false;
        }

        public override void PostUpdateEquips()
        {
            int horizonBootsType = ModContent.ItemType<ContinentOfJourney.Items.Accessories.Horizon>();

            for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
            {
                Item item = Player.armor[i];
                if (item != null && !item.IsAir && item.type == horizonBootsType)
                {
                    hasHorizonBoots = true;
                    break;
                }
            }

            if (hasHorizonBoots)
                Player.accRunSpeed = 6f;
                Player.maxRunSpeed = 6f;
                Player.rocketBoots = Player.vanityRocketBoots = 4;
                Player.moveSpeed += 0.08f;
                Player.iceSkate = true;
                Player.waterWalk = true;
                Player.fireWalk = true;
                Player.lavaMax += 420;
                Player.lavaRose = true;
                Player.desertBoots = true;
                Player.jumpBoost = true;
                Player.noFallDmg = true;

                if (fargoLoaded)
                {
                    var fargo = ModLoader.GetMod("FargowiltasSouls");
                    if (fargo != null)
                    {
                        fargo.Call("AddEffect", Player, "MasoAeolusFlower");
                        fargo.Call("AddEffect", Player, "MasoAeolusFrog");
                    }
                }
        }

    }
}
