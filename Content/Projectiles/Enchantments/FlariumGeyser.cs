using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using ssm.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SacredTools.Utilities;
using System;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using SacredTools.Content.Buffs;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.SoA.Forces.SyranForce;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class FlariumTornado : ModProjectile
    {
        private static readonly Point _textureSize = new Point(200, 300);

        public bool boo => Projectile.owner.ToPlayer().HasEffect<SyranEffect>();
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            Projectile.ai[0] += 0.1f;
            if (Projectile.ai[0] <= 6f)
            {
                float timer = Projectile.ai[0] * 10f;
                Projectile.alpha = (int)MathHelper.Lerp(255f, 0f, timer / 60f);
            }
            if (Projectile.timeLeft <= 60)
            {
                float timer = 60f - (float)Projectile.timeLeft;
                Projectile.alpha = (int)MathHelper.Lerp(0f, 255f, timer / 60f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<FlariumInfernoDebuff>(), 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawPos = Projectile.Center;
            float sinMod = 0f;
            int projHeight = TextureAssets.Projectile[Projectile.type].Value.Height;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            MiscShaderData miscShaderData = GameShaders.Misc["SacredTools:OutlineShader"];
            miscShaderData.UseOpacity(boo ? 1f : 0);
            miscShaderData.UseColor(Color.Black);
            miscShaderData.UseSecondaryColor(SoAColors.ColorUnblockable);
            DrawData drawData = new DrawData(texture, Projectile.position, texture.Bounds, Color.White);
            miscShaderData.Apply(drawData);
            for (int i = -10; i < 10; i++)
            {
                sinMod += 0.5f;
                float scaleFactor = 0.5f + 0.05f * (float)Math.Abs(i);
                drawPos.X = Projectile.Center.X + 50f * (float)Math.Sin(Projectile.ai[0] + sinMod) * scaleFactor;
                int animCounter = 10000 + Projectile.frameCounter;
                int fragmentFrame = (i + animCounter / 5) % Main.projFrames[Projectile.type];
                int frameHeight = projHeight / Main.projFrames[Projectile.type];
                int frame = frameHeight * fragmentFrame;
                Main.EntitySpriteDraw(texture, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle(0, frame, texture.Width, frameHeight), Projectile.GetAlpha(Color.White), 0f, new Vector2((float)texture.Width / 2f, frameHeight), scaleFactor, (Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
                drawPos.Y -= (float)frameHeight * scaleFactor - 2f;
            }
            Main.spriteBatch.End();
            Main.spriteBatch.ResetToProjectileDrawing();
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 drawPos = Projectile.Center;
            float sinMod = 0f;
            int projHeight = _textureSize.Y;
            for (int i = -10; i < 10; i++)
            {
                sinMod += 0.5f;
                float scaleFactor = 0.5f + 0.05f * (float)Math.Abs(i);
                drawPos.X = Projectile.Center.X + 50f * (float)Math.Sin(Projectile.ai[0] + sinMod) * scaleFactor;
                int frameHeight = projHeight / Main.projFrames[Projectile.type];
                Rectangle segmentHitbox = new Rectangle(0, 0, (int)((float)_textureSize.X * scaleFactor), (int)((float)frameHeight * scaleFactor));
                segmentHitbox.X = (int)(drawPos.X - (float)(segmentHitbox.Width / 2));
                segmentHitbox.Y = (int)(drawPos.Y - (float)segmentHitbox.Height);
                drawPos.Y -= (float)frameHeight * scaleFactor - 2f;
                if (targetHitbox.Intersects(segmentHitbox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
