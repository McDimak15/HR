using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Audio; // Required for SoundEngine
using ContinentOfJourney;
using ContinentOfJourney.Items;
using ContinentOfJourney.Items.Accessories.Bookmarks;
using ContinentOfJourney.Items.Material;
using ContinentOfJourney.Projectiles.Flame;
using ContinentOfJourney.Buffs;
using ContinentOfJourney.Buffs.JavalinBuffs;
using ContinentOfJourney.NPCs; // Required for Javelin_GlobalNPC

namespace HomewardRagnarok
{
    public class TestTubeCrossModFix : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public bool IsFlamethrower = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo ammoSource)
            {
                if (ammoSource.Item.useAmmo == AmmoID.Gel) IsFlamethrower = true;
            }
            else if (source is EntitySource_ItemUse itemSource)
            {
                if (itemSource.Item.useAmmo == AmmoID.Gel) IsFlamethrower = true;
            }
            else if (projectile.aiStyle == 8)
            {
                IsFlamethrower = true;
            }

            if (IsFlamethrower)
            {
                //Main.NewText("DEBUG: Flamethrower Projectile Spawned!"); 
            }

            if (projectile.type == ModContent.ProjectileType<Flame>())
            {
                Player player = Main.player[projectile.owner];
                Item heldItem = player.HeldItem;

                if (heldItem != null && !heldItem.IsAir && IsFlamethrower)
                {
                    if (heldItem.shoot > 0 && heldItem.shoot != projectile.type)
                    {
                        Vector2 oldCenter = projectile.Center;
                        Vector2 oldVelocity = projectile.velocity;
                        int oldDamage = projectile.damage;

                        projectile.type = heldItem.shoot;
                        projectile.SetDefaults(projectile.type);

                        projectile.Center = oldCenter;
                        projectile.velocity = oldVelocity;
                        projectile.damage = oldDamage;
                        projectile.owner = player.whoAmI;
                        projectile.active = true;

                        if (projectile.timeLeft < 2) projectile.timeLeft = 60;
                    }
                }
            }
        }

        private int CountItem(Player player, int targetInt)
        {
            BookmarkPlayer modPlayer = player.GetModPlayer<BookmarkPlayer>();
            if (!modPlayer.isUsingTestTube) return 0;

            int count = 0;
            for (int i = 0; i < modPlayer.bookMarkTier + 1; i++)
            {
                if (modPlayer.testTubeIngredient[i] == targetInt) count++;
            }
            return count;
        }

        private int CountItemArray(Player player, int[] targetInt)
        {
            BookmarkPlayer modPlayer = player.GetModPlayer<BookmarkPlayer>();
            if (!modPlayer.isUsingTestTube) return 0;

            int count = 0;
            for (int i = 0; i < modPlayer.bookMarkTier + 1; i++)
            {
                if (targetInt.Contains(modPlayer.testTubeIngredient[i])) count++;
            }
            return count;
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!IsFlamethrower) return;

            Player player = Main.player[projectile.owner];
            BookmarkPlayer modPlayer = player.GetModPlayer<BookmarkPlayer>();

            if (modPlayer.isUsingTestTube)
            {
                int efx = CountItem(player, ItemID.TatteredCloth);
                if (efx > 0)
                {
                    if (target.type == NPCID.GoblinArcher || target.type == NPCID.GoblinPeon || target.type == NPCID.GoblinScout || target.type == NPCID.GoblinShark || target.type == NPCID.GoblinSorcerer ||
                        target.type == NPCID.GoblinSummoner || target.type == NPCID.GoblinThief || target.type == NPCID.GoblinTinkerer || target.type == NPCID.GoblinWarrior || target.type == NPCID.BoundGoblin ||
                        target.type == NPCID.DD2GoblinBomberT1 || target.type == NPCID.DD2GoblinBomberT2 || target.type == NPCID.DD2GoblinBomberT3 ||
                        target.type == NPCID.DD2GoblinT1 || target.type == NPCID.DD2GoblinT2 || target.type == NPCID.DD2GoblinT3)
                        modifiers.SourceDamage *= 1.12f + (efx - 1) * 0.04f;
                }

                efx = CountItem(player, ItemID.Cactus);
                if (efx > 0) modifiers.Knockback *= 1f + 0.1f * efx;

                if (projectile.wet)
                {
                    efx = CountItemArray(player, new int[] { ItemID.JunoniaShell, ItemID.TulipShell, ItemID.LightningWhelkShell, ItemID.Seashell });
                    if (efx > 0) modifiers.SourceDamage += 0.15f + (efx - 1) * 0.04f;
                }

                efx = CountItem(player, ItemID.AncientBattleArmorMaterial);
                if (efx > 0)
                {
                    float percent = 0.2f * efx;
                    int damageToAdd = 0;
                    if (target.defense < projectile.ArmorPenetration)
                        damageToAdd += (projectile.ArmorPenetration - target.defense) / 2;

                    modifiers.FinalDamage.Flat += damageToAdd * percent;
                }

                if (modPlayer.testTubeIngredient.Contains(ItemID.WhitePearl)) modifiers.DamageVariationScale *= 1.1f;
                if (modPlayer.testTubeIngredient.Contains(ItemID.BlackPearl)) modifiers.DamageVariationScale *= 1.2f;
                if (modPlayer.testTubeIngredient.Contains(ItemID.PinkPearl)) modifiers.DamageVariationScale *= 1.5f;

                if (modPlayer.testTubeIngredient.Contains(ModContent.ItemType<NetherStar>()))
                {
                    bool activate = true;
                    for (int n = 0; n < Main.maxNPCs; n++)
                    {
                        NPC npc = Main.npc[n];
                        if (npc.boss && npc.active)
                        {
                            activate = false;
                            break;
                        }
                    }
                    if (Main.invasionType != InvasionID.None) activate = false;
                    if (activate) modifiers.SourceDamage *= 1.18f;
                }

                efx = CountItem(player, ModContent.ItemType<EssenceofDeath>());
                if (efx > 0)
                {
                    float chance = 0.05f * efx;
                    if (Main.rand.NextFloat() <= chance)
                    {
                        if (!ClashingAccessories.NotNormalNPCIDs.Contains(target.type) && !target.boss)
                        {
                            modifiers.FinalDamage.Base = target.life + target.defense / 2 + 1;
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!IsFlamethrower) return;

            Player player = Main.player[projectile.owner];
            BookmarkPlayer modPlayer = player.GetModPlayer<BookmarkPlayer>();

            if (modPlayer.isUsingTestTube)
            {
                // Re-added the missing knockout detection
                bool knockOut = false;
                if (target.life <= 0 && !target.active) knockOut = true;
                else if (target.realLife >= 0) if (damageDone >= Main.npc[target.realLife].life) knockOut = true;
                if (target.SpawnedFromStatue) knockOut = false;

                int efx = CountItem(player, ItemID.Pumpkin);
                if (efx > 0 && knockOut)
                {
                    int num = 2 + (efx - 1);
                    for (int i = 0; i < num; i++)
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f)), ModContent.ProjectileType<FTPumpkinBomb>(),
                            projectile.damage, 6f, projectile.owner);
                }

                efx = CountItem(player, ItemID.ButterflyDust);
                if (efx > 0 && knockOut && target.type == NPCID.Butterfly)
                {
                    int item = (Main.rand.Next(100) < efx * 15)
                            ? Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ModContent.ItemType<CrispyButterfly>()) : 0;
                    if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }

                efx = CountItem(player, ItemID.TissueSample);
                if (efx > 0 && target.CanBeChasedBy(this))
                {
                    Vector2 v = target.Center - projectile.Center;
                    v = v.SafeNormalize(Vector2.Zero);
                    Vector2 vector = target.Hitbox.ClosestPointInRect(projectile.Center) + v;
                    Vector2 spinningpoint = (target.Center - vector) * 0.8f;
                    spinningpoint = spinningpoint.RotatedBy(Main.rand.NextFloatDirection() * (float)Math.PI * 0.25f);
                    int num = Projectile.NewProjectile(projectile.GetSource_FromThis(), vector.X, vector.Y, spinningpoint.X, spinningpoint.Y, 975, projectile.damage, 3f, projectile.owner, 1f, target.whoAmI);
                    Main.projectile[num].StatusNPC(target.whoAmI);
                    Main.projectile[num].timeLeft = efx * 120;
                    Projectile.KillOldestJavelin(num, 975, target.whoAmI, new Point[5]); // Static method
                }

                efx = CountItem(player, ItemID.IceBlock);
                if (efx > 0) target.AddBuff(BuffID.Frostburn, 360 * efx);

                efx = CountItem(player, ItemID.FrostCore);
                if (efx > 0) target.AddBuff(BuffID.Frostburn2, 360 * efx);

                efx = CountItem(player, ItemID.Nanites);
                if (efx > 0) target.AddBuff(BuffID.Confused, 360 * efx);

                efx = CountItem(player, 3795);//desert spirit lamp
                if (efx > 0) target.AddBuff(BuffID.ShadowFlame, 360 * efx);

                efx = CountItem(player, ItemID.CursedFlame);
                if (efx > 0) target.AddBuff(BuffID.CursedInferno, 360 * efx);

                efx = CountItem(player, ItemID.SharkFin);
                if (efx > 0) target.AddBuff(ModContent.BuffType<DeepWaterBuff>(), 360 * efx);

                efx = CountItem(player, ItemID.Stinger);
                if (efx > 0) target.AddBuff(BuffID.Poisoned, 360 * efx);

                efx = CountItem(player, ItemID.VialofVenom);
                if (efx > 0) target.AddBuff(BuffID.Venom, 360 * efx);

                efx = CountItem(player, ItemID.Ichor);
                if (efx > 0) target.AddBuff(BuffID.Ichor, 360 * efx);

                efx = CountItem(player, ModContent.ItemType<SteelFeather>());
                if (efx > 0) target.AddBuff(ModContent.BuffType<VulnerableBuff>(), 360 * efx);

                efx = CountItem(player, ModContent.ItemType<DivineShard>());
                if (efx > 0) target.AddBuff(ModContent.BuffType<DivineFireBuff>(), 360 * efx);

                efx = CountItem(player, ModContent.ItemType<SpiralTissue>());
                if (efx > 0) target.AddBuff(ModContent.BuffType<PlagueBuff>(), 360 * efx);

                efx = CountItem(player, ModContent.ItemType<SolarFlareScoria>());
                if (efx > 0) target.AddBuff(ModContent.BuffType<SolarBurntBuff>(), 60 * efx);

                efx = CountItem(player, ItemID.ShadowScale);
                if (efx > 0)
                {
                    float damage = projectile.damage * (0.05f + (efx - 1) * 0.02f);
                    if (damage < 1f) damage = 1f;
                    Vector2 spawnPos = (projectile.Center + target.Center) / 2;
                    spawnPos.X = Terraria.Utils.Clamp(spawnPos.X, target.Hitbox.Left, target.Hitbox.Right);
                    spawnPos.Y = Terraria.Utils.Clamp(spawnPos.Y, target.Hitbox.Top, target.Hitbox.Bottom);
                    int proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, new Vector2(1, 0).RotatedBy((projectile.velocity.X < 0 ? -1 : 1) * Math.PI / 2).RotatedByRandom(Math.PI / 5),
                        ProjectileID.LightsBane, (int)damage, 3f, projectile.owner, Main.rand.NextFloat(0.9f, 1.2f));
                    Main.projectile[proj].ArmorPenetration = 100;
                    Main.projectile[proj].netUpdate = true;
                }

                if (modPlayer.testTubeIngredient.Contains(ItemID.UnicornHorn) && modPlayer.testTubeIngredient.Contains(ItemID.PixieDust))
                {
                    SoundEngine.PlaySound(SoundID.Item10, projectile.position);
                    for (int num588 = 0; num588 < 10; num588++)
                    {
                        Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, default(Color), 1.2f);
                    }

                    for (int num589 = 0; num589 < 3; num589++)
                    {
                        Gore.NewGore(projectile.GetSource_FromThis(), projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18));
                    }

                    float x = projectile.position.X + (float)Main.rand.Next(-400, 400);
                    float y = projectile.position.Y - (float)Main.rand.Next(600, 900);
                    Vector2 vector57 = new Vector2(x, y);
                    float num590 = projectile.Center.X - vector57.X;
                    float num591 = projectile.Center.Y - vector57.Y;
                    int num592 = 22;
                    float num593 = (float)Math.Sqrt(num590 * num590 + num591 * num591);
                    num593 = (float)num592 / num593;
                    num590 *= num593;
                    num591 *= num593;
                    int num594 = projectile.damage;

                    int num595 = Projectile.NewProjectile(projectile.GetSource_FromThis(), x, y, num590, num591, 92, num594 / 8, projectile.knockBack, projectile.owner);
                    Main.projectile[num595].ai[1] = projectile.position.Y;
                    Main.projectile[num595].ai[0] = 1f;
                }

                efx = CountItem(player, ItemID.GoldDust);
                if (efx > 0) target.AddBuff(BuffID.Midas, 30 * efx);

                efx = CountItem(player, ItemID.DarkShard);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<PowerDownBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<PowerDownBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<PowerDownBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<PowerDownBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<PowerDownBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.LightShard);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<SpeedDownBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<SpeedDownBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<SpeedDownBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<SpeedDownBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<SpeedDownBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.Cobweb);
                if (efx > 0)
                {
                    float chance = 0.05f + (efx - 1) * 0.05f;
                    if (Main.rand.NextFloat() <= chance) target.AddBuff(ModContent.BuffType<FTWebbedBuff>(), 60);
                }

                efx = CountItem(player, ItemID.BeeWax);
                if (efx > 0)
                {
                    float chance = 0.12f * efx;
                    if (Main.rand.NextFloat() <= chance)
                    {
                        int num114 = Main.rand.Next(1, 4);
                        if (Main.rand.Next(6) == 0) num114++;
                        if (Main.rand.Next(6) == 0) num114++;
                        if (player.strongBees && Main.rand.Next(3) == 0) num114++;

                        for (int num115 = 0; num115 < num114; num115++)
                        {
                            int num118 = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)),
                                player.beeType(), Main.rand.Next(2, 16), player.beeKB(3f), projectile.owner);
                            Main.projectile[num118].usesIDStaticNPCImmunity = true;
                            Main.projectile[num118].idStaticNPCHitCooldown = 8;
                            Main.projectile[num118].usesLocalNPCImmunity = false;
                            Main.projectile[num118].DamageType = DamageClass.Ranged;
                        }
                    }
                }

                efx = CountItem(player, ItemID.SoulofLight);
                if (efx > 0 && knockOut)
                {
                    int item = (Main.rand.Next(100) < efx * 10)
                            ? Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ItemID.SoulofLight) : 0;
                    if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }

                efx = CountItem(player, ItemID.SoulofNight);
                if (efx > 0 && knockOut)
                {
                    int item = (Main.rand.Next(100) < efx * 10)
                            ? Item.NewItem(projectile.GetSource_DropAsItem(), projectile.Hitbox, ItemID.SoulofNight) : 0;
                    if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);

                }

                efx = CountItem(player, ItemID.FragmentSolar);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<FTMeleeVulnerableBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<FTMeleeVulnerableBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<FTMeleeVulnerableBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<FTMeleeVulnerableBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<FTMeleeVulnerableBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.FragmentVortex);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<FTRangedVulnerableBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<FTRangedVulnerableBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<FTRangedVulnerableBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<FTRangedVulnerableBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<FTRangedVulnerableBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.FragmentNebula);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<FTMagicVulnerableBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<FTMagicVulnerableBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<FTMagicVulnerableBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<FTMagicVulnerableBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<FTMagicVulnerableBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.FragmentStardust);
                if (efx > 0)
                {
                    switch (efx)
                    {
                        case 1: target.AddBuff(ModContent.BuffType<FTSummonVulnerableBuff1>(), 540); break;
                        case 2: target.AddBuff(ModContent.BuffType<FTSummonVulnerableBuff2>(), 540); break;
                        case 3: target.AddBuff(ModContent.BuffType<FTSummonVulnerableBuff3>(), 540); break;
                        case 4: target.AddBuff(ModContent.BuffType<FTSummonVulnerableBuff4>(), 540); break;
                        case 5: target.AddBuff(ModContent.BuffType<FTSummonVulnerableBuff5>(), 540); break;
                    }
                }

                efx = CountItem(player, ItemID.Hellstone);
                if (efx > 0)
                {
                    int time = 360 * efx;
                    target.AddBuff(BuffID.OnFire3, time);
                    target.GetGlobalNPC<Javelin_GlobalNPC>().severeDebuff[2] = time;
                }

                efx = CountItem(player, ModContent.ItemType<Blood>());
                if (efx > 0)
                {
                    float chance = efx * 0.05f;
                    if (Main.rand.NextFloat() <= chance)
                        if (target.type != NPCID.TargetDummy && target.lifeMax > 5)
                        {
                            int proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center.X, projectile.Center.Y, 0, 0, ProjectileID.VampireHeal, 0, 0, projectile.owner, projectile.owner, Main.rand.Next(1, 6));
                            Main.projectile[proj].timeLeft = 300;
                        }
                }

                if (modPlayer.testTubeIngredient.Contains(ModContent.ItemType<JungleDewdrop>()))
                {
                    projectile.damage += (int)(projectile.damage * 0.08);
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            if (IsFlamethrower && projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;

                Player player = Main.player[projectile.owner];
                BookmarkPlayer modPlayer = player.GetModPlayer<BookmarkPlayer>();

                if (modPlayer.isUsingTestTube)
                {
                    int efx = CountItem(player, ItemID.JungleSpores);
                    if (efx > 0) projectile.velocity *= (1f + efx * 0.07f);

                    efx = CountItem(player, ItemID.Coral);
                    if (efx > 0) projectile.ignoreWater = true;

                    if (modPlayer.testTubeIngredient.Contains(ItemID.Starfish)) projectile.ignoreWater = true;
                    if (modPlayer.testTubeIngredient.Contains(ItemID.SoulofFlight)) projectile.ignoreWater = true;
                }
            }
        }
    }
}