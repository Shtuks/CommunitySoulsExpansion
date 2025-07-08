using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
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

                        if (type == ModContent.TileType<SpookyGrass>() ||
                            type == ModContent.TileType<SpookyGrassGreen>() ||
                            type == ModContent.TileType<CemeteryGrass>() ||
                            type == ModContent.TileType<DampGrass>())
                        {
                            tile.TileType = TileID.Grass;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<SpookyDirt>() ||
                                 type == ModContent.TileType<CemeteryDirt>() ||
                                 type == ModContent.TileType<DampSoil>())
                        {
                            tile.TileType = TileID.Dirt;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<SpookyStone>() ||
                                 type == ModContent.TileType<CemeteryStone>())
                        {
                            tile.TileType = TileID.Stone;
                            tileChanged = true;
                        }
                    }

                    ushort wall = tile.WallType;

                    if (wall == ModContent.WallType<SpookyGrassWall>() ||
                        wall == ModContent.WallType<CemeteryGrassWall>() ||
                        wall == ModContent.WallType<DampGrassWall>())
                    {
                        tile.WallType = WallID.Grass;
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
    }
}
