using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.FrigidSet.FrigidArmor;
using SpiritMod.Items.Accessory.Leather;
using SpiritMod.Items.Sets.FrigidSet;
using SpiritMod.Items.Sets.FrigidSet.Frostbite;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class FrigidEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(98, 100, 255);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 1;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<FrigidAttackSpeed>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidGloves").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<FrigidIceWall>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidHelm").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidChestplate").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "FrigidLegs").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<FrigidHelm>(1);
            recipe.AddIngredient<FrigidChestplate>(1);
            recipe.AddIngredient<FrigidLegs>(1);
            recipe.AddIngredient<IcySpear>(1);
            recipe.AddIngredient<HowlingScepter>(1);
            recipe.AddIngredient<FrigidGloves>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class FrigidAttackSpeed : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
        public class FrigidIceWall : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<FrigidEnchant>();
        }
    }
}
