using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ssm.Core;
using Clamity.Content.Biomes.FrozenHell.Items;

namespace ssm.Calamity.Addons.Clamity
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public static class ClamityConversion
    {
        public static void FrozenHellConvert(int i, int j, int size = 4)
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

                    if (tile.HasTile)
                    {
                        ushort type = tile.TileType;

                        if (type == ModContent.TileType<FrozenAshTile>())
                        {
                            tile.TileType = TileID.Ash;
                            changed = true;
                        }
                        else if (type == ModContent.TileType<FrozenHellstoneTile>())
                        {
                            tile.TileType = TileID.Hellstone;
                            changed = true;
                        }
                        else if (type == TileID.BreakableIce && l >= Main.UnderworldLayer && tile.WallType == 0)
                        {
                            WorldGen.KillTile(k, l, false, false, true);
                            tile.LiquidType = LiquidID.Lava;
                            tile.LiquidAmount = 255;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                            continue;
                        }
                    }

                    if (changed)
                    {
                        WorldGen.SquareTileFrame(k, l, true);
                        NetMessage.SendTileSquare(-1, k, l, 1);
                    }
                }
            }
        }
    }
}
