using SacredTools.Content.Items.Weapons.Dreamscape.Nihilus;
using SacredTools.Content.Projectiles.Weapons.Dreamscape.Nihilus;
using SacredTools.Projectiles.Dreamscape;
using ssm.Core;
using Terraria;
using Terraria.ModLoader;

namespace ssm.SoA
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SoAProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            if (proj.type == ModContent.ProjectileType<DesperatioFlame>())
            {
                proj.damage -= (int)(proj.damage * 0.4f);
            }
        }
        public override void AI(Projectile projectile)
        {
            if(projectile.type == ModContent.ProjectileType<TenebrisLink2>())
            {
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 1600, 8, 2);
                }
            }
            
            if (projectile.type == ModContent.ProjectileType<SpookGrenade>())
            {
                projectile.velocity *= 1.1f;
                if ((projectile.ai[1] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 700, 8, 2);
                }
            }

            if (projectile.type == ModContent.ProjectileType<DesperatioBullet>())
            {
                if ((projectile.ai[2] += 1f) >= 20f)
                {
                    ShtunUtils.HomeInOnNPC(projectile, true, 300, 8, 2);
                }
            }
        }
    }
}
