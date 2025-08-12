using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.ArcaneArmor;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using ThoriumMod.Utilities;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.TideTurnerEnchant;
using ClickerClass.Prefixes.ClickerPrefixes;
using ssm.Content.Projectiles.Enchantments;
using FargowiltasSouls;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class YewWoodEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<YewWoodEffect>(Item);
        }

        public class YewWoodEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<TideHunterEnchant>();
            public override bool ExtraAttackEffect => true;
            public override void TryAdditionalAttacks(Player player, int damage, DamageClass damageType)
            {
                Vector2 center = player.Center;
                Vector2 vector = Vector2.Normalize(Main.MouseWorld - center);

                if (Main.rand.Next(player.ForceEffect<YewWoodEffect>() ? 75 : 100) != 0)
                {
                    Projectile.NewProjectile(
                        player.GetSource_FromThis(),
                        player.Center,
                        vector,
                        ModContent.ProjectileType<VileArrow>(),
                        1,
                        0f,
                        player.whoAmI
                    );
                }

            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<YewWoodHelmet>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodBreastguard>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodLeggings>());
            recipe.AddIngredient(ModContent.ItemType<ThumbRing>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodBow>());
            recipe.AddIngredient(ModContent.ItemType<YewWoodFlintlock>());


            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
