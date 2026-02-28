using Terraria;
using Terraria.ModLoader;
using System;
using ContinentOfJourney;

namespace HomewardRagnarok.ContinentOfJourneyRebalance
{
    public class CoJBalancePatch : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            TemplatePlayer cojPlayer = Player.GetModPlayer<TemplatePlayer>();
            if (cojPlayer.BackpackEquipped == 9)
            {
                float damageMult = Math.Max(Player.numMinions + 2f, 2f);
                damageMult = (float)Math.Log2(damageMult) * 0.1f;
                damageMult = Math.Max(0.1f, damageMult);
                damageMult += (float)(Player.GetDamage(DamageClass.Summon).ApplyTo(1f) - 1f);

                Player.GetDamage(DamageClass.Melee) /= damageMult;
                Player.GetDamage(DamageClass.Ranged) /= damageMult;
                Player.GetDamage(DamageClass.Magic) /= damageMult;

                Player.GetDamage(DamageClass.Generic) *= damageMult;
                Player.GetDamage(DamageClass.Summon) /= damageMult;
            }
        }
    }
}