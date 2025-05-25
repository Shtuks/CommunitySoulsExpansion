using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.RunicSet.RunicArmor;
using SpiritMod.Items.Sets.RunicSet;
using SpiritMod.Items.Sets.SpiritBiomeDrops;
using SpiritMod.Items.Placeable.Furniture;
using SpiritMod.Items.Accessory;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class RunicEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(35, 200, 254);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 5;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<RunicSpawn>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RuneWizardScroll").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<RunicScroll>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicHood").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicPlate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "RunicGreaves").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<RunicHood>(1);
            recipe.AddIngredient<RunicPlate>(1);
            recipe.AddIngredient<RunicGreaves>(1);
            recipe.AddIngredient<SpiritRune>(1);
            recipe.AddIngredient<RuneWizardScroll>(1);
            recipe.AddIngredient<SpiritBiomePainting>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class RunicSpawn : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RunicEnchant>();
        }
        public class RunicScroll : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<HurricaneForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<RunicEnchant>();
        }
    }
}
