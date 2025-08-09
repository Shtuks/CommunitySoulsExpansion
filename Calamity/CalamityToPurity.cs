using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Walls;
using ssm.Core;
using System.Collections.Generic;

namespace ssm.Calamity
{
    [ExtendsFromMod(ModCompatibility.Calamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Calamity.Name)]
    public static class CalamityConversion
    {
        private static readonly Dictionary<ushort, ushort> tileConversions = new()
        {
            [(ushort)ModContent.TileType<AstralDirt>()] = TileID.Dirt,
            [(ushort)ModContent.TileType<AstralSnow>()] = TileID.SnowBlock,
            [(ushort)ModContent.TileType<NovaeSlag>()] = TileID.Hellstone,
            [(ushort)ModContent.TileType<CelestialRemains>()] = TileID.LihzahrdBrick,
            [(ushort)ModContent.TileType<AstralClay>()] = TileID.ClayBlock,
            [(ushort)ModContent.TileType<AstralMonolith>()] = TileID.PearlstoneBrick,
            [(ushort)ModContent.TileType<AstralStone>()] = TileID.Stone,
            [(ushort)ModContent.TileType<AstralGrass>()] = TileID.Grass,
            [(ushort)ModContent.TileType<AstralSand>()] = TileID.Sand,
            [(ushort)ModContent.TileType<AstralSandstone>()] = TileID.Sandstone,
            [(ushort)ModContent.TileType<HardenedAstralSand>()] = TileID.HardenedSand,
            [(ushort)ModContent.TileType<AstralIce>()] = TileID.IceBlock,
        };

        private static readonly Dictionary<ushort, ushort> wallConversions = new()
        {
            [(ushort)ModContent.WallType<AstralDirtWall>()] = WallID.Dirt,
            [(ushort)ModContent.WallType<AstralSnowWall>()] = WallID.SnowWallUnsafe,
            [(ushort)ModContent.WallType<AstralSnowWallSafe>()] = WallID.SnowWallUnsafe,
            [(ushort)ModContent.WallType<CelestialRemainsWall>()] = WallID.LihzahrdBrickUnsafe,
            [(ushort)ModContent.WallType<AstralGrassWall>()] = WallID.Grass,
            [(ushort)ModContent.WallType<AstralIceWall>()] = WallID.IceUnsafe,
            [(ushort)ModContent.WallType<AstralMonolithWall>()] = WallID.PearlstoneBrick,
            [(ushort)ModContent.WallType<AstralStoneWall>()] = WallID.Stone,
        };

        private static readonly HashSet<ushort> killTiles = new()
        {
            (ushort)ModContent.TileType<AstralNormalLargePiles>(),
            (ushort)ModContent.TileType<AstralNormalMediumPiles>(),
            (ushort)ModContent.TileType<AstralNormalSmallPiles>(),
            (ushort)ModContent.TileType<AstralDesertLargePiles>(),
            (ushort)ModContent.TileType<AstralDesertMediumPiles>(),
            (ushort)ModContent.TileType<AstralDesertSmallPiles>(),
            (ushort)ModContent.TileType<AstralIceLargePiles>(),
            (ushort)ModContent.TileType<AstralIceMediumPiles>(),
            (ushort)ModContent.TileType<AstralIceSmallPiles>(),

            (ushort)ModContent.TileType<AstralNormalStalactite>(),
            (ushort)ModContent.TileType<AstralDesertStalactite>(),
            (ushort)ModContent.TileType<AstralIceStalactite>(),

            (ushort)ModContent.TileType<AstralVines>(),
            (ushort)ModContent.TileType<AstralShortPlants>(),
            (ushort)ModContent.TileType<AstralTallPlants>(),
        };

        public static void AstralConvert(int i, int j, int size = 4)
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

                    if (tile.HasTile && killTiles.Contains(tile.TileType))
                    {
                        WorldGen.KillTile(k, l, false, false, true);
                        continue;
                    }

                    if (tile.HasTile && tileConversions.TryGetValue(tile.TileType, out ushort newType))
                    {
                        if (tile.TileType != newType)
                        {
                            tile.TileType = newType;
                            WorldGen.SquareTileFrame(k, l, true);
                            changed = true;
                        }
                    }

                    if (wallConversions.TryGetValue(tile.WallType, out ushort newWall))
                    {
                        if (tile.WallType != newWall)
                        {
                            tile.WallType = newWall;
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
