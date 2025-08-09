using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using ssm.Core;
using SacredTools.Content.Tiles.CraftingStations;
using SacredTools.Content.Tiles.Furniture.Asthral;
using Terraria.DataStructures;

namespace ssm.CrossMod.CraftingStations
{
    [ExtendsFromMod(ModCompatibility.SacredTools.Name)]
    [JITWhenModsEnabled(ModCompatibility.SacredTools.Name)]
    public class SyranCraftingStationTile : ModTile
	{
        public override void SetStaticDefaults()
		{
            Main.tileLighted[(int)((ModBlockType)this).Type] = true;
            Main.tileFrameImportant[(int)((ModBlockType)this).Type] = true;
            Main.tileNoAttach[(int)((ModBlockType)this).Type] = true;
            Main.tileLavaDeath[(int)((ModBlockType)this).Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.LavaDeath = false;
            TileID.Sets.CountsAsHoneySource[Type] = true;
            TileID.Sets.CountsAsLavaSource[Type] = true;
            TileID.Sets.CountsAsWaterSource[Type] = true;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinatePadding = 2;
            AnimationFrameHeight = 54;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.newTile.Origin = new Point16(2, 1);
            TileObjectData.addTile((int)((ModBlockType)this).Type);
            AddMapEntry(new Color(230, 10, 210), ((ModBlockType)this).CreateMapEntryName());
            TileID.Sets.DisableSmartCursor[(int)((ModBlockType)this).Type] = true;
            ((ModBlockType)this).DustType = 84;

            AdjTiles = new int[]
			{
				TileID.WorkBenches,
				TileID.Furnaces,
				TileID.Hellforge,
				TileID.AdamantiteForge,
				TileID.Anvils,
				TileID.MythrilAnvil,
				TileID.DemonAltar,
				TileID.LunarCraftingStation,
				TileID.TinkerersWorkbench,
				TileType<TiridiumInfuserTile>(),
				TileType<OblivionForgeTile>(),
				TileType<FlariumAnvilTile>(),
				TileType<FlariumForgeTile>(),
                TileType<NightmareFoundryTile>(),
                TileType<FlariumWorkBenchTile>(),
				TileType<AsthralWorkbench>()
			};
		}

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 16)
            {
                frameCounter = 0;
                frame = (frame + 1) % 6;
            }
        }
    }
}