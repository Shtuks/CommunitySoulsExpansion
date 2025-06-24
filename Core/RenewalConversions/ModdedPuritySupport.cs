using Terraria;
using Terraria.ModLoader;
using System;
using Spooky.Content.Generation;
using Fargowiltas.Projectiles;
using ssm.Core;
using CalamityMod.World;
using CalamityMod;

namespace ssm.Core.RenewalConversions
{
    [ExtendsFromMod(ModCompatibility.Spooky.Name)]
    [JITWhenModsEnabled(ModCompatibility.Spooky.Name)]
    public class SpookyPuritySupport : GlobalProjectile
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
                            if (ModLoader.TryGetMod("Spooky", out Mod SpookyMod))
                            {
                                TileConversionMethods.ConvertSpookyIntoPurity(i, j, 4);
                            }
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

                        if (ModLoader.TryGetMod("Spooky", out Mod SpookyMod))
                        {
                            TileConversionMethods.ConvertSpookyIntoPurity(i, j, 4);
                        }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public class ClamityPuritySupport : GlobalProjectile
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
                            if (ModLoader.TryGetMod("Clamity", out Mod Clamity))
                            {
                                ClamityConversion.FrozenHellToPurity(i, j, 4);
                            }
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

                        if (ModLoader.TryGetMod("Clamity", out Mod Clamity))
                        {
                            ClamityConversion.FrozenHellToPurity(i, j, 4);
                        }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class RedemptionPuritySupport : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (projectile.type == ModContent.ProjectileType<PurityNukeProj>())
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
                            if (WorldGen.InWorld(i, j))
                            {
                                if (ModLoader.TryGetMod("Redemption", out Mod Redemption))
                                {
                                    Tile tile = Framing.GetTileSafely(i, j);
                                    RedemptionConversion.ReverseWastelandTileConversion(tile, i, j);
                                }
                            }
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

                        if (WorldGen.InWorld(i, j))
                        {
                            if (ModLoader.TryGetMod("Redemption", out Mod Redemption))
                            {
                                Tile tile = Framing.GetTileSafely(i, j);
                                RedemptionConversion.ReverseWastelandTileConversion(tile, i, j);
                            }
                        }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SacredToolsPuritySupport : GlobalProjectile
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
                            if (ModLoader.TryGetMod("SacredTools", out Mod SacredTools))
                            {
                                SacredToolsConversion.ScorchedToPurity(i, j, 4);
                                SacredToolsConversion.ShrineToPurity(i, j, 4);
                            }
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

                        if (ModLoader.TryGetMod("SacredTools", out Mod SacredTools))
                        {
                            SacredToolsConversion.ScorchedToPurity(i, j, 4);
                            SacredToolsConversion.ShrineToPurity(i, j, 4);
                        }
                    }
                }
            }
        }
    }

    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public class CalamityPuritySupport : GlobalProjectile
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
                            if (ModLoader.TryGetMod("Calamity", out Mod Calamity))
                            {
                                CalamityConversion.AstralToPurity(i, j, 4);
                            }
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

                        if (ModLoader.TryGetMod("Calamity", out Mod Calamity))
                        {
                            CalamityConversion.AstralToPurity(i, j, 4);
                        }
                    }
                }
            }
        }
    }

        [ExtendsFromMod(ModCompatibility.Spirit.Name)]
        [JITWhenModsEnabled(ModCompatibility.Spirit.Name)]
    public class SpiritPuritySupport : GlobalProjectile
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
                            if (ModLoader.TryGetMod("SpiritMod", out Mod Spirit))
                            {
                                SpiritToPurityConversion.Convert(i, j, 4);
                            }
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

                        if (ModLoader.TryGetMod("SpiritMod", out Mod Spirit))
                        {
                            SpiritToPurityConversion.Convert(i, j, 4);
                        }
                    }
                }
            }
        }
    }
}