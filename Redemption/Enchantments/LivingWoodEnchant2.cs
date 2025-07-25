using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ssm.Core;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Redemption.Items.Armor.PreHM.LivingWood;
using Redemption.Items.Weapons.PreHM.Summon;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ssm.Content.Projectiles.Enchantments;
using static ssm.Redemption.Enchantments.DragonLeadEnchant;

namespace ssm.Redemption.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Redemption.Name)]
    [JITWhenModsEnabled(ModCompatibility.Redemption.Name)]
    public class LivingWoodEnchant2 : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return CSEConfig.Instance.Redemption;
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Item.rare = 10;
            Item.value = 40000;
        }

        public override Color nameColor => new Color(206, 182, 95);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LivingWoodEffect2>(Item);
        }

        public class LivingWoodEffect2 : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AdvancementForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LivingWoodEnchant2>();
            
            private int twigTimer;
            public override void PostUpdate(Player player)
            {
                twigTimer++;
                if (twigTimer >= 1200) 
                {
                    twigTimer = 0;
                    DropTwig(player);
                }
            }

            private void DropTwig(Player player)
            {
                if (player.whoAmI != Main.myPlayer) return;

                Vector2 position = player.Center + new Vector2(Main.rand.Next(-20, 20), 20);
                Projectile.NewProjectile(
                    player.GetSource_FromThis(),
                    position,
                    Vector2.Zero,
                    ModContent.ProjectileType<TwigProj>(),
                    20, 
                    0f,
                    player.whoAmI
                );
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient<LivingWoodHelmet>();
            recipe.AddIngredient<LivingWoodBody>();
            recipe.AddIngredient<LivingWoodLeggings>();
            recipe.AddIngredient(4281);
            recipe.AddIngredient<LogStaff>();
            recipe.AddIngredient(2196);
            recipe.AddTile(26);

            recipe.Register();
        }
    }
}
