using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using ssm.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ssm.Thorium.Enchantments.GraniteEnchant;

namespace ssm.Thorium
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class ThoriumTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Tile tile = Main.tile[i, j];
            Player player = Main.LocalPlayer;

            if (player is null || !player.active)
                return;

            if (player.HasEffect<GraniteEffect>() && !fail && TileID.Sets.Ore[tile.TileType])
            {
                var source = new EntitySource_TileBreak(i, j);
                Vector2 pos = new Vector2(i, j) * 16;
                ModTile moddedTile = TileLoader.GetTile(tile.TileType);
                if (moddedTile != null) 
                {
                    IEnumerable<Item> itemDrops = moddedTile.GetItemDrops(i, j);
                    if (itemDrops == null)
                        return;

                    foreach (Item item in itemDrops)
                    {
                        item.Prefix(-1);
                        int moddedOre = Item.NewItem(source, pos, item);
                        Main.item[moddedOre].TryCombiningIntoNearbyItems(moddedOre);
                    }
                }
                else
                {
                    int itemType = TileLoader.GetItemDropFromTypeAndStyle(tile.TileType);
                    Item.NewItem(source, pos, itemType);
                }
            }
        }
    }
}
