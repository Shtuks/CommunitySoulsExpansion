using CalamityMod.Projectiles.Melee;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ModContent.ProjectileType<GayBeam>())
            {
                projectile.velocity *= 1.1f;
            }
        }
    }
}
