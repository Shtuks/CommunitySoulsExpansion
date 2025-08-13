using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Donate;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using ssm.Core;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using ssm.Content.SoulToggles;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.NPCItems;

namespace ssm.Thorium.Enchantments
{
    [ExtendsFromMod(ModCompatibility.Thorium.Name)]
    [JITWhenModsEnabled(ModCompatibility.Thorium.Name)]
    public class IridescentEnchant : BaseEnchant
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddEffect<IridescentEffect>(Item);
        }

        public class IridescentEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AlfheimForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<IridescentEnchant>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = this.CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IridescentHelmet>());
            recipe.AddIngredient(ModContent.ItemType<IridescentMail>());
            recipe.AddIngredient(ModContent.ItemType<IridescentGreaves>());
            recipe.AddIngredient(ModContent.ItemType<Equalizer>());
            recipe.AddIngredient(ModContent.ItemType<LifeQuartzShield>());
            recipe.AddIngredient(ModContent.ItemType<HereticBreaker>());

            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
