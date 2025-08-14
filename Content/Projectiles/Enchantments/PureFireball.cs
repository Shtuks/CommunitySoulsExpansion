using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Enchantments
{
    public class PureFireballProj : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/SwarmDeactivatorDebug";
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.alpha = 100;
            Projectile.light = 0.8f;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.ai[2]++;

            if (Projectile.ai[2] > 60)
            {
                CSEUtils.HomeInOnNPC(Projectile, true, 500, 10, 0);
            }

            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch,
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 180);
            target.AddBuff(BuffID.OnFire3, 180);
            target.AddBuff(BuffID.Ichor, 180);
            target.AddBuff(BuffID.ShadowFlame, 180);
            target.AddBuff(BuffID.CursedInferno, 180);
            target.AddBuff(BuffID.BetsysCurse, 180);
            target.AddBuff(BuffID.CursedInferno, 180);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            CreateExplosion();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            CreateExplosion();
        }

        private void CreateExplosion()
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Torch,
                    0f, 0f, 100, default, 2.5f
                );
                dust.noGravity = true;
                dust.velocity *= 4f;
            }

            float explosionRadius = 48f;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly &&
                    Vector2.Distance(npc.Center, Projectile.Center) <= explosionRadius)
                {
                    npc.SimpleStrikeNPC(500, 0);
                }
            }
        }
    }
}