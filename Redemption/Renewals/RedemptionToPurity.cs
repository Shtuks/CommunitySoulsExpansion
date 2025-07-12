using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using Redemption.Tiles.Tiles;
using Redemption.Walls;
using System.Collections.Generic;

namespace ssm.Redemption.Renewals
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public static class RedemptionConversion
    {
        private static readonly Dictionary<ushort, ushort> tileConversions = new()
        {
            [(ushort)ModContent.TileType<IrradiatedStoneTile>()] = TileID.Stone,
            [(ushort)ModContent.TileType<IrradiatedEbonstoneTile>()] = TileID.Ebonstone,
            [(ushort)ModContent.TileType<IrradiatedCrimstoneTile>()] = TileID.Crimstone,

            [(ushort)ModContent.TileType<IrradiatedGrassTile>()] = TileID.Grass,
            [(ushort)ModContent.TileType<IrradiatedCorruptGrassTile>()] = TileID.CorruptGrass,
            [(ushort)ModContent.TileType<IrradiatedCrimsonGrassTile>()] = TileID.CrimsonGrass,

            [(ushort)ModContent.TileType<IrradiatedDirtTile>()] = TileID.Dirt,

            [(ushort)ModContent.TileType<IrradiatedIceTile>()] = TileID.IceBlock,
            [(ushort)ModContent.TileType<IrradiatedSnowTile>()] = TileID.SnowBlock,

            [(ushort)ModContent.TileType<IrradiatedSandTile>()] = TileID.Sand,
            [(ushort)ModContent.TileType<IrradiatedHardenedSandTile>()] = TileID.HardenedSand,
            [(ushort)ModContent.TileType<IrradiatedSandstoneTile>()] = TileID.Sandstone,

            [(ushort)ModContent.TileType<IrradiatedLivingWoodTile>()] = TileID.LivingWood,
            [(ushort)ModContent.TileType<PetrifiedWoodTile>()] = TileID.WoodBlock
        };

        private static readonly Dictionary<ushort, ushort> wallConversions = new()
        {
            [(ushort)ModContent.WallType<IrradiatedStoneWallTile>()] = WallID.Stone,
            [(ushort)ModContent.WallType<IrradiatedEbonstoneWallTile>()] = WallID.EbonstoneUnsafe,
            [(ushort)ModContent.WallType<IrradiatedCrimstoneWallTile>()] = WallID.CrimstoneUnsafe,

            [(ushort)ModContent.WallType<IrradiatedHardenedSandWallTile>()] = WallID.HardenedSand,
            [(ushort)ModContent.WallType<IrradiatedSandstoneWallTile>()] = WallID.Sandstone,

            [(ushort)ModContent.WallType<IrradiatedIceWallTile>()] = WallID.IceUnsafe,
            [(ushort)ModContent.WallType<IrradiatedSnowWallTile>()] = WallID.SnowWallUnsafe,

            [(ushort)ModContent.WallType<IrradiatedLivingWoodWallTile>()] = WallID.LivingWood,
            [(ushort)ModContent.WallType<IrradiatedDirtWallTile>()] = WallID.Dirt,
            [(ushort)ModContent.WallType<IrradiatedMudWallTile>()] = WallID.MudUnsafe,

            [(ushort)ModContent.WallType<PetrifiedWoodWallTile>()] = WallID.Wood
        };

        public static void IrradiatedConvert(int i, int j, int size = 4)
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

                    bool changed = false;

                    if (tile.HasTile && tileConversions.TryGetValue(tile.TileType, out ushort newTileType))
                    {
                        if (tile.TileType != newTileType)
                        {
                            tile.TileType = newTileType;
                            WorldGen.SquareTileFrame(k, l, true);
                            changed = true;
                        }
                    }

                     if (wallConversions.TryGetValue(tile.WallType, out ushort newWallType))
                    {
                        if (tile.WallType != newWallType)
                        {
                            tile.WallType = newWallType;
                            WorldGen.SquareWallFrame(k, l, true);
                            changed = true;
                        }
                    }

                    if (changed)
                        NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }
    }
}
