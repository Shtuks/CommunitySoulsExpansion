using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ssm.Content.NPCs.Guntera
{
    [AutoloadBossHead]
    public class Guntera : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 86;
            NPC.damage = 375;
            NPC.defense = 100;
            NPC.lifeMax = 1500000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            for (int i = 0; i < NPC.buffImmune.Length; i++)
                NPC.buffImmune[i] = true;
            NPC.buffImmune[ModContent.BuffType<Gun>()] = false;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(2);
            NPC.boss = true;
            Music = MusicID.Plantera;
            NPC.netAlways = true;
        }

        public override void AI()
        {
            NPC.timeLeft = 60;

            NPC.TargetClosest(false);

            if (NPC.HasValidTarget && NPC.Distance(Main.player[NPC.target].Center) < 6000)
            {
                NPC.timeLeft = 60;
            }
            else
            {
                NPC.active = false;
                NPC.life = 0;
                NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
            }

            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;

            if (NPC.localAI[1] == 0 && NPC.life < NPC.lifeMax * 0.4f)
            {
                NPC.localAI[1] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < Main.maxNPCs; ++i)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<GunteraHook>())
                        {
                            for (int j = 0; j < 3; ++j)
                            {
                                int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunteraTentacle>(), NPC.whoAmI, ai2: i, ai3: NPC.whoAmI);
                                if (n != Main.maxNPCs)
                                {
                                    if (Main.netMode == NetmodeID.Server)
                                        NetMessage.SendData(MessageID.SyncNPC, number: n);
                                }
                            }
                        }
                    }
                }
            }

            if (NPC.localAI[2] == 0 && NPC.life < NPC.lifeMax / 2)
            {
                NPC.localAI[2] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int n = NPC.NewNPC(NPC.GetSource_FromThis(),(int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunteraHook>(), NPC.whoAmI, ai3: NPC.whoAmI);
                        if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, number: n);
                    }
                }
            }
            
            if (NPC.localAI[3] == 0)
            {
                NPC.localAI[3] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunCelebration>(), NPC.whoAmI, NPC.whoAmI);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunMagnum>(), NPC.whoAmI, NPC.whoAmI, -1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunUzi>(), NPC.whoAmI, NPC.whoAmI, 1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunShotgun>(), NPC.whoAmI, NPC.whoAmI, -1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunQuad>(), NPC.whoAmI, NPC.whoAmI, 1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunSniper>(), NPC.whoAmI, NPC.whoAmI, -1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunSniper>(), NPC.whoAmI, NPC.whoAmI, 1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    for (int i = 0; i < 5; i++)
                    {
                        n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GunteraHook>(), NPC.whoAmI, ai3: NPC.whoAmI);
                        if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, number: n);
                    }
                }

                NPC.netUpdate = true;
            }

            bool flag1 = false;
            bool flag2 = false;
            if (Main.player[NPC.target].dead)
            {
                flag1 = true;
                flag2 = true;
            }
            int[] numArray = new int[11];
            float num1 = 0.0f;
            float num2 = 0.0f;
            int index1 = 0;
            for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
            {
                if (Main.npc[index2].active && Main.npc[index2].type == ModContent.NPCType<GunteraHook>())
                {
                    num1 += (float)Main.npc[index2].Center.X;
                    num2 += (float)Main.npc[index2].Center.Y;
                    numArray[index1] = index2;
                    ++index1;
                    if (index1 >= 11)
                        break;
                }
            }
            float num3 = num1 / (float)index1;
            float num4 = num2 / (float)index1;
            float num5 = 2.5f;
            float num6 = 0.025f;
            if (NPC.life < NPC.lifeMax / 2)
            {
                num5 = 5f;
                num6 = 0.05f;
            }
            if (NPC.life < NPC.lifeMax / 4)
                num5 = 7f;
            if (!Main.player[NPC.target].ZoneJungle || (double)Main.player[NPC.target].position.Y < Main.worldSurface * 16.0 || Main.player[NPC.target].position.Y > (double)((Main.maxTilesY - 200) * 16))
            {
                flag1 = true;
                num5 += 8f;
                num6 = 0.15f;

                NPC.damage = NPC.defDamage * 10;
                NPC.defense = NPC.defDefense * 10;
            }
            if (Main.expertMode)
            {
                num5 = (num5 + 1f) * 1.1f;
                num6 = (num6 + 0.01f) * 1.1f;
            }
            Vector2 vector2_1 = new Vector2(num3, num4);
            float num7 = (float)(Main.player[NPC.target].Center.X - vector2_1.X);
            float num8 = (float)(Main.player[NPC.target].Center.Y - vector2_1.Y);
            if (flag2)
            {
                num8 *= -1f;
                num7 *= -1f;
                num5 += 8f;
            }
            float num9 = (float)Math.Sqrt((double)num7 * (double)num7 + (double)num8 * (double)num8);
            int num10 = 500;
            if (flag1)
                num10 += 350;
            if (Main.expertMode)
                num10 += 150;
            if ((double)num9 >= (double)num10)
            {
                float num11 = (float)num10 / num9;
                num7 *= num11;
                num8 *= num11;
            }
            float num12 = num3 + num7;
            float num13 = num4 + num8;
            vector2_1 = NPC.Center;
            float num14 = num12 - (float)vector2_1.X;
            float num15 = num13 - (float)vector2_1.Y;
            float num16 = (float)Math.Sqrt((double)num14 * (double)num14 + (double)num15 * (double)num15);
            float num17;
            float num18;
            if ((double)num16 < (double)num5)
            {
                num17 = (float)NPC.velocity.X;
                num18 = (float)NPC.velocity.Y;
            }
            else
            {
                float num11 = num5 / num16;
                num17 = num14 * num11;
                num18 = num15 * num11;
            }
            if (NPC.velocity.X < (double)num17)
            {
                NPC.velocity.X += num6;
                if (NPC.velocity.X < 0.0 && (double)num17 > 0.0)
                {
                    NPC.velocity.X += num6 * 2f;
                }
            }
            else if (NPC.velocity.X > (double)num17)
            {
                NPC.velocity.X -= num6;
                if (NPC.velocity.X > 0.0 && (double)num17 < 0.0)
                {
                    NPC.velocity.X -= num6 * 2f;
                }
            }
            if (NPC.velocity.Y < (double)num18)
            {
                NPC.velocity.Y += num6;
                if (NPC.velocity.Y < 0.0 && (double)num18 > 0.0)
                {
                    NPC.velocity.Y += num6 * 2f;
                }
            }
            else if (NPC.velocity.Y > (double)num18)
            {
                NPC.velocity.Y -= num6;
                if (NPC.velocity.Y > 0.0 && (double)num18 < 0.0)
                {
                    NPC.velocity.Y -= num6 * 2f;
                }
            }

            if (NPC.life < NPC.lifeMax / 2)
                NPC.position += NPC.velocity;

            NPC.rotation = NPC.DirectionTo(Main.player[NPC.target].Center).ToRotation() + (float)Math.PI / 2;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Gun>(), 600);
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.life < NPC.lifeMax / 2)
            {
                if (NPC.frame.Y < 4 * frameHeight)
                    NPC.frame.Y = 4 * frameHeight;

                if (++NPC.frameCounter > 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
                        NPC.frame.Y = 4 * frameHeight;
                }
            }
            else
            {
                if (NPC.frame.Y >= 4 * frameHeight)
                    NPC.frame.Y = 0;

                if (++NPC.frameCounter > 6)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= 4 * frameHeight)
                        NPC.frame.Y = 0;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;//new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, SpriteEffects.None, 0f);
            
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].ai[0] == NPC.whoAmI &&
                    (Main.npc[i].type == ModContent.NPCType<GunCelebration>()
                    || Main.npc[i].type == ModContent.NPCType<GunMagnum>()
                    || Main.npc[i].type == ModContent.NPCType<GunQuad>()
                    || Main.npc[i].type == ModContent.NPCType<GunShotgun>()
                    || Main.npc[i].type == ModContent.NPCType<GunSniper>()
                    || Main.npc[i].type == ModContent.NPCType<GunUzi>()))
                {
                    texture2D13 = TextureAssets.Npc[Main.npc[i].type].Value;
                    rectangle = Main.npc[i].frame;
                    origin2 = rectangle.Size() / 2f;
                    Main.spriteBatch.Draw(texture2D13, Main.npc[i].Center - Main.screenPosition + new Vector2(0f, Main.npc[i].gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Main.npc[i].GetAlpha(drawColor), Main.npc[i].rotation, origin2, Main.npc[i].scale,
                        NPC.spriteDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
                }
            }
            return false;
        }
    }
}