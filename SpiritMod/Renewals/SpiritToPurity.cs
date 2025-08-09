using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SpiritMod.Tiles.Block;
using SpiritMod.Tiles.Walls.Natural;
using SpiritMod.Tiles.Ambient.Spirit;
using SpiritMod.Tiles.Ambient.Briar;
using ssm.Core;

namespace ssm.SpiritMod.Renewals
{
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    public static class SpiritToPurityConversion
    {
        public static void SpiritConvert(int i, int j, int size = 4)
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
                    Tile tileAbove = Main.tile[k, l - 1];
                    if (tile == null || tileAbove == null)
                        continue;

                    bool tileChanged = false;
                    bool wallChanged = false;
                    bool tileAboveChanged = false;

                    ushort wall = tile.WallType;
                    if (wall == ModContent.WallType<SpiritWallNatural>() ||
                        wall == ModContent.WallType<ReachWallNatural>())
                    {
                        tile.WallType = WallID.Grass;
                        wallChanged = true;
                    }

                    if (tile.HasTile)
                    {
                        ushort type = tile.TileType;

                        if (type == ModContent.TileType<SpiritStone>())
                        {
                            tile.TileType = TileID.Stone;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<SpiritDirt>())
                        {
                            tile.TileType = TileID.Dirt;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<SpiritGrass>())
                        {
                            tile.TileType = TileID.Grass;
                            tileChanged = true;

                            if (tileAbove.HasTile && tileAbove.TileType == ModContent.TileType<SpiritFoliage>())
                            {
                                tileAbove.TileType = TileID.Plants;
                                if (tileAbove.TileFrameX > 270)
                                    tileAbove.TileFrameX = 0;
                                tileAboveChanged = true;
                            }
                        }
                        else if (type == ModContent.TileType<BriarGrass>())
                        {
                            tile.TileType = TileID.Grass;
                            tileChanged = true;

                            if (tileAbove.HasTile && tileAbove.TileType == ModContent.TileType<BriarFoliage>())
                            {
                                tileAbove.TileType = TileID.Plants;
                                if (tileAbove.TileFrameX > 270)
                                    tileAbove.TileFrameX = 0;
                                tileAboveChanged = true;
                            }
                        }
                        else if (type == ModContent.TileType<Spiritsand>())
                        {
                            tile.TileType = TileID.Sand;
                            tileChanged = true;
                        }
                        else if (type == ModContent.TileType<SpiritIce>())
                        {
                            tile.TileType = TileID.IceBlock;
                            tileChanged = true;
                        }
                    }

                    if (tileChanged)
                        WorldGen.SquareTileFrame(k, l, true);
                    if (wallChanged)
                        WorldGen.SquareWallFrame(k, l, true);
                    if (tileChanged || wallChanged)
                        NetMessage.SendTileSquare(-1, k, l, 1);

                    if (tileAboveChanged)
                    {
                        WorldGen.SquareTileFrame(k, l - 1, true);
                        NetMessage.SendTileSquare(-1, k, l - 1, 1);
                    }
                }
            }
        }
    }
}
