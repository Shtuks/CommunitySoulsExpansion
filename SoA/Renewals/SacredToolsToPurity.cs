using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using SacredTools.Content.Tiles.Solid;
using SacredTools.Content.Tiles.Cinder;
using SacredTools.Content.Tiles.Plants;
using SacredTools.Content.Walls;
using SacredTools.Tiles;

namespace ssm.SoA.Renewals
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public static class SacredToolsConversion
    {
        public static void FlariumConvert(int i, int j, int size = 4)
        {
            int sizeSq = size * size;

            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (!WorldGen.InWorld(k, l, 1))
                        continue;

                    int dx = k - i;
                    int dy = l - j;
                    if (dx * dx + dy * dy > sizeSq)
                        continue;

                    Tile tile = Main.tile[k, l];
                    if (tile == null)
                        continue;

                    bool tileChanged = false;
                    bool wallChanged = false;

                    if (tile.HasTile)
                    {
                        ushort type = tile.TileType;

                        if (type == ModContent.TileType<ThermalRackTile>())
                        {
                            // Dungeon override only if near surface
                            tile.TileType = (l < Main.maxTilesY - 200)
                                ? TileID.BlueDungeonBrick
                                : TileID.Stone;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<CinderDirtTile>())
                        {
                            tile.TileType = TileID.Dirt;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<CinderGrassTile>())
                        {
                            tile.TileType = TileID.Grass;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<ScorchedSandTile>())
                        {
                            tile.TileType = TileID.Sand;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<ScorchedSandstoneTile>())
                        {
                            tile.TileType = TileID.Sandstone;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<HardenedScorchedSandTile>())
                        {
                            tile.TileType = TileID.HardenedSand;
                            tileChanged = true;
                        }
                    }

                    ushort wall = tile.WallType;
                    if (wall == ModContent.WallType<ThermalRackWallWall>())
                    {
                        tile.WallType = WallID.Stone;
                        wallChanged = true;
                    }
                    else if (wall == ModContent.WallType<FlameGrassWallWall>())
                    {
                        tile.WallType = WallID.Grass;
                        wallChanged = true;
                    }
                    else if (wall == ModContent.WallType<ScorchedSandstoneWallWall>())
                    {
                        tile.WallType = WallID.Sandstone;
                        wallChanged = true;
                    }
                    else if (wall == ModContent.WallType<HardenedScorchedSandWallWall>())
                    {
                        tile.WallType = WallID.HardenedSand;
                        wallChanged = true;
                    }

                    if (tileChanged)
                        WorldGen.SquareTileFrame(k, l, true);
                    if (wallChanged)
                        WorldGen.SquareWallFrame(k, l, true);
                    if (tileChanged || wallChanged)
                        NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }

        public static void ShrineConvert(int i, int j, int size = 4)
        {
            int sizeSq = size * size;

            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (!WorldGen.InWorld(k, l, 1))
                        continue;

                    int dx = k - i;
                    int dy = l - j;
                    if (dx * dx + dy * dy > sizeSq)
                        continue;

                    Tile tile = Main.tile[k, l];
                    if (tile == null || !tile.HasTile)
                        continue;

                    if (tile.TileType == ModContent.TileType<ShrineGrassTile>())
                    {
                        tile.TileType = TileID.Grass;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1);
                    }
                }
            }
        }
    }
}
