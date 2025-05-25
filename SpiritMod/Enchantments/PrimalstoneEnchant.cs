using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.AtlasDrops.PrimalstoneArmor;
using SpiritMod.Items.BossLoot.AtlasDrops;
using SpiritMod.Items.Sets.SwordsMisc.EternalSwordTree;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class PrimalstoneEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(164, 193, 176);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 8;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ModContent.Find<ModItem>(this.SpiritMod.Name, "TitanboundBulwark").UpdateAccessory(player, false);
            if (AccessoryEffectLoader.AddEffect<PrimalStoneArmorEffect>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneFaceplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneBreastplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "PrimalstoneLeggings").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<PrimalstoneFaceplate>(1);
            recipe.AddIngredient<PrimalstoneBreastplate>(1);
            recipe.AddIngredient<PrimalstoneLeggings>(1);
            recipe.AddIngredient<DemoncomboSword>(1);
            recipe.AddIngredient<Mountain>(1);
            recipe.AddIngredient<TitanboundBulwark>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PrimalStoneArmorEffect : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<AtlantisForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PrimalstoneEnchant>();
        }
    }
}
