using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace ssm.Content.Projectiles.Enchantments
{
    public class SatelliteShard : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 300; 
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1; 
        }

        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;

            if (Projectile.timeLeft < 30)
            {
                Projectile.alpha = (int)((1f - Projectile.timeLeft / 30f) * 255);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
