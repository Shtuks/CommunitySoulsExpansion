using System;
using System.Collections.Generic;
using FargowiltasSouls;
using FargowiltasSouls.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ssm.Content.NPCs.MutantEX.Projectiles;
using ssm.Content.NPCs.MutantEX.Projectiles.Fargo;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.NPCs.MutantEX
{
    public class MonstrosityBoss : ModProjectile
    {
        public bool auraTrail;

        public bool sansEye;

        public float SHADOWMUTANTREAL;
        public override string Texture => "ssm/Content/NPCs/MutantEX/MutantEX";

        public int npcType => ModContent.NPCType<MutantEX>();

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 54;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TimeFreezeImmune = true;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            if (Projectile.hide)
            {
                behindProjectiles.Add(index);
            }
        }

        public override void AI()
        {
            NPC npc = FargoSoulsUtil.NPCExists(Projectile.ai[1], npcType);
            if (npc != null)
            {
                Projectile.Center = npc.Center;
                Projectile.alpha = npc.alpha;
                Projectile.direction = Projectile.spriteDirection = npc.direction;
                Projectile.timeLeft = 30;
                auraTrail = npc.localAI[3] >= 3f;
                Projectile.hide = Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearAim>()] > 0 || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearDash>()] > 0 || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSpearSpin>()] > 0 || Main.player[Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MutantSlimeRain>()] > 0;
                sansEye = npc.ai[0] == 10f && npc.ai[1] > 150f || npc.ai[0] == -5f && npc.ai[2] > 330f && npc.ai[2] < 420f;
                if (npc.ai[0] == 10f)
                {
                    SHADOWMUTANTREAL += 0.03f;
                    if (SHADOWMUTANTREAL > 0.75f)
                    {
                        SHADOWMUTANTREAL = 0.75f;
                    }
                }
                Projectile.localAI[1] = sansEye ? MathHelper.Lerp(Projectile.localAI[1], 1f, 0.05f) : 0f;
                Projectile.ai[0] = sansEye ? Projectile.ai[0] + 1f : 0f;
                if (npc.ai[0] >= 11f || npc.ai[0] < 0f)
                {
                    sansEye = true;
                    Projectile.ai[0] = -1f;
                }
                if (!Main.dedServ)
                {
                    Projectile.frame = (int)(npc.frame.Y / (float)(TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                }
                if (npc.frameCounter == 0.0 && (Projectile.localAI[0] += 1f) >= 19f)
                {
                    Projectile.localAI[0] = 0f;
                }
                SHADOWMUTANTREAL -= 0.01f;
                if (SHADOWMUTANTREAL < 0f)
                {
                    SHADOWMUTANTREAL = 0f;
                }
            }
            else
            {
                sansEye = false;
                if (Main.netMode != 1)
                {
                    Projectile.Kill();
                }
            }
        }

        public override void Kill(int timeLeft)
        {
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D texture2D14 = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/MutantSoul", AssetRequestMode.ImmediateLoad).Value;
            int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Texture2D aura = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/MutantAura", AssetRequestMode.ImmediateLoad).Value;
            int auraFrameHeight = aura.Height / 19;
            int auraY = auraFrameHeight * (int)Projectile.localAI[0];
            Rectangle auraRectangle = new Rectangle(0, auraY, aura.Width, auraFrameHeight);
            Color color26 = Projectile.GetAlpha(Projectile.hide && Main.netMode == 1 ? Lighting.GetColor((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16) : lightColor);
            SpriteEffects effects = Projectile.spriteDirection >= 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            float scale = (Main.mouseTextColor / 200f - 0.35f) * 0.4f + 0.9f;
            scale *= Projectile.scale;
            if (auraTrail || SHADOWMUTANTREAL > 0f)
            {
                Main.EntitySpriteDraw(texture2D14, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, Color.White * Projectile.Opacity, Projectile.rotation, origin2, scale, effects, 0);
            }
            if (auraTrail)
            {
                Color color25 = Color.White * Projectile.Opacity;
                color25.A = 200;
                for (float i = 0f; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i += 0.25f)
                {
                    Color color27 = color25 * 0.5f;
                    color27 *= (ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    int max0 = (int)i - 1;
                    if (max0 < 0)
                    {
                        max0 = 0;
                    }
                    float num165 = Projectile.oldRot[max0];
                    Vector2 center = Vector2.Lerp(Projectile.oldPos[(int)i], Projectile.oldPos[max0], 1f - i % 1f);
                    center += Projectile.Size / 2f;
                    Main.EntitySpriteDraw(texture2D14, center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
                }
                Main.EntitySpriteDraw(aura, -16f * Vector2.UnitY + Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)auraRectangle, color25, Projectile.rotation, auraRectangle.Size() / 2f, scale, effects, 0);
            }
            else
            {
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                {
                    Color color27 = color26;
                    color27 *= (ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / (float)ProjectileID.Sets.TrailCacheLength[Projectile.type];
                    Vector2 value4 = Projectile.oldPos[i];
                    float num165 = Projectile.oldRot[i];
                    Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), (Rectangle?)rectangle, color27, num165, origin2, Projectile.scale, effects, 0);
                }
            }
            color26 = Color.Lerp(color26, Color.Black, SHADOWMUTANTREAL);
            Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color26, Projectile.rotation, origin2, Projectile.scale, effects, 0f);
            if (sansEye)
            {
                Color color = new Color(100, 255, 230);
                bool num166 = Projectile.ai[0] == -1f;
                float effectiveTime = Projectile.ai[0];
                float rotation = (float)Math.PI * 2f * Projectile.localAI[1];
                float modifier = Math.Min(1f, (float)Math.Sin(Math.PI * (double)effectiveTime / 120.0) * 2f);
                float opacity = num166 ? 1f : Math.Min(1f, modifier * 2f);
                float sansScale = num166 ? Projectile.scale * Main.cursorScale * 0.8f * Main.rand.NextFloat(0.75f, 1.25f) : Projectile.scale * modifier * Main.cursorScale * 1.25f;
                Texture2D star = ModContent.Request<Texture2D>("ssm/Assets/ExtraTextures/MutantEX/LifeStar", AssetRequestMode.ImmediateLoad).Value;
                Rectangle rect = new Rectangle(0, 0, star.Width, star.Height);
                Vector2 origin = new Vector2(star.Width / 2 + sansScale, star.Height / 2 + sansScale);
                Vector2 drawPos = Projectile.Center;
                drawPos.X += 8 * Projectile.spriteDirection;
                drawPos.Y -= 11f;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
                Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color * opacity, rotation, origin, sansScale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(star, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, Color.White * opacity * 0.75f, rotation, origin, sansScale, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            }
            return false;
        }
    }
}