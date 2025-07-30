using Terraria;
using Terraria.Chat;
using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.DataStructures;
using Terraria.ID;

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