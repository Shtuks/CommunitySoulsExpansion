using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria;
using ssm.Core;
using SacredTools.Content.Tiles.Solid;
using SacredTools.Content.Tiles.Cinder;
using SacredTools.Content.Tiles.Plants;
using SacredTools.Content.Walls;
using SacredTools.Tiles;


namespace ssm.Core.RenewalConversions
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public static class SacredToolsConversion
    {
        public static void ScorchedToPurity(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) &&
                        (Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt(size * size + size * size))
                    {
                        Tile tile = Main.tile[k, l];
                        if (tile != null)
                        {
                            // Thermal Rack → Stone
                            if (tile.TileType == ModContent.TileType<ThermalRackTile>())
                            {
                                tile.TileType = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Cinder Dirt → Dirt
                            if (tile.TileType == ModContent.TileType<CinderDirtTile>())
                            {
                                tile.TileType = TileID.Dirt;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Cinder Grass → Grass
                            if (tile.TileType == ModContent.TileType<CinderGrassTile>())
                            {
                                tile.TileType = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Scorched Sand → Sand
                            if (tile.TileType == ModContent.TileType<ScorchedSandTile>())
                            {
                                tile.TileType = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Scorched Sandstone → Sandstone
                            if (tile.TileType == ModContent.TileType<ScorchedSandstoneTile>())
                            {
                                tile.TileType = TileID.Sandstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Hardened Scorched Sand → Hardened Sand
                            if (tile.TileType == ModContent.TileType<HardenedScorchedSandTile>())
                            {
                                tile.TileType = TileID.HardenedSand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Dungeon variants → original Dungeon Brick (assume blue for simplicity)
                            if (tile.TileType == ModContent.TileType<ThermalRackTile>() &&
                                WorldGen.InWorld(k, l, 1) &&
                                l < Main.maxTilesY - 200) // quick sanity check
                            {
                                tile.TileType = TileID.BlueDungeonBrick;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Wall: Thermal Rack Wall → Stone Wall
                            if (tile.WallType == ModContent.WallType<ThermalRackWallWall>())
                            {
                                tile.WallType = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Wall: Flame Grass Wall → Grass Wall
                            if (tile.WallType == ModContent.WallType<FlameGrassWallWall>())
                            {
                                tile.WallType = WallID.Grass;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Wall: Scorched Sandstone Wall → Sandstone Wall
                            if (tile.WallType == ModContent.WallType<ScorchedSandstoneWallWall>())
                            {
                                tile.WallType = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Wall: Hardened Scorched Sand Wall → Hardened Sand Wall
                            if (tile.WallType == ModContent.WallType<HardenedScorchedSandWallWall>())
                            {
                                tile.WallType = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                    }
                }
            }
        }

        public static void ShrineToPurity(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) &&
                        (Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt(size * size + size * size))
                    {
                        Tile tile = Main.tile[k, l];
                        if (tile != null)
                        {
                            // Thermal Rack → Stone
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
    }
}
