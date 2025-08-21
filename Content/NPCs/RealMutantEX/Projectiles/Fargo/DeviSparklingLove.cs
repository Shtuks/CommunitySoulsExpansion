using FargowiltasSouls;
//using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.RealMutantEX.Projectiles
{
    public class DeviSparklingLove : ModProjectile
    {
        public int scaleCounter;

        public override string Texture => "FargowiltasSouls/Content/Items/Weapons/FinalUpgrades/SparklingLove";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
            Projectile.alpha = 250;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().DeletionImmuneRank = 2;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            Player plr = Main.LocalPlayer;
            if (npc != null)
            {
                if (Projectile.localAI[0] == 0f)
                {
                    Projectile.localAI[0] = 1f;
                    Projectile.localAI[1] = Projectile.DirectionFrom(npc.Center).ToRotation();
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, -1f, -17f);
                    }
                }
                if (Projectile.alpha > 0)
                {
                    Projectile.alpha -= 4;
                    if (Projectile.alpha < 0)
                    {
                        Projectile.alpha = 0;
                    }
                }
                if ((Projectile.localAI[0] += 1f) > 31f)
                {
                    Projectile.localAI[0] = 1f;
                    if (++this.scaleCounter < 3)
                    {
                        Projectile.position = Projectile.Center;
                        Projectile.width *= 2;
                        Projectile.height *= 2;
                        Projectile.scale *= 2f;
                        Projectile.Center = Projectile.position;
                        this.MakeDust();
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, -1f, (float)(-16 + this.scaleCounter));
                        }
                        SoundEngine.PlaySound(SoundID.Item92, (Vector2?)Projectile.Center);
                    }
                }
                Vector2 offset = new Vector2(Projectile.ai[1], 0f).RotatedBy(npc.ai[3] + Projectile.localAI[1]);
                Projectile.Center = npc.Center + offset * Projectile.scale;
                if (Projectile.timeLeft == 8)
                {
                    SoundEngine.PlaySound(SoundID.NPCDeath6, (Vector2?)Projectile.Center);
                    SoundEngine.PlaySound(SoundID.Item92, (Vector2?)Projectile.Center);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0f, Main.myPlayer, -1f, -14f);
                    }
                    if (!Main.dedServ && Main.LocalPlayer.active)
                    {
                        Main.LocalPlayer.GetModPlayer<CSEPlayer>().Screenshake = 30;
                    }
                    if (Main.netMode != 1)
                    {
                        float baseRotation = (WorldSavingSystem.EternityMode ? Main.rand.NextFloat((float)Math.PI * 2f) : 0f);
                        int max = 8;
                        if (WorldSavingSystem.EternityMode)
                        {
                            max = 12;
                        }
                        if (WorldSavingSystem.MasochistModeReal)
                        {
                            max = 8;
                        }
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 target = 600f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * (float)i + baseRotation);
                            Vector2 speed = 2f * target / 90f;
                            float acceleration = (0f - speed.Length()) / 90f;
                            float rotation = speed.ToRotation();
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                        }
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 pos = Projectile.Center + (plr.Center - Projectile.Center).RotatedBy(2.0943334102630615);
                            Vector2 target = 600f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * (float)i + baseRotation);
                            Vector2 speed = 2f * target / 90f;
                            float acceleration = (0f - speed.Length()) / 90f;
                            float rotation = speed.ToRotation();
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                        }
                        for (int i = 0; i < max; i++)
                        {
                            Vector2 pos = Projectile.Center + (plr.Center - Projectile.Center).RotatedBy(4.188666820526123);
                            Vector2 target = 600f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * (float)i + baseRotation);
                            Vector2 speed = 2f * target / 90f;
                            float acceleration = (0f - speed.Length()) / 90f;
                            float rotation = speed.ToRotation();
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                            Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                        }
                        if (WorldSavingSystem.MasochistModeReal)
                        {
                            for (int i = 0; i < max; i++)
                            {
                                Vector2 target = 300f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * ((float)i + 0.5f) + baseRotation);
                                Vector2 speed = 2f * target / 90f;
                                float acceleration = (0f - speed.Length()) / 90f;
                                float rotation = speed.ToRotation();
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), Projectile.Center, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                            }
                            for (int i = 0; i < max; i++)
                            {
                                Vector2 pos = Projectile.Center + (plr.Center - Projectile.Center).RotatedBy(2.0943334102630615);
                                Vector2 target = 300f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * ((float)i + 0.5f) + baseRotation);
                                Vector2 speed = 2f * target / 90f;
                                float acceleration = (0f - speed.Length()) / 90f;
                                float rotation = speed.ToRotation();
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                            }
                            for (int i = 0; i < max; i++)
                            {
                                Vector2 pos = Projectile.Center + (plr.Center - Projectile.Center).RotatedBy(4.188666820526123);
                                Vector2 target = 300f * Vector2.UnitX.RotatedBy((float)Math.PI * 2f / (float)max * ((float)i + 0.5f) + baseRotation);
                                Vector2 speed = 2f * target / 90f;
                                float acceleration = (0f - speed.Length()) / 90f;
                                float rotation = speed.ToRotation();
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<DeviEnergyHeart>(), (int)((double)Projectile.damage * 0.75), 0f, Main.myPlayer, rotation + (float)Math.PI / 2f, acceleration);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation + (float)Math.PI / 2f);
                                Projectile.NewProjectile(Terraria.Entity.InheritSource(Projectile), pos, speed, ModContent.ProjectileType<GlowLine>(), Projectile.damage, 0f, Main.myPlayer, 2f, rotation - (float)Math.PI / 2f);
                            }
                        }
                    }
                }
                Projectile.direction = (Projectile.spriteDirection = npc.direction);
                Projectile.rotation = npc.ai[3] + Projectile.localAI[1] + (float)Math.PI / 2f + (float)Math.PI / 4f;
                if (Projectile.spriteDirection >= 0)
                {
                    Projectile.rotation -= (float)Math.PI / 2f;
                }
            }
            else
            {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            this.MakeDust();
        }

        private void MakeDust()
        {
            Vector2 start = Projectile.width * Vector2.UnitX.RotatedBy(Projectile.rotation - (float)Math.PI / 4f);
            if (Math.Abs(start.X) > (float)(Projectile.width / 2))
            {
                start.X = Projectile.width / 2 * Math.Sign(start.X);
            }
            if (Math.Abs(start.Y) > (float)(Projectile.height / 2))
            {
                start.Y = Projectile.height / 2 * Math.Sign(start.Y);
            }
            int length = (int)start.Length();
            start = Vector2.Normalize(start);
            float scaleModifier = (float)this.scaleCounter / 3f + 0.5f;
            for (int j = -length; j <= length; j += 80)
            {
                Vector2 dustPoint = Projectile.Center + start * j;
                dustPoint.X -= 23f;
                dustPoint.Y -= 23f;
                for (int index1 = 0; index1 < 15; index1++)
                {
                    int index2 = Dust.NewDust(dustPoint, 46, 46, 86, 0f, 0f, 0, default(Color), scaleModifier * 2.5f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].velocity *= 16f * scaleModifier;
                    int index3 = Dust.NewDust(dustPoint, 46, 46, 86, 0f, 0f, 0, default(Color), scaleModifier);
                    Main.dust[index3].velocity *= 8f * scaleModifier;
                    Main.dust[index3].noGravity = true;
                }
                for (int i = 0; i < 5; i++)
                {
                    int d = Dust.NewDust(dustPoint, 46, 46, 86, 0f, 0f, 0, default(Color), Main.rand.NextFloat(1f, 2f) * scaleModifier);
                    Main.dust[d].velocity *= Main.rand.NextFloat(1f, 4f) * scaleModifier;
                }
            }
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
            SpriteEffects effects = ((Projectile.spriteDirection <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[0], ModContent.NPCType<RealMutantEX>());
            if (npc != null && npc.velocity != Vector2.Zero)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
                GameShaders.Armor.GetShaderFromItemId(1018).Apply(Projectile);
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                {
                    Color color27 = new Color(255, 255, 255, 50);
                    color27 *= 0.5f;
                    color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
                }
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            }
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Color.White * Projectile.Opacity, Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }
    }
}