using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria;
using Redemption.Tiles.Tiles;
using Redemption.Walls;
using ssm.Core;

namespace ssm.Redemption.Renewals
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public static class RedemptionConversion
    {
        public static void IrradiatedConvert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) &&
                        (Math.Abs(k - i) + Math.Abs(l - j)) < Math.Sqrt(size * size + size * size))
                    {
                        Tile tile = Main.tile[k, l];
                        if (tile == null) continue;

                        if (tile.HasTile)
                        {
                            if (tile.TileType == ModContent.TileType<IrradiatedStoneTile>())
                                ConvertTile(k, l, TileID.Stone);
                            else if (tile.TileType == ModContent.TileType<IrradiatedEbonstoneTile>())
                                ConvertTile(k, l, TileID.Ebonstone);
                            else if (tile.TileType == ModContent.TileType<IrradiatedCrimstoneTile>())
                                ConvertTile(k, l, TileID.Crimstone);
                            else if (tile.TileType == ModContent.TileType<IrradiatedGrassTile>())
                                ConvertTile(k, l, TileID.Grass);
                            else if (tile.TileType == ModContent.TileType<IrradiatedCorruptGrassTile>())
                                ConvertTile(k, l, TileID.CorruptGrass);
                            else if (tile.TileType == ModContent.TileType<IrradiatedCrimsonGrassTile>())
                                ConvertTile(k, l, TileID.CrimsonGrass);
                            else if (tile.TileType == ModContent.TileType<IrradiatedDirtTile>())
                                ConvertTile(k, l, TileID.Dirt);
                            else if (tile.TileType == ModContent.TileType<IrradiatedIceTile>())
                                ConvertTile(k, l, TileID.IceBlock);
                            else if (tile.TileType == ModContent.TileType<IrradiatedSnowTile>())
                                ConvertTile(k, l, TileID.SnowBlock);
                            else if (tile.TileType == ModContent.TileType<IrradiatedSandTile>())
                                ConvertTile(k, l, TileID.Sand);
                            else if (tile.TileType == ModContent.TileType<IrradiatedHardenedSandTile>())
                                ConvertTile(k, l, TileID.HardenedSand);
                            else if (tile.TileType == ModContent.TileType<IrradiatedSandstoneTile>())
                                ConvertTile(k, l, TileID.Sandstone);
                            else if (tile.TileType == ModContent.TileType<IrradiatedLivingWoodTile>())
                                ConvertTile(k, l, TileID.LivingWood);
                            else if (tile.TileType == ModContent.TileType<PetrifiedWoodTile>())
                                ConvertTile(k, l, TileID.WoodBlock);
                        }

                        if (tile.WallType == ModContent.WallType<IrradiatedStoneWallTile>())
                            ConvertWall(k, l, WallID.Stone);
                        else if (tile.WallType == ModContent.WallType<IrradiatedEbonstoneWallTile>())
                            ConvertWall(k, l, WallID.EbonstoneUnsafe);
                        else if (tile.WallType == ModContent.WallType<IrradiatedCrimstoneWallTile>())
                            ConvertWall(k, l, WallID.CrimstoneUnsafe);
                        else if (tile.WallType == ModContent.WallType<IrradiatedHardenedSandWallTile>())
                            ConvertWall(k, l, WallID.HardenedSand);
                        else if (tile.WallType == ModContent.WallType<IrradiatedSandstoneWallTile>())
                            ConvertWall(k, l, WallID.Sandstone);
                        else if (tile.WallType == ModContent.WallType<IrradiatedIceWallTile>())
                            ConvertWall(k, l, WallID.IceUnsafe);
                        else if (tile.WallType == ModContent.WallType<IrradiatedSnowWallTile>())
                            ConvertWall(k, l, WallID.SnowWallUnsafe);
                        else if (tile.WallType == ModContent.WallType<IrradiatedLivingWoodWallTile>())
                            ConvertWall(k, l, WallID.LivingWood);
                        else if (tile.WallType == ModContent.WallType<IrradiatedDirtWallTile>())
                            ConvertWall(k, l, WallID.Dirt);
                        else if (tile.WallType == ModContent.WallType<IrradiatedMudWallTile>())
                            ConvertWall(k, l, WallID.MudUnsafe);
                        else if (tile.WallType == ModContent.WallType<PetrifiedWoodWallTile>())
                            ConvertWall(k, l, WallID.Wood);
                    }
                }
            }
        }

        private static void ConvertTile(int i, int j, ushort type)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileType != type)
            {
                tile.TileType = type;
                WorldGen.SquareTileFrame(i, j, true);
                NetMessage.SendTileSquare(-1, i, j, 1);
            }
        }

        private static void ConvertWall(int i, int j, ushort type)
        {
            Tile tile = Main.tile[i, j];
            if (tile.WallType != type)
            {
                tile.WallType = type;
                WorldGen.SquareWallFrame(i, j, true);
                NetMessage.SendTileSquare(-1, i, j, 1);
            }
        }
    }
}