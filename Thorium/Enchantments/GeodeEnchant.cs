using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.Geode;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ssm.Core;
using ThoriumMod.Items.Tracker;
using ThoriumMod.Items.BasicAccessories;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.Projectiles.Enchantments;
using ssm.Content.SoulToggles;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class GeodeEnchant : BaseEnchant
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ShtunConfig.Instance.Thorium;
        }

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

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
            //player.pick += 9;
            player.AddEffect<GeodeEffect>(Item);
        }

        public class GeodeEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<MidgardForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<GeodeEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<GeodeHelmet>());
            recipe.AddIngredient(ModContent.ItemType<GeodeChestplate>());
            recipe.AddIngredient(ModContent.ItemType<GeodeGreaves>());
            recipe.AddIngredient(ModContent.ItemType<CrystalGeode>(), 100);
            recipe.AddIngredient(ModContent.ItemType<CrystalSpearTip>());
            recipe.AddIngredient(ModContent.ItemType<GeodePickaxe>());

            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
