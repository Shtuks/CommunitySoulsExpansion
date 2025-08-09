using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ssm.Content.NPCs.Guntera
{
    public class GunteraTentacle : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override string Texture => "Terraria/Images/NPC_264";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.PlanterasTentacle];
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 24;
            NPC.damage = 400;
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
            //NPC.GetGlobalNPC<FargoSoulsGlobalNPC>().SpecialEnchantImmune = true;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = 0;
            return true;
        }

        public override void AI()
        {
            NPC.timeLeft = 60;

            int ai2 = (int)NPC.ai[2];
            if (!(ai2 >= 0 && ai2 < Main.maxNPCs && Main.npc[ai2].active && Main.npc[ai2].type == ModContent.NPCType<GunteraHook>()))
            {
                NPC.active = false;
                return;
            }

            NPC.position += Main.npc[ai2].velocity;

            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;

            int ai3 = (int)NPC.ai[3];
            if (Main.netMode != 1)
            {
                NPC.localAI[0] -= 1f;
                if ((double)NPC.localAI[0] <= 0.0)
                {
                    NPC.localAI[0] = (float)Main.rand.Next(120, 480);
                    NPC.ai[0] = (float)Main.rand.Next(-100, 101);
                    NPC.ai[1] = (float)Main.rand.Next(-100, 101);
                    NPC.netUpdate = true;
                }
            }
            NPC.TargetClosest(true);
            float num1 = 0.2f;
            float num2 = 200f;
            if ((double)Main.npc[ai3].life < (double)Main.npc[ai3].lifeMax * 0.25)
                num2 += 100f;
            if ((double)Main.npc[ai3].life < (double)Main.npc[ai3].lifeMax * 0.1)
                num2 += 100f;
            if (Main.expertMode)
            {
                float num31 = (float)(1.0 - (double)NPC.life / (double)NPC.lifeMax);
                num2 += num31 * 300f;
                num1 += 0.3f;
            }

            float num3 = (float)Main.npc[ai2].position.X + (float)(Main.npc[ai2].width / 2);
            float num4 = (float)Main.npc[ai2].position.Y + (float)(Main.npc[ai2].height / 2);
            Vector2 vector2 = new Vector2(num3, num4);
            float num5 = num3 + NPC.ai[0];
            float num6 = num4 + NPC.ai[1];
            float num7 = num5 - (float)vector2.X;
            float num8 = num6 - (float)vector2.Y;
            float num9 = (float)Math.Sqrt((double)num7 * (double)num7 + (double)num8 * (double)num8);
            float num10 = num2 / num9;
            float num11 = num7 * num10;
            float num12 = num8 * num10;
            if (NPC.position.X < (double)num3 + (double)num11)
            {
                NPC.velocity.X += num1;
                if (NPC.velocity.X < 0.0 && (double)num11 > 0.0)
                {
                    NPC.velocity.X *= 0.9f;
                }
            }
            else if (NPC.position.X > (double)num3 + (double)num11)
            {
                NPC.velocity.X -= num1;
                if (NPC.velocity.X > 0.0 && (double)num11 < 0.0)
                {
                    NPC.velocity.X *= 0.9f;
                }
            }
            if (NPC.position.Y < (double)num4 + (double)num12)
            {
                NPC.velocity.Y += num1;
                if (NPC.velocity.Y < 0.0 && (double)num12 > 0.0)
                {
                    NPC.velocity.Y *= 0.9f;
                }
            }
            else if (NPC.position.Y > (double)num4 + (double)num12)
            {
                NPC.velocity.Y -= num1;
                if (NPC.velocity.Y > 0.0 && (double)num12 < 0.0)
                {
                    NPC.velocity.Y *= 0.9f;
                }
            }
            if (NPC.velocity.X > 8)
                NPC.velocity.X = 8;
            if (NPC.velocity.X < -8)
                NPC.velocity.X = -8;
            if (NPC.velocity.Y > 8)
                NPC.velocity.Y = 8;
            if (NPC.velocity.Y < -8)
                NPC.velocity.Y = -8;
            if ((double)num11 > 0.0)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2((double)num12, (double)num11);
            }
            if ((double)num11 >= 0.0)
                return;
            NPC.spriteDirection = -1;
            NPC.rotation = (float)Math.Atan2((double)num12, (double)num11) + 3.14f;

            if (!Main.player[NPC.target].ZoneJungle || (double)Main.player[NPC.target].position.Y < Main.worldSurface * 16.0 || Main.player[NPC.target].position.Y > (double)((Main.maxTilesY - 200) * 16))
            {
                NPC.damage = NPC.defDamage * 10;
                NPC.defense = NPC.defDefense * 10;
            }

            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.9 || Main.npc[ai2].life < Main.npc[ai2].lifeMax * 0.9)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.8 || Main.npc[ai2].life < Main.npc[ai2].lifeMax * 0.8)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.7 || Main.npc[ai2].life < Main.npc[ai2].lifeMax * 0.7)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.6 || Main.npc[ai2].life < Main.npc[ai2].lifeMax * 0.6)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.5 || Main.npc[ai2].life < Main.npc[ai2].lifeMax * 0.5)
                NPC.localAI[1]++;
            if (!Main.player[NPC.target].ZoneJungle || (double)Main.player[NPC.target].position.Y < Main.worldSurface * 16.0 || Main.player[NPC.target].position.Y > (double)((Main.maxTilesY - 200) * 16))
            {
                NPC.localAI[1] += 3;
                NPC.damage = NPC.defDamage * 10;
                NPC.defense = NPC.defDefense * 10;
            }

            if (NPC.localAI[1] > 80)
            {
                NPC.localAI[1] = Main.rand.Next(-20, 20);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    float speedModifier = Main.rand.NextFloat(2, 20f);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(),NPC.Center, NPC.DirectionTo(Main.player[NPC.target].Center) * speedModifier,
                        ModContent.ProjectileType<GunteraBullet>(), NPC.damage / 4, 0f, Main.myPlayer,
                        NPC.Distance(Main.player[NPC.target].Center) / speedModifier);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter = NPC.frameCounter + 1.0;
            if (NPC.frameCounter >= 6.0)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
                NPC.frame.Y = 0;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Gun>(), 600);
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[2] > -1 && NPC.ai[2] < Main.maxNPCs && Main.npc[(int)NPC.ai[2]].active
                && Main.npc[(int)NPC.ai[2]].type == ModContent.NPCType<GunteraHook>())
            {
                Texture2D texture = TextureAssets.Chain27.Value;
                Vector2 position = NPC.Center;
                Vector2 mountedCenter = Main.npc[(int)NPC.ai[2]].Center;
                Rectangle? sourceRectangle = new Rectangle?();
                Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
                float num1 = texture.Height;
                Vector2 vector24 = mountedCenter - position;
                float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
                bool flag = true;
                if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                    flag = false;
                if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                    flag = false;
                while (flag)
                    if (vector24.Length() < num1 + 1.0)
                    {
                        flag = false;
                    }
                    else
                    {
                        Vector2 vector21 = vector24;
                        vector21.Normalize();
                        position += vector21 * num1;
                        vector24 = mountedCenter - position;
                        Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                        color2 = NPC.GetAlpha(color2);
                        Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                    }
            }

            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;//new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}