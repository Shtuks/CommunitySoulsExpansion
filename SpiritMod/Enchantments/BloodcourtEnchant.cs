using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.BloodcourtSet.BloodCourt;
using SpiritMod.Items.Accessory.TalismanTree.GrislyTongue;
using SpiritMod.Items.Accessory.SanguineWardTree;
using SpiritMod.Items.Sets.ArcaneZoneSubclass;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class BloodcourtEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(190, 6, 6);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 20000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<BloodCourtBolt>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtHead").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodCourtLeggings").UpdateArmorSet(player);
            }
            if (AccessoryEffectLoader.AddEffect<BloodCourtWard>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "BloodWard").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<BloodCourtTongue>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "GrislyTongue").UpdateAccessory(player, false);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BloodCourtHead>(1);
            recipe.AddIngredient<BloodCourtChestplate>(1);
            recipe.AddIngredient<BloodCourtLeggings>(1);
            recipe.AddIngredient<HealingCodex>(1);
            recipe.AddIngredient<GrislyTongue>(1);
            recipe.AddIngredient<BloodWard>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class BloodCourtBolt : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
        }
        public class BloodCourtTongue : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
        }
        public class BloodCourtWard : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<BloodcourtEnchant>();
        }
    }
}