using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Ceiling
{
    public class CeilingOfMoonLordEye : ModNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.SecretBosses;
        }
        public override void SetDefaults()
        {
            NPC.width = 74;
            NPC.height = 74;
            NPC.damage = 125;
            NPC.defense = 350;
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
        }

        public override void AI()
        {
            NPC.timeLeft = 60;

            int ai0 = (int)NPC.ai[0];
            if (!(ai0 >= 0 && ai0 < Main.maxNPCs && Main.npc[ai0].active && Main.npc[ai0].type == ModContent.NPCType<CeilingOfMoonLord>()))
            {
                NPC.active = false;
                return;
            }

            NPC.realLife = ai0;
            NPC.target = Main.npc[NPC.realLife].target;
            NPC.direction = NPC.spriteDirection = (int)NPC.ai[1];

            NPC.Center = Main.npc[NPC.realLife].Center;
            NPC.position.X += 115 * NPC.ai[1];

            int shootNum = 5;
            NPC.localAI[1] += 1.5f;
            if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax * 0.75)
            {
                NPC.localAI[1]++;
                shootNum++;
            }
            if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax * 0.5)
            {
                NPC.localAI[1]++;
                shootNum++;
            }
            if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax * 0.25)
            {
                NPC.localAI[1]++;
                shootNum += 2;
            }
            if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax * 0.1)
            {
                NPC.localAI[1] += 4f;
                shootNum += 6;
            }

            if (NPC.localAI[2] == 0)
            {
                if (NPC.localAI[1] > 600)
                {
                    NPC.localAI[1] = 0f;
                    NPC.localAI[2] = 1f;
                }
            }
            else
            {
                if (NPC.localAI[1] > 45)
                {
                    NPC.localAI[1] = 0;
                    if (++NPC.localAI[2] >= shootNum)
                        NPC.localAI[2] = 0;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vel = 9f * NPC.DirectionTo(Main.player[NPC.target].Center + Main.player[NPC.target].velocity * 15f);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vel, ProjectileID.PhantasmalBolt, NPC.damage / 6, 0f, Main.myPlayer);
                    }
                }
            }

            if (++NPC.localAI[0] >= 300)
            {
                if (NPC.localAI[0] % 20 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<CeilingSphere>(), NPC.damage / 6, 0f, Main.myPlayer, NPC.target);
                }

                if (NPC.localAI[0] > 420)
                {
                    NPC.localAI[0] = 0;
                }
            }
            else if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax / 2)
            {
                NPC.localAI[0]++;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
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
    }
}