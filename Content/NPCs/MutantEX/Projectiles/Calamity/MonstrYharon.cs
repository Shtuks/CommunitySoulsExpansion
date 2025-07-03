using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ssm.Core;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX.Projectiles.Calamity
{
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    public class MonstrYharon : ModProjectile
    {
        public override string Texture => "CalamityMod/NPCs/Yharon/Yharon";
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.AlternativeSiblings;
        }

        public int p = -1;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 240;
            Projectile.alpha = 100;
            CooldownSlot = 1;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.hurtCooldowns[1] == 0;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(p);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            p = reader.ReadInt32();
        }

        public override bool? CanDamage()
        {
            return Projectile.localAI[0] > 85f;
        }

        public override bool PreAI()
        {
            if (Projectile.localAI[0] > 85f)
            {
                int num22 = 5;
                for (int index1 = 0; index1 < num22; index1++)
                {
                    Vector2 vector = (Vector2.Normalize(Projectile.velocity) * new Vector2((Projectile.width + 50) / 2f, Projectile.height) * 0.75f).RotatedBy((index1 - (num22 / 2 - 1)) * Math.PI / num22) + Projectile.Center;
                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Vector2 vector2_3 = vector2_2;
                    int index2 = Dust.NewDust(vector + vector2_3, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, default, 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(41, Main.LocalPlayer);
                    Main.dust[index2].velocity /= 4f;
                    Main.dust[index2].velocity -= Projectile.velocity;
                }
            }
            return true;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0f)
            {
                Projectile.localAI[1] = 1f;
                SoundEngine.PlaySound(SoundID.Zombie20, (Vector2?)Projectile.Center);
                p = ShtunUtils.AnyBossAlive() ? Main.npc[FargoSoulsGlobalNPC.boss].target : Player.FindClosest(Projectile.Center, 0, 0);
                Projectile.netUpdate = true;
            }
            if ((Projectile.localAI[0] += 1f) > 85f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
                Projectile.frameCounter = 5;
                Projectile.frame = 6;
                return;
            }
            int ai0 = p;
            if (Projectile.localAI[0] == 125f)
            {
                Projectile.velocity = Main.player[ai0].Center - Projectile.Center;
                Projectile.velocity.Normalize();
                Projectile.velocity *= 24f;
                Projectile.rotation = Projectile.velocity.ToRotation();
                Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
                Projectile.frameCounter = 5;
                Projectile.frame = 6;
                return;
            }
            Vector2 vel = Main.player[ai0].Center - Projectile.Center;
            Projectile.rotation = vel.ToRotation();
            if (vel.X > 0f)
            {
                vel.X -= 300f;
                Projectile.direction = Projectile.spriteDirection = 1;
            }
            else
            {
                vel.X += 300f;
                Projectile.direction = Projectile.spriteDirection = -1;
            }
            Vector2 distance = (Main.player[ai0].Center + new Vector2(Projectile.ai[0], Projectile.ai[1]) - Projectile.Center) / 4f;
            Projectile.velocity = (Projectile.velocity * 19f + distance) / 20f;
            Projectile.position += Main.player[ai0].velocity / 2f;
            if (++Projectile.frameCounter > 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 5)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public virtual void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Dragonfire>(), 900);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);
            SpriteEffects spriteEffects = Projectile.spriteDirection <= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (Projectile.localAI[0] > 85f)
            {
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 2)
                {
                    Color color27 = Color.Lerp(color26, Color.Red, 0.25f);
                    color27 *= (ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (1.5f * ProjectileID.Sets.TrailCacheLength[Projectile.type]);
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    if (Projectile.spriteDirection < 0)
                    {
                        num165 += (float)Math.PI;
                    }
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, spriteEffects, 0);
                }
            }
            float drawRotation = Projectile.rotation;
            if (Projectile.spriteDirection < 0)
            {
                drawRotation += (float)Math.PI;
            }
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), drawRotation, origin2, Projectile.scale, spriteEffects, 0);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            float ratio = (255 - Projectile.alpha) / 255f;
            float white = MathHelper.Lerp(ratio, 1f, 0.25f);
            if (white > 1f)
            {
                white = 1f;
            }
            return new Color((int)(lightColor.R * white), (int)(lightColor.G * white), (int)(lightColor.B * white), (int)(lightColor.A * ratio));
        }
    }
}