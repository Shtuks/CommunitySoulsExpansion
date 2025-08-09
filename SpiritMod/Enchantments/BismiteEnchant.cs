using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.BismiteSet.BismiteArmor;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Sets.BismiteSet;
using SpiritMod.Items.Sets.BriarChestLoot;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BismiteEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(164, 202, 74);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<BismiteShieldEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteShield").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<BismiteExplosion>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteHelmet").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BismiteLeggings").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismiteHelmet>(1);
            recipe.AddIngredient<BismiteChestplate>(1);
            recipe.AddIngredient<BismiteLeggings>(1);
            recipe.AddIngredient<BismiteShield>(1);
            recipe.AddIngredient<BismiteChakra>(1);
            recipe.AddIngredient<ReachBoomerang>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BismiteExplosion : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismiteEnchant>();
        }
        public class BismiteShieldEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BismiteEnchant>();
        }
    }
}