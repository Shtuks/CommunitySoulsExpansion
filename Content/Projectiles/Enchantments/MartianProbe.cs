using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Enchantments
{
    public class MartianProbe : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";

        public bool shouldHome = false;
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Projectile.ai[1]++;

            if (Projectile.ai[1] == 60)
            {
                Projectile.velocity = Vector2.Zero;
                shouldHome = true;
            }
            if (shouldHome) 
            {
                CSEUtils.HomeInOnNPC(Projectile, true, 9000, 8, 2);
            }
        }

        public override void Kill(int timeLeft)
        {
            Explode();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Explode();
        }

        private void Explode()
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2Circular(3f, 3f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, speed * 2f);
                d.noGravity = true;

                Dust smoke = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Smoke,
                    speed * 1.5f
                );
                smoke.scale = 1.5f;
            }

            int shrapnelCount = Main.rand.Next(6, 9);
            for (int i = 0; i < shrapnelCount; i++)
            {
                Vector2 velocity = new Vector2(0, 4f).RotatedBy(MathHelper.TwoPi * i / shrapnelCount);
                velocity = velocity.RotatedByRandom(0.3f);

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    velocity,
                    ModContent.ProjectileType<Shrapnel>(),
                    Projectile.damage / 2,
                    2f,
                    Projectile.owner
                );
            }
        }
    }
    public class Shrapnel : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            Projectile.velocity.Y += 0.2f;

            Projectile.velocity.X *= 0.98f;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            if (Main.rand.NextBool(3))
            {
                Dust d = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Torch,
                    Vector2.Zero
                );
                d.noGravity = true;
                d.scale = 0.8f;
            }
        }
    }
}