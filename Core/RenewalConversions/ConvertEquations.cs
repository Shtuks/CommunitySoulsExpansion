using Terraria;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ssm.Core.RenewalConversions
{
    public static class ConvertEquation
    {
        public static void Convert(Projectile projectile, string ConvertInto, bool IsSupreme)
        {
            if (!IsSupreme)
            {
                int radius = 150;
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {

                        int i = (int)(projectile.Center.X / 16f) + x;
                        int j = (int)(projectile.Center.Y / 16f) + y;
                        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)
                        {
                            if (ConvertInto == "Purity")
                                ssmConvertToPurity.ConvertAllToPurity(i, j);
                            // else if (ConvertInto == "Flarium")
                        }
                    }
                }
            }
            else
            {
                ModContent.GetInstance<DelayedWorldConversionSystem>().StartConversion(ConvertInto);
            }
        }
    }
    public class DelayedWorldConversionSystem : ModSystem
    {
        private Queue<Point> tilesToConvert = new();
        private string currentConversion = null;
        private int tilesPerTick = 2500; // Adjust to balance performance

        public void StartConversion(string convertInto)
        {
            currentConversion = convertInto;
            tilesToConvert.Clear();

            int maxX = Main.maxTilesX;
            int maxY = Main.maxTilesY;

            // Queue all world tiles
            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    tilesToConvert.Enqueue(new Point(x, y));
                }
            }

            Main.NewText("Started Supreme Conversion: " + convertInto, Color.Orange);
        }

        public override void PreUpdateWorld()
        {
            if (tilesToConvert.Count == 0 || string.IsNullOrEmpty(currentConversion))
                return;

            int count = 0;

            while (tilesToConvert.Count > 0 && count < tilesPerTick)
            {
                Point p = tilesToConvert.Dequeue();
                count++;

                Tile tile = Main.tile[p.X, p.Y];
                if (tile.HasTile)
                {

                    if (currentConversion == "Purity")
                        ssmConvertToPurity.ConvertAllToPurity(p.X, p.Y);
                }

                if (tilesToConvert.Count == 0)
                {
                    Main.NewText("Supreme Conversion Complete!", Color.LimeGreen);
                    currentConversion = null;
                }
            }
        }
    }
}