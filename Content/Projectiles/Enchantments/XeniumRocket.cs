using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Enchantments
{
    public class XeniumRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Chlorophyte, 0f, 0f);

            CSEUtils.HomeInOnNPC(Projectile, true, 800, 6, 2);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(
                Projectile.GetSource_Death(),
                Projectile.Center,
                Vector2.Zero,
                ProjectileID.RocketI,
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }
}