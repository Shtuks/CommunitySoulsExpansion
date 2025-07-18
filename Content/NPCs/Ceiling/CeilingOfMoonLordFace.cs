using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.Ceiling
{
    public class CeilingOfMoonLordFace : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 274;
            NPC.height = 122;
            NPC.damage = 125;
            NPC.defense = 350;
            NPC.lifeMax = 325000;
            NPC.HitSound = SoundID.NPCHit57;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            for (int i = 0; i < NPC.buffImmune.Length; i++)
                NPC.buffImmune[i] = true;
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
            NPC.position.Y += 100;

            if (Main.npc[NPC.realLife].life < Main.npc[NPC.realLife].lifeMax / 2)
                NPC.localAI[0]++;
            if (++NPC.localAI[0] > 660)
            {
                NPC.localAI[0] = 0;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int modifier = Main.player[NPC.target].Center.X < NPC.Center.X ? -1 : 1;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, -Vector2.UnitY.RotatedBy(Math.PI / 2 * modifier), ModContent.ProjectileType<CeilingDeathray>(),
                        NPC.damage / 4, 0f, Main.myPlayer, (float)Math.PI / 2 * modifier / 120 * 1.3f, NPC.whoAmI);
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            projectile.timeLeft = 0;
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