using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.BasicAccessories;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Buffs;
using ssm.Content.SoulToggles;
using FargowiltasSouls;
using ssm.Content.Projectiles.Enchantments;
using static ssm.Thorium.Enchantments.BronzeEnchant;
using static ssm.Thorium.Enchantments.DurasteelEnchant;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class SteelEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 1;
            Item.value = 40000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SteelEffect>(Item);
        }

        public class SteelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SteelEnchant>();

            public int dashCooldown = 0;
            public int buffDuration = 0;
            private bool wasDashing = false;
            public override void PostUpdate(Player player)
            {
                if (dashCooldown > 0)
                {
                    dashCooldown--;
                }

                if (wasDashing && !(player.dashTime > 0))
                {
                    OnDashEnd(player);
                }
                wasDashing = player.dashTime > 0;

                if (buffDuration > 0)
                {
                    buffDuration--;
                    player.statDefense += 10;
                }
            }
            private void OnDashEnd(Player player)
            {
                if (dashCooldown > 0) return; 

                dashCooldown = 15 * 60; 
                buffDuration = 5 * 60;  

                if (player.whoAmI == Main.myPlayer)
                {
                    NPC target = FindNearestEnemy(player);
                    if (target != null)
                    {
                        Vector2 velocity = (target.Center - player.Center).SafeNormalize(Vector2.Zero) * 12f;
                        Projectile.NewProjectile(
                            player.GetSource_FromThis(),
                            player.Center,
                            velocity,
                            ModContent.ProjectileType<SteelSwordProjectile>(),
                            150, 
                            5f, 
                            player.whoAmI);
                    }
                }

                if (player.HasEffect<DurasteelEffect>() && player.GetThoriumPlayer().MetalShieldMax < 101)
                {
                    player.GetThoriumPlayer().MetalShieldMax += 25;
                }
            }
            private NPC FindNearestEnemy(Player player)
            {
                NPC closest = null;
                float minDistance = float.MaxValue;

                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.CanBeChasedBy())
                    {
                        float distance = Vector2.DistanceSquared(player.Center, npc.Center);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closest = npc;
                        }
                    }
                }

                return closest;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<SteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<SteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<ThoriumShield>());
            recipe.AddIngredient(ModContent.ItemType<SpikedBracer>());
            recipe.AddIngredient(ModContent.ItemType<SteelBlade>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
