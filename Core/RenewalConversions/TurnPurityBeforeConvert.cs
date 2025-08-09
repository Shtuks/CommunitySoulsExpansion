using Terraria;
using System;


namespace ssm.Core.RenewalConversions
{
    public class TurnPurityBeforeConvert
    {
        public static void RenewalPurify(Projectile projectile)
        {
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

        public static void RenewalSupremePurify(Projectile projectile)
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