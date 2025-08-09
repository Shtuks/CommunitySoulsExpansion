using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.BossLoot.InfernonDrops;
using SpiritMod.Items.BossLoot.InfernonDrops.InfernonArmor;
using SpiritMod.Items.Sets.MagicMisc.TerraStaffTree;
using SpiritMod.Items.Accessory;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class PainMongerEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(234, 93, 15);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 4;
            Item.value = 40000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<PainMongerGuard>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalVisor").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalBreastplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "InfernalGreaves").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<PainMongerMaw>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "HellEater").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<InfernalVisor>(1);
            recipe.AddIngredient<InfernalBreastplate>(1);
            recipe.AddIngredient<InfernalGreaves>(1);
            recipe.AddIngredient<InfernalStaff>(1);
            recipe.AddIngredient<HellStaff>(1);
            recipe.AddIngredient<HellEater>(1);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
        public class PainMongerGuard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PainMongerEnchant>();
        }
        public class PainMongerMaw : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<PainMongerEnchant>();
        }
    }
}