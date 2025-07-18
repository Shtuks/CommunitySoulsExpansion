using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.GameContent;

namespace ssm.Content.NPCs.Ceiling
{
    [AutoloadBossHead]
    public class CeilingOfMoonLord : ModNPC
    {
        private int ceilingProj = -1;
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 60;
            NPC.damage = 125;
            NPC.defense = 50;
            NPC.lifeMax = 325000;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(1, 50);
            NPC.boss = true;
            Music = MusicID.LunarBoss;
            NPC.netAlways = true;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(ceilingProj);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            ceilingProj = reader.ReadInt32();
        }

        public override void AI()
        {
            NPC.timeLeft = 60;

            if (!NPC.HasValidTarget)
                NPC.TargetClosest(false);

            if (!(ceilingProj >= 0 && ceilingProj < Main.maxProjectiles 
                && Main.projectile[ceilingProj].active && Main.projectile[ceilingProj].type == ModContent.ProjectileType<CeilingProj>()))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    ceilingProj = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<CeilingProj>(), 0, 0f, Main.myPlayer, 0f, NPC.whoAmI);

                NPC.netUpdate = true;
            }

            if (NPC.localAI[3] == 0)
            {
                NPC.localAI[3] = 1;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordEye>(), NPC.whoAmI, NPC.whoAmI, 1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    NPC.ai[0] = n;

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordEye>(), NPC.whoAmI, NPC.whoAmI, -1);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    NPC.ai[1] = n;

                    n = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<CeilingOfMoonLordFace>(), NPC.whoAmI, NPC.whoAmI);
                    if (n != Main.maxNPCs && Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.SyncNPC, number: n);

                    NPC.ai[2] = n;
                }

                NPC.netUpdate = true;
            }

            if (Main.LocalPlayer.active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
            {
                Main.LocalPlayer.AddBuff(ModContent.BuffType<Moonified>(), 2);

                if (Main.LocalPlayer.Center.Y < NPC.Center.Y - 32 //teleport and hurt if player is EVER above boss or too far to sides
                    || Main.LocalPlayer.Center.Y > NPC.Center.Y + 1500
                    || Math.Abs(Main.LocalPlayer.Center.X - NPC.Center.X) > 1500)
                {
                    Vector2 teleportPos = NPC.Center;
                    teleportPos.Y += 250;

                    bool hurt = Main.LocalPlayer.Center.Y < NPC.Center.Y;

                    for (int i = 0; i < 50; i++)
                    {
                        int d = Dust.NewDust(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, 229, 0f, 0f, 0, default(Color), 2.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 9f;
                    }

                    Main.LocalPlayer.Teleport(teleportPos);
                    NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Main.LocalPlayer.whoAmI, teleportPos.X, teleportPos.Y, 1);
                    Main.LocalPlayer.velocity = Vector2.Zero;
                    if (hurt)
                    {
                        Main.LocalPlayer.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 1);
                        Main.LocalPlayer.immune = false;
                        Main.LocalPlayer.immuneTime = 0;
                        Main.LocalPlayer.hurtCooldowns[0] = 0;
                        Main.LocalPlayer.hurtCooldowns[1] = 0;
                    }

                    for (int i = 0; i < 50; i++)
                    {
                        int d = Dust.NewDust(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, 229, 0f, 0f, 0, default(Color), 2.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 9f;
                    }
                }

                if (Main.LocalPlayer.ZoneUnderworldHeight) //kill when reached underworld
                {
                    Main.LocalPlayer.KillMe(PlayerDeathReason.ByOther(12), 999999, 0, false);
                }
            }

            if (NPC.position.Y > Main.maxTilesY * 16) //despawn upon exiting the bottom of world
            {
                NPC.active = false;
            }

            NPC.velocity.Y = 1.5f;
            if (NPC.life < NPC.lifeMax * 0.75)
                NPC.velocity.Y += 0.25f;
            if (NPC.life < NPC.lifeMax * 0.5)
                NPC.velocity.Y += 0.4f;
            if (NPC.life < NPC.lifeMax * 0.25)
                NPC.velocity.Y += 0.5f;
            if (NPC.life < NPC.lifeMax * 0.1)
                NPC.velocity.Y += 0.6f;
            if (NPC.life < NPC.lifeMax * 0.66 && Main.expertMode)
                NPC.velocity.Y += 0.3f;
            if (NPC.life < NPC.lifeMax * 0.33 && Main.expertMode)
                NPC.velocity.Y += 0.3f;
            if (NPC.life < NPC.lifeMax * 0.05 && Main.expertMode)
                NPC.velocity.Y += 0.6f;
            if (NPC.life < NPC.lifeMax * 0.035 && Main.expertMode)
                NPC.velocity.Y += 0.6f;
            if (NPC.life < NPC.lifeMax * 0.025 && Main.expertMode)
                NPC.velocity.Y += 0.6f;
            if (Main.expertMode)
                NPC.velocity.Y = NPC.velocity.Y * 1.35f + 0.35f;

            if (Math.Abs(Main.player[NPC.target].Center.X - NPC.Center.X) > 150)
                NPC.velocity.X = 2 * NPC.velocity.Y * Math.Sign(Main.player[NPC.target].Center.X - NPC.Center.X);

            if (NPC.life < NPC.lifeMax / 2)
                NPC.localAI[0]++;

            if (++NPC.localAI[0] > 300)
            {
                NPC.localAI[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient) //spray homing eyes
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + Main.rand.NextVector2Square(0f, NPC.width), Vector2.UnitX.RotatedByRandom(Math.PI) * 6f,
                            ProjectileID.PhantasmalEye, NPC.damage / 7, 0f, Main.myPlayer);
                    }
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, 229, 0f, 0f, 0, default(Color), 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 3f;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}