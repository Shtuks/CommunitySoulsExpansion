using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ssm.Content.Projectiles.Enchantments
{
    public class FlariumGeyser : ModProjectile
    {
        public override string Texture => "ssm/Content/Items/Consumables/Sadism";
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
            Projectile.alpha = 100;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
        }

        public override void AI()
        {
            if (Projectile.ai[0] > 0)
            {
                Projectile.timeLeft = (int)Projectile.ai[0];
                Projectile.ai[0] = 0;
            }

            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height / 2,
                    DustID.FlameBurst,
                    0f,
                    -2f,
                    100,
                    default,
                    2f
                );
                dust.noGravity = true;
                dust.velocity.Y *= 0.5f;
            }

            if (Main.rand.NextBool(20))
            {
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.lifeMax > 5 && npc.Distance(Projectile.Center) < Projectile.width / 2)
                    {
                        npc.SimpleStrikeNPC(Projectile.damage, 0);
                    }
                }
            }
        }
    }
}
