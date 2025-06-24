using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Redemption.Tiles.Tiles;
using Redemption.Walls;

namespace ssm.Core.RenewalConversions
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public static class RedemptionConversion
    {
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
        public static void ReverseWastelandTileConversion(Tile tile, int x1, int y1)
        {
            if (tile.HasTile)
            {
                switch (tile.TileType)
                {
                    case var t when t == ModContent.TileType<IrradiatedStoneTile>():
                        ConvertTile(x1, y1, TileID.Stone);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedEbonstoneTile>():
                        ConvertTile(x1, y1, TileID.Ebonstone);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedCrimstoneTile>():
                        ConvertTile(x1, y1, TileID.Crimstone);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedGrassTile>():
                        ConvertTile(x1, y1, TileID.Grass);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedCorruptGrassTile>():
                        ConvertTile(x1, y1, TileID.CorruptGrass);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedCrimsonGrassTile>():
                        ConvertTile(x1, y1, TileID.CrimsonGrass);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedDirtTile>():
                        ConvertTile(x1, y1, TileID.Dirt);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedIceTile>():
                        ConvertTile(x1, y1, TileID.IceBlock);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedSnowTile>():
                        ConvertTile(x1, y1, TileID.SnowBlock);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedSandTile>():
                        ConvertTile(x1, y1, TileID.Sand);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedHardenedSandTile>():
                        ConvertTile(x1, y1, TileID.HardenedSand);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedSandstoneTile>():
                        ConvertTile(x1, y1, TileID.Sandstone);
                        break;
                    case var t when t == ModContent.TileType<IrradiatedLivingWoodTile>():
                        ConvertTile(x1, y1, TileID.LivingWood);
                        break;
                    case var t when t == ModContent.TileType<PetrifiedWoodTile>():
                        ConvertTile(x1, y1, TileID.WoodBlock);
                        break;
                }
            }

            switch (tile.WallType)
            {
                case var w when w == ModContent.WallType<IrradiatedStoneWallTile>():
                    ConvertWall(x1, y1, WallID.Stone);
                    break;
                case var w when w == ModContent.WallType<IrradiatedEbonstoneWallTile>():
                    ConvertWall(x1, y1, WallID.EbonstoneUnsafe);
                    break;
                case var w when w == ModContent.WallType<IrradiatedCrimstoneWallTile>():
                    ConvertWall(x1, y1, WallID.CrimstoneUnsafe);
                    break;
                case var w when w == ModContent.WallType<IrradiatedHardenedSandWallTile>():
                    ConvertWall(x1, y1, WallID.HardenedSand);
                    break;
                case var w when w == ModContent.WallType<IrradiatedSandstoneWallTile>():
                    ConvertWall(x1, y1, WallID.Sandstone);
                    break;
                case var w when w == ModContent.WallType<IrradiatedIceWallTile>():
                    ConvertWall(x1, y1, WallID.IceUnsafe);
                    break;
                case var w when w == ModContent.WallType<IrradiatedSnowWallTile>():
                    ConvertWall(x1, y1, WallID.SnowWallUnsafe);
                    break;
                case var w when w == ModContent.WallType<IrradiatedLivingWoodWallTile>():
                    ConvertWall(x1, y1, WallID.LivingWood);
                    break;
                case var w when w == ModContent.WallType<IrradiatedDirtWallTile>():
                    ConvertWall(x1, y1, WallID.Dirt);
                    break;
                case var w when w == ModContent.WallType<IrradiatedMudWallTile>():
                    ConvertWall(x1, y1, WallID.MudUnsafe);
                    break;
                case var w when w == ModContent.WallType<PetrifiedWoodWallTile>():
                    ConvertWall(x1, y1, WallID.Wood);
                    break;
            }
        }
    }
}