using Terraria;
using ssm.Core;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.ThrownItems;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using static ssm.Thorium.Enchantments.PyromancerEnchant;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class LifeBinderEnchant : BaseEnchant
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
            Item.rare = 4;
            Item.value = 120000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<LifeBinderEffect>(Item);
        }

        public class LifeBinderEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<LifeBinderEnchant>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<LifeBinderMask>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderBreastplate>());
            recipe.AddIngredient(ModContent.ItemType<LifeBinderGreaves>());
            recipe.AddIngredient(ModContent.ItemType<IridescentEnchant>());
            recipe.AddIngredient(ModContent.ItemType<DewCollector>());
            recipe.AddIngredient(ModContent.ItemType<SunrayStaff>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
