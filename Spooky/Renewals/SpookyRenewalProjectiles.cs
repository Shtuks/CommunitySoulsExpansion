using Terraria.ModLoader;
using Spooky.Content.Items.SpookyBiome.Misc;
using Spooky.Content.Items.Cemetery.Misc;
using ssm.Core;
using Spooky.Content.Generation;
using Terraria;
using System;
using Fargowiltas.Projectiles;
using ssm.Core.RenewalConversions;

namespace ssm.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewal";

        public SpookyRenewalProj() : base("SpookyRenewal", ModContent.ProjectileType<SpookySolutionProj>(), 1, false)
        {
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 150;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        ssmConvertToPurity.ConvertAllToPurity(i, j);
                        TileConversionMethods.ConvertPurityIntoSpooky(i, j);
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SpookyRenewalSupreme";
        public SpookyRenewalSupremeProj() : base("SpookyRenewalSupreme", ModContent.ProjectileType<SpookySolutionProj>(), 1, true)
        {
        }
        public override void OnKill(int timeLeft)
        {
            for (int x = -Main.maxTilesX; x < Main.maxTilesX; x++)
            {
                for (int y = -Main.maxTilesY; y < Main.maxTilesY; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    ssmConvertToPurity.ConvertAllToPurity(i, j);
                    TileConversionMethods.ConvertPurityIntoSpooky(i, j);
                }
            }
        }
    }
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewal";
        public SwampyRenewalProj() : base("SwampyRenewal", ModContent.ProjectileType<CemeterySolutionProj>(), 4, false)
        {
        }

        public override void OnKill(int timeLeft)
        {
            int radius = 150;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                    {
                        ssmConvertToPurity.ConvertAllToPurity(i, j);
                        TileConversionMethods.ConvertPurityIntoCemetery(i, j);
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SwampyRenewalSupremeProj : RenewalBaseProj
    {
        public override string Texture => "ssm/Spooky/Renewals/SwampyRenewalSupreme";
        public SwampyRenewalSupremeProj() : base("SwampyRenewalSupreme", ModContent.ProjectileType<CemeterySolutionProj>(), 4, true)
        {
        }

        public override void OnKill(int timeLeft)
        {
            for (int x = -Main.maxTilesX; x < Main.maxTilesX; x++)
            {
                for (int y = -Main.maxTilesY; y < Main.maxTilesY; y++)
                {
                    int i = (int)(Projectile.Center.X / 16f) + x;
                    int j = (int)(Projectile.Center.Y / 16f) + y;

                    ssmConvertToPurity.ConvertAllToPurity(i, j);
                    TileConversionMethods.ConvertPurityIntoCemetery(i, j);
                }
            }
        }
    }
}