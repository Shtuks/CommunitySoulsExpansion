using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria;
using Clamity.Content.Biomes.FrozenHell.Items;
using ssm.Core;

namespace ssm.Core.RenewalConversions
{
    [ExtendsFromMod(ModCompatibility.Clamity.Name)]
    [JITWhenModsEnabled(ModCompatibility.Clamity.Name)]
    public static class ClamityConversion
    {
        public static void FrozenHellToPurity(int i, int j, int size = 4)
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
                            // Convert FrozenAsh → Ash
                            if (tile.TileType == ModContent.TileType<FrozenAshTile>())
                            {
                                tile.TileType = TileID.Ash;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Convert FrozenHellstone → Hellstone
                            if (tile.TileType == ModContent.TileType<FrozenHellstoneTile>())
                            {
                                tile.TileType = TileID.Hellstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            // Convert breakable ice back to lava (if eligible)
                            if (tile.TileType == TileID.BreakableIce && tile.HasTile)
                            {
                                // Heuristic: underworld layer and no wall
                                if (l >= Main.UnderworldLayer && tile.WallType == 0)
                                {
                                    WorldGen.KillTile(k, l, false, false, true);
                                    tile.LiquidType = LiquidID.Lava;
                                    tile.LiquidAmount = 255;
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
}
