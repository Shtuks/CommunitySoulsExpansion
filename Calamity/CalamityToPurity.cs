using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Walls;
using ssm.Core; 

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public static class CalamityConversion
    {
        public static void AstralConvert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (!WorldGen.InWorld(k, l, 1) || (Math.Abs(k - i) + Math.Abs(l - j)) >= Math.Sqrt(size * size + size * size))
                        continue;

                    Tile tile = Main.tile[k, l];
                    if (tile == null)
                        continue;

                    int type = tile.TileType;
                    int wall = tile.WallType;

                    // Astral block-to-vanilla tile conversions
                    if (type == ModContent.TileType<AstralDirt>())
                        tile.TileType = TileID.Dirt;
                        
                    else if (type == ModContent.TileType<AstralSnow>())
                        tile.TileType = TileID.SnowBlock;

                    else if (type == ModContent.TileType<NovaeSlag>())
                        tile.TileType = TileID.Hellstone;

                    else if (type == ModContent.TileType<CelestialRemains>())
                        tile.TileType = TileID.LihzahrdBrick;

                    else if (type == ModContent.TileType<AstralClay>())
                        tile.TileType = TileID.ClayBlock;

                    else if (type == ModContent.TileType<AstralMonolith>())
                        tile.TileType = TileID.PearlstoneBrick;

                    else if (type == ModContent.TileType<AstralStone>())
                        tile.TileType = TileID.Stone;

                    else if (type == ModContent.TileType<AstralGrass>())
                        tile.TileType = TileID.Grass;

                    else if (type == ModContent.TileType<AstralSand>())
                        tile.TileType = TileID.Sand;

                    else if (type == ModContent.TileType<AstralSandstone>())
                        tile.TileType = TileID.Sandstone;

                    else if (type == ModContent.TileType<HardenedAstralSand>())
                        tile.TileType = TileID.HardenedSand;

                    else if (type == ModContent.TileType<AstralIce>())
                        tile.TileType = TileID.IceBlock;

                    // Simple vanilla tile conversions for decorative piles (using your FrozenHell style)
                    else if (type == ModContent.TileType<AstralNormalLargePiles>() ||
                             type == ModContent.TileType<AstralNormalMediumPiles>() ||
                             type == ModContent.TileType<AstralNormalSmallPiles>() ||
                             type == ModContent.TileType<AstralDesertLargePiles>() ||
                             type == ModContent.TileType<AstralDesertMediumPiles>() ||
                             type == ModContent.TileType<AstralDesertSmallPiles>() ||
                             type == ModContent.TileType<AstralIceLargePiles>() ||
                             type == ModContent.TileType<AstralIceMediumPiles>() ||
                             type == ModContent.TileType<AstralIceSmallPiles>())
                    {
                        WorldGen.KillTile(k, l, false, false, true);
                    }

                    // Stalactites → kill and let vanilla regrow
                    else if (type == ModContent.TileType<AstralNormalStalactite>() ||
                             type == ModContent.TileType<AstralDesertStalactite>() ||
                             type == ModContent.TileType<AstralIceStalactite>())
                    {
                        WorldGen.KillTile(k, l, false, false, true);
                    }

                    // Astral grass foliage, vines, etc.
                    else if (type == ModContent.TileType<AstralVines>() ||
                             type == ModContent.TileType<AstralShortPlants>() ||
                             type == ModContent.TileType<AstralTallPlants>())
                    {
                        WorldGen.KillTile(k, l, false, false, true);
                    }

                    // Walls
                    if (wall == ModContent.WallType<AstralDirtWall>())
                        tile.WallType = WallID.Dirt;

                    else if (wall == ModContent.WallType<AstralSnowWall>() ||
                             wall == ModContent.WallType<AstralSnowWallSafe>())
                        tile.WallType = WallID.SnowWallUnsafe;

                    else if (wall == ModContent.WallType<CelestialRemainsWall>())
                        tile.WallType = WallID.LihzahrdBrickUnsafe;

                    else if (wall == ModContent.WallType<AstralGrassWall>())
                        tile.WallType = WallID.Grass;

                    else if (wall == ModContent.WallType<AstralIceWall>())
                        tile.WallType = WallID.IceUnsafe;

                    else if (wall == ModContent.WallType<AstralMonolithWall>())
                        tile.WallType = WallID.PearlstoneBrick;

                    else if (wall == ModContent.WallType<AstralStoneWall>())
                        tile.WallType = WallID.Stone;

                    // Final touches
                    WorldGen.SquareTileFrame(k, l, true);
                    WorldGen.SquareWallFrame(k, l, true);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }
}
