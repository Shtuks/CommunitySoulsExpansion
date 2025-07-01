using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Spooky.Content.Tiles.SpookyBiome;
using Spooky.Content.Tiles.Cemetery;
using Spooky.Content.Tiles.SpiderCave;
using ssm.Core;

namespace ssm.Spooky.Renewals
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public static class SpookyConversion
    {
        public static void SpookyConvert(int i, int j, int size = 4)
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

                    if (type == ModContent.TileType<SpookyGrass>())
                        tile.TileType = TileID.Grass;
                        
                    else if (type == ModContent.TileType<SpookyGrassGreen>())
                        tile.TileType = TileID.Grass;

                    else if (type == ModContent.TileType<CemeteryGrass>())
                        tile.TileType = TileID.Grass;

                    else if (type == ModContent.TileType<DampGrass>())
                        tile.TileType = TileID.Grass;

                    else if (type == ModContent.TileType<SpookyDirt>())
                        tile.TileType = TileID.Dirt;

                    else if (type == ModContent.TileType<CemeteryDirt>())
                        tile.TileType = TileID.Dirt;

                    else if (type == ModContent.TileType<DampSoil>())
                        tile.TileType = TileID.Dirt;

                    else if (type == ModContent.TileType<SpookyStone>())
                        tile.TileType = TileID.Stone;

                    else if (type == ModContent.TileType<CemeteryStone>())
                        tile.TileType = TileID.Stone;


                    // Walls
                    if (wall == ModContent.WallType<SpookyGrassWall>())
                        tile.WallType = WallID.Grass;

                    else if (wall == ModContent.WallType<CemeteryGrassWall>())
                    tile.WallType = WallID.Grass;

                    else if (wall == ModContent.WallType<DampGrassWall>())
                    tile.WallType = WallID.Grass;   

                    WorldGen.SquareTileFrame(k, l, true);
                    WorldGen.SquareWallFrame(k, l, true);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }
}
