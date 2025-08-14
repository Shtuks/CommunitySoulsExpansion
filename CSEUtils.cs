using Terraria;
using Terraria.Chat;
using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ID;
using System.Collections.Generic;

namespace ssm
{
    public static class CSEUtils
    {
        public static float PlayerGetDistanceToNPC(Player player, NPC targetNPC)
        {
            if (targetNPC == null || !targetNPC.active)
            {
                return 99999f;
            }

            Vector2 playerPosition = player.Center;
            Vector2 npcPosition = targetNPC.Center;

            float distance = Vector2.Distance(playerPosition, npcPosition);

            return distance;
        }

        public static Point FindTileOrigin(int x, int y)
        {
            int originX = x;
            int originY = y;
            int maxSteps = 20; 

            for (int i = 0; i < maxSteps; i++)
            {
                Tile tile = Main.tile[originX, originY];
                if (tile == null || !tile.HasTile) break;

                bool moved = false;

                if (tile.TileFrameX != 0)
                {
                    originX--;
                    moved = true;
                }

                if (tile.TileFrameY != 0)
                {
                    originY--;
                    moved = true;
                }

                if (!moved) break;
            }

            return new Point(originX, originY);
        }
        public static string GetItemInternalName(Item item)
        {
            if(item.ModItem != null)
            {
                return $"{item.ModItem.Mod.Name}/{item.ModItem.Name}";
            }
            return ItemID.Search.GetName(item.type);
        }
        public static float ProjGetDistanceToNPC(Projectile player, NPC targetNPC)
        {
            if (targetNPC == null || !targetNPC.active)
            {
                return 99999f;
            }
            Vector2 playerPosition = player.Center;
            Vector2 npcPosition = targetNPC.Center;

            float distance = Vector2.Distance(playerPosition, npcPosition);

            return distance;
        }
        public static Point? FindNearestMultitile(Vector2 searchPosition, int tileType, int maxDistance = 100)
        {
            Point startTile = searchPosition.ToTileCoordinates();
            Point? nearestStart = null;
            float minDistanceSq = float.MaxValue;

            int left = Math.Max(0, startTile.X - maxDistance);
            int right = Math.Min(Main.maxTilesX - 1, startTile.X + maxDistance);
            int top = Math.Max(0, startTile.Y - maxDistance);
            int bottom = Math.Min(Main.maxTilesY - 1, startTile.Y + maxDistance);

            HashSet<Point> processedStarts = new HashSet<Point>();

            for (int x = left; x <= right; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    Tile tile = Main.tile[x, y];
                    if (tile == null || !tile.HasTile || tile.TileType != tileType)
                        continue;

                    Point start = FindTileOrigin(x, y);

                    if (!processedStarts.Add(start))
                        continue;

                    Tile originTile = Main.tile[start.X, start.Y];
                    if (originTile == null || !originTile.HasTile || originTile.TileType != tileType)
                        continue;

                    Vector2 startWorldPos = new Vector2(start.X * 16f + 8f, start.Y * 16f + 8f);
                    float distanceSq = Vector2.DistanceSquared(searchPosition, startWorldPos);

                    if (distanceSq < minDistanceSq)
                    {
                        minDistanceSq = distanceSq;
                        nearestStart = start;
                    }
                }
            }
            return nearestStart;
        }
        public static void AddToNextEmptySlot(ref int startIndex, Item[] items, int itemType, int price)
        {
            for (int i = startIndex; i < items.Length; i++)
            {
                if (items[i].type == ItemID.None)
                {
                    items[i] = new Item(itemType);
                    items[i].shopCustomPrice = price;
                    startIndex = i + 1;
                    break;
                }
            }
        }
        public static bool IsModItem(Item item, string mod)
        {
            if (item.ModItem is not null)
            {
                string modName = item.ModItem.Mod.Name;
                return modName.Equals(mod);
            }

            return false;
        }
        public static Projectile ProjectileRain(IEntitySource source, Vector2 targetPos, float xLimit, float xVariance, float yLimitLower, float yLimitUpper, float projSpeed, int projType, int damage, float knockback, int owner)
        {
            float x = targetPos.X + Main.rand.NextFloat(-xLimit, xLimit);
            float y = targetPos.Y - Main.rand.NextFloat(yLimitLower, yLimitUpper);
            Vector2 spawnPosition = new Vector2(x, y);
            Vector2 velocity = targetPos - spawnPosition;
            velocity.X += Main.rand.NextFloat(-xVariance, xVariance);
            float speed = projSpeed;
            float targetDist = velocity.Length();
            targetDist = speed / targetDist;
            velocity.X *= targetDist;
            velocity.Y *= targetDist;
            return Projectile.NewProjectileDirect(source, spawnPosition, velocity, projType, damage, knockback, owner);
        }
        public static void HomeInOnNPC(Projectile projectile, bool ignoreTiles, float distanceRequired, float homingVelocity, float inertia)
        {
            if (!projectile.friendly)
                return;

            Vector2 destination = projectile.Center;
            float maxDistance = distanceRequired;
            bool locatedTarget = false;

            float npcDistCompare = 30000f;
            int index = -1;
            foreach (NPC n in Main.ActiveNPCs)
            {
                float extraDistance = (n.width / 2) + (n.height / 2);
                if (!n.CanBeChasedBy(projectile, false) || !projectile.WithinRange(n.Center, maxDistance + extraDistance))
                    continue;

                float currentNPCDist = Vector2.Distance(n.Center, projectile.Center);
                if ((currentNPCDist < npcDistCompare) && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, n.Center, 1, 1)))
                {
                    npcDistCompare = currentNPCDist;
                    index = n.whoAmI;
                }
            }
            if (index != -1)
            {
                destination = Main.npc[index].Center;
                locatedTarget = true;
            }

            if (locatedTarget)
            {
                Vector2 homeDirection = (destination - projectile.Center).SafeNormalize(Vector2.UnitY);
                projectile.velocity = (projectile.velocity * inertia + homeDirection * homingVelocity) / (inertia + 1f);
            }
        }
        public static Player ToPlayer(this int ins)
        {
            if (ins < 0 || !Main.player[ins].active)
            {
                return Main.LocalPlayer;
            }

            return Main.player[ins];
        }
        public static CSENpcs CSE(this NPC npc)
            => npc.GetGlobalNPC<CSENpcs>();
        public static CSEPlayer CSE(this Player player)
            => player.GetModPlayer<CSEPlayer>();
        
        public static bool AnyBossAlive()
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                    return true;
            }
            return false;
        }
        public static void DisplayLocalizedText(string key, Color? textColor = null)
        {
            if (!textColor.HasValue)
            {
                textColor = Color.Green;
            }
            if (Main.netMode == 0)
            {
                Main.NewText((object)Language.GetTextValue(key), (Color?)textColor.Value);
            }
            else if (Main.netMode == 2 || Main.netMode == 1)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key, Array.Empty<object>()), textColor.Value, -1);
            }
        }
    }
}