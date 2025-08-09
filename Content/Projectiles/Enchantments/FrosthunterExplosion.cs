using FargowiltasSouls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Enchantments
{
    public class FrosthunterExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 5;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults() 
        {
            Projectile.scale = 1f;
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.FargoSouls().CanSplit = false;
            Projectile.FargoSouls().DeletionImmuneRank = 2;
        }

        public override void AI()
        {
            if (Projectile.position.HasNaNs())
            {
                Projectile.Kill();
                return;
            }

            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame--;
                    Projectile.Kill();
                }
            }

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.Item88, Projectile.Center);
                Projectile.scale *= Main.rand.NextFloat(1f, 3f);
                Projectile.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; 
            int y3 = num156 * Projectile.frame; 
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }

    }
}
