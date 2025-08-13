using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Granite;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.ThrownItems;
using ssm.Core;
using ThoriumMod.Items.Painting;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using static ssm.Thorium.Enchantments.BiotechEnchant;
using FargowiltasSouls.Core.Toggler.Content;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GraniteEnchant : BaseEnchant
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
            Item.rare = 2;
            Item.value = 60000;
        }

        public override Color nameColor => new(255, 128, 0);

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<GraniteEffect>(Item);
        }

        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GraniteHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GraniteChestGuard>());
            recipe.AddIngredient(ModContent.ItemType<GraniteGreaves>());
            recipe.AddIngredient(ModContent.ItemType<HeartOfStone>());
            recipe.AddIngredient(ModContent.ItemType<ShockAbsorber>());
            recipe.AddIngredient(ModContent.ItemType<ObsidianStriker>(), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

        public class GraniteEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<SvartalfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GraniteEnchant>();
        }
    }
}
