using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Steel;
using ThoriumMod.Items.DD;
using ssm.Core;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ThoriumMod.Items.Thorium;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.LodestoneEnchant;
using static ssm.Thorium.Enchantments.BronzeEnchant;
using static ssm.Thorium.Enchantments.SteelEnchant;
using ssm.Content.Projectiles.Enchantments;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class DurasteelEnchant : BaseEnchant
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
            Item.rare = 3;
            Item.value = 80000;
        }

        public override Color nameColor => new(255, 128, 0);
        public override void UpdateInventory(Player player)
        {
            player.AddEffect<DurasteelEffectOres>(Item);
        }

        public override void UpdateVanity(Player player)
        {
            player.AddEffect<DurasteelEffectOres>(Item);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<SteelEffect>(Item);
            player.AddEffect<DurasteelEffect>(Item);
        }

        public class DurasteelEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DurasteelEnchant>();
           
        }

        public class DurasteelEffectOres : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DurasteelEnchant>();
            public override bool MutantsPresenceAffects => true;

            public override void OnHitNPCEither(Player player, NPC target, NPC.HitInfo hitInfo, DamageClass damageClass, int baseDamage, Projectile projectile, Item item)
            {
                TryDoubleOreDrops(target);
            }

            private void TryDoubleOreDrops(NPC target)
            {
                if (Main.rand.NextBool(2) && target.life <= 0)
                {
                    for (int i = 0; i < target.npcSlots; i++)
                    {
                        
                    }
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<DurasteelHelmet>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelChestplate>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelGreaves>());
            recipe.AddIngredient(ModContent.ItemType<SteelEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DurasteelBlade>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
