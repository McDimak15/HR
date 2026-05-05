using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ContinentOfJourney.Items.Accessories.PermanentUpgradesSystem;
using HomewardRagnarok.Items.Accessories;
using HomewardRagnarok.Buffs;
using HomewardRagnarok.Config;

namespace HomewardRagnarok
{
    public class HomeRagPlayer : ModPlayer
    {
        public bool hasRiftGenerator;
        public bool herbalistAcidVenom;
        public bool equippedColdBlood;
        public bool equippedWhaleBoneCharm;
        public bool equippedImprovedColdBlood;
        public bool equippedImprovedWhaleBone;
        public float whaleBoneHealPool = 0;
        public int devourDelay = 0;
        private int debuffTickCounter;
        private int improvedDebuffTickCounter;

        public override void ResetEffects()
        {
            hasRiftGenerator = false;
            herbalistAcidVenom = false;
            equippedColdBlood = false;
            equippedWhaleBoneCharm = false;
            equippedImprovedColdBlood = false;
            equippedImprovedWhaleBone = false;
        }

        public override void PostUpdateEquips()
        {
            if (devourDelay > 0) devourDelay--;

            if (equippedImprovedWhaleBone)
            {
                Player.endurance += 0.30f;
            }
            else if (equippedWhaleBoneCharm)
            {
                Player.endurance += 0.20f;
            }

            if (Player.TryGetModPlayer(out PermanentUpgradesPlayer permPlayer) && ServerConfig.Instance.PermanentToAccessories == true)
            {
                permPlayer.PermanentUpgradesActivated[0] = false;
                permPlayer.PermanentUpgradesActivated[1] = false;
            }

            if (hasRiftGenerator && Player.whoAmI == Main.myPlayer)
            {
                int projType = ModContent.ProjectileType<DevourScarf>();
                if (Player.ownedProjectileCounts[projType] <= 0)
                {
                    Projectile.NewProjectile(Player.GetSource_Accessory(Player.HeldItem), Player.Center, Vector2.Zero, projType, 250, 2f, Player.whoAmI);
                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (equippedImprovedWhaleBone)
            {
                if (info.Damage > Player.statLifeMax2 / 4)
                {
                    whaleBoneHealPool += info.Damage * (1f / 3f);
                    Player.AddBuff(ModContent.BuffType<WhaleBoneRecovery>(), 300);
                }
            }
            else if (equippedWhaleBoneCharm)
            {
                if (info.Damage > Player.statLifeMax2 / 4)
                {
                    whaleBoneHealPool += info.Damage * 0.25f;
                    Player.AddBuff(ModContent.BuffType<WhaleBoneRecovery>(), 300);
                }
            }
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff<WhaleBoneRecovery>() && whaleBoneHealPool > 0)
            {
                float healSpeed = equippedImprovedWhaleBone ? 0.15f : 0.10f;

                if (whaleBoneHealPool < healSpeed) healSpeed = whaleBoneHealPool;

                Player.lifeRegen += (int)(healSpeed * 120);
                whaleBoneHealPool -= healSpeed;
            }
            if (!Player.HasBuff<WhaleBoneRecovery>())
            {
                whaleBoneHealPool = 0;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (equippedImprovedColdBlood && Player.lifeRegen < 0)
            {
                Player.lifeRegen = (int)(Player.lifeRegen * 0.60f);
            }
            else if (equippedColdBlood && Player.lifeRegen < 0)
            {
                Player.lifeRegen = (int)(Player.lifeRegen * 0.75f);
            }
        }

        public override void PostUpdateBuffs()
        {
            if (equippedColdBlood && !equippedImprovedColdBlood)
            {
                debuffTickCounter++;
                if (debuffTickCounter >= 3)
                {
                    debuffTickCounter = 0;
                    for (int i = 0; i < Player.MaxBuffs; i++)
                    {
                        if (Player.buffType[i] > 0 && Player.buffTime[i] > 0 && Main.debuff[Player.buffType[i]])
                        {
                            Player.buffTime[i]--;
                        }
                    }
                }
            }

            if (equippedImprovedColdBlood)
            {
                // add 2 ticks every frame, when ticks more than 3 remove debuff time
                improvedDebuffTickCounter += 2;
                int extraTicksToRemove = 0;

                while (improvedDebuffTickCounter >= 3)
                {
                    improvedDebuffTickCounter -= 3;
                    extraTicksToRemove++;
                }

                if (extraTicksToRemove > 0)
                {
                    for (int i = 0; i < Player.MaxBuffs; i++)
                    {
                        if (Player.buffType[i] > 0 && Player.buffTime[i] > 0 && Main.debuff[Player.buffType[i]])
                        {
                            Player.buffTime[i] -= extraTicksToRemove;
                            if (Player.buffTime[i] < 0) Player.buffTime[i] = 0;
                        }
                    }
                }
            }
        }
    }
}