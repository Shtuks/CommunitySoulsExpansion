using Terraria;
using Terraria.ModLoader;
using System;
using Fargowiltas.Projectiles;
using ssm.Core.RenewalConversions;

namespace ssm.Core.RenewalConversions
{
    public static class ConvertEquation
    {
        public static void Convert(Projectile projectile, string ConvertInto, bool IsSupreme)
        {
            if (!IsSupreme)
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
                            if (ConvertInto == "Purity")
                                ssmConvertToPurity.ConvertAllToPurity(i, j);
                            // else if (ConvertInto == "Flarium")
                        }
                    }
                }
            }
            else
            {
                int centerX = Main.maxTilesX / 2;
                int centerY = Main.maxTilesY / 2;
                for (int x = 0; x < centerX; x++)
                {
                    for (int y = 0; y < centerY; y++)
                    {
                        if (ConvertInto == "Purity")
                            ssmConvertToPurity.ConvertAllToPurity(centerX, centerY);
                        // else if (ConvertInto == "Flarium")
                    }
                }

                // Top-right corner
                for (int x = centerX; x < Main.maxTilesX; x++)
                {
                    for (int y = 0; y < centerY; y++)
                    {
                        if (ConvertInto == "Purity")
                            ssmConvertToPurity.ConvertAllToPurity(centerX, centerY);
                        // else if (ConvertInto == "Flarium")
                    }
                }

                // Bottom-left corner
                for (int x = 0; x < centerX; x++)
                {
                    for (int y = centerY; y < Main.maxTilesY; y++)
                    {
                        if (ConvertInto == "Purity")
                            ssmConvertToPurity.ConvertAllToPurity(centerX, centerY);
                        // else if (ConvertInto == "Flarium")
                    }
                }

                // Bottom-right corner
                for (int x = centerX; x < Main.maxTilesX; x++)
                {
                    for (int y = centerY; y < Main.maxTilesY; y++)
                    {
                        if (ConvertInto == "Purity")
                            ssmConvertToPurity.ConvertAllToPurity(centerX, centerY);
                        // else if (ConvertInto == "Flarium")
                    }
                }
            }
        }
    }
}