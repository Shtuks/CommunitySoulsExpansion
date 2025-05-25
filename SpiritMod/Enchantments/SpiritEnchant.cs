using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.SpiritSet.SpiritArmor;
using SpiritMod.Items.Sets.SwordsMisc.EternalSwordTree;
using SpiritMod.Items.Sets.SpiritSet;
using SpiritMod.Items.Accessory;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Sets.SeraphSet;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SpiritEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(0, 186, 242);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<SpiritFangEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ShadowSingeFang").UpdateAccessory(player, false);
            }
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritHeadgear").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritBodyArmor").UpdateArmorSet(player);
            ModContent.Find<ModItem>(this.SpiritMod.Name, "SpiritLeggings").UpdateArmorSet(player);
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<SpiritHeadgear>(1);
            recipe.AddIngredient<SpiritBodyArmor>(1);
            recipe.AddIngredient<SpiritLeggings>(1);
            recipe.AddIngredient<SpiritSaber>(1);
            recipe.AddIngredient<GlowSting>(1);
            recipe.AddIngredient<ShadowSingeFang>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class SpiritFangEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SpiritEnchant>();
        }
    }
}
