using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using SpiritMod.Items.Armor.Daybloom;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Placeable.Furniture;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SunflowerEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(246, 197, 26);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomHead").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomBody").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "DaybloomLegs").UpdateArmorSet(player);
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<DaybloomHead>(1);
            recipe.AddIngredient<DaybloomBody>(1);
            recipe.AddIngredient<DaybloomLegs>(1);
            recipe.AddIngredient<BriarFlowerItem>(1);
            recipe.AddIngredient<HangingSunPot>(3);
            recipe.AddIngredient(4429,1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
