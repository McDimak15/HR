using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ContinentOfJourney.NPCs.Boss_GoblinChariot;

namespace HomewardRagnarok.Mods.ContinentOfJourneyRebalance.NPCs
{
    public class GoblinChariotRework : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (npc.type == ModContent.NPCType<GoblinChariot>())
            {
                if (npc.target < 0 || npc.target == 255) npc.TargetClosest(true);
                Player player = Main.player[npc.target];

                if (npc.ai[0] == 6)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        switch (npc.ai[3])
                        {
                            case 0: npc.ai[0] = 7; break;
                            case 1: npc.ai[0] = Main.rand.NextBool() ? 10 : 13; break;
                            case 2: npc.ai[0] = 7; break;
                            case 3: npc.ai[0] = Main.rand.NextBool() ? 9 : 13; break;
                            case 4: npc.ai[0] = 7; break;
                            case 5: npc.ai[0] = Main.rand.NextBool() ? 9 : 10; break;
                        }
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3]++;
                        if (npc.ai[3] > 5) npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    return false;
                }

                // case 13 stomp
                if (npc.ai[0] == 13)
                {
                    npc.ai[1]++;

                    Vector2 prediction = player.velocity * 20f;
                    Vector2 targetPos = player.Center + prediction - Vector2.UnitY * 400f;

                    // Move Up 
                    if (npc.ai[2] == 0)
                    {
                        float dist = Vector2.Distance(npc.Center, targetPos);
                        Vector2 moveVector = targetPos - npc.Center;

                        if (dist > 0)
                        {
                            moveVector.Normalize();
                            float speed = Math.Min(dist / 8f, 28f); 
                            npc.velocity = Vector2.Lerp(npc.velocity, moveVector * speed, 0.2f);
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            bool closeEnoughX = Math.Abs(npc.Center.X - targetPos.X) < 80f;
                            bool reachedHeight = npc.Center.Y < player.Center.Y - 300f;

                            if ((closeEnoughX && reachedHeight) || npc.ai[1] > 60)
                            {
                                npc.ai[2] = 1;
                                npc.ai[1] = 0;
                                npc.netUpdate = true;
                            }
                        }
                    }
                    // Top Delay 
                    else if (npc.ai[2] == 1)
                    {
                        float driftX = (targetPos.X - npc.Center.X) * 0.08f;
                        npc.velocity.X = MathHelper.Lerp(npc.velocity.X, driftX, 0.1f);
                        npc.velocity.Y = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 8f) * 1.2f;

                        if (npc.ai[1] > 20 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            npc.ai[2] = 2;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                        }

                        if (npc.ai[1] == 20) SoundEngine.PlaySound(SoundID.NPCHit53, npc.Center);
                    }
                    // Slam
                    else if (npc.ai[2] == 2)
                    {
                        npc.velocity.X *= 0.95f; 
                        npc.velocity.Y += 1.2f;  
                        if (npc.velocity.Y > 32f) npc.velocity.Y = 32f;

                        if (Main.rand.NextBool(2))
                        {
                            Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Smoke, 0, -2f, 100, default, 1.5f).noGravity = true;
                        }

                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            if (npc.Center.Y > player.Center.Y + 160f || npc.ai[1] > 80)
                            {
                                npc.ai[2] = 3;
                                npc.ai[1] = 0;
                                npc.netUpdate = true;
                            }
                        }

                        if (npc.ai[2] == 3 && npc.ai[1] == 0)
                        {
                            OnLanding(npc);
                        }
                    }
                    // Bottom Delay
                    else if (npc.ai[2] == 3)
                    {
                        npc.velocity *= 0.05f;
                        if (npc.ai[1] > 12 && Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            npc.ai[2] = 4;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                        }
                    }
                    // Return
                    else if (npc.ai[2] == 4)
                    {
                        float hoverHeight = player.Center.Y - 250f;
                        float diff = npc.Center.Y - hoverHeight;

                        if (diff > 0)
                        {
                            float returnSpeed = MathHelper.Clamp(diff / 30f, 8f, 26f);
                            npc.velocity.Y = MathHelper.Lerp(npc.velocity.Y, -returnSpeed, 0.25f);
                        }
                        else if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            npc.velocity *= 0.7f;
                            if (npc.ai[1] > 10)
                            {
                                npc.ai[0] = 6;
                                npc.ai[1] = 0;
                                npc.ai[2] = 0;
                                npc.netUpdate = true;
                            }
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        private void OnLanding(NPC npc)
        {
            if (Main.netMode != NetmodeID.Server)
            {
                var shakeModifier = new Terraria.Graphics.CameraModifiers.PunchCameraModifier(npc.Center, Vector2.UnitY, 15f, 15f, 20, 1000f, npc.FullName);
                Main.instance.CameraModifiers.Add(shakeModifier);
            }

            SoundEngine.PlaySound(SoundID.Item14, npc.Center);
            for (int i = 0; i < 30; i++)
            {
                Vector2 speed = Main.rand.NextVector2Unit() * Main.rand.NextFloat(5f, 10f);
                Dust d = Dust.NewDustDirect(npc.Center, 0, 0, DustID.Smoke, speed.X, speed.Y, 100, default, 2f);
                d.noGravity = Main.rand.NextBool();
            }
        }
    }
}