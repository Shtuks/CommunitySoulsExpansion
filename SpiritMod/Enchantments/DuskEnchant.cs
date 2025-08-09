using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.DuskingDrops.DuskArmor;
using SpiritMod.Items.BossLoot.DuskingDrops;
using SpiritMod.Items.DonatorItems;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Sets.AccessoriesMisc.CrystalFlower;
using SpiritMod.Items.Accessory.ElectricGuitar;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class DuskEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(132, 77, 244);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<DuskRunes>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskHood").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "DuskLeggings").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<DuskGuitar>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ElectricGuitar").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DuskHood>(1);
            recipe.AddIngredient<DuskPlate>(1);
            recipe.AddIngredient<DuskLeggings>(1);
            recipe.AddIngredient<BladeofYouKai>(1);
            recipe.AddIngredient<ElectricGuitar>(1);
            recipe.AddIngredient<DuskingPainting>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class DuskRunes : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DuskEnchant>();
        }
        public class DuskGuitar : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<DuskEnchant>();
        }
    }
}