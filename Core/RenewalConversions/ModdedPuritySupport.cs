using Terraria;
using Terraria.ModLoader;
using System;
using Fargowiltas.Projectiles;
using ssm.Core.RenewalConversions;

namespace ssm.Core.RenewalConversions
{
    public class ModdedPuritySupport : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<PurityNukeProj>())
            {
                ConvertEquation.Convert(projectile, "Purity", false);
            }

            else if (projectile.type == ModContent.ProjectileType<PurityNukeSupremeProj>())
            {
                ConvertEquation.Convert(projectile, "Purity", true);
            }
        }   
    }
}