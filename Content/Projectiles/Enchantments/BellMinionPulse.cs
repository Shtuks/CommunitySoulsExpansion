using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using ssm.Core;

namespace ssm.Content.Projectiles.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class BellMinionPulse : ModProjectile
    {
        public override string Texture => "ssm/Assets/ExtraTextures/Shockwave";
        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.penetrate = -1;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.scale = 0.1f;
            Projectile.timeLeft = 120;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            Projectile.localAI[0] += 1f;

            if (Projectile.localAI[0] < 60f)
            {
                if (Projectile.localAI[0] < 30f)
                {
                    Projectile.alpha -= 5;
                }
                else
                {
                    Projectile.alpha += 5;
                }

                Projectile.scale += 0.1f;
            }
            else
            {
                Projectile.alpha = 255;
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 position = Projectile.Center - Main.screenPosition;
            Rectangle value2 = new Rectangle(0, 0, value.Width, value.Height);
            Vector2 origin = new Vector2((float)value.Width / 2f, (float)value.Height / 2f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            Main.EntitySpriteDraw(value, position, value2, Projectile.GetAlpha(Color.Cyan) * 0.9f, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
}
