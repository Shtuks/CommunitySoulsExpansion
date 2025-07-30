using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ssm.Content.Buffs;

namespace ssm.Content.Projectiles.Enchantments
{
    public class NightmareBubble : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile.velocity.Y *= 0.99f;

            Lighting.AddLight(Projectile.Center, 0.5f, 0f, 0.5f);
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                             DustID.PurpleTorch, 0f, -0.5f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.boss)
            {
                target.target.ToPlayer().aggro -= (int)(target.target.ToPlayer().aggro * 0.15f);
            }
            else
            {
                target.AddBuff(ModContent.BuffType<FearBuff>(), 180);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                             DustID.PurpleTorch, 0f, -1f);
            }
        }
    }
}