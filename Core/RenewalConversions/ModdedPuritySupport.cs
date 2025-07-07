using Terraria;
using Terraria.ModLoader;
using System;
using Fargowiltas.Projectiles;

namespace ssm.Core.RenewalConversions
{
    public class ModdedPuritySupport : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<PurityNukeProj>())
            {
                // Standard circle purification
                int radius = 150;

                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        int i = (int)(projectile.Center.X / 16f) + x;
                        int j = (int)(projectile.Center.Y / 16f) + y;

                        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                        {
                            ssmConvertToPurity.ConvertAllToPurity(i, j);
                        }
                    }
                }
            }


            else if (projectile.type == ModContent.ProjectileType<PurityNukeSupremeProj>())
            {
                for (int x = -Main.maxTilesX; x < Main.maxTilesX; x++)
                {
                    for (int y = -Main.maxTilesY; y < Main.maxTilesY; y++)
                    {
                        int i = (int)(projectile.Center.X / 16f) + x;
                        int j = (int)(projectile.Center.Y / 16f) + y;

                        ssmConvertToPurity.ConvertAllToPurity(i, j);
                    }
                }
            }
        }
    }
}