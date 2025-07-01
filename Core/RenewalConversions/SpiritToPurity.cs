using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System;
using SpiritMod.Tiles.Block;
using SpiritMod.Tiles.Walls.Natural;
using SpiritMod.Tiles.Ambient.Spirit;
using SpiritMod.Tiles.Ambient.Briar;

namespace ssm.Core.RenewalConversions
{
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    public static class SpiritToPurityConversion
    {
        public static void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (!WorldGen.InWorld(k, l, 1) || (Math.Abs(k - i) + Math.Abs(l - j)) >= Math.Sqrt(size * size + size * size))
                        continue;

                    Tile tile = Framing.GetTileSafely(k, l);
                    Tile tileAbove = Framing.GetTileSafely(k, l - 1);

                    // Wall: SpiritWallNatural → Grass Wall (vanilla)
                    if (tile.WallType == (ushort)ModContent.WallType<SpiritWallNatural>())
                    {
                        tile.WallType = WallID.Grass;
                        WorldGen.SquareWallFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }

                    if (tile.WallType == (ushort)ModContent.WallType<ReachWallNatural>())
                    {
                        tile.WallType = WallID.Grass;
                        WorldGen.SquareWallFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }   

                    // Tile: SpiritStone → Stone
                    if (tile.TileType == (ushort)ModContent.TileType<SpiritStone>())
                    {
                        tile.TileType = TileID.Stone;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }

                    // Tile: SpiritDirt → Dirt
                    else if (tile.TileType == (ushort)ModContent.TileType<SpiritDirt>())
                    {
                        tile.TileType = TileID.Dirt;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }

                    // Tile: SpiritGrass → Grass
                    else if (tile.TileType == (ushort)ModContent.TileType<SpiritGrass>())
                    {
                        tile.TileType = TileID.Grass;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);

                        // If foliage exists above and is SpiritFoliage, revert it
                        if (tileAbove.TileType == (ushort)ModContent.TileType<SpiritFoliage>())
                        {
                            tileAbove.TileType = TileID.Plants;
                            if (tileAbove.TileFrameX > 270)
                                tileAbove.TileFrameX = 0;

                            WorldGen.SquareTileFrame(k, l - 1, true);
                            NetMessage.SendTileSquare(-1, k, l - 1, 1, TileChangeType.None);
                        }
                    }

                    else if (tile.TileType == (ushort)ModContent.TileType<BriarGrass>())
                    {
                        tile.TileType = TileID.Grass;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);

                        // If foliage exists above and is BriarFoliage, revert it
                        if (tileAbove.TileType == (ushort)ModContent.TileType<BriarFoliage>())
                        {
                            tileAbove.TileType = TileID.Plants;
                            if (tileAbove.TileFrameX > 270)
                                tileAbove.TileFrameX = 0;

                            WorldGen.SquareTileFrame(k, l - 1, true);
                            NetMessage.SendTileSquare(-1, k, l - 1, 1, TileChangeType.None);
                        }
                    }

                    // Tile: Spiritsand → Sand
                    else if (tile.TileType == (ushort)ModContent.TileType<Spiritsand>())
                    {
                        tile.TileType = TileID.Sand;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }

                    // Tile: SpiritIce → Ice
                    else if (tile.TileType == (ushort)ModContent.TileType<SpiritIce>())
                    {
                        tile.TileType = TileID.IceBlock;
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1, TileChangeType.None);
                    }
                }
            }
        }
    }
}
