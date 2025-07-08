using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Enchantments
{
    public class SwordRainProjectile : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;

            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Iron);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Stoned, 120);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Iron);
            }
        }
    }
}
