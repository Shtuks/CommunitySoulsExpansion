using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace ssm.Content.Projectiles.Enchantments
{
    public class SpaceJunkProj : ModProjectile, IPixelatedPrimitiveRenderer
    {
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override void AI()
        {
            // Gravity
            Projectile.velocity.Y += 0.2f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }

            // Rotation
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Homing for retaliation projectiles
            if (Projectile.ai[0] != 0 && Main.npc[(int)Projectile.ai[0]].active)
            {
                NPC target = Main.npc[(int)Projectile.ai[0]];
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = (Projectile.velocity * 10f + direction * 5f) / 11f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int shardCount = Main.rand.Next(2, 5); 

            for (int i = 0; i < shardCount; i++)
            {
                Vector2 position = Projectile.Center;

                int shard = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    position,
                    Vector2.Zero, 
                    ModContent.ProjectileType<SatelliteShard>(),
                    Projectile.damage / 2, 
                    0,
                    Projectile.owner);

                Main.projectile[shard].rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            }
            return true; 
        }

        public float WidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.scale * Projectile.width * 1.3f;
            return MathHelper.SmoothStep(baseWidth, 3.5f, completionRatio);
        }

        public Color ColorFunction(float completionRatio)
        {
            return Color.Lerp(Color.Yellow, Color.Transparent, completionRatio) * 0.7f;
        }
        public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
        {
            ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
            FargoSoulsUtil.SetTexture1(FargosTextureRegistry.ColorNoiseMap.Value);
            PrimitiveRenderer.RenderTrail(Projectile.oldPos, new(WidthFunction, ColorFunction, _ => Projectile.Size * 0.5f, Pixelate: true, Shader: shader), 25);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
            int y3 = num156 * Projectile.frame;
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = SpriteEffects.None;

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, effects, 0);
            return false;
        }
    }
}
