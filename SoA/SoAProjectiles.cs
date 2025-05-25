using SacredTools.Content.Projectiles.Weapons.Dreamscape.Nihilus;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    public class SoAProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if(projectile.type == ModContent.ProjectileType<TenebrisLink2>())
            {
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 1600, 8, 2);
                }
            }
        }
    }
}
