using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ssm.Content.NPCs.Guntera
{
    public class GunCelebration : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 78;
            NPC.height = 28;
            NPC.damage = 500;
            NPC.defense = 100;
            NPC.lifeMax = 1500000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            for (int i = 0; i < NPC.buffImmune.Length; i++)
                NPC.buffImmune[i] = true;
            NPC.buffImmune[ModContent.BuffType<Gun>()] = false;
            NPC.aiStyle = -1;
        }

        public virtual void Offset(NPC guntera)
        {
            NPC.Center = guntera.Center + new Vector2(0, -64).RotatedBy(guntera.rotation);
        }

        public override void AI()
        {
            NPC.timeLeft = 60;

            if (NPC.localAI[3] == 0)
            {
                NPC.localAI[3] = Main.rand.NextFloat(4);
            }

            int ai0 = (int)NPC.ai[0];
            if (!(ai0 >= 0 && ai0 < Main.maxNPCs && Main.npc[ai0].active && Main.npc[ai0].type == ModContent.NPCType<Guntera>()))
            {
                NPC.active = false;
                return;
            }

            NPC.TargetClosest(false);
            NPC.direction = NPC.spriteDirection = NPC.ai[0] < 0 ? -1 : 1;

            Offset(Main.npc[ai0]);

            float targetRotation = NPC.DirectionTo(Main.player[NPC.target].Center).ToRotation();
            float bottom = (float)Math.PI * NPC.localAI[3];
            float top = (float)Math.PI * (NPC.localAI[3] + 2);
            if (targetRotation > top)
                targetRotation -= (float)Math.PI * 2;
            if (targetRotation < bottom)
                targetRotation += (float)Math.PI * 2;

            NPC.rotation += Math.Sign(targetRotation - NPC.rotation) * MathHelper.ToRadians(1);
            if (NPC.rotation > top)
                NPC.rotation -= (float)Math.PI * 2;
            if (NPC.rotation < bottom)
                NPC.rotation += (float)Math.PI * 2;

            NPC.damage = NPC.defDamage;
            NPC.defense = NPC.defDefense;

            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.9 || Main.npc[ai0].life < Main.npc[ai0].lifeMax * 0.9)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.8 || Main.npc[ai0].life < Main.npc[ai0].lifeMax * 0.8)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.7 || Main.npc[ai0].life < Main.npc[ai0].lifeMax * 0.7)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.6 || Main.npc[ai0].life < Main.npc[ai0].lifeMax * 0.6)
                NPC.localAI[1]++;
            NPC.localAI[1]++;
            if (NPC.life < NPC.lifeMax * 0.5 || Main.npc[ai0].life < Main.npc[ai0].lifeMax * 0.5)
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

                float speedModifier = 1f - (float)NPC.life / NPC.lifeMax;
                if (speedModifier < 1f - (float)Main.npc[ai0].life / Main.npc[ai0].lifeMax)
                    speedModifier = 1f - (float)Main.npc[ai0].life / Main.npc[ai0].lifeMax;
                speedModifier *= 12f;
                speedModifier += 3;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, NPC.rotation.ToRotationVector2() * speedModifier,
                        ModContent.ProjectileType<GunteraBullet>(), NPC.damage / 4, 0f, Main.myPlayer, 
                        NPC.Distance(Main.player[NPC.target].Center) / speedModifier);
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            projectile.timeLeft = 0;
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
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale,
                NPC.spriteDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            return false;
        }
    }
}