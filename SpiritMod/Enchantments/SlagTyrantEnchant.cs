using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using SpiritMod.Items.Sets.SlagSet.FieryArmor;
using SpiritMod.Items.Sets.ClubSubclass;
using SpiritMod.Items.Sets.SlagSet;
using SpiritMod.Items.Accessory;
using ssm.Content.SoulToggles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using ssm.Core;

namespace ssm.SpiritMod.Enchantments
{
    [JITWhenModsEnabled(ModCompatibility.SpiritMod.Name)]
    [ExtendsFromMod(ModCompatibility.SpiritMod.Name)]
    public class SlagTyrantEnchant : BaseEnchant
    {
        public override Color nameColor => new Color(211, 61, 8);
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = 3;
            Item.value = 30000;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (AccessoryEffectLoader.AddEffect<SlagTyrantSummon>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "CimmerianScepter").UpdateAccessory(player, false);
            }
            if (AccessoryEffectLoader.AddEffect<SlagTyrantBurst>(player, base.Item))
            {
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusHelm").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusGreaves").UpdateArmorSet(player);
                ModContent.Find<ModItem>(this.SpiritMod.Name, "ObsidiusPlate").UpdateArmorSet(player);
            }
        }
        private readonly Mod SpiritMod = ModLoader.GetMod("SpiritMod");
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<ObsidiusHelm>(1);
            recipe.AddIngredient<ObsidiusGreaves>(1);
            recipe.AddIngredient<ObsidiusPlate>(1);
            recipe.AddIngredient<Blasphemer>(1);
            recipe.AddIngredient<FierySummonStaff>(1);
            recipe.AddIngredient<CimmerianScepter>(1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
        public class SlagTyrantBurst : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SlagTyrantEnchant>();
        }
        public class SlagTyrantSummon : AccessoryEffect
        {
            public override Header ToggleHeader => Header.GetHeader<FrostburnForceHeader>();
            public override int ToggleItemType => ModContent.ItemType<SlagTyrantEnchant>();
        }
    }
}